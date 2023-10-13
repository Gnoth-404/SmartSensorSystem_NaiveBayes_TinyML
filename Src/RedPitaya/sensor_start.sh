#bin#!/bin/bash

# Set the LD_LIBRARY_PATH environment variable and execute the 'iic' command
cd /root/redpitaya/Examples/Communication/test/proc 
LD_LIBRARY_PATH=/root/redpitaya/Examples/Communication/test/proc/lib ./iic
