#ifndef __IIC_H__
#define __IIC_H__


#ifdef __cplusplus
extern "C" {
#endif


#define SW_VERSION 0.22


//********************************************
// global structs
//********************************************
typedef enum
{
	RUN_NONE	= 0,
	RUN_ADC,
	RUN_FFT,
	RUN_FEAT,
	RUN_DEMO,
	RUN_LEARN_NN,
	RUN_CALC_NN
}run_t;



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
void iic_set_log( int16_t cnt_log_in, uint8_t param );
void iic_set_run( run_t run_in, bool param );
void iic_set_sensor_number( uint8_t param );
void iic_set_threshold( float param1, float param2 );
void iic_set_learn_nn(uint8_t param1);
void iic_set_calc_nn(bool param1);
void iic_set_window_width( uint32_t w_width_in );

#ifdef __cplusplus
}
#endif
#endif
