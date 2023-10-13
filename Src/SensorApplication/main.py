# Standard library imports
from datetime import datetime
from pathlib import Path
from queue import Queue
from stat import S_ISDIR, S_ISREG
import json
import os
import sys
import datetime
import threading
import socket
# Third-party library imports
from PIL import Image, ImageTk
from sklearn.metrics import (accuracy_score, confusion_matrix, f1_score, precision_score, recall_score, roc_curve,roc_auc_score )
from sklearn.model_selection import train_test_split
from sklearn.naive_bayes import GaussianNB
from tkinter import PhotoImage
from tkinter.filedialog import askdirectory, askopenfilename

import ttkbootstrap as ttk
from ttkbootstrap.constants import *

from ttkbootstrap.dialogs import Messagebox
import struct
import emlearn
import matplotlib.pyplot as plt
import numpy as np
import pandas as pd
import paramiko
import seaborn as sns
from functools import partial

from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg

PATH = Path(__file__).parent / 'assets'


class RedpitayaUI(ttk.Frame):

    def __init__(self, master):
        super().__init__(master)
        # Stop all thread when closing window
        self.master.protocol("WM_DELETE_WINDOW", self.on_closing)
        self.pack(fill=BOTH, expand=YES)
        
        self.images = [
            PhotoImage(
                name='reset', 
                file=PATH / 'icons8_reset_24px.png'),
            PhotoImage(
                name='sync', 
                file=PATH / 'icons8-sync-16.png'),
            PhotoImage(
                name='submit', 
                file=PATH / 'icons8_submit_progress_24px.png'),
            PhotoImage(
                name='seatbelt', 
                file=PATH / 'icons8-seatbelt-18.png'),
            PhotoImage(
                name='direction', 
                file=PATH / 'icons8_move_16px.png'),
            PhotoImage(
                name='movement', 
                file=PATH / 'icons8-walking-18.png'),
            PhotoImage(
                name='cardoor', 
                file=PATH / 'icons8-car-door-18.png'),
            PhotoImage(
                name='redpitaya', 
                file=PATH / '123.png'),
            PhotoImage(
                name='upload',
                file = PATH / 'icons8-upload-30.png'),
            PhotoImage(
                name='start',
                file = PATH / 'icons8-start-24.png'),
            PhotoImage(
                name='stop',
                file = PATH / 'icons8-stop-24.png'),
            PhotoImage(
                name='browse',
                file = PATH / 'icons8-ellipsis-16.png'),
            PhotoImage(
                name='train',
                file = PATH / 'icons8-ai-40.png'),
            PhotoImage(
                name='connect',
                file = PATH / 'icons8-no-connection-24.png'),
            PhotoImage(
                name='backup',
                file = PATH / 'icons8-data-backup-20.png')
        ]

        self.load_config()
        self.ssh_client = paramiko.SSHClient()
        self.ssh_client.set_missing_host_key_policy(paramiko.AutoAddPolicy())


        ##############################
        # GUI Layout
        for i in range(3):
            self.columnconfigure(i, weight=1)
        self.rowconfigure(0, weight=1)



        col1 = ttk.Frame(self, padding=10)
        col1.grid(row=0, column=0, sticky=NSEW)
        
        # device info
        dev_info = ttk.Labelframe(col1, text='Device Info', padding=10)
        #dev_info.pack(side=TOP, fill=X, expand=YES)
        dev_info.grid(row=0, column=0, sticky=NSEW, pady=(0, 10))
        col1.rowconfigure(0, weight=0)
        col1.columnconfigure(0, weight=1)
        
        # header
        dev_info_header = ttk.Frame(dev_info, padding=5)
        dev_info_header.pack(side=TOP, anchor=CENTER, fill=X)

        # Create a wrapper frame to center-align the content
        center_wrapper = ttk.Frame(dev_info_header)
        center_wrapper.pack(side=TOP, expand=YES)

        # restart connection button
        re_btn = ttk.Button(
            master=center_wrapper,
            image='reset',
            bootstyle=LINK,
            command=self.restart_button_pressed
        )
        re_btn.pack(side=LEFT)

        self.lbl = ttk.Label(center_wrapper, text='Red Pitaya, SRF02', font='Helvetica 10 bold', bootstyle='danger')
        self.lbl.pack(side=LEFT, padx=15)

        # Start connection button
        start_btn = ttk.Button(
            master=center_wrapper,
            image='connect',
            bootstyle=LINK,
            command=self.start_button_pressed
        )
        start_btn.pack(side=LEFT)

        # image
        ttk.Label(dev_info, image='redpitaya').pack(anchor='center')

        self.status_label = ttk.Label(dev_info, text="Waiting for connection...", anchor=CENTER, font='Helvetica 10 italic')
        self.status_label.pack(fill=X, pady=10)
        self.progress_var = ttk.IntVar()
        self.status_bar = ttk.Progressbar(dev_info, mode='determinate',bootstyle='primary-striped', variable=self.progress_var)
        self.progress_var.set(10)
        self.status_bar.pack(fill=X, pady=10)
        ####################################################################
        # Program Red Pitaya
        
        program_info = ttk.Labelframe(col1, text='Programming', padding=20)
        #program_info.pack(side=TOP, fill= BOTH, expand=YES, pady=(10, 0))
        program_info.grid(row=1, column=0, sticky=NSEW, pady=(10, 0))
        col1.rowconfigure(1, weight=0)
        col1.columnconfigure(1, weight=0)
        # program_info.rowconfigure(1, weight=2)
        # program_info.columnconfigure(0, weight=3)

        self.cbo_program = ttk.Combobox(
            master=program_info,
            values=['Programming Sensor', 'Uploading model', 'Running Sensor','Stopping Sensor','Others'],
            
        )
        self.cbo_program.current(0)
        self.cbo_program.pack(fill=X, padx=(10, 0))

        c_model_frame = ttk.Frame(program_info)
        c_model_frame.pack(fill=X, padx=(10, 0), pady=(10, 10))
        
        lbl = ttk.Label(
            master=c_model_frame,
            text='C Model:',
        )
        lbl.pack(side=LEFT, padx=(0, 10))
        self.modelC_path = ttk.Entry(c_model_frame, textvariable='modelC_path')

        self.modelC_path.pack(side=LEFT, fill=X, expand=YES)
        self.modelC_button = ttk.Button(c_model_frame, image="browse",bootstyle=LINK, command=lambda: self.browse_model_file('modelC_path'))
        self.modelC_button.pack(side= LEFT)


        execute = ttk.Button(
            master=program_info,
            image='upload',
            text='Execute',
            compound=BOTTOM,
            command=self.execute_button_pressed
        )
        execute.pack(padx=5, pady=20,side=TOP, fill=X)

        ##############################
        # Column 2
        col2 = ttk.Frame(self, padding=10)
        col2.grid(row=0, column=1, sticky=NSEW)

        # Training model
        preprocessing_data = ttk.Labelframe(col2, text='Data Preprocessing', padding=(15, 10))
        preprocessing_data.pack(side=TOP, fill=BOTH, expand=YES)

        op1 = ttk.Label(preprocessing_data, text='Building Dataset', font='Helvetica 10 bold')
        op1.pack(fill=X, pady=5)

        self.seatbelt_var = ttk.IntVar(preprocessing_data)
        self.door_var = ttk.IntVar(preprocessing_data)
        self.movement_var = ttk.IntVar(preprocessing_data)
        
        # no horizontal preprocessing_data
        op2 = ttk.Checkbutton(
            master=preprocessing_data,
            text='Seatbelt',
            variable=self.seatbelt_var,
            command=self.dataset_file_name

        )
        op2.pack(fill=X, padx=(20, 0), pady=5)

        btn = ttk.Button(
            master=op2,
            image='seatbelt',
            bootstyle=LINK,
            command=self.print_size
        )
        btn.pack(side=RIGHT)

        # inverse
        op3 = ttk.Checkbutton(
            master=preprocessing_data,
            text='Door',
            variable=self.door_var,
            command=self.dataset_file_name
        )
        op3.pack(fill=X, padx=(20, 0), pady=5)

        btn = ttk.Button(
            master=op3,
            image='cardoor',
            bootstyle=LINK,
        )
        btn.pack(side=RIGHT)


        op4 = ttk.Checkbutton(
            master=preprocessing_data,
            text='Movement',
            variable=self.movement_var,
            command = self.dataset_file_name
        )
        op4.pack(fill=X, padx=(20, 0), pady=5)

        btn = ttk.Button(
            master=op4,
            image='movement',
            bootstyle=LINK,
        )
        btn.pack(side=RIGHT)


        # scroll speed
        data_sample = ttk.Frame(preprocessing_data)
        data_sample.pack(fill=X, padx=(20, 0), pady=5)

        lbl = ttk.Label(data_sample, text='Sync:')
        lbl.pack(side=LEFT)

        scale = ttk.Scale(data_sample, value=35, from_=1, to=100)
        scale.pack(side=LEFT, fill=X, expand=YES, padx=5)

        scroll_speed_btn = ttk.Button(
            master=data_sample,
            image='sync',
            bootstyle=LINK,
            command=self.start_sync
        )
        scroll_speed_btn.pack(side=LEFT)
        self.stop_data_event = threading.Event()
        
        output_frame = ttk.Frame(preprocessing_data)
        output_frame.pack(fill=X, pady=5)
        
        output_dir = ttk.Label(output_frame, text='Filename:', font='Helvetica 10 italic bold')
        output_dir.pack(side= LEFT)
        
        self.dataset_name = ttk.Entry(output_frame, textvariable='dataset_name')
        self.dataset_name.pack(side = LEFT, fill=X, expand=YES, padx=5)
        
        sync_data = ttk.Button(output_frame, image='backup', bootstyle=LINK, command=self.build_dataset)
        sync_data.pack(side=RIGHT)
        
        

        # Training model
        
        self.training_model = ttk.Labelframe(
            master=col2,
            text='Trainning model',
            padding=(15, 10)
        )



        self.training_model.pack(
            side=TOP,
            fill=BOTH,
            expand=YES,
            pady=(10, 0)
        )


        # Browsing training data
        model_file_frame = ttk.Frame(self.training_model)
        model_file_frame.pack(fill=X,  pady=(5, 10))
        
        self.model_file_label = ttk.Label(model_file_frame, text="Dataset:")
        self.model_file_label.pack(side=LEFT)
        self.model_file_path = ttk.Entry(model_file_frame, textvariable='model_file_path')
        self.model_file_path.pack(side=LEFT, fill=X, expand=YES, padx=5)
        self.model_file_button = ttk.Button(model_file_frame, image="browse",bootstyle=LINK, command=lambda: self.browse_model_file('model_file_path'))
        self.model_file_button.pack(side= LEFT)

        # Create a frame for the checkbuttons
        checkbuttons_frame = ttk.Frame(self.training_model)
        checkbuttons_frame.pack(side=LEFT, fill=BOTH, expand=YES)
        self.emlModel_var = ttk.BooleanVar(checkbuttons_frame)

        self.emlConfusionMatrix_var = ttk.BooleanVar(checkbuttons_frame)
        self.emlConfusionMatrix_var.trace_add('write', self.toggle_op8_off)

        self.emlROC_var = ttk.BooleanVar(checkbuttons_frame)
        self.emlROC_var.trace_add('write', self.toggle_op7_off)
        
        
        self.op6 = ttk.Checkbutton(checkbuttons_frame, text='  Emlearn model', variable=self.emlModel_var)
        self.op6.pack(fill=X, pady=5)
        self.op7 = ttk.Checkbutton(checkbuttons_frame, text='Confusion matrix', variable=self.emlConfusionMatrix_var, bootstyle="success-square-toggle", state=DISABLED, command= self.update_plot)
        self.op7.pack(fill=X, pady=5)
        self.op8 = ttk.Checkbutton(checkbuttons_frame, text='RoC curve', variable=self.emlROC_var, bootstyle="success-square-toggle", state=DISABLED, command= self.update_plot)
        self.op8.pack(fill=X, pady=5)

        # Create a frame for the img_ex button
        img_ex_frame = ttk.Frame(self.training_model)
        img_ex_frame.pack(side=RIGHT, fill=BOTH, expand=YES)

        # Add the img_ex button to the img_ex_frame
        img_ex = ttk.Button(img_ex_frame, image='train', text='Train', compound='top', bootstyle='primary', command=self.train_model)
        img_ex.pack(side=TOP, anchor='center')  # Adjust the anchor and padding as needed
        
        score_frame = ttk.Frame(checkbuttons_frame)
        score_frame.pack(side=TOP, fill='both', expand=1, pady=10)
        lbl = ttk.Label(score_frame, text='Metrics:', font='Helvetica 10 bold')
        lbl.pack(side=TOP, fill=X, pady=5)

        # Define the labels
        metrics = [
            ("F1 Score:", 'f1_label'),
            ("Accuracy(%):", 'accuracy_label'),
            ("Recall:", 'recall_label'),
            ("Precision:", 'precision_label'),
        ]

        # Arrange the labels in two columns using nested frames
        for text, attr in metrics:
            row_frame = ttk.Frame(score_frame)  # Create a new frame for each row
            row_frame.pack(side=TOP, fill=X,padx=10, pady=5)  # Pack the row frame inside the score_frame

            label_text = ttk.Label(row_frame, text=text, font='Helvetica 10', bootstyle="primary")
            label_text.pack(side=LEFT, padx=5)  # Pack the label inside the row frame

            label_value = ttk.Label(row_frame, text='0.00', font='Helvetica 10')
            label_value.pack(side=RIGHT, padx=5)  # Pack the value inside the row frame

            setattr(self, attr, label_value)  # Assign to the attribute of the object

        # Run another thread to see log from UDP client
        # Create a queue to communicate with the thread
        col3 = ttk.Frame(self, padding=10)
        col3.grid(row=0, column=2, sticky=NSEW)

        # two finger gestures
        udp_debugging = ttk.Labelframe(
            master=col3,
            text='UDP Client',
            padding=10
        )
        udp_debugging.pack(side=TOP, fill=BOTH)

        
        self.debug_var = ttk.BooleanVar(udp_debugging)
        self.op9 = ttk.Checkbutton(udp_debugging, text='Manual / Debug mode',bootstyle="success-round-toggle", variable = self.debug_var, command = self.toggle_udp)
        self.op9.pack(fill=X, pady=5)
        self.stop_run_thread_event= threading.Event()
        self.stop_udp_event = threading.Event()
        self.stop_data_event = threading.Event()
        debugging = [
            ("Difference Average:", 'difference_average_label'),
            ("Center Frequency:", 'center_frequency_label'),
            ("Number of Peaks:", 'number_of_peaks_label'),
            ("Peak Frequency Distance:", 'mean_peak_frequency_distance_label'),
            ("Distance:",'distance_label'),
        ]

        # Arrange the labels in two columns using nested frames
        for text, attr in debugging:
            row_frame = ttk.Frame(udp_debugging)  # Create a new frame for each row
            row_frame.pack(side=TOP, fill=X,padx=10)  # Pack the row frame inside the score_frame

            label_text = ttk.Label(row_frame, text=text, font='Helvetica 10', bootstyle="primary")
            label_text.pack(side=LEFT, padx=5)  # Pack the label inside the row frame

            label_value = ttk.Label(row_frame, text='0.00', font='Helvetica 10 bold')
            label_value.pack(side=RIGHT)  # Pack the value inside the row frame

            setattr(self, attr, label_value)  # Assign to the attribute of the object
        

        self.custom_meter = ttk.Meter(
            master=udp_debugging,
            arcoffset=180,              # Start at the 9 o'clock position
            amounttotal=100,           # Max value
            amountused=100,              # Initial value
            metersize=150,             # Size of meter
            bootstyle="info",       # Color style for human
            meterthickness=10,         # Thickness of indicator
            showtext=False,             # Show text
            interactive=False,         
            subtext="Human" ,
            subtextfont="Helvetica 15 bold",
            subtextstyle="primary",
            stripethickness=10
        )
        
        self.custom_meter.pack(side=TOP,pady=10)
        ##############################
        # Debugging section
        self.debugging = ttk.Labelframe(
            master=col3,
            text='Debugging',
            padding=(15, 10)
        )
        self.debugging.pack(
            side=TOP,
            fill=BOTH,
            expand=YES,
            pady=(10, 0)
        )

        # turn on all checkbuttons
        for i in range(6,8):
            self.setvar(f'op{i}', 1)

        # turn off select buttons
        for j in [2, 3, 4]:
            self.setvar(f'op{j}', 0)
        ##############################
        # Debugging section        
        # 
        self.debugging.after(10, self.initialize_canvas)
        self.synced_file =0
        self.total_files=0
        self.lock = threading.Lock()
    # Load configuration
    def load_config(self):
        with open(PATH /'config.json', 'r') as config_file:
            config = json.load(config_file)
            self.hostname = config['hostname']
            self.username = config['username']
            self.password = config['password']
            self.local_folder = config['local_folder']
            self.data_folder = config['data_folder']
            self.proc_folder = config['proc_folder']
            self.model_folder = config['model_folder']
    
    def start_button_pressed(self):
        self.connect_to_ssh()
        
    def restart_button_pressed(self):
        # Close the existing connection
        self.ssh_client.close()
        # Update the status label
        self.status_label.config(text="Reconnecting...")
        self.status_label.update_idletasks()
        # Reconnect to SSH
        self.connect_to_ssh()

    def execute_button_pressed(self):
        if not self.is_ssh_connected():
            self.status_label.config(text="SSH not connected.")
            return

        choice = self.cbo_program.get()

        execute_map = {
            'Programming Sensor': partial(self.execute_command_in_remote_folder, 'make'),
            'Uploading model': self.upload_cbo,
            'Running Sensor': self.run_cbo,
            'Stopping Sensor': self.stop_cbo
        }
        
        execute = execute_map.get(choice)
        if execute:
            execute()
        else:
            print(f"Unknown choice: {choice}")
            # Handle or log unknown choice

    def toggle_op8_off(self, *args):
        if self.emlConfusionMatrix_var.get():
            self.emlROC_var.set(False)

    def toggle_op7_off(self, *args):
        if self.emlROC_var.get():
            self.emlConfusionMatrix_var.set(False)
    def program_cbo(self):
        self.execute_command_in_remote_folder('make')
        self.status_label.config(text="Programming sensor successfully")

    def upload_cbo(self):
        local_file_path = self.modelC_path.get()
        if local_file_path:
            self.upload_model()
            self.status_label.config(text="Model uploaded successfully")
        else:
            self.status_label.config(text="Please select a model file")
    def run_cbo(self):
        self.execute_command_in_remote_folder('./sensor_start.sh')
        self.status_label.config(text="Sensor is running")
    def stop_cbo(self):
        self.execute_command_in_remote_folder('kill -9 $(pgrep iic)')
        self.status_label.config(text="Sensor stopped")

    def connect_to_ssh(self):
        try:
            self.status_label.config(text="Reconnecting to sensor...")
            self.ssh_client.connect(self.hostname, username=self.username, password=self.password, timeout=10)
            self.sftp=self.ssh_client.open_sftp()
            self.status_label.config(text="Sensor connected successfully")
            self.lbl.config(bootstyle="success")
        except Exception as e:
            self.status_label.config(text=f"Connection failed")
    
    def run_sensor_command_thread(self):
        if hasattr(self, 'run_thread') and self.run_thread.is_alive():
            return
        self.stop_run_thread_event.clear()  # Clear the stop signal event
        print("Starting command thread...")
        self.run_thread = threading.Thread(target=self.run_cbo)
        self.run_thread.start()
    
    def stop_sensor_command_thread(self):
        print("Stopping command thread...")
        self.stop_run_thread_event.set()
        if self.run_thread.is_alive():
            self.run_thread.join()
        print("Command thread stopped.")

    def is_ssh_connected(self):
        transport = self.ssh_client.get_transport()
        return transport is not None and transport.is_active()
    
    def execute_command_in_remote_folder(self, command):
        """Execute a command in the remote folder"""
        command_to_execute = f"cd {self.proc_folder}; {command}"
        stdin, stdout, stderr = self.ssh_client.exec_command(command_to_execute)
        
        self.output_log = stdout.read().decode()
        print(self.output_log)
        self.error_log = stderr.read().decode()
        if self.error_log:
            print(f"Error occurred: {self.error_log}")
            self.status_label.config(text=f"Error occurred. Please check the log")
        self.status_label.config(text="Command executed successfully")
        

    def upload_model(self):
        """Upload the model file to the remote folder"""
        remote_file_path = os.path.join(self.model_folder,'model_f10.h').replace(os.path.sep, '/')
        try:
            # Check if the remote file exists
            if S_ISREG(self.ssh_client.open_sftp().lstat(remote_file_path).st_mode):
                # Remove the remote file
                self.ssh_client.open_sftp().remove(remote_file_path)
        except FileNotFoundError as e:
            # If the file doesn't exist, print the exception message and continue
            print("File not found:", e)

        # Upload the local file to the remote folder
        local_file_path = self.modelC_path.get()
        self.ssh_client.open_sftp().put(local_file_path, remote_file_path)
        
    def toggle_udp(self):
        if self.debug_var.get():
            # If the button is checked (on):
            if hasattr(self, 'udp_thread') and self.udp_thread.is_alive():
                # If the thread already exists and is running, don't start a new one
                return
            self.stop_udp_event.clear()  # Clear the stop signal event
            self.udp_thread = threading.Thread(target=self.listen_to_udp)
            self.udp_thread.start()
        else:
            self.stop_udp_event.set()
            if hasattr(self, 'udp_thread') and self.udp_thread.is_alive():
                self.udp_thread.join()


    def listen_to_udp(self):
        # Create and setup your UDP socket
        try:
            message = "-b 1"
            bytes_to_send = str.encode(message)
            server_address_port = (self.hostname, 61231)
            buffer_size = 65536
            struct_format = '10f i f'  # Modify this if the struct format changes
            struct_size = struct.calcsize(struct_format)
            udp_client_socket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
            udp_client_socket.sendto(bytes_to_send, server_address_port)
            print("UDP client started")
            udp_client_socket.settimeout(1)
            while not self.stop_udp_event.is_set():
                try:
                    packet = udp_client_socket.recv(buffer_size)

                    if len(packet) == struct_size:
                        unpacked_data = struct.unpack(struct_format, packet)
                        self.feature_values = unpacked_data[:10]
                        self.classification = unpacked_data[10]
                        self.distance= unpacked_data[11]
                        self.update_labels()
                    else:
                        print("Packet size mismatch")
                except socket.timeout:
                    continue
        except Exception as e:
            print("Error occurred:", e)
        finally:
            udp_client_socket.close()




    def update_labels(self):
        """Update the text of the labels based on the parsed values."""
        self.difference_average_label.config(text=f"{self.feature_values[0]:.2f}")
        self.center_frequency_label.config(text=f"{self.feature_values[2]:.2f} ±({self.feature_values[3]:.2f})")
        self.number_of_peaks_label.config(text=f"{self.feature_values[5]:.2f} ±({self.feature_values[6]:.2f})")
        self.mean_peak_frequency_distance_label.config(text=f"{self.feature_values[8]:.2f} ±({self.feature_values[9]:.2f})")
        self.distance_label.config(text=f"{self.distance:.2f} (cm)")
        if self.classification == 1:
            self.custom_meter["subtext"] = "Human"
            self.custom_meter["bootstyle"] = "success"
            self.custom_meter["subtextstyle"] = "success"
        else:
            self.custom_meter["subtext"] = "Non- Human"
            self.custom_meter["bootstyle"] = "danger"
            self.custom_meter["subtextstyle"] = "dark"
    def train_model(self):
        if not self.model_file_path.get():
            self.status_label.config(text="Please choose a dataset file")
            return
        data = pd.read_excel(self.model_file_path.get(), engine='openpyxl')
        selected_columns = [0,1,2,5]
        X = data.iloc[:, selected_columns].values
        y = data.iloc[:, 10].values
        X_train, self.X_test, y_train, self.y_test = train_test_split(X, y, test_size=0.2, random_state=42)
        self.model = GaussianNB()
        self.model.fit(X_train, y_train)

        y_pred = self.model.predict(self.X_test)
        # Evaluate the model
        self.cm = confusion_matrix(self.y_test, y_pred)
        self.thresh = self.cm.max() / 2.
        self.f1 = f1_score(self.y_test, y_pred, average='macro')
        self.accuracy = accuracy_score(self.y_test, y_pred)
        self.recall = recall_score(self.y_test, y_pred, average='macro')
        self.precision = precision_score(self.y_test, y_pred, average='macro')
        
        self.f1_label.config(text=f"{self.f1:.2f}")
        self.accuracy_label.config(text=f"{self.accuracy:.2f}")
        self.recall_label.config(text=f"{self.recall:.2f}")   
        self.precision_label.config(text=f"{self.precision:.2f}")
        self.status_label.config(text="Model trained successfully")
        # Unlock Debugging Options 
        self.op7.config(state='normal')
        self.op8.config(state='normal')
        self.convert_model()
    
    def initialize_canvas(self):
        self.fig, self.ax = plt.subplots(figsize=(5, 5))
        self.canvas = FigureCanvasTkAgg(self.fig, master=self.debugging)
        self.canvas.get_tk_widget().pack(side=BOTTOM, fill=BOTH, expand=True)
    
    def update_canvas(self):
        self.canvas.draw()
        self.canvas.flush_events()

    def clear_plot(self):
        self.ax.clear()

    def plot_confusion_matrix(self):
        """Plot the confusion matrix"""
        self.clear_plot()
        self.ax.matshow(self.cm, interpolation='nearest', cmap=plt.cm.Blues)
        self._decorate_confusion_matrix_plot()
        self.update_canvas()

    def plot_roc_curve(self):
        """Plot the ROC curve"""
        y_pred_proba = self.model.predict_proba(self.X_test)[::, 1]
        fpr, tpr, _ = roc_curve(self.y_test, y_pred_proba)
        auc = roc_auc_score(self.y_test, y_pred_proba)
        self.clear_plot()
        self.ax.plot(fpr, tpr, label=f"AUC = {auc:.2f}")
        self._decorate_roc_curve_plot()
        self.update_canvas()

    def _decorate_confusion_matrix_plot(self):
        tick_marks = np.arange(2)
        plt.xticks(tick_marks, ['NH', 'H'])
        plt.yticks(tick_marks, ['NH', 'H'])
        plt.xlabel('Predicted Label')
        plt.ylabel('True Label')
        for i in range(self.cm.shape[0]):
            for j in range(self.cm.shape[1]):
                color = "white" if self.cm[i, j] > self.thresh else "black"
                self.ax.text(j, i, str(self.cm[i, j]), va='center', ha='center', color=color)

    def _decorate_roc_curve_plot(self):
        self.ax.legend(loc=4)
        self.ax.set_xlabel('False Positive Rate')
        self.ax.set_ylabel('True Positive Rate')
        self.ax.set_title('ROC Curve')
    
    def update_plot(self):
        if self.emlConfusionMatrix_var.get():  # If confusion matrix is checked
            self.plot_confusion_matrix()
            self.emlROC_var.set(False)  # Deselect the ROC curve
        elif self.emlROC_var.get():  # If ROC curve is checked
            self.plot_roc_curve()
            self.emlConfusionMatrix_var.set(False)  # Deselect the confusion matrix
    
    def browse_model_file(self, entry):
        """Open dialogue to get directory and update variable"""
        self.update_idletasks()
        #d = askdirectory()
        e = askopenfilename()
        if e:
            self.setvar(entry, e)
        
    def dataset_file_name(self):
        """Create a file name for the dataset"""
        current_date = datetime.datetime.now().strftime('%d_%m')
        seatbelt_value = 'SB' if self.seatbelt_var.get() else 'NSB'
        door_value = 'D' if self.door_var.get() else 'ND'
        movement_value = 'MM' if self.movement_var.get() else 'NMM'
        self.filename = f"{current_date}_{seatbelt_value}_{door_value}_{movement_value}.xlsx"
        self.setvar('dataset_name', self.filename)
    
    def convert_model(self):
        """Convert model to C code"""
        if self.emlModel_var.get():
            path = './data_redpitaya/model_f10.h'
            cmodel = emlearn.convert(self.model, method='loadable')
            cmodel.save(path, name='model_f10')
            self.status_label.config(text='Model converted to C code successfully')
            self.setvar('modelC_path', path)
    

    #####################################################
    #Sync data function
    
    def get_data(self):
        """Get sensor data from Red Pitaya"""
        while not self.stop_data_event.is_set():
        # Start with the base directory
            dirs_to_process = [self.data_folder]

            while dirs_to_process:
                current_dir = dirs_to_process.pop()

                for entry in self.sftp.listdir_attr(current_dir):
                    remotepath = os.path.join(current_dir, entry.filename).replace('\\', '/')
                    
                    # Calculate relative path difference and mirror the structure locally
                    relative_path = os.path.relpath(remotepath, self.data_folder)
                    localpath = os.path.join(self.local_folder, relative_path)

                    mode = entry.st_mode
                    if S_ISREG(mode):
                        # Ensure local directory exists before downloading
                        local_directory = os.path.dirname(localpath)
                        if not os.path.exists(local_directory):
                            os.makedirs(local_directory)
                        
                        self.sftp.get(remotepath, localpath)
                        self.sftp.remove(remotepath)
                        self.synced_file += 1
                        self.update_progress_bar()
                    elif S_ISDIR(mode):
                        try:
                            os.mkdir(localpath)
                        except OSError:
                            pass
                        # Add the new directory to the list to process
                        dirs_to_process.append(remotepath)
            
            self.status_label.config(text='Data synced. Please building dataset.')
            self.stop_get_data_thread()
    def count_files(self):
        """Count the number of files to sync"""
        dirs_to_process = [self.data_folder]

        while dirs_to_process:
            current_dir = dirs_to_process.pop()

            for entry in self.sftp.listdir_attr(current_dir):
                mode = entry.st_mode
                if S_ISREG(mode):
                    with self.lock:  # Ensure thread safety
                        self.total_files += 1
                elif S_ISDIR(mode):
                    remotepath = os.path.join(current_dir, entry.filename).replace('\\', '/')
                    dirs_to_process.append(remotepath)
    
    def update_progress_bar(self):
        with self.lock:  # Ensure thread safety
            progress_percentage = self.synced_file / self.total_files * 100
        self.progress_var.set(progress_percentage)
    
    def start_sync(self):
        self.count_files()
        self.status_label.config(text=f"Syncing data... Files to sync: {self.total_files}")

        self.start_get_data_thread()
        

    def start_get_data_thread(self):
        self.stop_data_event.clear()

        if hasattr(self, 'data_thread') and self.data_thread.is_alive():
                # If the thread already exists and is running, don't start a new one
            return
        self.data_thread = threading.Thread(target=self.get_data)
        self.data_thread.start()

    def stop_get_data_thread(self):
        if hasattr(self, 'data_thread'):
            self.stop_data_event.set()
            #self.data_thread.join()


    def build_dataset(self):
        """Build the dataset from the binary files"""
        input_folder = self.local_folder
        output_file = self.local_folder + '/' + self.filename

        data_frame = pd.DataFrame()
        subfolders = [f for f in os.listdir(input_folder) if os.path.isdir(os.path.join(input_folder, f))]
        for subfolder in subfolders:
            subfolder_path = os.path.join(input_folder, subfolder)
            file_list = [f for f in os.listdir(subfolder_path) if os.path.isfile(os.path.join(subfolder_path, f)) and f.endswith('.bin')]

            for file_name in file_list:
                input_path = os.path.join(subfolder_path, file_name)
                struct_size = os.path.getsize(input_path)
                with open(input_path, 'rb') as f:
                    binary_data = f.read()
                # Unpack the binary data into a list of floats
                float_values = struct.unpack(f"{struct_size // 4}f", binary_data)
                # Split the float values into chunks of 10 features
                chunks = [float_values[i:i+10] for i in range(0, len(float_values), 10)]
                df = pd.DataFrame(chunks, columns=[f"Feature {i+1}" for i in range(10)])

                df["Label"] = 1 if subfolder == "human" else 0

                data_frame = pd.concat([data_frame, df], ignore_index=True)
                
                os.remove(input_path)
        # Save the data_frame to an Excel file
        data_frame.to_excel(output_file, index=False)
        self.status_label.config(text='Dataset built successfully')
    def callback(self):
        """Demo callback"""
        Messagebox.ok(
            title='Button callback', 
            message="You pressed a button."
        )
    def print_size(self):
        print(self.winfo_width(), self.winfo_height())
    def on_closing(self):
        self.debug_var.set(False)
        self.toggle_udp()
        self.stop_get_data_thread()
        self.master.destroy()
        sys.exit()

if __name__ == '__main__':
    app = ttk.Window("Red Pitaya GUI", "litera")
    app.geometry("1144x740")
    app.resizable(0, 0)
    RedpitayaUI(app)
    app.mainloop()
