#ifndef __UDP_SERVER_H__
#define __UDP_SERVER_H__


#ifdef __cplusplus
extern "C" {
#endif
#include "iic.h"
//********************************************
// public functions
//********************************************
void ServerThread( void *threadid );
void send_binary( uint16_t *data_in, size_t count );


#ifdef __cplusplus
}
#endif
#endif
