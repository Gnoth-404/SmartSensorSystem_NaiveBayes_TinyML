#include <arpa/inet.h>
#include <fcntl.h>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <errno.h>
#include <stdint.h>
#include <stdbool.h>
#include <sys/socket.h>
#include <sys/time.h>

#include "rp.h"
#include "nn.h"


//********************************************
// private defines
//********************************************
#define NUM_INPUTS			2
#define NUM_HIDDEN_NEURONS	4
#define NUM_HIDDEN_LAYERS	4
#define NUM_OUTPUTS			3
#define MIN_ERROR			0.01
#define MAX_EPOCHS			4000

#define STEP(x,y)			(( ((float)x - (float)y) >= 0 ) ? (1u):(0u) )
#define SIGMOID(x)			(1.0/(1.0 + exp(-x)))


//********************************************
// private structs
//********************************************



//********************************************
// extern variables
//********************************************



//********************************************
// local variables
//********************************************
static volatile bool b_init=false;
static float WeightIH[NUM_INPUTS][NUM_HIDDEN_NEURONS];
static float WeigthHH[NUM_HIDDEN_LAYERS][NUM_HIDDEN_NEURONS][NUM_HIDDEN_NEURONS];
static float WeightHO[NUM_HIDDEN_NEURONS][NUM_OUTPUTS];
static float DeltaWeightIH[NUM_INPUTS][NUM_HIDDEN_NEURONS];
static float DeltaWeightHH[NUM_HIDDEN_LAYERS][NUM_HIDDEN_NEURONS][NUM_HIDDEN_NEURONS];
static float DeltaWeightHO[NUM_HIDDEN_NEURONS][NUM_OUTPUTS];
static float Neurons[NUM_HIDDEN_LAYERS][NUM_HIDDEN_NEURONS];
static float Outputs[NUM_OUTPUTS];
//static float eta = 0.01;
//static float alpha = 0.05;



//********************************************
// local function prototypes
//********************************************



//********************************************
// public functions
//********************************************

/*
 *		@brief			This function has to be called first!
 *
 *
*/
int init_neuronal_network( void )
{
	int ret=0;
	
	if( !b_init )
	{
		b_init ^= true;
		// set all input weights and deltas randomly!
		for( uint8_t ix1=0; ix1<=NUM_INPUTS; ix1++ ){
			for( uint8_t ix2=0; ix2<=NUM_HIDDEN_NEURONS; ix2++ ){
					WeightIH[ix1][ix2]			= (float)rand()/(float)(RAND_MAX) - 0.5;			// value between -0,5 ... +0,5
					DeltaWeightIH[ix1][ix2]		= 0.0;
			}
		}
		
		// set all hidden weights and deltas randomly!
		for( uint8_t ix1=0; ix1<=NUM_HIDDEN_LAYERS; ix1++ ){
			for( uint8_t ix2=0; ix2<NUM_HIDDEN_NEURONS; ix2++){
				for( uint8_t ix3=0; ix3<NUM_HIDDEN_NEURONS; ix3++ ){
					WeigthHH[ix1][ix2][ix3]				= (float)rand()/(float)(RAND_MAX) - 0.5;			// value between -0,5 ... +0,5
					DeltaWeightHH[ix1][ix2][ix3]		= 0.0;
				}
			}
		}
		
		// set all output weights and deltas randomly!
		for( uint8_t ix1=0; ix1<=NUM_HIDDEN_NEURONS; ix1++){
			for( uint8_t ix2=0; ix2<NUM_OUTPUTS; ix2++){
				WeightHO[ix1][ix2]			=  (float)rand()/(float)(RAND_MAX) - 0.5;
				DeltaWeightHO[ix1][ix2]		= 0.0;
			}
			
		}
	}
	else{
		ret=1;
	}

	
	return(ret);
}



int learn_neuronal_network( float *inputs_in, uint8_t class_in )
{
	
	/*
	int ret = 0;
	float ErrorOH[NUM_OUTPUTS][NUM_HIDDEN_LAYERS] = 0.0 ;
	float Sum;
	float DeltaH[NUM_HIDDEN_NEURONS	+ 1];
	float SumDOW[NUM_HIDDEN_NEURONS + 1];
	float DeltaOH;
	float Target[NUM_OUTPUTS];			// actual class!
	float Output;						// calculated output
	float SumO;							// calculated sum for output sigmoid
	
	
	if( (class_in>0) && (class_in<=NUM_OUTPUTS) ){
		class_in--;		// 0...NUM_OUTPUTS-1	-> Klasse
	}
	
	// set all outputs to the awaited value
	for( uint8_t i=0; i<NUM_OUTPUTS; i++){
		Target[i] = ( i==class_in )?(10.0):(0.0);
	}
	
	for( uint16_t epoch=0; epoch<(uint16_t)MAX_EPOCHS; epoch++) {
			
			// calculate the nn with old values
			nn_calc_forward( inputs_in );
			
			for( uint8_t i=0; i<NUM_OUTPUTS; i++){
				
			}
			
			// calculate the error square
			Error += 0.5 * (Target - Output) * (Target - Output) ;
			// calculate 
			DeltaOH = Output*(1.0 - Output)*(Target - Output) ;
			
			
			for( uint8_t j = 1 ; j <= NUM_HIDDEN_NEURONS ; j++ ) {
				SumDOW[j] = 0.0 ;
				SumDOW[j] += WeightHO[j] * DeltaOH ;
				DeltaH[j] = SumDOW[j] * Neurons[j] * (1.0 - Neurons[j]);
			}
			
			for( uint8_t j = 1 ; j <= NUM_HIDDEN_NEURONS ; j++ ) {
				DeltaWeightIH[0][j] = eta * DeltaH[j] + alpha * DeltaWeightIH[0][j] ;
				WeightIH[0][j] += DeltaWeightIH[0][j] ;
				for( uint8_t i = 1 ; i <= NUM_INPUTS ; i++ ) {
					DeltaWeightIH[i][j] = eta * Input[i] * DeltaH[j] + alpha * DeltaWeightIH[i][j];
					WeightIH[i][j] += DeltaWeightIH[i][j] ;
				}
			}
			
			
			DeltaWeightHO[0] = eta * DeltaOH + alpha * DeltaWeightHO[0] ;
			WeightHO[0] += DeltaWeightHO[0] ;
			for( uint8_t j = 1 ; j <= NUM_HIDDEN_NEURONS ; j++ ) {
				DeltaWeightHO[j] = eta * Neurons[j] * DeltaOH + alpha * DeltaWeightHO[j] ;
				WeightHO[j] += DeltaWeightHO[j] ;
			}
			
		if( Error <= (float)MIN_ERROR ){
			ret=0;
			break;
		}
		
	} // epoch < MAX_EPOCHS
	
	*/
	return(0);
}





// forward calculation
float nn_calc_forward( float *inputs_in )
{
	float Sum;
	
	// Compute activation of first hidden layer neurons
	for( uint8_t i = 0 ; i <NUM_HIDDEN_NEURONS; i++ ) {
		Sum = 0.0;
		for( uint8_t j = 0 ; j < NUM_INPUTS ; j++ ) {
			Sum += inputs_in[j] * WeightIH[j][i] ;
		}
		// first hidden layer is written
		Neurons[0][i] = SIGMOID( Sum );
	}
	
	// Compute activation of all hidden layers neurons
	if( NUM_HIDDEN_LAYERS > 1 ){
		for( uint8_t h=0; h<NUM_HIDDEN_LAYERS; h++){
			for( uint8_t i=0; i<NUM_HIDDEN_NEURONS; i++){
				Sum = 0.0;
				for( uint8_t j=0; j<NUM_HIDDEN_NEURONS; j++){
					Sum += Neurons[h][j] * WeigthHH[h][i][j];
				}
				// set output for next hidden layer
				Neurons[h+1][i] = SIGMOID( Sum );
			}
		}
	}
	
	// Compute activation of last hidden layer to output(s)
	for( uint8_t h=0; h<NUM_OUTPUTS; h++ ){
		Sum = 0.0;
		for( uint8_t i=0; i<NUM_HIDDEN_NEURONS; i++ ){
			Sum += Neurons[ NUM_HIDDEN_LAYERS - 1][i] * WeightHO[i][h];
		}
		Outputs[h] = SIGMOID(Sum);
	}
	return(Outputs[0]);
}




//********************************************
// local functions
//********************************************



