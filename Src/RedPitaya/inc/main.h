#ifndef __MAIN_H__
#define __MAIN_H__


#ifdef __cplusplus
extern "C" {
#endif


#define THREAD_PAUSE_MS				(100)													//     50 ms
#define THREAD_PAUSE_US				(THREAD_PAUSE_MS*1000)									// 100.000 µs = 100 ms (per cycle)




//********************************************
// global structs
//********************************************


//********************************************
// global variables
//********************************************
extern pthread_mutex_t				run_meas_mutex;
extern pthread_cond_t				run_meas_cond;



#ifdef __cplusplus
}
#endif
#endif
