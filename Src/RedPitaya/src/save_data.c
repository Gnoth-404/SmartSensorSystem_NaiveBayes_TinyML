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


//********************************************
// private defines
//********************************************


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
// int save_data_binary( void *p_data, uint16_t number )
// {
// 	FILE *file = NULL;
// 	char file_name[40];
	
// 	sprintf(file_name, SAVE_DATA_BINARY, (uint16_t)(counter++/10));
	
// 	if( (file = fopen(file_name,"a")) < 0 ){
// 		return(-1);
// 	}
	
// 	fwrite( (uint16_t *)p_data, (size_t)(sizeof(uint16_t)), number, file);
	
// 	fclose(file);
	
// 	return(0);
// }

int save_data_binary(void* p_data, uint16_t number, const char* directory)
{
    FILE* file = NULL;
    char file_name[50]; // Increase the size to accommodate the file path

    sprintf(file_name, directory, (uint16_t)(counter++ / 10));

    if ((file = fopen(file_name, "a")) == NULL) {
        return -1;
    }

    fwrite((uint16_t*)p_data, sizeof(uint16_t), number, file);

    fclose(file);

    return 0;
}

int init_data( const char* directory )
{
	char file_name[50];
	for( uint16_t ix=0; ix<=999; ix++){
		sprintf(file_name, directory, ix);
		if( remove( file_name ) < 0 ){
			break;
		}
	}
	counter=0;
	return(0);
}

int save_data_feature(void* feat, uint16_t number, const char* directory)
{
    FILE* file = NULL;
    char file_name[50]; // Increase the size to accommodate the file path

    sprintf(file_name, directory, (uint16_t)(counter++ / 10));

    if ((file = fopen(file_name, "ab")) == NULL) {
        return -1;
    }

    fwrite((uint16_t*)feat, sizeof(float), number, file);

    fclose(file);

    return 0;
}

