
/* This code is sued for starting continous ranging on SRF02 sensor
 * 
 * (c) Red Pitaya  http://www.redpitaya.com
 *
 * This part of code is written in C programming language.
 * Please visit http://en.wikipedia.org/wiki/C_(programming_language)
 * for more details on the language used herein.
 */
#include <fcntl.h>
#include <linux/i2c-dev.h>
#include <string.h>
#include <unistd.h>
#include <errno.h>
#include <stdint.h>
#include <float.h>

// daemon includes
#include <pthread.h>
#include <syslog.h>

#include "rp.h"
#include "_kiss_fft_guts.h"
#include "kiss_fft.h"
#include "kiss_fftr.h"
#include "main.h"
#include "iic.h"
#include "udp_server.h"
#include "save_data.h"
#include "nn.h"


//	global defines
#define I2C_SLAVE_FORCE 		   	0x0706
#define I2C_SLAVE    			   	0x0703    /* Change slave address            */
#define I2C_FUNCS    			   	0x0705    /* Get the adapter functionality */
#define I2C_RDWR					0x0707    /* Combined R/W transfer (one stop only)*/

#define	V_SONIC_WAVE				(float)(343.2)													// [m/s] Schallgeschwindigkeit in Luft

#define ADC_MAX_SAMPLE_FREQUENCY	125000000														// [Hz] --> 125 MHz
#define ADC_SAMPLE_DECIMATION		64																// [-]
#define ADC_SAMPLE_FREQUENCY		( ADC_MAX_SAMPLE_FREQUENCY / ADC_SAMPLE_DECIMATION )
#define ADC_SAMPLE_TIME				8																// [ns]
#define ADC_SAMPLE_TIME_NS			(uint32_t)( ADC_SAMPLE_DECIMATION * ADC_SAMPLE_TIME )			// [ns] --> 8*64=512 ns / sample
#define	ADC_START_DELAY_US			(uint32_t)( 0.30 * 2 * 1e6 / V_SONIC_WAVE )						// [µs] --> 2 * 0,30 m / 343,2 m/s = 1.748 µs
#define ADC_BUFFER_DELAY_US			(uint32_t)(( ADC_BUFFER_SIZE * ADC_SAMPLE_TIME_NS ) / (1e3))	// [µs] --> (16.384 * 512 ns ) / 1000 = 8.388 µs
#define ADC_MID_US					(ADC_START_DELAY_US + ( ADC_BUFFER_DELAY_US / 2 ))

#define FFT_MIN_FREQ				35000
#define FFT_MAX_FREQ				45000															// [Hz]
#define FFT_WINDOW_STD				8192															// FFT window width in samples [-]

#define F1_FILTER_WIDTH				10
#define F2_FILTER_WIDTH				10
#define F6_FILTER_WIDTH				10
#define F9_FILTER_WIDTH				10

#define LEDx_INIT()					rp_DpinSetDirection( RP_LED0, RP_OUT ), \
									rp_DpinSetDirection( RP_LED1, RP_OUT ), \
									rp_DpinSetDirection( RP_LED2, RP_OUT ),	\
									rp_DpinSetDirection( RP_LED3, RP_OUT ), \
									rp_DpinSetDirection( RP_LED4, RP_OUT ), \

									rp_DpinSetState( RP_LED0, RP_LOW ),		\
									rp_DpinSetState( RP_LED1, RP_LOW ),		\
									rp_DpinSetState( RP_LED2, RP_LOW ),		\
									rp_DpinSetState( RP_LED3, RP_LOW ),		\
									rp_DpinSetState( RP_LED4, RP_LOW )


#define LED0_OFF					rp_DpinSetState( RP_LED0, RP_LOW  )
#define LED1_OFF					rp_DpinSetState( RP_LED1, RP_LOW  )
#define LED2_OFF					rp_DpinSetState( RP_LED2, RP_LOW  )
#define LED3_OFF					rp_DpinSetState( RP_LED3, RP_LOW  )
#define LED4_OFF					rp_DpinSetState( RP_LED4, RP_LOW  )

#define LED0_ON						rp_DpinSetState( RP_LED0, RP_HIGH )
#define LED1_ON						rp_DpinSetState( RP_LED1, RP_HIGH )
#define LED2_ON						rp_DpinSetState( RP_LED2, RP_HIGH )
#define LED3_ON						rp_DpinSetState( RP_LED3, RP_HIGH )
#define LED4_ON						rp_DpinSetState( RP_LED4, RP_HIGH )



typedef enum
{
	ERR_NO				=  0,
	ERR_LOGIC			= -1,
	ERR_FILE_CREATION	= -2,
	ERR_FILE_OPEN		= -3,
	ERR_DATA			= -4,
	ERR_COM				= -5,
	ERR_I2C				= -6,
	ERROR_FILE			= -7,
	ERR_TCP				= -8,
	ERR_FATAL			= -127
}error_t;



typedef enum
{
	SEAT_EMPTY = 0,
	SEAT_COVERED_OBJECT,
	SEAT_COVERED_HUMAN,
	SEAT_INITIALIZE
}seat_t;



typedef enum
{
	DEMO_START = 0,
	DEMO_INITIALIZE,
	DEMO_RUN
}demo_t;



/* Typedefs for global structs: adc, fft, iic and udp */
struct iic_s
{
	int			fd;
	int			address;
	char		buf[4];
	char		*fileName;
}iic = {
		.address	= 0x70,
		.fileName	= "/dev/i2c-0"
};



typedef struct
{
	bool		auto_adjust_window;
	uint16_t	w_width;
	uint16_t	w_start;
	uint16_t	object_ix;
	uint16_t	start_delay_us;
	uint32_t	buffer_size;
}adc_t;



typedef struct
{
	uint16_t	freq_min;
	uint16_t	freq_max;
	uint16_t	freq_center;
	uint16_t	freq_min_index;
	uint16_t	freq_max_index;
	uint16_t	freq_center_index;
	float		freq_factor;
}fft_t;



typedef struct
{
	float HeaderLength;
	float DataLength;
	float Class;
	float DataType;
	float X_Interval;
	float Scaling;
	float SampleFrequency;
	float ADCResolution;
	float AmbTemperature;
	float MeasDelay;
	float Distance;
	float FFTWindowLength;
	float FFTWindowOffsetIndex;
	float SWVersion;
	float reserved1;
	float reserved2;
	uint16_t p_data[ADC_BUFFER_SIZE];
}Header_t;



typedef struct
{
	float		 F1;
	float		 F2;
	float		 F3;
	float		 F4;
	float		 F5;
	float		 F6;
	float		 F7;
	float		 F8;
	float		 F9;
	float		F10;
}feat_t;



typedef struct
{
	uint16_t	idx;
	uint16_t	idx_max;
	float		latest_val;
	float		sum;
	float		filt_val;
	float		*buffer;
}filter_t;







// global constants
static const uint16_t WindowWidths[] = { 1024, 2048, 4096, 8192 };


// global variables definition
static feat_t				feat;
static filter_t				filter_F1;
static filter_t				filter_F3;
static filter_t				filter_fft_F6;
static filter_t				filter_fft_F9;
static filter_t				filter_distance;

// variable s for I/O
static uint16_t				counter_demo;
static float				class;
static float				distance;
static float				time_of_flight_us;
static Header_t				Header;
static adc_t				adc;
static fft_t				fft;
static uint16_t				cnt_log_adc;			// max: 10.000 measurements --> 1000 seconds ( 10 measurements / file )
static uint16_t				cnt_log_fft;			// max: 10.000 measurements --> 1000 seconds ( 10 measurements / file )
static uint8_t				sensor_number	=     0;
static float				F1_threshold	=   140;
static float				F10_threshold	= 10000;
static run_t				run;





rp_dpin_t					led_0 = RP_LED0;		// LED0 is used for run != RUN_NONE
rp_dpin_t					led_1 = RP_LED1;		// LED1 is used for class (off=object / on=human)
rp_dpin_t					led_2 = RP_LED2;		
rp_dpin_t					led_3 = RP_LED3;
rp_pinDirection_t			led_dir = RP_OUT;



void init_adc(void)
{
	rp_Init();
	rp_DpinSetDirection( RP_DIO0_P, RP_IN);							// DIO0 as input
	rp_DpinSetState( RP_DIO0_P, RP_LOW);							// DIO0 set to low
	//rp_AcqSetDecimation( RP_DEC_64 );								// Decimation 64 --> 64 * 8 ns = 512 ns / sample
	rp_AcqSetSamplingRate( RP_SMP_1_953M );							// Sample rate 1.953Msps; Buffer time length 8.388ms; Decimation 64
	rp_AcqSetArmKeep( false );
	rp_AcqSetTriggerSrc( RP_TRIG_SRC_DISABLED );					// Trigger disabled
	rp_AcqSetTriggerDelay( 0 );
	rp_AcqSetAveraging(true);
	
	// set the length of the buffer for ADC
	adc.buffer_size			= (uint32_t)ADC_BUFFER_SIZE;		// 16.384
}


// ************************************************************ //
// ******************** Filter functions ********************** //
// ************************************************************ //
void init_filter( filter_t *f, uint16_t count_in )
{
	f->idx_max = count_in;
	f->idx		= 0;
	f->sum		= 0;
	f->buffer	= calloc( count_in, sizeof(float) );
}


// universal running average filter for x values
void run_mean_filter( filter_t *f, float *val_in )
{
	f->sum -= f->buffer[ f->idx ] - *val_in;
	f->buffer[ (f->idx)++ ] = *val_in;
	f->idx %= f->idx_max;
	f->filt_val = (float)(f->sum / f->idx_max);
}


// universal mean diff filter for x values
void run_diff_filter( filter_t *f, float *val_in )
{
	uint32_t new_diff = abs( f->latest_val - *val_in );
	f->sum +=  new_diff - f->buffer[f->idx];
	f->buffer[f->idx++] = new_diff;
	f->idx %= f->idx_max;
	f->latest_val = *val_in;
	f->filt_val = (float)( f->sum / f->idx_max);
}


// universal variance filter for x values
float run_variance_filter( filter_t *f)
{
	float sum=0;
	// build sum of the square of the differences between
	// single and mean value
	for( uint8_t ix=0; ix<f->idx_max; ix++ )
	{
		sum += (f->buffer[ix] - f->filt_val) * (f->buffer[ix] - f->filt_val);
	}
	// divide the sum by the count of measurements
	return( (float)(sum / (float)f->idx_max) );
}





// ************************************************************ //
// ******************** Feature calculation ******************* //
// ************************************************************ //
void calc_feature_F1( filter_t *f, float *val_in, float *val_out )
{
	run_diff_filter( f, val_in );
	*val_out = f->filt_val;
}



void calc_feature_F3( filter_t *f, float *val_in, float *val_out )
{
	run_mean_filter( f, val_in );
	*val_out	= f->filt_val;
}



void calc_feature_F4( filter_t *f, float *val_out )
{
	*val_out = run_variance_filter( f );
}


// F5 -> number of peaks between 39.5 and 41.5 kHz
void calc_feature_F5( fft_t *f, uint16_t *data, float *val_out )
{
	uint16_t min_ix = (uint16_t)( (float)39500 / f->freq_factor ) - 1;
	uint16_t max_ix = (uint16_t)( (float)41500 / f->freq_factor ) + 1;
	uint16_t cnt = 0;
	for( uint16_t ix=min_ix; ix < max_ix; ix++ ){
		if( (data[ix] < data[ix+1]) && (data[ix+1] > data[ix+2]) ){
			cnt++;
		}
	}
	*val_out = (float)cnt;
}


// F6 -> mean of F5 over 10 values
void calc_feature_F6( filter_t *f, float *val_in, float *val_out )
{
	run_mean_filter( f, val_in );
	*val_out = f->filt_val;
}



void calc_feature_F7( filter_t *f, feat_t *fe )
{
	fe->F7 = run_variance_filter( f );
}



void calc_feature_F8( fft_t *f, uint16_t *data, feat_t *fe )
{
	uint16_t min_ix = (uint16_t)( (float)(f->freq_center - 1000.0) / f->freq_factor );
	uint16_t max_ix = (uint16_t)( (float)(f->freq_center + 1000.0) / f->freq_factor );
	uint16_t c_ix = f->freq_center_index;
	uint16_t *p1, *p2;
	p1 = p2 = &data[ c_ix ];
	uint16_t dif1, dif2;
	bool flag1=false, flag2=false;
	dif1 = dif2 = 0;
	
	for( uint16_t ix = 0; ix < ((max_ix - min_ix)/2)-2; ix++ )
	{
		
		if( !flag2 ){
			p2++;
			if( ( *(p2+1) > *p2 ) && ( *(p2+2) < *(p2+1)  ) ){
				// next maximum found above/right from center frequency
				flag2 ^= true;
				dif2 = ((c_ix + (ix+1))*f->freq_factor) - f->freq_center;// f->freq_center - *(p2+1);
			}
			
		}
		
		if( !flag1 ){
			p1--;
			if( ( *(p1-1) > *p1 ) && ( *(p1-2) < *(p1-1)  ) ){
				// next maximum found under/left from center frequency
				flag1 ^= true;
				dif1 = f->freq_center - ((c_ix - (ix+1))*f->freq_factor);
			}
		}
		if( flag1 && flag2 ){break;}
	}
	fe->F8 = (dif1 + dif2) / 2;
}



void calc_feature_F9( filter_t *f, feat_t *feat )
{
	run_mean_filter( f, &feat->F8 );
	feat->F9 = f->filt_val;
}



void calc_feature_f10( filter_t *f, feat_t *fe )
{
	fe->F10 = run_variance_filter( f );
}



void set_min_max_fft_frequency( adc_t *a, fft_t *f, uint16_t freq_min_in, uint16_t freq_max_in, uint16_t w_width_in )
{
	
	a->w_width	= w_width_in;
	
	f->freq_factor	 	= (float)( ADC_SAMPLE_FREQUENCY / (float)(a->w_width * 2.0 ) );
	f->freq_min 		= ( freq_min_in < FFT_MIN_FREQ )?( FFT_MIN_FREQ ):( freq_min_in );
	f->freq_max 		= ( freq_max_in > FFT_MAX_FREQ )?( FFT_MAX_FREQ ):( freq_max_in );
	
	f->freq_min_index = (uint32_t)( (float)(f->freq_min / f->freq_factor) );
	f->freq_max_index = (uint32_t)( (float)(f->freq_max / f->freq_factor) );
}



/*
* @brief	This function is called, if ADC data has to be generated
*
* @details	The function calculates time differences, which are needed for 
*			starting and stopping the measurement depending on needed distance. 
* 			
*
*
*/
int start_adc( adc_t *a, int16_t *data, struct iic_s *i )
{
	// I²C programming - start ultrasonic
	i->buf[0] = 0;
	//iic.buf[1] = 0x51;			// measurement of distance
	i->buf[1] = 0x52;				// measurement of time 
	
	if ((write(iic.fd, iic.buf, 2)) != 2) {
		exit(ERR_I2C);
	}
	
	// wait for sonic to pass by 
	// the minimum distance
	usleep( ADC_START_DELAY_US + a->start_delay_us );
	
	// ADC is started
	rp_AcqStart();
	
	// wait until buffer is full
	usleep( ADC_BUFFER_DELAY_US );
	
	// ADC is stopped
	rp_AcqStop();
	
	// get data out of ADC buffer.
	rp_AcqGetLatestDataRaw( RP_CH_1, &a->buffer_size, data );
	
	do{
		usleep(500);
		read(iic.fd, iic.buf, 3 );
	}while( iic.buf[1]== 0xFF );
	
	
	time_of_flight_us = (float)( iic.buf[1]<<8 | iic.buf[2] );
	
	// active auto windowing feature
	if( (a->auto_adjust_window) && ( time_of_flight_us > ADC_MID_US ) ){
		a->start_delay_us = (time_of_flight_us - ADC_MID_US);
	}
	else{
		a->start_delay_us = 0;
	}
	
	// the index of the discovered object is calculated by percentaged delay 
	// between time-of-flight and adc start delay, multiplied by the count of adc buffer
	a->object_ix = (uint16_t)( (uint32_t)( ((time_of_flight_us - (ADC_START_DELAY_US + a->start_delay_us)) * a->buffer_size)/ADC_BUFFER_DELAY_US ) );
	
	return(0);
}




/*
* @brief	This function measures the distance to an object
*
* @details	The function searches the maximum amplitude and
*			its position. 
*
*
*
*/
bool measure_distance( float *distance_out )
{
	bool ret=false;
	// calculate the distance of the measured object
	// and write back to distance_out pointer
	*distance_out = V_SONIC_WAVE * (time_of_flight_us / 2e6);
	if( (*distance_out >= 0.30) && (*distance_out <= 5.00)){
		ret = true;
	}
	
	return(ret);
}



/*
* @brief		This function executes the FFT for given ADC values
* 
* @details		The function needs a signed integer 16 bit pointer to an array of the length <length>
*				and a pointer to an array where the result(s) will be stored.
*
* @length		length: the amount of data, which will be analized
* @data_in		*data_in: values between -2^12...2^12 ( array of adc values)
* @data_out		*data_out: pointer to array of the length (length/2+1)
*
*/
int calc_fft(adc_t *a, fft_t *f, int16_t *data )
{
	// create an output buffer for FFT
	// create a config variable for FFT
	uint32_t w_width = a->w_width;
	uint16_t object_ix = a->object_ix;
	uint16_t buf_size = a->buffer_size;
	 int16_t *data_in;
	kiss_fftr_cfg	fft_cfg;
	kiss_fft_cpx *fft_out = (kiss_fft_cpx *)calloc( (w_width) , sizeof(kiss_fft_cpx));
	uint32_t *fft_amp = calloc( w_width, sizeof(uint32_t) );
	float re, im;
	float max_amplitude = 0.0;
	
	if( object_ix < w_width/2 ){
		data_in = &data[0];						// object located "left" in signal
	}
	else if(object_ix > ( buf_size - w_width/2)){
		data_in = &data[buf_size - w_width/2];	// object located "right" in signal
	}
	else{
		data_in = &data[object_ix - w_width/2];	// object located around "mid" of signal
	}
	
	// allocate temporary space for calculation
	fft_cfg = kiss_fftr_alloc(w_width,0,NULL,NULL);
	
	
	// execute the FFT
	kiss_fftr( fft_cfg, (kiss_fft_scalar *) data_in, fft_out );
	
	
	// build the sum of imaginary and real part of the FFT
	// and search the max_amplitude
	for( uint16_t ix = f->freq_min_index; ix <= f->freq_max_index; ix++ ){
		re = (float)fft_out[ix].r, im = (float)fft_out[ix].i;
		fft_amp[ix-f->freq_min_index] = (uint32_t)(sqrtf( re*re + im*im ));
		if( fft_amp[ix-f->freq_min_index] > max_amplitude ){
			max_amplitude = (float)fft_amp[ix-f->freq_min_index];
			f->freq_center_index = ix;
		}
	}
	
	max_amplitude /= 1000.0;
	
	f->freq_center = f->freq_center_index * f->freq_factor;
	
	// normalize data
	for( uint16_t ix = 0; ix <= (f->freq_max_index - f->freq_min_index + 1); ix++ ){
		data[ix] = (uint16_t)( ((float)fft_amp[ix]) / max_amplitude );
	}
	
	free( fft_out );
	free( fft_cfg );
	free( fft_amp );
	
	return(ERR_NO);
}



uint8_t box_classificator( feat_t *feat )
{
	uint8_t ret = 0;
	
	// soft or hard object
	ret = ( (feat->F1 > F1_threshold) && (feat->F10 > F10_threshold) ) ? ( (uint8_t)2):( (uint8_t)1);
	
	return( ret );
}





// Thread No. 2 for measuring!
/*
*	@brief		Thread waits for signal from ServerThread in blocked mode
*				and processes the udp.command(s). 
*	@name		MeasureThread
*/
void MeasureThread( void *threadid )
{
	// variables for FFT and measurement
	bool					retval;								// return value for all adc/fft functions to proof distance of possible object
	bool 					send_data;
	static uint32_t			counter_ms			= 0;
	uint8_t					object_class;
	
	// variable for upd communication
	// char msg_tx[80];
	uint16_t *pData = ( uint16_t *)&(Header.p_data);
	
	run = RUN_NONE;
	
	init_adc();
	
	// init all LEDs (LED0 - LED3)
	LEDx_INIT();
	
	init_filter( &filter_F1, F1_FILTER_WIDTH);
	init_filter( &filter_F3, F2_FILTER_WIDTH);
	init_filter( &filter_fft_F6, F6_FILTER_WIDTH);
	init_filter( &filter_fft_F9, F9_FILTER_WIDTH);
	init_filter( &filter_distance, 6 );
	
	
	// Open I²C port for reading and writing
	if ((iic.fd = open(iic.fileName, O_RDWR)) < 0) {						
		exit(1);
	}
	
	// Set the port options and set the address of the device we wish to speak to
	if (ioctl(iic.fd, I2C_SLAVE_FORCE, iic.address) < 0) {					
		exit(1);
	}
	
	// initialize minimum distance, adc_delay offset and max/min fft frequency
	set_min_max_fft_frequency( (adc_t *)&adc, (fft_t *)&fft, FFT_MIN_FREQ, FFT_MAX_FREQ, WindowWidths[3] );
	
	
	
	Header.HeaderLength		= (float)(sizeof(Header_t) - sizeof(Header.p_data));
	Header.SampleFrequency	= (float)(ADC_SAMPLE_FREQUENCY);
	Header.ADCResolution	= (float)12;
	Header.SWVersion		= (float)SW_VERSION;
	
	// While loop - stay here while in "normal" operation
	while( 1 )
	{
		( run != RUN_NONE )?( LED0_ON ):( LED0_OFF );
		
		
		// if demo mode is active, the auto-adjustment of object focusing has to be switched off
		adc.auto_adjust_window = !( run == RUN_DEMO );
		
		// increment a counter; this is the time-base for all messurements and outputs [ms]
		counter_ms = (counter_ms + THREAD_PAUSE_MS) % 1500;
		
		// wait for signal from TimeThread (get signal every 100 ms)
		// thread will be set into blocked mode
		pthread_cond_wait(&run_meas_cond, &run_meas_mutex);
		
		
		start_adc( (adc_t *)&adc, (int16_t *)pData, &iic );
		retval = measure_distance( &distance );
		
		
		
		if( retval && ( run != RUN_ADC ) )
		{
			if( cnt_log_adc > 0 )
			{
				cnt_log_adc--;
				Header.DataLength = ADC_BUFFER_SIZE * 2;									// length in byte --> 32.768 * 2
				save_data_binary(&Header, (Header.HeaderLength / 2 + ADC_BUFFER_SIZE));
				if( cnt_log_adc == 0 ){
					LED3_OFF;
				}
			}
			// calculate the mean of the distance over x measurements
			run_mean_filter( &filter_distance, &distance );
			
			// start the FFT
			calc_fft( (adc_t *)&adc, (fft_t *)&fft, (int16_t *)pData);
			
			// get F2 (peak frequency)
			feat.F2 = fft.freq_center;
			
			// get F1 (mean difference over 10 times F2 )
			calc_feature_F1( &filter_F1, &feat.F2, &feat.F1 );
			
			// get F3 (mean over 10 times F2)
			calc_feature_F3( &filter_F3, &feat.F2, &feat.F3 );
			
			// get F4 (variance over 10 times F2)
			calc_feature_F4( &filter_F3, &feat.F4 );
			
			// get F5 (count of peaks within range of  39.5 kHz to 41.5 kHz)
			calc_feature_F5( (fft_t *)&fft, (uint16_t *)pData, &feat.F5 );
			
			// get F6 (mean over 10 times F5)
			calc_feature_F6( &filter_fft_F6, &feat.F5, &feat.F6 );
			
			// get F7 (variance over 10 times F5)
			calc_feature_F7( &filter_fft_F6, &feat );
			
			// get F8 (mean distance of 2 peaks (left/right) beneth F2)
			calc_feature_F8( (fft_t *)&fft, (uint16_t *)pData, &feat );
			
			// get F9 (mean over 10 times F8)
			calc_feature_F9( &filter_fft_F9, &feat );
			
			// get F10 (variance over 10 times F8)
			calc_feature_f10( &filter_fft_F9, &feat );
			
			// try to identify a soft / hard object_class
			object_class = box_classificator( &feat );
			
			if( cnt_log_fft > 0 )
			{
				cnt_log_fft--;
				Header.DataLength	= (fft.freq_max_index - fft.freq_min_index + 1) * sizeof(uint16_t);
				Header.X_Interval	= (float)(fft.freq_factor);
				Header.FFTWindowLength = (float)(adc.w_width);
				Header.FFTWindowOffsetIndex = (float)(fft.freq_min_index);
				save_data_binary( &Header, (Header.HeaderLength / 2 + (Header.DataLength / 2) ));
				if( cnt_log_fft == 0 ){
					LED4_OFF;
				}
			}
			
			( object_class == 2 )?( LED1_ON ):( LED1_OFF );
		}
		
		Header.DataType		= run;
		
		// the "run" variable is (re-)set by the ServerThread via UDP
		switch(run)
		{
			case RUN_NONE:
				break;
			case RUN_ADC:
				if( (counter_ms % 500) == 0 )
				{
					send_data = true;
				}
				break;
			case RUN_FFT:
				Header.DataLength	= (fft.freq_max_index - fft.freq_min_index + 1) * sizeof(uint16_t);
				Header.X_Interval	= (float)(fft.freq_factor);
				send_data = true;
				break;
			// the demo mode provides the output of object classes, which are recognized
			// It is used for the demo mode in car. Form2 is fed by the output data.
			case RUN_DEMO:
				if( (counter_demo++ % 2) == 0 )
				{
					pData[0] = (uint16_t)(sensor_number);
					pData[1] = (uint16_t)(object_class);
					pData[2] = (uint16_t)(distance * 100);
					send_data = true;
				}
				break;
			case RUN_LEARN_NN:
				break;
			case RUN_CALC_NN:
				break;
			default:
				run = RUN_NONE;
				break;
		}
		
		if( send_data ){
			send_data ^= true;
			Header.Class		= (float)(object_class);
			Header.Distance		= (float)(distance);
			Header.MeasDelay	= (float)adc.start_delay_us;
			memcpy( (uint16_t *)Header.p_data, pData, ADC_BUFFER_SIZE * sizeof(uint16_t));
			send_binary( (uint8_t *)&Header, (uint16_t)(Header.HeaderLength + Header.DataLength) );
		}
		
	} // end while(1)
	
	
	rp_Release();
	
	pthread_exit(NULL);
}



//********************************************
// public functions
//********************************************
void iic_set_log( int16_t cnt_log_in, uint8_t param )
{
	if( cnt_log_in < 0 )
	{
		init_data();
	}
	else if( cnt_log_in <= 10000 )
	{
			cnt_log_adc = ( param & 0x01 )?( cnt_log_in ):( 0 );
			cnt_log_fft = ( param & 0x02 )?( cnt_log_in ):( 0 );
			( cnt_log_adc > 0 )?(LED3_ON):(LED3_OFF);
			( cnt_log_fft > 0 )?(LED4_ON):(LED4_OFF);
	}
}



void iic_set_run( run_t run_in, bool param )
{
	run = (param)?(run_in):(RUN_NONE);
	Header.DataType = run;
	if( run == RUN_ADC ){
		Header.DataLength	= ( ADC_BUFFER_SIZE ) * sizeof(uint16_t);
		Header.X_Interval	= (float)(ADC_SAMPLE_TIME_NS);
	}
	else if( run == RUN_DEMO ){
		Header.DataLength	= ( 3 ) * sizeof(uint16_t);
		counter_demo = 0;
	}
}



void iic_set_sensor_number( uint8_t param )
{
	if( param > 0 && param < 3 ){
		sensor_number = param;
		Header.reserved2 = (int32_t)sensor_number;
		( sensor_number & 0x02 )?( rp_DpinSetState( led_2, RP_HIGH ) ):( rp_DpinSetState( led_2, RP_LOW ) );
	}
}



void iic_set_threshold( float param1, float param2 )
{
	F1_threshold	= param1;
	F10_threshold	= param2;
}



void iic_set_learn_nn(uint8_t param1)
{
	//if param1 == 0 --> stop learning
	run = (param1 == 0)?(RUN_NONE):(RUN_LEARN_NN);
	// assign the lower 2 bits of param1 to the class variable
	class = param1 & 0x03;
}



void iic_set_calc_nn(bool param1)
{
	run = (param1)?(RUN_CALC_NN):(RUN_NONE);
}



void iic_set_window_width( uint32_t w_width_in )
{
	set_min_max_fft_frequency( (adc_t *)&adc, (fft_t *)&fft, FFT_MIN_FREQ, FFT_MAX_FREQ, (uint16_t)WindowWidths[ w_width_in % 4 ] );
}


