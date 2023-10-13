namespace UDP_Client
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.BoxConfig = new System.Windows.Forms.GroupBox();
            this.ButtonProgramRP = new System.Windows.Forms.Button();
            this.ButtonStartServer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonCheckServer = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxRemotePort1 = new System.Windows.Forms.TextBox();
            this.TextBoxLocalIP2 = new System.Windows.Forms.TextBox();
            this.TextBoxLocalIP1 = new System.Windows.Forms.TextBox();
            this.TextBoxRemoteIP2 = new System.Windows.Forms.TextBox();
            this.TextBoxRemoteIP1 = new System.Windows.Forms.TextBox();
            this.BoxRxTx = new System.Windows.Forms.GroupBox();
            this.TbLogCount = new System.Windows.Forms.TextBox();
            this.CbADC = new System.Windows.Forms.CheckBox();
            this.CbFFT = new System.Windows.Forms.CheckBox();
            this.GPLearn = new System.Windows.Forms.GroupBox();
            this.BoxMaxFiles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CbLearnObject = new System.Windows.Forms.ComboBox();
            this.TbComment = new System.Windows.Forms.TextBox();
            this.ButtonSendCmd = new System.Windows.Forms.Button();
            this.ButtonStartDemo = new System.Windows.Forms.CheckBox();
            this.CbWindowWith = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ButtonStartADC = new System.Windows.Forms.CheckBox();
            this.TbSendCmd = new System.Windows.Forms.TextBox();
            this.ButtonStartFFT = new System.Windows.Forms.CheckBox();
            this.ButtonStartConverter = new System.Windows.Forms.CheckBox();
            this.LabelRxPayload = new System.Windows.Forms.Label();
            this.LabelRPSWVersion = new System.Windows.Forms.Label();
            this.CBSaveData = new System.Windows.Forms.CheckBox();
            this.ButtonRxReset = new System.Windows.Forms.Button();
            this.TextBoxRX = new System.Windows.Forms.RichTextBox();
            this.ButtonStartClient = new System.Windows.Forms.CheckBox();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.BoxInformation = new System.Windows.Forms.GroupBox();
            this.LabelSaveDir = new System.Windows.Forms.Label();
            this.LabelProgDir = new System.Windows.Forms.Label();
            this.LabelWorkDir = new System.Windows.Forms.Label();
            this.LabelOpenDataDir = new System.Windows.Forms.Label();
            this.LabelOpenProgramDir = new System.Windows.Forms.Label();
            this.LabelOpenWorkingDir = new System.Windows.Forms.Label();
            this.LabelGUISWVersion = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.LableStatusGlobal = new System.Windows.Forms.Label();
            this.BoxChart = new System.Windows.Forms.GroupBox();
            this.LabelPlotSpeed = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.LabelPlotCount = new System.Windows.Forms.Label();
            this.ChartFFT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.TimerRxSpeed = new System.Windows.Forms.Timer(this.components);
            this.T1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PicSensor1 = new System.Windows.Forms.PictureBox();
            this.PicSensor2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.ButtonStartLogging = new System.Windows.Forms.Button();
            this.BoxConfig.SuspendLayout();
            this.BoxRxTx.SuspendLayout();
            this.GPLearn.SuspendLayout();
            this.BoxInformation.SuspendLayout();
            this.BoxChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicSensor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicSensor2)).BeginInit();
            this.SuspendLayout();
            // 
            // BoxConfig
            // 
            this.BoxConfig.Controls.Add(this.ButtonProgramRP);
            this.BoxConfig.Controls.Add(this.ButtonStartServer);
            this.BoxConfig.Controls.Add(this.label2);
            this.BoxConfig.Controls.Add(this.ButtonCheckServer);
            this.BoxConfig.Controls.Add(this.label7);
            this.BoxConfig.Controls.Add(this.label6);
            this.BoxConfig.Controls.Add(this.label9);
            this.BoxConfig.Controls.Add(this.label1);
            this.BoxConfig.Controls.Add(this.TextBoxRemotePort1);
            this.BoxConfig.Controls.Add(this.TextBoxLocalIP2);
            this.BoxConfig.Controls.Add(this.TextBoxLocalIP1);
            this.BoxConfig.Controls.Add(this.TextBoxRemoteIP2);
            this.BoxConfig.Controls.Add(this.TextBoxRemoteIP1);
            this.BoxConfig.Location = new System.Drawing.Point(12, 136);
            this.BoxConfig.Name = "BoxConfig";
            this.BoxConfig.Size = new System.Drawing.Size(237, 264);
            this.BoxConfig.TabIndex = 0;
            this.BoxConfig.TabStop = false;
            this.BoxConfig.Text = "UDP client config";
            // 
            // ButtonProgramRP
            // 
            this.ButtonProgramRP.Enabled = false;
            this.ButtonProgramRP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonProgramRP.Location = new System.Drawing.Point(6, 202);
            this.ButtonProgramRP.Name = "ButtonProgramRP";
            this.ButtonProgramRP.Size = new System.Drawing.Size(210, 23);
            this.ButtonProgramRP.TabIndex = 0;
            this.ButtonProgramRP.Text = "program sensor";
            this.ButtonProgramRP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonProgramRP.UseVisualStyleBackColor = true;
            this.ButtonProgramRP.Click += new System.EventHandler(this.ButtonProgramRP_Click);
            // 
            // ButtonStartServer
            // 
            this.ButtonStartServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonStartServer.Location = new System.Drawing.Point(6, 231);
            this.ButtonStartServer.Name = "ButtonStartServer";
            this.ButtonStartServer.Size = new System.Drawing.Size(210, 23);
            this.ButtonStartServer.TabIndex = 0;
            this.ButtonStartServer.Text = "(re-) start sensor";
            this.ButtonStartServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonStartServer.UseVisualStyleBackColor = true;
            this.ButtonStartServer.Click += new System.EventHandler(this.ButtonStartSensor_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sensor port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonCheckServer
            // 
            this.ButtonCheckServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonCheckServer.Location = new System.Drawing.Point(6, 173);
            this.ButtonCheckServer.Name = "ButtonCheckServer";
            this.ButtonCheckServer.Size = new System.Drawing.Size(210, 23);
            this.ButtonCheckServer.TabIndex = 0;
            this.ButtonCheckServer.Text = "check sensor connection";
            this.ButtonCheckServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonCheckServer.UseVisualStyleBackColor = true;
            this.ButtonCheckServer.Click += new System.EventHandler(this.ButtonCheckSensor_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "local IP-address 2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "local IP-address 1";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Sensor 2 IP-address";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sensor 1 IP-address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBoxRemotePort1
            // 
            this.TextBoxRemotePort1.Location = new System.Drawing.Point(131, 73);
            this.TextBoxRemotePort1.Name = "TextBoxRemotePort1";
            this.TextBoxRemotePort1.Size = new System.Drawing.Size(85, 20);
            this.TextBoxRemotePort1.TabIndex = 1;
            this.TextBoxRemotePort1.Text = "0";
            this.TextBoxRemotePort1.TextChanged += new System.EventHandler(this.TextBoxRemotePort1_TextChanged);
            // 
            // TextBoxLocalIP2
            // 
            this.TextBoxLocalIP2.Location = new System.Drawing.Point(131, 144);
            this.TextBoxLocalIP2.Name = "TextBoxLocalIP2";
            this.TextBoxLocalIP2.Size = new System.Drawing.Size(85, 20);
            this.TextBoxLocalIP2.TabIndex = 1;
            this.TextBoxLocalIP2.Text = "127.0.0.1";
            // 
            // TextBoxLocalIP1
            // 
            this.TextBoxLocalIP1.Location = new System.Drawing.Point(131, 117);
            this.TextBoxLocalIP1.Name = "TextBoxLocalIP1";
            this.TextBoxLocalIP1.Size = new System.Drawing.Size(85, 20);
            this.TextBoxLocalIP1.TabIndex = 1;
            this.TextBoxLocalIP1.Text = "127.0.0.1";
            // 
            // TextBoxRemoteIP2
            // 
            this.TextBoxRemoteIP2.Location = new System.Drawing.Point(131, 47);
            this.TextBoxRemoteIP2.Name = "TextBoxRemoteIP2";
            this.TextBoxRemoteIP2.Size = new System.Drawing.Size(85, 20);
            this.TextBoxRemoteIP2.TabIndex = 1;
            this.TextBoxRemoteIP2.Text = "0.0.0.0";
            this.TextBoxRemoteIP2.TextChanged += new System.EventHandler(this.TextBoxRemoteIP2_TextChanged);
            // 
            // TextBoxRemoteIP1
            // 
            this.TextBoxRemoteIP1.Location = new System.Drawing.Point(131, 21);
            this.TextBoxRemoteIP1.Name = "TextBoxRemoteIP1";
            this.TextBoxRemoteIP1.Size = new System.Drawing.Size(85, 20);
            this.TextBoxRemoteIP1.TabIndex = 1;
            this.TextBoxRemoteIP1.Text = "0.0.0.0";
            this.TextBoxRemoteIP1.TextChanged += new System.EventHandler(this.TextBoxRemoteIP1_TextChanged);
            // 
            // BoxRxTx
            // 
            this.BoxRxTx.Controls.Add(this.ButtonStartLogging);
            this.BoxRxTx.Controls.Add(this.TbLogCount);
            this.BoxRxTx.Controls.Add(this.CbADC);
            this.BoxRxTx.Controls.Add(this.CbFFT);
            this.BoxRxTx.Controls.Add(this.GPLearn);
            this.BoxRxTx.Controls.Add(this.ButtonSendCmd);
            this.BoxRxTx.Controls.Add(this.ButtonStartDemo);
            this.BoxRxTx.Controls.Add(this.CbWindowWith);
            this.BoxRxTx.Controls.Add(this.label10);
            this.BoxRxTx.Controls.Add(this.ButtonStartADC);
            this.BoxRxTx.Controls.Add(this.TbSendCmd);
            this.BoxRxTx.Controls.Add(this.ButtonStartFFT);
            this.BoxRxTx.Controls.Add(this.ButtonStartConverter);
            this.BoxRxTx.Controls.Add(this.LabelRxPayload);
            this.BoxRxTx.Controls.Add(this.LabelRPSWVersion);
            this.BoxRxTx.Controls.Add(this.CBSaveData);
            this.BoxRxTx.Controls.Add(this.ButtonRxReset);
            this.BoxRxTx.Controls.Add(this.TextBoxRX);
            this.BoxRxTx.Enabled = false;
            this.BoxRxTx.Location = new System.Drawing.Point(255, 136);
            this.BoxRxTx.Name = "BoxRxTx";
            this.BoxRxTx.Size = new System.Drawing.Size(240, 509);
            this.BoxRxTx.TabIndex = 1;
            this.BoxRxTx.TabStop = false;
            this.BoxRxTx.Text = "Send / Receive";
            // 
            // TbLogCount
            // 
            this.TbLogCount.Location = new System.Drawing.Point(189, 313);
            this.TbLogCount.Name = "TbLogCount";
            this.TbLogCount.Size = new System.Drawing.Size(45, 20);
            this.TbLogCount.TabIndex = 1;
            this.TbLogCount.Text = "5";
            this.TbLogCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CbADC
            // 
            this.CbADC.AutoSize = true;
            this.CbADC.Location = new System.Drawing.Point(135, 290);
            this.CbADC.Name = "CbADC";
            this.CbADC.Size = new System.Drawing.Size(48, 17);
            this.CbADC.TabIndex = 13;
            this.CbADC.Text = "ADC";
            this.CbADC.UseVisualStyleBackColor = true;
            this.CbADC.CheckedChanged += new System.EventHandler(this.CbXXX_CheckedChanged);
            // 
            // CbFFT
            // 
            this.CbFFT.AutoSize = true;
            this.CbFFT.Location = new System.Drawing.Point(189, 290);
            this.CbFFT.Name = "CbFFT";
            this.CbFFT.Size = new System.Drawing.Size(45, 17);
            this.CbFFT.TabIndex = 13;
            this.CbFFT.Text = "FFT";
            this.CbFFT.UseVisualStyleBackColor = true;
            this.CbFFT.CheckedChanged += new System.EventHandler(this.CbXXX_CheckedChanged);
            // 
            // GPLearn
            // 
            this.GPLearn.Controls.Add(this.BoxMaxFiles);
            this.GPLearn.Controls.Add(this.label3);
            this.GPLearn.Controls.Add(this.label8);
            this.GPLearn.Controls.Add(this.CbLearnObject);
            this.GPLearn.Controls.Add(this.TbComment);
            this.GPLearn.Enabled = false;
            this.GPLearn.Location = new System.Drawing.Point(6, 104);
            this.GPLearn.Name = "GPLearn";
            this.GPLearn.Size = new System.Drawing.Size(208, 105);
            this.GPLearn.TabIndex = 12;
            this.GPLearn.TabStop = false;
            this.GPLearn.Text = "learn configs";
            // 
            // BoxMaxFiles
            // 
            this.BoxMaxFiles.Location = new System.Drawing.Point(130, 40);
            this.BoxMaxFiles.Name = "BoxMaxFiles";
            this.BoxMaxFiles.Size = new System.Drawing.Size(72, 20);
            this.BoxMaxFiles.TabIndex = 1;
            this.BoxMaxFiles.Text = "5";
            this.BoxMaxFiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "object:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "measurements:";
            this.label8.Click += new System.EventHandler(this.LabelSaveDir_Click);
            // 
            // CbLearnObject
            // 
            this.CbLearnObject.FormattingEnabled = true;
            this.CbLearnObject.Location = new System.Drawing.Point(130, 13);
            this.CbLearnObject.Name = "CbLearnObject";
            this.CbLearnObject.Size = new System.Drawing.Size(72, 21);
            this.CbLearnObject.TabIndex = 12;
            // 
            // TbComment
            // 
            this.TbComment.Location = new System.Drawing.Point(6, 71);
            this.TbComment.Name = "TbComment";
            this.TbComment.Size = new System.Drawing.Size(196, 20);
            this.TbComment.TabIndex = 1;
            // 
            // ButtonSendCmd
            // 
            this.ButtonSendCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonSendCmd.Location = new System.Drawing.Point(98, 45);
            this.ButtonSendCmd.Name = "ButtonSendCmd";
            this.ButtonSendCmd.Size = new System.Drawing.Size(68, 23);
            this.ButtonSendCmd.TabIndex = 0;
            this.ButtonSendCmd.Text = "send cmd";
            this.ButtonSendCmd.UseVisualStyleBackColor = true;
            this.ButtonSendCmd.Click += new System.EventHandler(this.ButtonSendCmd_Click);
            // 
            // ButtonStartDemo
            // 
            this.ButtonStartDemo.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButtonStartDemo.Location = new System.Drawing.Point(6, 347);
            this.ButtonStartDemo.Name = "ButtonStartDemo";
            this.ButtonStartDemo.Size = new System.Drawing.Size(103, 23);
            this.ButtonStartDemo.TabIndex = 11;
            this.ButtonStartDemo.Text = "start demo";
            this.ButtonStartDemo.UseVisualStyleBackColor = true;
            this.ButtonStartDemo.CheckedChanged += new System.EventHandler(this.ButtonStartX_Click);
            // 
            // CbWindowWith
            // 
            this.CbWindowWith.FormattingEnabled = true;
            this.CbWindowWith.Location = new System.Drawing.Point(136, 79);
            this.CbWindowWith.Name = "CbWindowWith";
            this.CbWindowWith.Size = new System.Drawing.Size(72, 21);
            this.CbWindowWith.TabIndex = 12;
            this.CbWindowWith.SelectedIndexChanged += new System.EventHandler(this.CbWindowWith_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Sensor SW Version:";
            this.label10.Click += new System.EventHandler(this.LabelSaveDir_Click);
            // 
            // ButtonStartADC
            // 
            this.ButtonStartADC.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButtonStartADC.Location = new System.Drawing.Point(6, 256);
            this.ButtonStartADC.Name = "ButtonStartADC";
            this.ButtonStartADC.Size = new System.Drawing.Size(102, 23);
            this.ButtonStartADC.TabIndex = 11;
            this.ButtonStartADC.Text = "start ADC";
            this.ButtonStartADC.UseVisualStyleBackColor = true;
            this.ButtonStartADC.CheckedChanged += new System.EventHandler(this.ButtonStartX_Click);
            // 
            // TbSendCmd
            // 
            this.TbSendCmd.Location = new System.Drawing.Point(15, 47);
            this.TbSendCmd.Name = "TbSendCmd";
            this.TbSendCmd.Size = new System.Drawing.Size(77, 20);
            this.TbSendCmd.TabIndex = 1;
            this.TbSendCmd.Text = "-l 10";
            // 
            // ButtonStartFFT
            // 
            this.ButtonStartFFT.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButtonStartFFT.Location = new System.Drawing.Point(6, 227);
            this.ButtonStartFFT.Name = "ButtonStartFFT";
            this.ButtonStartFFT.Size = new System.Drawing.Size(102, 23);
            this.ButtonStartFFT.TabIndex = 11;
            this.ButtonStartFFT.Text = "start FFT";
            this.ButtonStartFFT.UseVisualStyleBackColor = true;
            this.ButtonStartFFT.CheckedChanged += new System.EventHandler(this.ButtonStartX_Click);
            // 
            // ButtonStartConverter
            // 
            this.ButtonStartConverter.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButtonStartConverter.Location = new System.Drawing.Point(114, 227);
            this.ButtonStartConverter.Name = "ButtonStartConverter";
            this.ButtonStartConverter.Size = new System.Drawing.Size(100, 23);
            this.ButtonStartConverter.TabIndex = 11;
            this.ButtonStartConverter.Text = "start converter";
            this.ButtonStartConverter.UseVisualStyleBackColor = true;
            this.ButtonStartConverter.CheckedChanged += new System.EventHandler(this.ButtonStartX_Click);
            // 
            // LabelRxPayload
            // 
            this.LabelRxPayload.Location = new System.Drawing.Point(6, 486);
            this.LabelRxPayload.Name = "LabelRxPayload";
            this.LabelRxPayload.Size = new System.Drawing.Size(208, 20);
            this.LabelRxPayload.TabIndex = 1;
            this.LabelRxPayload.Text = "0";
            this.LabelRxPayload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelRPSWVersion
            // 
            this.LabelRPSWVersion.AutoSize = true;
            this.LabelRPSWVersion.Location = new System.Drawing.Point(114, 24);
            this.LabelRPSWVersion.Name = "LabelRPSWVersion";
            this.LabelRPSWVersion.Size = new System.Drawing.Size(13, 13);
            this.LabelRPSWVersion.TabIndex = 0;
            this.LabelRPSWVersion.Text = "0";
            this.LabelRPSWVersion.Click += new System.EventHandler(this.LabelVersionInfo_Click);
            // 
            // CBSaveData
            // 
            this.CBSaveData.AutoSize = true;
            this.CBSaveData.Location = new System.Drawing.Point(6, 81);
            this.CBSaveData.Name = "CBSaveData";
            this.CBSaveData.Size = new System.Drawing.Size(49, 17);
            this.CBSaveData.TabIndex = 9;
            this.CBSaveData.Text = "save";
            this.CBSaveData.UseVisualStyleBackColor = true;
            this.CBSaveData.CheckedChanged += new System.EventHandler(this.CBSaveLearningData_CheckedChanged);
            // 
            // ButtonRxReset
            // 
            this.ButtonRxReset.Location = new System.Drawing.Point(6, 461);
            this.ButtonRxReset.Name = "ButtonRxReset";
            this.ButtonRxReset.Size = new System.Drawing.Size(67, 22);
            this.ButtonRxReset.TabIndex = 0;
            this.ButtonRxReset.Text = "clear RX";
            this.ButtonRxReset.UseVisualStyleBackColor = true;
            this.ButtonRxReset.Click += new System.EventHandler(this.ButtonRX_Click);
            // 
            // TextBoxRX
            // 
            this.TextBoxRX.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxRX.Location = new System.Drawing.Point(6, 392);
            this.TextBoxRX.Name = "TextBoxRX";
            this.TextBoxRX.Size = new System.Drawing.Size(228, 63);
            this.TextBoxRX.TabIndex = 2;
            this.TextBoxRX.Text = "";
            // 
            // ButtonStartClient
            // 
            this.ButtonStartClient.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButtonStartClient.AutoSize = true;
            this.ButtonStartClient.Location = new System.Drawing.Point(82, 622);
            this.ButtonStartClient.Name = "ButtonStartClient";
            this.ButtonStartClient.Size = new System.Drawing.Size(65, 23);
            this.ButtonStartClient.TabIndex = 2;
            this.ButtonStartClient.Text = "start client";
            this.ButtonStartClient.UseVisualStyleBackColor = true;
            this.ButtonStartClient.CheckedChanged += new System.EventHandler(this.ButtonStartUDPClient_CheckedChanged);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(12, 622);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(64, 23);
            this.ButtonExit.TabIndex = 3;
            this.ButtonExit.Text = "exit app";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // BoxInformation
            // 
            this.BoxInformation.Controls.Add(this.LabelSaveDir);
            this.BoxInformation.Controls.Add(this.LabelProgDir);
            this.BoxInformation.Controls.Add(this.LabelWorkDir);
            this.BoxInformation.Controls.Add(this.LabelOpenDataDir);
            this.BoxInformation.Controls.Add(this.LabelOpenProgramDir);
            this.BoxInformation.Controls.Add(this.LabelOpenWorkingDir);
            this.BoxInformation.Controls.Add(this.LabelGUISWVersion);
            this.BoxInformation.Controls.Add(this.label12);
            this.BoxInformation.Controls.Add(this.label5);
            this.BoxInformation.Location = new System.Drawing.Point(255, 12);
            this.BoxInformation.Name = "BoxInformation";
            this.BoxInformation.Size = new System.Drawing.Size(757, 118);
            this.BoxInformation.TabIndex = 5;
            this.BoxInformation.TabStop = false;
            this.BoxInformation.Text = "Information";
            // 
            // LabelSaveDir
            // 
            this.LabelSaveDir.AutoSize = true;
            this.LabelSaveDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelSaveDir.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSaveDir.Location = new System.Drawing.Point(98, 100);
            this.LabelSaveDir.Name = "LabelSaveDir";
            this.LabelSaveDir.Size = new System.Drawing.Size(51, 15);
            this.LabelSaveDir.TabIndex = 1;
            this.LabelSaveDir.Text = "DataDir";
            this.LabelSaveDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelProgDir
            // 
            this.LabelProgDir.AutoSize = true;
            this.LabelProgDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelProgDir.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelProgDir.Location = new System.Drawing.Point(97, 80);
            this.LabelProgDir.Name = "LabelProgDir";
            this.LabelProgDir.Size = new System.Drawing.Size(51, 15);
            this.LabelProgDir.TabIndex = 1;
            this.LabelProgDir.Text = "ProgDir";
            this.LabelProgDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelWorkDir
            // 
            this.LabelWorkDir.AutoSize = true;
            this.LabelWorkDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelWorkDir.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelWorkDir.Location = new System.Drawing.Point(97, 60);
            this.LabelWorkDir.Name = "LabelWorkDir";
            this.LabelWorkDir.Size = new System.Drawing.Size(69, 15);
            this.LabelWorkDir.TabIndex = 1;
            this.LabelWorkDir.Text = "WorkingDir";
            this.LabelWorkDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelWorkDir.Click += new System.EventHandler(this.LabelWorkingDir_Click);
            // 
            // LabelOpenDataDir
            // 
            this.LabelOpenDataDir.Location = new System.Drawing.Point(7, 95);
            this.LabelOpenDataDir.Name = "LabelOpenDataDir";
            this.LabelOpenDataDir.Size = new System.Drawing.Size(85, 20);
            this.LabelOpenDataDir.TabIndex = 1;
            this.LabelOpenDataDir.Text = "Data Dir:";
            this.LabelOpenDataDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelOpenDataDir.Click += new System.EventHandler(this.LabelOpenDataDir_Click);
            // 
            // LabelOpenProgramDir
            // 
            this.LabelOpenProgramDir.Location = new System.Drawing.Point(7, 75);
            this.LabelOpenProgramDir.Name = "LabelOpenProgramDir";
            this.LabelOpenProgramDir.Size = new System.Drawing.Size(85, 20);
            this.LabelOpenProgramDir.TabIndex = 1;
            this.LabelOpenProgramDir.Text = "Prog Dir:";
            this.LabelOpenProgramDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelOpenProgramDir.Click += new System.EventHandler(this.LabelOpenProgramDir_Click);
            // 
            // LabelOpenWorkingDir
            // 
            this.LabelOpenWorkingDir.Location = new System.Drawing.Point(7, 55);
            this.LabelOpenWorkingDir.Name = "LabelOpenWorkingDir";
            this.LabelOpenWorkingDir.Size = new System.Drawing.Size(84, 20);
            this.LabelOpenWorkingDir.TabIndex = 1;
            this.LabelOpenWorkingDir.Text = "Working dir:";
            this.LabelOpenWorkingDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelOpenWorkingDir.Click += new System.EventHandler(this.LabelOpenWorkingDir_Click);
            // 
            // LabelGUISWVersion
            // 
            this.LabelGUISWVersion.AutoSize = true;
            this.LabelGUISWVersion.Location = new System.Drawing.Point(98, 38);
            this.LabelGUISWVersion.Name = "LabelGUISWVersion";
            this.LabelGUISWVersion.Size = new System.Drawing.Size(35, 13);
            this.LabelGUISWVersion.TabIndex = 0;
            this.LabelGUISWVersion.Text = "V0.22";
            this.LabelGUISWVersion.Click += new System.EventHandler(this.LabelVersionInfo_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "GUI SW Version";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "UAS Frankfurt - FB2";
            // 
            // LableStatusGlobal
            // 
            this.LableStatusGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LableStatusGlobal.Location = new System.Drawing.Point(12, 403);
            this.LableStatusGlobal.Name = "LableStatusGlobal";
            this.LableStatusGlobal.Size = new System.Drawing.Size(237, 42);
            this.LableStatusGlobal.TabIndex = 1;
            this.LableStatusGlobal.Text = "info:";
            // 
            // BoxChart
            // 
            this.BoxChart.Controls.Add(this.LabelPlotSpeed);
            this.BoxChart.Controls.Add(this.label13);
            this.BoxChart.Controls.Add(this.label11);
            this.BoxChart.Controls.Add(this.LabelPlotCount);
            this.BoxChart.Controls.Add(this.ChartFFT);
            this.BoxChart.Enabled = false;
            this.BoxChart.Location = new System.Drawing.Point(501, 136);
            this.BoxChart.Name = "BoxChart";
            this.BoxChart.Size = new System.Drawing.Size(511, 509);
            this.BoxChart.TabIndex = 6;
            this.BoxChart.TabStop = false;
            this.BoxChart.Text = "Data ADC / FFT";
            // 
            // LabelPlotSpeed
            // 
            this.LabelPlotSpeed.Location = new System.Drawing.Point(128, 486);
            this.LabelPlotSpeed.Name = "LabelPlotSpeed";
            this.LabelPlotSpeed.Size = new System.Drawing.Size(55, 20);
            this.LabelPlotSpeed.TabIndex = 1;
            this.LabelPlotSpeed.Text = "0";
            this.LabelPlotSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 486);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 20);
            this.label13.TabIndex = 1;
            this.label13.Text = "Plots per second:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 466);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "Plots:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelPlotCount
            // 
            this.LabelPlotCount.Location = new System.Drawing.Point(128, 466);
            this.LabelPlotCount.Name = "LabelPlotCount";
            this.LabelPlotCount.Size = new System.Drawing.Size(55, 20);
            this.LabelPlotCount.TabIndex = 1;
            this.LabelPlotCount.Text = "0";
            this.LabelPlotCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ChartFFT
            // 
            this.ChartFFT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.AxisY.Maximum = 1000D;
            chartArea4.AxisY.Minimum = 0D;
            chartArea4.Name = "ChartArea0";
            this.ChartFFT.ChartAreas.Add(chartArea4);
            this.ChartFFT.Location = new System.Drawing.Point(6, 19);
            this.ChartFFT.Name = "ChartFFT";
            series4.ChartArea = "ChartArea0";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.IsVisibleInLegend = false;
            series4.Name = "Series0";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.ChartFFT.Series.Add(series4);
            this.ChartFFT.Size = new System.Drawing.Size(499, 420);
            this.ChartFFT.TabIndex = 0;
            this.ChartFFT.Text = "ChartDiagramm";
            // 
            // TimerRxSpeed
            // 
            this.TimerRxSpeed.Tick += new System.EventHandler(this.TimerRxSpeed_Tick);
            // 
            // T1
            // 
            this.T1.Tick += new System.EventHandler(this.T1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::UDP_Client.Properties.Resources.UAS_Logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(237, 118);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // PicSensor1
            // 
            this.PicSensor1.BackColor = System.Drawing.Color.Red;
            this.PicSensor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PicSensor1.Location = new System.Drawing.Point(82, 571);
            this.PicSensor1.Name = "PicSensor1";
            this.PicSensor1.Size = new System.Drawing.Size(20, 20);
            this.PicSensor1.TabIndex = 13;
            this.PicSensor1.TabStop = false;
            // 
            // PicSensor2
            // 
            this.PicSensor2.BackColor = System.Drawing.Color.Red;
            this.PicSensor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PicSensor2.Location = new System.Drawing.Point(82, 597);
            this.PicSensor2.Name = "PicSensor2";
            this.PicSensor2.Size = new System.Drawing.Size(20, 20);
            this.PicSensor2.TabIndex = 13;
            this.PicSensor2.TabStop = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 571);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Sensor 1:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(12, 597);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 20);
            this.label14.TabIndex = 1;
            this.label14.Text = "Sensor 2:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonStartLogging
            // 
            this.ButtonStartLogging.Enabled = false;
            this.ButtonStartLogging.Location = new System.Drawing.Point(6, 286);
            this.ButtonStartLogging.Name = "ButtonStartLogging";
            this.ButtonStartLogging.Size = new System.Drawing.Size(102, 23);
            this.ButtonStartLogging.TabIndex = 14;
            this.ButtonStartLogging.Text = "start logging";
            this.ButtonStartLogging.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonStartLogging.UseVisualStyleBackColor = true;
            this.ButtonStartLogging.Click += new System.EventHandler(this.ButtonStartLogging_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 657);
            this.Controls.Add(this.PicSensor2);
            this.Controls.Add(this.PicSensor1);
            this.Controls.Add(this.BoxChart);
            this.Controls.Add(this.LableStatusGlobal);
            this.Controls.Add(this.BoxInformation);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.ButtonStartClient);
            this.Controls.Add(this.BoxRxTx);
            this.Controls.Add(this.BoxConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "UDP client - UAS Frankfurt @2020";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Close);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.BoxConfig.ResumeLayout(false);
            this.BoxConfig.PerformLayout();
            this.BoxRxTx.ResumeLayout(false);
            this.BoxRxTx.PerformLayout();
            this.GPLearn.ResumeLayout(false);
            this.GPLearn.PerformLayout();
            this.BoxInformation.ResumeLayout(false);
            this.BoxInformation.PerformLayout();
            this.BoxChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartFFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicSensor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicSensor2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox BoxConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxRemoteIP1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxRemotePort1;
        private System.Windows.Forms.GroupBox BoxRxTx;
        private System.Windows.Forms.RichTextBox TextBoxRX;
        private System.Windows.Forms.Button ButtonRxReset;
        private System.Windows.Forms.CheckBox ButtonStartClient;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox BoxInformation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBoxLocalIP1;
        private System.Windows.Forms.Button ButtonStartServer;
        private System.Windows.Forms.Button ButtonCheckServer;
        private System.Windows.Forms.Label LabelRPSWVersion;
        private System.Windows.Forms.Button ButtonProgramRP;
        private System.Windows.Forms.Label LabelWorkDir;
        private System.Windows.Forms.Label LabelOpenWorkingDir;
        private System.Windows.Forms.Label LabelProgDir;
        private System.Windows.Forms.Label LabelOpenProgramDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label LableStatusGlobal;
        private System.Windows.Forms.GroupBox BoxChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartFFT;
        private System.Windows.Forms.Label LabelSaveDir;
        private System.Windows.Forms.Label LabelOpenDataDir;
        private System.Windows.Forms.Timer TimerRxSpeed;
        private System.Windows.Forms.Label LabelPlotCount;
        private System.Windows.Forms.Timer T1;
        private System.Windows.Forms.Label LabelPlotSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox BoxMaxFiles;
        private System.Windows.Forms.CheckBox ButtonStartFFT;
        private System.Windows.Forms.CheckBox ButtonStartConverter;
        private System.Windows.Forms.CheckBox CBSaveData;
        private System.Windows.Forms.Label LabelRxPayload;
        private System.Windows.Forms.CheckBox ButtonStartADC;
        private System.Windows.Forms.TextBox TbComment;
        private System.Windows.Forms.CheckBox ButtonStartDemo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CbLearnObject;
        private System.Windows.Forms.GroupBox GPLearn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TextBoxRemoteIP2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TextBoxLocalIP2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label LabelGUISWVersion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button ButtonSendCmd;
        private System.Windows.Forms.TextBox TbSendCmd;
        private System.Windows.Forms.PictureBox PicSensor1;
        private System.Windows.Forms.PictureBox PicSensor2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox CbWindowWith;
        private System.Windows.Forms.CheckBox CbADC;
        private System.Windows.Forms.CheckBox CbFFT;
        private System.Windows.Forms.TextBox TbLogCount;
        private System.Windows.Forms.Button ButtonStartLogging;
    }
}

