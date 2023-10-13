namespace UDP_Client
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GbThresholds = new System.Windows.Forms.GroupBox();
            this.ButtonSetDistanceThresholds = new System.Windows.Forms.Button();
            this.TbThresholdMediumDistance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TbThresholdLargeDistance = new System.Windows.Forms.TextBox();
            this.CbMeasureMode = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TbF1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TbF10 = new System.Windows.Forms.TextBox();
            this.ButSendThresholds = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TbSensor2Class = new System.Windows.Forms.TextBox();
            this.TbSensor1Distance = new System.Windows.Forms.TextBox();
            this.TbSensor2Distance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbHeightObject2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TbOffset2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TbSensor1Class = new System.Windows.Forms.TextBox();
            this.TbHeightObject1 = new System.Windows.Forms.TextBox();
            this.TbOffset1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ButSendOffset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.TimerSensor = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.GbThresholds.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.GbThresholds);
            this.groupBox1.Controls.Add(this.CbMeasureMode);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.ButSendOffset);
            this.groupBox1.Location = new System.Drawing.Point(12, 537);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 176);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensor(s)";
            // 
            // GbThresholds
            // 
            this.GbThresholds.Controls.Add(this.ButtonSetDistanceThresholds);
            this.GbThresholds.Controls.Add(this.TbThresholdMediumDistance);
            this.GbThresholds.Controls.Add(this.label5);
            this.GbThresholds.Controls.Add(this.label6);
            this.GbThresholds.Controls.Add(this.TbThresholdLargeDistance);
            this.GbThresholds.Location = new System.Drawing.Point(405, 95);
            this.GbThresholds.Name = "GbThresholds";
            this.GbThresholds.Size = new System.Drawing.Size(293, 73);
            this.GbThresholds.TabIndex = 22;
            this.GbThresholds.TabStop = false;
            this.GbThresholds.Text = "Thresholds";
            // 
            // ButtonSetDistanceThresholds
            // 
            this.ButtonSetDistanceThresholds.Location = new System.Drawing.Point(197, 14);
            this.ButtonSetDistanceThresholds.Name = "ButtonSetDistanceThresholds";
            this.ButtonSetDistanceThresholds.Size = new System.Drawing.Size(85, 28);
            this.ButtonSetDistanceThresholds.TabIndex = 14;
            this.ButtonSetDistanceThresholds.Text = "set thresholds";
            this.ButtonSetDistanceThresholds.UseVisualStyleBackColor = true;
            this.ButtonSetDistanceThresholds.Click += new System.EventHandler(this.ButtonSetDistanceThresholds_Click);
            // 
            // TbThresholdMediumDistance
            // 
            this.TbThresholdMediumDistance.Location = new System.Drawing.Point(130, 14);
            this.TbThresholdMediumDistance.Name = "TbThresholdMediumDistance";
            this.TbThresholdMediumDistance.Size = new System.Drawing.Size(61, 20);
            this.TbThresholdMediumDistance.TabIndex = 15;
            this.TbThresholdMediumDistance.Text = "30";
            this.TbThresholdMediumDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "medium threshold [cm]: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "large threshold [cm]: ";
            // 
            // TbThresholdLargeDistance
            // 
            this.TbThresholdLargeDistance.Location = new System.Drawing.Point(130, 40);
            this.TbThresholdLargeDistance.Name = "TbThresholdLargeDistance";
            this.TbThresholdLargeDistance.Size = new System.Drawing.Size(61, 20);
            this.TbThresholdLargeDistance.TabIndex = 15;
            this.TbThresholdLargeDistance.Text = "30";
            this.TbThresholdLargeDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CbMeasureMode
            // 
            this.CbMeasureMode.AutoSize = true;
            this.CbMeasureMode.Location = new System.Drawing.Point(6, 99);
            this.CbMeasureMode.Name = "CbMeasureMode";
            this.CbMeasureMode.Size = new System.Drawing.Size(62, 17);
            this.CbMeasureMode.TabIndex = 21;
            this.CbMeasureMode.Text = "Mode 1";
            this.CbMeasureMode.UseVisualStyleBackColor = true;
            this.CbMeasureMode.CheckedChanged += new System.EventHandler(this.CbMeasureMode_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TbF1);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.TbF10);
            this.groupBox5.Controls.Add(this.ButSendThresholds);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(404, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(293, 70);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Box classifier";
            // 
            // TbF1
            // 
            this.TbF1.Location = new System.Drawing.Point(130, 19);
            this.TbF1.Name = "TbF1";
            this.TbF1.Size = new System.Drawing.Size(61, 20);
            this.TbF1.TabIndex = 15;
            this.TbF1.Text = "-";
            this.TbF1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "F1: ";
            // 
            // TbF10
            // 
            this.TbF10.Location = new System.Drawing.Point(130, 45);
            this.TbF10.Name = "TbF10";
            this.TbF10.Size = new System.Drawing.Size(61, 20);
            this.TbF10.TabIndex = 15;
            this.TbF10.Text = "-";
            this.TbF10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ButSendThresholds
            // 
            this.ButSendThresholds.Location = new System.Drawing.Point(197, 19);
            this.ButSendThresholds.Name = "ButSendThresholds";
            this.ButSendThresholds.Size = new System.Drawing.Size(85, 28);
            this.ButSendThresholds.TabIndex = 14;
            this.ButSendThresholds.Text = "set thresholds";
            this.ButSendThresholds.UseVisualStyleBackColor = true;
            this.ButSendThresholds.Click += new System.EventHandler(this.ButSendThresholds_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "F10: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TbSensor2Class);
            this.groupBox3.Controls.Add(this.TbSensor1Distance);
            this.groupBox3.Controls.Add(this.TbSensor2Distance);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.TbHeightObject2);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.TbOffset2);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.TbSensor1Class);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.TbHeightObject1);
            this.groupBox3.Controls.Add(this.TbOffset1);
            this.groupBox3.Location = new System.Drawing.Point(704, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 151);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sensors";
            // 
            // TbSensor2Class
            // 
            this.TbSensor2Class.Location = new System.Drawing.Point(189, 125);
            this.TbSensor2Class.Name = "TbSensor2Class";
            this.TbSensor2Class.Size = new System.Drawing.Size(61, 20);
            this.TbSensor2Class.TabIndex = 15;
            this.TbSensor2Class.Text = "-";
            this.TbSensor2Class.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TbSensor1Distance
            // 
            this.TbSensor1Distance.Location = new System.Drawing.Point(122, 47);
            this.TbSensor1Distance.Name = "TbSensor1Distance";
            this.TbSensor1Distance.Size = new System.Drawing.Size(61, 20);
            this.TbSensor1Distance.TabIndex = 15;
            this.TbSensor1Distance.Text = "-";
            this.TbSensor1Distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TbSensor2Distance
            // 
            this.TbSensor2Distance.Location = new System.Drawing.Point(189, 47);
            this.TbSensor2Distance.Name = "TbSensor2Distance";
            this.TbSensor2Distance.Size = new System.Drawing.Size(61, 20);
            this.TbSensor2Distance.TabIndex = 15;
            this.TbSensor2Distance.Text = "-";
            this.TbSensor2Distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Object class:";
            // 
            // TbHeightObject2
            // 
            this.TbHeightObject2.Location = new System.Drawing.Point(189, 99);
            this.TbHeightObject2.Name = "TbHeightObject2";
            this.TbHeightObject2.Size = new System.Drawing.Size(61, 20);
            this.TbHeightObject2.TabIndex = 15;
            this.TbHeightObject2.Text = "-";
            this.TbHeightObject2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Object size:";
            // 
            // TbOffset2
            // 
            this.TbOffset2.Location = new System.Drawing.Point(189, 73);
            this.TbOffset2.Name = "TbOffset2";
            this.TbOffset2.Size = new System.Drawing.Size(61, 20);
            this.TbOffset2.TabIndex = 15;
            this.TbOffset2.Text = "-";
            this.TbOffset2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Object distance offset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Sensor 2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Sensor 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Object distance";
            // 
            // TbSensor1Class
            // 
            this.TbSensor1Class.Location = new System.Drawing.Point(122, 125);
            this.TbSensor1Class.Name = "TbSensor1Class";
            this.TbSensor1Class.Size = new System.Drawing.Size(61, 20);
            this.TbSensor1Class.TabIndex = 15;
            this.TbSensor1Class.Text = "-";
            this.TbSensor1Class.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TbHeightObject1
            // 
            this.TbHeightObject1.Location = new System.Drawing.Point(122, 99);
            this.TbHeightObject1.Name = "TbHeightObject1";
            this.TbHeightObject1.Size = new System.Drawing.Size(61, 20);
            this.TbHeightObject1.TabIndex = 15;
            this.TbHeightObject1.Text = "-";
            this.TbHeightObject1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TbOffset1
            // 
            this.TbOffset1.Location = new System.Drawing.Point(122, 73);
            this.TbOffset1.Name = "TbOffset1";
            this.TbOffset1.Size = new System.Drawing.Size(61, 20);
            this.TbOffset1.TabIndex = 15;
            this.TbOffset1.Text = "-";
            this.TbOffset1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 28);
            this.button1.TabIndex = 14;
            this.button1.Text = "exit demo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonExitClick);
            // 
            // ButSendOffset
            // 
            this.ButSendOffset.Location = new System.Drawing.Point(6, 19);
            this.ButSendOffset.Name = "ButSendOffset";
            this.ButSendOffset.Size = new System.Drawing.Size(70, 28);
            this.ButSendOffset.TabIndex = 14;
            this.ButSendOffset.Text = "set offset";
            this.ButSendOffset.UseVisualStyleBackColor = true;
            this.ButSendOffset.Click += new System.EventHandler(this.ButtonSetOffset_Klick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.LabelStatus);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(980, 519);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Object recognition";
            // 
            // LabelStatus
            // 
            this.LabelStatus.BackColor = System.Drawing.Color.Transparent;
            this.LabelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelStatus.Location = new System.Drawing.Point(3, 16);
            this.LabelStatus.Margin = new System.Windows.Forms.Padding(0);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(974, 500);
            this.LabelStatus.TabIndex = 14;
            this.LabelStatus.Text = "status";
            this.LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimerSensor
            // 
            this.TimerSensor.Tick += new System.EventHandler(this.TimerSensorPing);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1004, 725);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DEMO - UDP client - UAS Frankfurt @2020";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GbThresholds.ResumeLayout(false);
            this.GbThresholds.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ButSendOffset;
        private System.Windows.Forms.TextBox TbSensor1Distance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.TextBox TbSensor2Distance;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TbF10;
        private System.Windows.Forms.TextBox TbF1;
        private System.Windows.Forms.Button ButSendThresholds;
        private System.Windows.Forms.Timer TimerSensor;
        private System.Windows.Forms.TextBox TbSensor1Class;
        private System.Windows.Forms.TextBox TbSensor2Class;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbThresholdMediumDistance;
        private System.Windows.Forms.TextBox TbOffset1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbThresholdLargeDistance;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TbOffset2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button ButtonSetDistanceThresholds;
        private System.Windows.Forms.CheckBox CbMeasureMode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TbHeightObject1;
        private System.Windows.Forms.TextBox TbHeightObject2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox GbThresholds;
    }
}