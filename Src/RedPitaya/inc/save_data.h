#ifndef __SAVE_DATA_H__
#define __SAVE_DATA_H__


#ifdef __cplusplus
extern "C" {
#endif


//********************************************
// global structs
//********************************************


//********************************************
// global variables
//********************************************


//********************************************
// public functions prototypes
//********************************************
int save_data( char *p_data );
int save_data_binary(void* p_data, uint16_t number, const char* directory);
int init_data( const char* directory );
int save_data_feature(void* feat, uint16_t number, const char* directory);

#ifdef __cplusplus
}
#endif
#endif
