#ifndef __NN_H__
#define __NN_H__


#ifdef __cplusplus
extern "C" {
#endif


//********************************************
// public functions
//********************************************
int init_neuronal_network( void );
int learn_neuronal_network( float *inputs_in, uint8_t class_in );
int calc_neuronal_network( float *inputs_in, float *class_out );
float nn_calc_forward( float *inputs_in );

#ifdef __cplusplus
}
#endif
#endif
