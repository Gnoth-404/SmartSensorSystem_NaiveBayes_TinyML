using Renci.SshNet;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using WinSCP;

namespace UDP_Client
{

    enum DataType_e
    {
        RUN_NONE,
        RUN_ADC,
        RUN_FFT,
        RUN_FEAT,
        RUN_DEMO,
        RUN_LOG,
        RUN_LEARN_NN,
        RUN_CALC_NN,
        RUN_CONFIG
    };



    struct Header_s
    {
        public float HeaderLength;
        public float DataLength;
        public float Class;
        public float DataType;
        public float X_Interval;
        public float Scaling;
        public float SampleFrequency;
        public float ADCResolution;
        public float AmbTemperature;
        public float MeasDelay;
        public float Distance;
        public float FFTWindowLength;
        public float FFTWindowOffsetIndex;
        public float SWVersion;
        public float reserved1;
        public float reserved2;
    }



    struct AppConfig_s
    {
        public uint ActiveSensors;
        public String[] SensorIPs;
        public int SensorPort;
        public int LocalPort;
        public int PlotCounter;
        public int PlotsMaximum;
        public bool SaveStreams;
        public int SaveStreamsCount;
        public CheckBox CheckedButton;
        public DataType_e RunActive;
        public DataType_e RunActiveOld;
        public bool Active;
        public bool HeaderChanged;

        public void SetDefault( )
        {
            this.SensorIPs = new string[2];
            this.Active = false;
            this.ActiveSensors        = 0;
            this.SensorIPs[0]= "192.168.128.1";
            this.SensorIPs[1] = "192.168.129.1";
            this.SensorPort = 61231;
            this.LocalPort = 61231;
            this.PlotCounter = 0;
            this.PlotsMaximum = 1;
            this.SaveStreams = false;
            this.SaveStreamsCount = 0;
            this.CheckedButton = null;
            this.RunActive = DataType_e.RUN_NONE;
            this.RunActiveOld = DataType_e.RUN_NONE;
            this.HeaderChanged = true;
        }
       
    }



    struct FFTConfig_s
    {
        public int MinFrequency;
        public int MinFrequencyIndex;
        public int MaxFrequency;
        public int MaxFrequencyIndex;
        public int DataCount;
        public int ColorCounter;
        public double SampleFrequency;
        public double SampleDecimation;
        public double FrequencyFactor;
        public bool ConfigChanged;

        public void SetDefault()
        {
            // (double)119.2093, (int)293, (int)377
            this.MinFrequency       = 35000;
            this.MinFrequencyIndex  = 293;
            this.MaxFrequency       = 45000;
            this.MaxFrequencyIndex  = 377;
            this.DataCount          = MaxFrequencyIndex - MinFrequencyIndex + 1;
            this.ColorCounter       = 0;
            this.SampleFrequency    = 125 * Math.Pow(10, 6); // [Hz] --> 125 MHz 
            this.SampleDecimation   = 64;
            this.FrequencyFactor    = (SampleFrequency / SampleDecimation) / (Math.Pow(2, 14));
            this.ConfigChanged        = false;
        }
    }



    struct ChartConfig_s
    {
        public double YMax;
        public double YMin;
        public double XMax;
        public double XMin;
        public UInt32 XInc;
        public UInt32 YInc;
        public String XTitle;
        public String YTitle;

        public void SetADC()
        {
            XMin = 0;
            XMax = 16000;
            YMin = -10000;
            YMax = +10000;
            XInc = 4000;
            YInc = 2000;
            XTitle = "Samples [-]";
            YTitle = "decimal Values";
        }

        public void SetFFT(double XMinIn=35000, double XMaxIn=45000)
        {
            XMin = XMinIn;
            XMax = XMaxIn;
            YMin = 0.0;
            //YMin = Double.NaN;
            YMax = 1000.0;
            //YMax = Double.NaN;
            XInc = 1000;
            YInc = 250;
            XTitle = "Frequency [Hz]";
            YTitle = "Amplitude sqrt(re² + im²)";
        }

    }


    


    public partial class Form1 : Form
    {
        internal static Form1 FM1 = null;
        internal static Form2 FM2 = null;
        internal static Form3 FM3 = null;


        private AutoResetEvent FillDataThreadSignal = new AutoResetEvent(false);
        private AutoResetEvent ProgRPThreadSignal = new AutoResetEvent(false);
        private AutoResetEvent UDP_ThreadSignal = new AutoResetEvent(false);

        Thread UDP_Thread = null;
        Thread FillDataThread = null;
        Thread ProgRPThread = null;
        IPAddress[] HostIPs;


        // UDP variables
        IPEndPoint[] EndPoint = new IPEndPoint[2] { null, null };
        IPEndPoint EndPointLocal = null;
        UdpClient UDP_Socket = null;
        bool UDPThreadFunctionRun = false;

        int PlotCounterOld = 0;

        readonly Color[] MyColor = { Color.Red, Color.Blue, Color.Green, Color.Orange,
                                     Color.DarkRed, Color.DarkBlue, Color.DarkGreen, Color.DarkOrange,
                                     Color.LightPink, Color.LightBlue, Color.LightGreen, Color.MediumOrchid};
        readonly String ProgDirRemote = "/root/iic";
        readonly String[] LearnObjects = { "nothing", "Wall", "Ceiling","HiFi board", "Human" };
        readonly int[] WindowWidths = { 1024, 2048, 4096, 8192 };

        String LibDirRemote = "";
        String ProgDirLocal = "";
        String WorkDirLocal = "";
        String SaveDirLocal = "";
        String SaveDirFFTLocal = "";
        String SaveDirADCLocal = "";
        String ActualSaveFile = "";

        UInt16[] XValues = { };

        AppConfig_s AppConfig = new AppConfig_s();
        FFTConfig_s FFTConfig = new FFTConfig_s();
        ChartConfig_s ChartConfig = new ChartConfig_s();

        private Header_s Header;
        private Byte[] HeaderBuffer = { };
        private Byte[] DataBuffer = { };

        // start up / initialize
        public Form1()
        {
            InitializeComponent();
        }


        // start up program
        private void Form1_Load(object sender, EventArgs e)
        {
            // restore standard values
            // #####################################
            AppConfig.SetDefault();
            FFTConfig.SetDefault();
            // #####################################
            // end

            FM1 = this;

            // set all necessary directories
            SetWorkingDirectories(Directory.GetCurrentDirectory());
            

            // set IP-addresses
            TextBoxRemoteIP1.Text = AppConfig.SensorIPs[0];
            TextBoxRemoteIP2.Text = AppConfig.SensorIPs[1];
            TextBoxRemotePort1.Text = AppConfig.SensorPort.ToString();


            // fill the ComboBox for Window widths used for FFT
            CbWindowWith.Items.Clear();
            for(int ix = 0; ix < WindowWidths.Length; ix++)
            {
                CbWindowWith.Items.Add(WindowWidths[ix].ToString());
            }
            CbWindowWith.SelectedIndex = WindowWidths.Length - 1;



            CbLearnObject.Items.Clear();
            for(int ix=0; ix<LearnObjects.Length; ix++)
            {
                CbLearnObject.Items.Add(LearnObjects[ix]);
            }
            CbLearnObject.SelectedIndex = 1;

            LabelRPSWVersion.Text = "VX.YY";
            LabelGUISWVersion.Text = "V0.22";

            CheckSensor();

            // create threads for background activity
            ProgRPThread = new Thread(new ThreadStart(ProgRPThreadFunction));
            FillDataThread = new Thread(new ThreadStart(FillDataThreadFunction));
            UDP_Thread = new Thread(new ThreadStart(UDPThreadFunction));

            // configure all threads
            FillDataThread.IsBackground = true;
            FillDataThread.Priority = ThreadPriority.Normal;
            UDP_Thread.IsBackground = true;
            UDP_Thread.Priority = ThreadPriority.AboveNormal;
            ProgRPThread.IsBackground = true;
            ProgRPThread.Priority = ThreadPriority.Normal;

            FillDataThread.Start();
            UDP_Thread.Start();
            ProgRPThread.Start();
        }



        // end / exit program
        private void Form1_Close(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (UDP_Socket != null)
                {
                    UDP_Socket.Close();
                }
            }
            catch{}
           
        }



        // button actions / functions
        private void ButtonRX_Click(object sender, EventArgs e)
        {
            TextBoxRX.Text = "";
        }



        private void ButtonCheckSensor_Click(object sender, EventArgs e)
        {
            SetStatus("Check for sensors!");
            CheckSensor();
            if(AppConfig.ActiveSensors == 1)
            {
                SetStatus("Sensor 1 active!");
            }
            else if(AppConfig.ActiveSensors == 2)
            {
                SetStatus("Sensor 2 active!");
            }
            else if(AppConfig.ActiveSensors == 3)
            {
                SetStatus("Sensor 1 and 2 active!");
            }
            else
            {
                SetStatus("No active sensor!");
            }
        }



        private void ButtonStartX_Click(object sender, EventArgs e)
        {
            CheckBox ButtonIn = (CheckBox)sender;
            String ButtonName = ButtonIn.Name;
            bool Checked = ButtonIn.Checked;
            String Cmd;
            String tmp="";

            AppConfig.CheckedButton = ButtonIn;

            ButtonStartADC.Enabled = !Checked;
            ButtonStartFFT.Enabled = !Checked;
            ButtonStartConverter.Enabled = !Checked;
            ButtonStartDemo.Enabled = !Checked;
            AppConfig.PlotCounter = 0;
            AppConfig.SaveStreams = (Checked) ? (CBSaveData.Checked) : (false);
            AppConfig.SaveStreamsCount = Convert.ToInt32(BoxMaxFiles.Text);
            AppConfig.PlotsMaximum = (AppConfig.SaveStreams) ? (1) : (3);
            AppConfig.PlotCounter = 0;
            LabelPlotCount.Text = "0";
            CBSaveData.Checked = AppConfig.SaveStreams;

            switch (ButtonName)
            {
                case "ButtonStartFFT":
                    ActualSaveFile = SaveDirFFTLocal + "\\fft_" + (((TbComment.Text) == "") ? ("") : (TbComment.Text + "_")) + ".txt";
                    if( !File.Exists(ActualSaveFile) && AppConfig.SaveStreams)
                    {
                        for(int ix=FFTConfig.MinFrequencyIndex; ix <= FFTConfig.MaxFrequencyIndex; ix++)
                        {
                            tmp += (ix * FFTConfig.FrequencyFactor).ToString("0") + ( (ix < FFTConfig.MaxFrequencyIndex) ?("\t") :("") );
                        }
                        tmp += "\n";
                        File.AppendAllText(ActualSaveFile, tmp);
                    }
                    ButtonIn.Text = (Checked) ? ("stop FFT") : ("start FFT");
                    CBSaveData.Enabled = !Checked;
                    Cmd = (Checked) ? ("-f 1") : ("-f 0");
                    UDPSendData(Cmd);
                    break;

                case "ButtonStartADC":
                    ActualSaveFile = SaveDirADCLocal + "\\adc_" + (((TbComment.Text) == "") ? ("") : (TbComment.Text + "_")) + ".txt";
                    ButtonIn.Text = (Checked) ? ("stop ADC") : ("start ADC");
                    Cmd = (Checked) ? ("-a 1") : ("-a 0");
                    UDPSendData(Cmd);
                    break;

                case "ButtonStartDemo":
                    ButtonIn.Text = (Checked) ? ("stop demo") : ("start demo");
                    if (FM2 == null && Checked)
                    {
                        // Die sensor IDs werden vergeben,
                        // damit die Zuordnung im Demo-Mode funktioniert
                        UDPSendData("-i 1",0);
                        UDPSendData("-i 2",1);

                        FM2 = new Form2();
                        FM2.ShowDialog();
                        FM2 = null;
                        ButtonIn.Checked = !Checked;
                    }
                    break;
                case "ButtonStartConverter":
                    if(FM3 == null && Checked)
                    {
                        FM3 = new Form3();
                        FM3.ShowDialog();
                        FM3 = null;
                        ButtonIn.Checked = !Checked;
                    }
                    break;
                default:
                    break;
            }

            ButtonIn.Enabled = true;

        }



        private void ButtonStartSensor_Click(object sender, EventArgs e)
        {
            String Cmd = "pkill iic";

            SetStatus("stop sensor...");
            SShSendData(Cmd, 0);
            SShSendData(Cmd, 1);
            SetStatus("start sensor...");
            Cmd = "./iic";
            SShSendData(Cmd,0);
            SShSendData(Cmd,1);
        }



        private void ButtonProgramRP_Click(object sender, EventArgs e)
        {

            ProgRPThreadSignal.Set();
            
        }



        private void ButtonStartUDPClient_CheckedChanged(object sender, EventArgs e)
        {
            bool Checked = ((CheckBox)sender).Checked;
            String IP1 = AppConfig.SensorIPs[0];
            String IP2 = AppConfig.SensorIPs[1];
            int Port = AppConfig.SensorPort;

            // Clientconfiguration for UDP
            EndPoint[0] = new IPEndPoint(IPAddress.Parse(IP1), (int)Port);
            EndPoint[1] = new IPEndPoint(IPAddress.Parse(IP2), (int)Port);
            EndPointLocal = new IPEndPoint(IPAddress.Any, (int)Port);

            BoxConfig.Enabled = !Checked;
            BoxRxTx.Enabled = Checked;
            BoxChart.Enabled = Checked;

            if (UDP_Socket != null){
                UDP_Socket.Close();
            }

            AppConfig.Active = Checked;
            ButtonStartClient.Text = (Checked)?( "stop client"):("start client");

            if (Checked)
            {
                UDP_Socket = new UdpClient(EndPointLocal);
                CBSaveData.Checked = !Checked;
            }
            else
            {
                LabelRPSWVersion.Text = "VX.YYz";
            }

            StartUdpThread(Checked);
        }



        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        // ##########################################
        // Thread functions
        // ##########################################

        public void StartUdpThread(bool Start = true)
        {
            UDPThreadFunctionRun = Start;
            if (Start)
            {
                UDP_ThreadSignal.Set();
            }
        }



        public void UDPThreadFunction()
        {
            Byte[] RxBuffer;
            float HeaderLength, DataLength;

            while (true)
            {
                if (!UDPThreadFunctionRun)
                {
                    // wait for Signal!
                    UDP_ThreadSignal.WaitOne();
                }
                try
                {
                    // go into blocked mode and wait for header data
                    RxBuffer = UDP_Socket.Receive(ref EndPointLocal);

                    if( ((UInt16)FillDataThread.ThreadState & (UInt16)System.Threading.ThreadState.WaitSleepJoin) != 0 )
                    {
                        // extract header information
                        // proof, if a minimum of two floats are received ( 2 * 4 byte)
                        if (RxBuffer.Length >= sizeof(float))
                        {
                            HeaderLength = BitConverter.ToSingle(RxBuffer, 0);
                            DataLength   = BitConverter.ToSingle(RxBuffer, 4);
                            HeaderBuffer = new byte[ (int)HeaderLength ];
                            DataBuffer   = new byte[ (int)DataLength ];
                            Buffer.BlockCopy(RxBuffer, 0, HeaderBuffer, 0, (int)HeaderLength);
                            Buffer.BlockCopy(RxBuffer, (int)HeaderLength, DataBuffer, 0, (int)DataLength);
                            FillDataThreadSignal.Set();
                        }

                    }

                }
                catch { }
            }
        }



        public void FillDataThreadFunction()
        {
            for (; ; )
            {
                string TmpString = "";
                bool WriteValueToRXBox = false;

                // wait for signal from UDP thread!
                FillDataThreadSignal.WaitOne();

                if( !CreateHeader())
                {
                    continue;
                }

                MethodInvoker QuestionDelegate = delegate
                {
                    // get the information about the kind of data, which was received as payload
                    AppConfig.RunActive = (DataType_e)Header.DataType;

                    if (AppConfig.RunActive != AppConfig.RunActiveOld)
                    {
                        StartMeasure(AppConfig.RunActive);
                        AppConfig.RunActiveOld = AppConfig.RunActive;
                    }


                    switch (AppConfig.RunActive)
                    {
                        case DataType_e.RUN_NONE:
                            // should never be reached
                            break;
                        case DataType_e.RUN_ADC:
                            CreateSeries(ref DataBuffer, false);
                            break;
                        case DataType_e.RUN_FFT:
                            CreateSeries(ref DataBuffer);
                            TmpString = "Dist: " + ( Header.Distance.ToString("0.00m") + " | Class: " + Header.Class.ToString("0") );
                            WriteValueToRXBox = true;
                            break;
                        case DataType_e.RUN_FEAT:
                            break;
                        case DataType_e.RUN_DEMO:
                            if (FM2 != null)
                            {
                                FM2.ReceiveData(ref DataBuffer);
                            }
                            break;
                        case DataType_e.RUN_LEARN_NN:
                            for (int ix = 0; ix < ( (int)Header.DataLength / 2); ix++)
                            {
                                TmpString += (BitConverter.ToInt16(DataBuffer, ix * 2)).ToString("0");
                            }
                            WriteValueToRXBox = true;
                            break;
                        case DataType_e.RUN_CALC_NN:
                            TmpString = Header.reserved1.ToString("0.00");
                            WriteValueToRXBox = true;
                            break;
                        case DataType_e.RUN_CONFIG:

                            break;
                        default:
                            break;

                    }

                    // only, if Form2 is closed, data will be written into Rxbox!
                    if (FM2 == null)
                    {
                        if (WriteValueToRXBox)
                        {
                        //TmpString = "Dist: " + ((double)Header[(int)Header_e.Distance] / 1000).ToString("0.00") + "m | Class: " + (Header[(int)Header_e.Class]).ToString() + "\n";
                            TextBoxRX.AppendText(TmpString + "\n");
                            TextBoxRX.ScrollToCaret();
                        }
                        TmpString = "Bytes: Head[" + Header.HeaderLength.ToString("0") + "] | Data[" + Header.DataLength.ToString("0") + "]";
                        LabelPlotCount.Text = (++AppConfig.PlotCounter).ToString();
                        LabelRxPayload.Text = TmpString;
                    }


                    if (AppConfig.SaveStreams)
                    {
                        if ((--AppConfig.SaveStreamsCount) <= 0)
                        {
                            AppConfig.SaveStreams = false;
                            AppConfig.CheckedButton.Checked = !AppConfig.CheckedButton.Checked;
                        }
                    }

                };
                Invoke(QuestionDelegate);
            }
        }



        public void ProgRPThreadFunction()
        {
            // init here

            while(true)
            {
                // stay here in blocked mode, waiting for signal to run!
                ProgRPThreadSignal.WaitOne();

                // signal received, now run!
                uint ret = PingSensor();
                WinSCP.Session SCP_Client = new WinSCP.Session();
                WinSCP.SessionOptions ScpSensorOptions;// = new SessionOptions();


                for (int ix = 0; ix < 2; ix++)
                {
                    if ((ret & (1 << ix)) != 0 )
                    {
                        ScpSensorOptions = new WinSCP.SessionOptions { Protocol = Protocol.Scp, HostName = AppConfig.SensorIPs[ix], UserName = "root", Password = "root", GiveUpSecurityAndAcceptAnySshHostKey = true, PortNumber = 22, };
                        SetStatus("programming sensor " + (ix + 1).ToString(), true, false, 10000);
                        SShSendData("pkill iic", ix);
                        SCP_Client.Open(ScpSensorOptions);
                        if (!SCP_Client.FileExists(ProgDirRemote))
                        {
                            SCP_Client.CreateDirectory(ProgDirRemote);
                        }
                        SCP_Client.SynchronizeDirectories(SynchronizationMode.Remote, ProgDirLocal, ProgDirRemote, true, true, SynchronizationCriteria.Either);
                        SShSendData("crontab /root/iic/cronjob", ix);
                        SetStatus("make iic", true, false, 10000);
                        SShSendData("ldconfig -v " + LibDirRemote, ix);
                        SShSendData("make", ix);
                        if (!SCP_Client.FileExists(ProgDirRemote + "/iic"))
                        {
                            SetStatus("failure sensor " + (ix + 1).ToString(), true, true);
                        }
                        else
                        {
                            SetStatus("success sensor " + (ix + 1).ToString());
                        }
                        SCP_Client.Close();
                        Thread.Sleep(500);
                    }
                }

            }
        }



        public void UDPSendData(string tx_data, uint SensorNumber = 0)
        {
            uint ix = SensorNumber % (uint)EndPoint.Length;
            Byte[] sendBytes = Encoding.ASCII.GetBytes(tx_data);

            UDP_Socket.Send(sendBytes, sendBytes.Length, EndPoint[ix]);
        }



        public int SShSendData( string tx_data, int SensorNumber=0 )
        {
            int ix = (int)( SensorNumber % AppConfig.SensorIPs.Length );
            SshClient RP_Client;

            if ( (AppConfig.ActiveSensors & ( 1 << ix )) != 0)
            {
                RP_Client = new SshClient(AppConfig.SensorIPs[ix], 22, "root", "root");
                tx_data = "(cd " + ProgDirRemote + "; " + tx_data + ")";
                RP_Client.Connect();
                RP_Client.RunCommand(tx_data);
                RP_Client.Disconnect();
            }

            return 0;
        }




        // Private functions
        private bool CreateHeader()
        {
            bool ret = false;
            int HeaderLength = (int)(BitConverter.ToSingle(HeaderBuffer, 0)) / 4;

            // Header-information count: 16 ( 16 x float = 16 * 4 byte = 64 byte / header )
            if (HeaderLength >= 16)
            {
                int ix = 0;
                Header.HeaderLength         = BitConverter.ToSingle(HeaderBuffer, ix);
                Header.DataLength           = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.Class                = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.DataType             = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.X_Interval           = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.Scaling              = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.SampleFrequency      = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.ADCResolution        = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.AmbTemperature       = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.MeasDelay            = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.Distance             = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.FFTWindowLength      = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.FFTWindowOffsetIndex = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.SWVersion            = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.reserved1            = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                Header.reserved2            = BitConverter.ToSingle(HeaderBuffer, (ix += 4));
                ret = true;
            }

            return (ret);
        }



        public uint PingSensor()
        {
            Ping RP_Ping = new Ping();
            IPStatus stat;
            uint ret = 0 ;

            try
            {
                stat = RP_Ping.Send(AppConfig.SensorIPs[0], 50, Encoding.ASCII.GetBytes("0")).Status;
                ret |= (stat == IPStatus.Success) ? ((uint)1) : ((uint)0);

                stat = RP_Ping.Send(AppConfig.SensorIPs[1], 50, Encoding.ASCII.GetBytes("0")).Status;
                ret |= (stat == IPStatus.Success) ? ((uint)2) : ((uint)0);

                AppConfig.ActiveSensors = ret;
            }
            catch { }
            

            return (ret);
        }



        private void LabelWorkingDir_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            folderBrowserDialog1.SelectedPath = Directory.GetCurrentDirectory();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetWorkingDirectories(folderBrowserDialog1.SelectedPath);
            }
        }



        private void LabelOpenWorkingDir_Click(object sender, EventArgs e)
        {
            Process.Start(WorkDirLocal);
        }



        private void LabelOpenProgramDir_Click(object sender, EventArgs e)
        {
            Process.Start(ProgDirLocal);
        }



        private void LabelOpenDataDir_Click(object sender, EventArgs e)
        {
            Process.Start(SaveDirLocal);
        }



        private void SetWorkingDirectories(String data_in)
        {
            LibDirRemote = ProgDirRemote + "/lib";

            WorkDirLocal = data_in;
            ProgDirLocal = WorkDirLocal + "\\proc";
            SaveDirLocal = WorkDirLocal + "\\save";
            SaveDirADCLocal = SaveDirLocal;// + "\\ADC";
            SaveDirFFTLocal = SaveDirLocal;// + "\\FFT";
            Directory.CreateDirectory(ProgDirLocal);
            Directory.CreateDirectory(SaveDirADCLocal);
            Directory.CreateDirectory(SaveDirFFTLocal);
            LabelWorkDir.Text = WorkDirLocal;
            LabelProgDir.Text = ProgDirLocal;
            LabelSaveDir.Text = SaveDirLocal;
        }

        

        private void SetStatus(String data_in, bool action=true, bool ErrorIn=false, int mSec=3000)
        {
            MethodInvoker QuestionDelegate = delegate
            {
                LableStatusGlobal.Text = "info: " + data_in;
                if (action)
                {
                    LableStatusGlobal.ForeColor = (ErrorIn)?(Color.Red): (Color.Blue);
                    T1.Stop();
                    T1.Enabled = true;
                    T1.Interval = mSec;
                    T1.Start();
                }
                   
                else
                {
                    LableStatusGlobal.ForeColor = Color.Black;
                }

            };
            Invoke(QuestionDelegate);
        }



        // Timer function(s)
        private void T1_Tick(object sender, EventArgs e)
        {
            SetStatus("idle", false);
            T1.Stop();
            T1.Enabled = false;
        }



        // Chart / Series function(s)
        private void SetChart( bool RunFFTIn = true )
        {
            ChartArea CA = ChartFFT.ChartAreas[0];

            // X-Axis
            CA.IsSameFontSizeForAllAxes = true;
            CA.AxisX.TitleForeColor = Color.Green;
            CA.AxisX.MajorGrid.LineColor = Color.SlateGray;
            CA.AxisX.MinorGrid.Enabled = true;
            CA.AxisX.MinorGrid.LineColor = Color.LightGray;
            CA.AxisX.Title = ChartConfig.XTitle;
            CA.AxisX.Minimum = ChartConfig.XMin;
            CA.AxisX.Maximum = ChartConfig.XMax;
            CA.AxisX.Interval = ChartConfig.XInc;
            CA.AxisX.MajorGrid.Interval = ChartConfig.XInc;
            CA.AxisX.MinorGrid.Interval = ChartConfig.XInc / 2;

            // Y-Axis
            CA.AxisY.TitleForeColor = Color.Green;
            CA.AxisY.MajorGrid.LineColor = Color.SlateGray;
            CA.AxisY.MinorGrid.Enabled = true;
            CA.AxisY.MinorGrid.LineColor = Color.LightGray;
            CA.AxisY.Title = ChartConfig.YTitle;
            CA.AxisY.Minimum = ChartConfig.YMin;
            CA.AxisY.Maximum = ChartConfig.YMax;
            CA.AxisY.IsStartedFromZero = true;
            CA.AxisY.Interval = ChartConfig.YInc;
            CA.AxisY.MajorGrid.Interval = ChartConfig.YInc;
            CA.AxisY.MinorGrid.Interval = ChartConfig.YInc / 2;

            
        }



        private bool CreateSeries(ref byte[] Data, bool FFT=true)
        {
            bool ret = false;
            float n = 0, m = 1;
            Series SeriesUpdate = new Series();
            String temp = "";
            int DataLength = (int)Header.DataLength / 2;

            // two bytes are connected to one sample!
            Int16[] YValues = new Int16[DataLength];
            

            for (int ix = 0; ix < (DataLength); ix++)
            {
                YValues[ix] = BitConverter.ToInt16(Data, ix*2);
                if (AppConfig.SaveStreams)
                {
                    temp += YValues[ix].ToString() + ((DataLength - ix > 1)?("\t"):(""));
                }
            }


            if(YValues.Length != XValues.Length)
            {
                XValues = new UInt16[DataLength];

                if (FFT)
                {
                    FFTConfig.FrequencyFactor = Header.X_Interval;

                    n = (float)FFTConfig.MinFrequency;
                    m = (float)FFTConfig.FrequencyFactor;
                }

                for (int ix = 0; ix < ( (int)Header.DataLength / 2); ix++)
                {
                    XValues[ix] = (UInt16)(n + (ix * m));
                }
            }



            while (ChartFFT.Series.Count > AppConfig.PlotsMaximum)
            {
                ChartFFT.Series.RemoveAt(0);
            }

            FFTConfig.ColorCounter++;
            SeriesUpdate.ChartType = SeriesChartType.Line;
            SeriesUpdate.BorderWidth = 1;
            SeriesUpdate.Color = MyColor[FFTConfig.ColorCounter % (MyColor.Length)];
            ChartFFT.Series.Add(SeriesUpdate);


            // perhaps add?
            SeriesUpdate.Points.DataBindXY(XValues, YValues);

            ChartFFT.ChartAreas[0].RecalculateAxesScale();


            if (AppConfig.SaveStreams)
            {
                temp += "\n";
                File.AppendAllText(ActualSaveFile, temp);
            }

            return ret;
        }



        private void TimerRxSpeed_Tick(object sender, EventArgs e)
        {
            LabelPlotSpeed.Text = Convert.ToString( Math.Abs(AppConfig.PlotCounter - PlotCounterOld)/ (TimerRxSpeed.Interval/1000) );
            PlotCounterOld = AppConfig.PlotCounter;
        }



        private void StartMeasure( DataType_e DataTypeNew )
        {
            // Version info is extracted at first
            LabelRPSWVersion.Text = Header.reserved1.ToString("V0.00");

            // this function is called if a new kind of payload came in!!
            switch ( DataTypeNew )
            {
                case DataType_e.RUN_ADC:
                    ChartConfig.SetADC();
                    SetChart(false);
                    break;
                case DataType_e.RUN_FFT:
                    ChartConfig.SetFFT();
                    SetChart();
                    break;
                case DataType_e.RUN_FEAT:
                    break;
                default:
                    // do nothing
                    break;
            }


            
            PlotCounterOld = 0;
            TimerRxSpeed.Interval = 2000;
            TimerRxSpeed.Start();
        }





        private void LabelSaveDir_Click(object sender, EventArgs e)
        {
            Process.Start(SaveDirLocal);
        }



        private void LabelVersionInfo_Click(object sender, EventArgs e)
        {
            Process.Start(ProgDirLocal + "\\src\\iic.c");
        }


        
        private void CBSaveLearningData_CheckedChanged(object sender, EventArgs e)
        {
            GPLearn.Enabled = ((CheckBox)sender).Checked;
            CbWindowWith.Enabled = !GPLearn.Enabled;
        }



        private void TextBoxRemoteIP1_TextChanged(object sender, EventArgs e)
        {
            AppConfig.SensorIPs[0] = TextBoxRemoteIP1.Text;
        }



        private void TextBoxRemoteIP2_TextChanged(object sender, EventArgs e)
        {
            AppConfig.SensorIPs[1] = TextBoxRemoteIP2.Text;
        }



        private void TextBoxRemotePort1_TextChanged(object sender, EventArgs e)
        {
            AppConfig.SensorPort = Int32.Parse(TextBoxRemotePort1.Text);
        }



        private void ButtonSendCmd_Click(object sender, EventArgs e)
        {
            UDPSendData(TbSendCmd.Text);
        }



        private void CheckSensor()
        {
            bool active;
            HostIPs = Dns.GetHostAddresses(Environment.MachineName);
            String HostIP = "";
            
            AppConfig.ActiveSensors = PingSensor();

            TextBoxLocalIP1.Text = "127.0.0.1";
            TextBoxLocalIP2.Text = "127.0.0.1";

            active = (AppConfig.ActiveSensors > 0);

            for (int ix = 0; ix < HostIPs.Length; ix++)
            {
                HostIP = HostIPs[ix].ToString();
                if (HostIP.Contains(AppConfig.SensorIPs[0].Substring(0, 11)))
                {
                    TextBoxLocalIP1.Text = HostIPs[ix].ToString();
                }
                else if (HostIP.Contains(AppConfig.SensorIPs[1].Substring(0, 11)))
                {
                    TextBoxLocalIP2.Text = HostIPs[ix].ToString();
                }
            }

            ButtonStartClient.Enabled = active;
            ButtonProgramRP.Enabled = active;
            ButtonStartServer.Enabled = active;

            PicSensor1.BackColor = ( (AppConfig.ActiveSensors & 0x01) != 0 ) ? (Color.Green) : (Color.Red);
            PicSensor2.BackColor = ( (AppConfig.ActiveSensors & 0x02) != 0 ) ? (Color.Green) : (Color.Red);

        }



        private void CbWindowWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AppConfig.Active)
            {
                UDPSendData("-w " + CbWindowWith.SelectedIndex.ToString(), 0);
                UDPSendData("-w " + CbWindowWith.SelectedIndex.ToString(), 1);
            }
        }



        private void CbXXX_CheckedChanged(object sender, EventArgs e)
        {
            ButtonStartLogging.Enabled = (CbFFT.Checked || CbADC.Checked);
        }



        private void ButtonStartLogging_Click(object sender, EventArgs e)
        {
            string Cmd;
            Cmd = "-l " + TbLogCount.Text + " " + (((CbFFT.Checked) ? (2) : (0)) | ((CbADC.Checked) ? (1) : (0))).ToString("0");
            UDPSendData(Cmd);
        }
    }


}
