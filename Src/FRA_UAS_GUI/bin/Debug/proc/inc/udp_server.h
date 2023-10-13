#ifndef __UDP_SERVER_H__
#define __UDP_SERVER_H__


#ifdef __cplusplus
extern "C" {
#endif


//********************************************
// public functions
//********************************************
void ServerThread( void *threadid );
void send_binary( uint8_t *data_in, uint16_t count );


#ifdef __cplusplus
}
#endif
#endif
