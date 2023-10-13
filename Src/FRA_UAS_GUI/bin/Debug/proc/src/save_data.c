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



//********************************************
// private defines
//********************************************
#define SAVE_DATA_BINARY	"/root/iic/data_%03i.bin"


//********************************************
// extern variables
//********************************************



//********************************************
// local variables
//********************************************
static volatile uint16_t counter=0;



//********************************************
// local function prototypes
//********************************************


//********************************************
// public functions
//********************************************
int save_data_binary( void *p_data, uint16_t number )
{
	FILE *file = NULL;
	char file_name[40];
	
	sprintf(file_name, SAVE_DATA_BINARY, (uint16_t)(counter++/10));
	
	if( (file = fopen(file_name,"a")) < 0 ){
		return(-1);
	}
	
	fwrite( (uint16_t *)p_data, (size_t)(sizeof(uint16_t)), number, file);
	
	fclose(file);
	
	return(0);
}



int init_data( void )
{
	char file_name[40];
	for( uint16_t ix=0; ix<=999; ix++){
		sprintf(file_name, SAVE_DATA_BINARY, ix);
		if( remove( file_name ) < 0 ){
			break;
		}
	}
	counter=0;
	return(0);
}


//********************************************
// local functions
//********************************************




