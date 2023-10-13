#include <arpa/inet.h>
#include <fcntl.h>
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
#include "udp_server.h"
#include "iic.h"

// daemon includes
#include <pthread.h>
#include <syslog.h>


#define UDP_PORT		61231


//********************************************
// private structs
//********************************************
typedef struct
{
	int			socket;
	socklen_t 	length;
	char		command;
	struct		sockaddr_in serveraddr, clientaddr;
	int32_t		parameter[2];
}udp_t;



//********************************************
// extern variables
//********************************************



//********************************************
// local variables
//********************************************
static udp_t				udp={-1,-1};


//********************************************
// local function prototypes
//********************************************
static void parse_udp_message( char *msg_rx );



//********************************************
// public functions
//********************************************
// Thread No. 1 for UDP!
/*
*	@brief		Thread waits for UDP message(s) in blocked mode
*				and parses the received message(s) and value(s).
*	@name		ServerThread
*/
void ServerThread( void *threadid )
{
	char rx_buffer[40];
	memset( &udp.serveraddr,	0, sizeof(udp.serveraddr) );
	memset( &udp.clientaddr,	0, sizeof(udp.clientaddr) ); 
	
	udp.serveraddr.sin_family = AF_INET;
	udp.serveraddr.sin_port = htons( UDP_PORT );
	udp.serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	udp.length = sizeof(udp.clientaddr);
	
	// Create a socket with UDP protocol
	udp.socket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
	bind( udp.socket, (struct sockaddr *)&udp.serveraddr, sizeof(udp.serveraddr));
	
	while( 1 ){
		memset( (void *)rx_buffer, (int)0, (size_t)(strlen( rx_buffer )) );
		// Go into blocked mode to free CPU; wait for UDP message
		if( recvfrom( udp.socket, rx_buffer, sizeof(rx_buffer), 0, (struct sockaddr *)&udp.clientaddr, &udp.length ) < 0 ){
			break;
		}
		
		parse_udp_message( rx_buffer );
		
	} // end while()
	
	close( udp.socket );
	pthread_exit(NULL);
}



void send_binary( uint8_t *data_in, uint16_t count )
{
	//uint16_t Sent;
	if( udp.socket >= 0 ){
		sendto( udp.socket, (uint8_t *)data_in, count, MSG_DONTWAIT,  (struct sockaddr *) &udp.clientaddr, udp.length);
	}
}



//********************************************
// local functions
//********************************************
static void parse_udp_message( char *msg_rx )
{
	char *rx_param;
	
	if( msg_rx[0] == '-' ){
		
		udp.command = msg_rx[1];
		if( (rx_param = strstr( msg_rx, " ")) != NULL ){
			udp.parameter[0] = atoi( rx_param++ );
			if( (rx_param = strstr( rx_param, " ")) != NULL ){
				udp.parameter[1] = atoi( rx_param++ );
			}
		}
		
		switch( udp.command )
		{
			case 'a':
				iic_set_run( RUN_ADC, (udp.parameter[0] == 1) );
				break;
			case 'd':
				// start/stop demo mode
				iic_set_run( RUN_DEMO, (udp.parameter[0] == 1) );
				break;
			case 'f':
				// start stream for FFT
				iic_set_run( RUN_FFT, (udp.parameter[0] == 1) );
				break;
			case 'i':
				// set the sensor number
				iic_set_sensor_number( (uint8_t)( abs( udp.parameter[0] )) );
				break;
			case 'l':
				iic_set_log( (int16_t)udp.parameter[0], (uint8_t)udp.parameter[1] );
				break;
			case 'n':
				iic_set_learn_nn( udp.parameter[0] );
				break;
			case 'o':
				iic_set_calc_nn( (udp.parameter[0] == 1) );
				break;
			case 't':
				// change threshold values for boxclassifier
				iic_set_threshold( (float)( abs(udp.parameter[0]) ), (float)( abs(udp.parameter[1]) ) );
				break;
			case 'w':
				iic_set_window_width( (uint32_t)(abs(udp.parameter[0])) );
				break;
			default:
			break;
		} // end switch udp.command
	} // end if '-'
	
}





