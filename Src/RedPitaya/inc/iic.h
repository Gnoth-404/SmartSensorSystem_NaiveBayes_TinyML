#ifndef __IIC_H__
#define __IIC_H__


#ifdef __cplusplus
extern "C" {
#endif


#define SW_VERSION 0.69
#define SAVE_ADC "/root/iic/data_adc_%03i.bin"
#define SAVE_FFT "/root/iic/data_fft_%03i.bin"
#define SAVE_DATA_BINARY "/root/iic/data_%03i.bin"
#define SAVE_FEAT_HUMAN  "/root/iic/human/human_feat_%03i.bin"
#define SAVE_FEAT_NONHUMAN  "/root/iic/nonhuman/nonhuman_feat_%03i.bin"
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
//********************************************
// global structs
//********************************************
typedef enum
{
	RUN_NONE	= 0,
	RUN_ADC,
	RUN_FFT,
	RUN_BOX,
	RUN_BAYES,
	RUN_DEMO,
	RUN_LEARN_NN,
	RUN_CALC_NN
}run_t;

typedef enum 
{
	NONE = 0,
	HUMAN,
	NON_HUMAN,
	WALL
}object_t;






typedef struct
{
	uint16_t	idx;
	uint16_t	idx_max;
	float		latest_val;
	float		sum;
	float		filt_val;
	float		*buffer;
}filter_t;

//********************************************
// global variables
//********************************************



//********************************************
// global enums
//********************************************



//********************************************
// global variables
//********************************************
void MeasureThread( void *threadid );
void iic_set_log( int16_t cnt_log_in,uint8_t ObjectType, uint8_t run_mode, const char *directory);
void iic_set_run( run_t run_in, bool param );
void iic_set_sensor_number( uint8_t param );
void iic_set_threshold( float param1, float param2 );
void iic_set_learn_nn(uint8_t param1);
void iic_set_calc_nn(bool param1);
void iic_set_window_width( uint32_t w_width_in );
void ButtonThread( void *threadid );
#ifdef __cplusplus
}
#endif
#endif
