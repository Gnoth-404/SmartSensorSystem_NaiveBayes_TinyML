
/* This code is sued for starting continous ranging on SRF02 sensor
 * 
 * (c) Red Pitaya  http://www.redpitaya.com
 *
 * This part of code is written in C programming language.
 * Please visit http://en.wikipedia.org/wiki/C_(programming_language)
 * for more details on the language used herein.
 */
#include <stdlib.h>
#include <unistd.h>
#include <pthread.h>
#include <syslog.h>
#include "rp.h"
#include "main.h"
#include "iic.h"
#include "udp_server.h"


//********************************************
// local variables
//********************************************
static const uint32_t		thread_pause_us = THREAD_PAUSE_US;
static volatile bool		run_thread = true;



//********************************************
// function prototypes
//********************************************
void	TimeThread( void *threadid );



//********************************************
// process entry-point
//********************************************
int main(int argc, char **argv)
{
	pthread_t t_ServerThread, t_MeasureThread, t_TimeThread;
	pthread_attr_t attr;
	void *th_status;
	
	// kill the parent-process
	// and fork the current execution to force a new PID and
	// start as a daemon
	daemon(0, 0);
	
	
	// start the udp server task in background!
	// set attribute for detaching status for each thread!
	pthread_attr_init( &attr );
	pthread_attr_setdetachstate( &attr, PTHREAD_CREATE_JOINABLE );
	
	
	// init all mutexes which are needed
	pthread_mutex_init( &run_meas_mutex, NULL );
	pthread_cond_init( &run_meas_cond, NULL );
	
	
	// create and start the threads
	pthread_create( &t_ServerThread,	&attr, (void *)&ServerThread,	NULL );
	pthread_create( &t_MeasureThread,	&attr, (void *)&MeasureThread,	NULL );
	pthread_create( &t_TimeThread,		&attr, (void *)&TimeThread,		NULL );
	
	
	// free the attribute again
	pthread_attr_destroy( &attr );
	
	
	// wait for three threads to terminate
	// go into blocked mode
	pthread_join( t_ServerThread,	&th_status );
	pthread_join( t_MeasureThread,	&th_status );
	pthread_join( t_TimeThread,		&th_status );
	
	
	// end main as last thread!
	pthread_exit( NULL );
	return( EXIT_SUCCESS );
}



// ************************************************************ //
// ******************** Time thread **** ********************** //
// ************************************************************ //
void TimeThread( void *threadid )
{
	while( run_thread )
	{
		usleep( thread_pause_us );
		pthread_cond_signal(&run_meas_cond);
	}
}

