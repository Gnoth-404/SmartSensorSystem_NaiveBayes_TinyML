#define VERSION_1


using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace UDP_Client
{
    struct Sensor_s
    {   
        // distance
        private int Distance;
        private int DistFiltIndex;
        private int[] DistFiltArray;
        private int DistFiltSum;
        private int DistanceOffset;

        // class
        private int Class;
        private int ClassFiltIndex;
        private int[] ClassFiltArray;
        private int ClassFiltSum;
        private float ClassFiltered;
        
        public void ReInit(int FilterMembers=1)
        {
            this.Class = 1;
            this.ClassFiltIndex = 0;
            this.ClassFiltArray = new int[FilterMembers];
            this.ClassFiltSum = 0;
            this.ClassFiltered = 0;

            this.Distance = 0;
            this.DistFiltIndex = 0;
            this.DistFiltArray = new int[FilterMembers];
            this.DistFiltSum = 0;
            this.DistanceOffset = 0;
        }
        public void SetValues(int Class_In, int Distance_In)
        {
            this.DistFiltSum += Distance_In - this.DistFiltArray[DistFiltIndex];
            this.DistFiltArray[DistFiltIndex++] = Distance_In;
            this.DistFiltIndex %= DistFiltArray.Length;
            this.Distance = this.DistFiltSum / this.DistFiltArray.Length;

            this.ClassFiltSum += (Class_In) - this.ClassFiltArray[ClassFiltIndex];
            this.ClassFiltArray[ClassFiltIndex++] = Class_In;
            this.ClassFiltIndex %= ClassFiltArray.Length;
            this.ClassFiltered = (float)this.ClassFiltSum / (float)this.ClassFiltArray.Length;
            if (Class <= 1)
            {
                this.Class = (this.ClassFiltered >= 1.7) ? (2) : (1);
            }
            else
            {
                this.Class = (this.ClassFiltered <= 1.3) ? (1) : (2);
            }
        }
        public void SetDistanceOffset()
        {
            this.DistanceOffset = this.Distance;
        }
        public int GetDistanceOffset()
        {
            return (this.DistanceOffset);
        }
        public int GetDistance()
        {
            return ( this.Distance );
        }
        public int GetSize()
        {
            return ( Math.Abs(this.Distance - this.DistanceOffset) );
        }
        public int GetClass( )
        {
            return( ( ( this.Distance > (this.DistanceOffset + 5)) || (this.Distance < (this.DistanceOffset - 5)  ) )?( this.Class ):(1) );
        }
    }



    public partial class Form2 : Form
    {
        //readonly String[] ActiveSensorsNames = { "None", "First", "Second", "Both" };
        readonly String[] Modes = {  "Seat empty",               // 0
                                    "Object detected",          // 1
                                    "Small object detected",    // 2
                                    "Medium object detected",   // 3
                                    "Large object detected",    // 4
                                    "Human detected",           // 5
                                    "Small human detected",     // 6
                                    "Medium human detected",    // 7
                                    "Large human detected",     // 8
                                    "Seat initialize"           // 9
                                    };
        readonly Color[,] Colors1 = {   { Color.Green, Color.White },           // 0
                                        { Color.Red, Color.Black },             // 1
                                        { Color.Red, Color.Black },             // 2
                                        { Color.Red, Color.Black },             // 3
                                        { Color.Red, Color.Black },             // 4
                                        { Color.Blue, Color.White },            // 5
                                        { Color.Blue, Color.White },            // 6
                                        { Color.Blue, Color.White },            // 7
                                        { Color.Blue, Color.White },            // 8
                                        { Color.Yellow, Color.Black } };        // 9
        readonly int ObjectMinSize = 5;

        private Sensor_s[] Sensors = new Sensor_s[2];
        private byte[] DataIn;
        private int Feature_1 = 160;
        private int Feature_10 = 12000;
        private int SensorMediumSizeThreshold = 50;
        private int SensorLargeSizeThreshold = 70;
        private uint ActiveSensors = 0;
        private bool SizeDetection = false;
        private int SensorMode = 0;
        private static TextBox[] TB;

        // Thred infos
        private AutoResetEvent DataThreadSignal = new AutoResetEvent(false);
        Thread DataThread = null;

        internal static Form1 FM1 = Form1.FM1;

        public Form2()
        {
            InitializeComponent();
        }



        private void Form2_Load(object sender, EventArgs e)
        {

            // Create, configure and start the background thread for data reception
            DataThread= new Thread( new ParameterizedThreadStart( DataThreadFunction ) );
            DataThread.Priority = ThreadPriority.AboveNormal;
            DataThread.IsBackground = true;
            DataThread.Start();

            // initialize all sensors
            for(int ix=0; ix<Sensors.Length; ix++)
            {
                Sensors[ix].ReInit(5);
            }

            TB = new TextBox[] { TbSensor1Class, TbSensor2Class, TbSensor1Distance, TbSensor2Distance, TbHeightObject1, TbHeightObject2 };

            GbThresholds.Enabled = CbMeasureMode.Checked;

            TbF1.Text = Feature_1.ToString();
            TbF10.Text = Feature_10.ToString();
            TbOffset2.Text = "50";
            TbThresholdMediumDistance.Text = "20";
            TbThresholdLargeDistance.Text = "40";

            // init the sensor information
            TestSensors();

            // send threshold values to sensor(s)
            // and initialize the demo-mode
            ButtonSetDistanceThresholds.PerformClick();
            ButSendThresholds.PerformClick();
            ButSendOffset.PerformClick();

            SetStatus();
            TestSensors();

            FM1.UDPSendData("-i 1", 0);
            FM1.UDPSendData("-i 2", 1);
            FM1.UDPSendData("-d 1", 0);
            Thread.Sleep(50);
            FM1.UDPSendData("-d 1", 1);

            // this timer tests every 2 seconds, which sensor is active
            TimerSensor.Interval=5000;
            TimerSensor.Start();

        }



        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            FM1.UDPSendData("-d 0",0);
            FM1.UDPSendData("-d 0",1);
        }



        // this function is used to get the data from UDP client (Form1)
        public void ReceiveData(ref byte[] PayLoadIn )
        {

            // Payload has to be (minimum) 6 bytes --> 3 x uint16_t data
            if( PayLoadIn.Length >= 6 )
            {
                DataIn = new byte[PayLoadIn.Length];
                // copy the data and send signal to background thread
                PayLoadIn.CopyTo(DataIn, 0);
                DataThreadSignal.Set();
            }
        }



        private void ButtonSetOffset_Klick(object sender, EventArgs e)
        {
            // save the actual distance as the distance Offset
            Sensors[0].SetDistanceOffset();
            Sensors[1].SetDistanceOffset();
            TbOffset1.Text = Sensors[0].GetDistanceOffset().ToString("0 cm");
            TbOffset2.Text = Sensors[1].GetDistanceOffset().ToString("0 cm");
        }



        private void ButSendThresholds_Click(object sender, EventArgs e)
        {
            String Tmp;
            Feature_1 = int.Parse( TbF1.Text );
            Feature_10 = int.Parse( TbF10.Text );

            Tmp = "-t " + Feature_1.ToString() + " " + Feature_10.ToString();
            FM1.UDPSendData(Tmp,0);
            FM1.UDPSendData(Tmp,1);
        }



        private void TimerSensorPing(object sender, EventArgs e)
        {
            TestSensors();
        }



        private void TestSensors()
        {
            ActiveSensors = FM1.PingSensor();
            SizeDetection = (ActiveSensors == 3);
            CbMeasureMode.Enabled = SizeDetection;
        }



        // this function runs as a thread in background
        // It is activated at start-up of Form2 and used by function "ReceiveData()"
        public void DataThreadFunction(object Data)
        {
            int SensorID, SensorClass, SensorDistance, ObjectClassFromSensor1, ObjectSizeFromSensor1, ObjectSizeFromSensor2, Status = 0;

            for (;;)
            {
                // The thread waits here for the signal from UDP
                // in blocked mode
                DataThreadSignal.WaitOne();

               
                SensorID = ( BitConverter.ToUInt16(DataIn, 0) - 1) % Sensors.Length;     // 0 or 1
                SensorClass = BitConverter.ToUInt16(DataIn, 2);
                SensorDistance = BitConverter.ToUInt16(DataIn, 4);

                Sensors[SensorID].SetValues(SensorClass, SensorDistance);

                        
                // object type from Sensor 1
                ObjectClassFromSensor1 = Sensors[0].GetClass();
                ObjectSizeFromSensor1 = Sensors[0].GetSize();
                // object size / height from Sensor 2
                ObjectSizeFromSensor2 = Sensors[1].GetSize();

                if ( (ObjectClassFromSensor1 == 1) || (ObjectSizeFromSensor1 < ObjectMinSize ))
                {
                    // seat discovered
                    Status = 0;
                }
                else // ObjectClassFromSensor1 >= 2
                {
                    if (!SizeDetection) // one sensor active
                    {
                        // human
                        Status = 5;
                    }
                    else // two sensors active
                    {
                        // sensor two only gives information about object "present" or not
                        if(SensorMode == 0)
                        {
                            if(ObjectSizeFromSensor2 > ObjectMinSize)
                            {
                                // large human
                                Status = 8;
                            }
                            else
                            {
                                // small human
                                Status = 6;
                            }
                        }
                        else // sensor two gives infomrmation about height
                        {
                            if(ObjectSizeFromSensor2 >= SensorLargeSizeThreshold)
                            {
                                // Large human
                                Status = 8;
                            }
                            else if(ObjectSizeFromSensor2 >= SensorMediumSizeThreshold)
                            {
                                // medium human
                                Status = 7;
                            }
                            else
                            {
                                // small human
                                Status = 6;
                            }
                        }
                    }
                }


                MethodInvoker QuestionDelegate = delegate
                {
                    // fill textboxes with received data
                    TB[SensorID].Text = Sensors[SensorID].GetClass().ToString("0"); // SensorClass.ToString("0");
                    TB[SensorID + 2].Text = Sensors[SensorID].GetDistance().ToString("0 cm"); // SensorDistance.ToString("0") + " cm";
                    TB[SensorID + 4].Text = Sensors[SensorID].GetSize().ToString("0") + " cm";
                    SetStatus(Status);
                };
                Invoke(QuestionDelegate);
                

            } // end for(;;)
        }



        private void SetStatus(int NewStatus=9)
        {
            LabelStatus.BackColor = Colors1[NewStatus, 0];
            LabelStatus.ForeColor = Colors1[NewStatus, 1];
            LabelStatus.Text = Modes[NewStatus];
        }



        private void ButtonExitClick(object sender, EventArgs e)
        {
            this.Close();
        }



        private void ButtonSetDistanceThresholds_Click(object sender, EventArgs e)
        {
            SensorMediumSizeThreshold = int.Parse(TbThresholdMediumDistance.Text);
            SensorLargeSizeThreshold = int.Parse(TbThresholdLargeDistance.Text);

            if( SensorMediumSizeThreshold > SensorLargeSizeThreshold)
            {
                SensorMediumSizeThreshold = SensorLargeSizeThreshold;
                TbThresholdLargeDistance.Text = SensorLargeSizeThreshold.ToString("0");
                TbThresholdMediumDistance.Text = SensorMediumSizeThreshold.ToString("0");
            }
        }



        private void CbMeasureMode_CheckedChanged(object sender, EventArgs e)
        {
            SensorMode = (((CheckBox)sender).Checked) ? (1) : (0);
            ((CheckBox)sender).Text = (((CheckBox)sender).Checked) ? ("Mode 2") : ("Mode 1");
            GbThresholds.Enabled = (SensorMode == 1);
        }
    }
}
