namespace UDP_Client
{
    partial class Form3
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
            this.TB_Input = new System.Windows.Forms.TextBox();
            this.Label_Input = new System.Windows.Forms.Label();
            this.Label_Output = new System.Windows.Forms.Label();
            this.TB_Output = new System.Windows.Forms.TextBox();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.ButtonConvert = new System.Windows.Forms.Button();
            this.ToolTipConverting = new System.Windows.Forms.ToolTip(this.components);
            this.FileExplorer = new System.Windows.Forms.OpenFileDialog();
            this.ButtonOpenFile = new System.Windows.Forms.Button();
            this.ButtonDownloadBinFiles = new System.Windows.Forms.Button();
            this.TbOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TB_Input
            // 
            this.TB_Input.Location = new System.Drawing.Point(88, 12);
            this.TB_Input.Name = "TB_Input";
            this.TB_Input.Size = new System.Drawing.Size(471, 20);
            this.TB_Input.TabIndex = 0;
            this.TB_Input.TextChanged += new System.EventHandler(this.TB_Input_TextChanged);
            // 
            // Label_Input
            // 
            this.Label_Input.AutoSize = true;
            this.Label_Input.Location = new System.Drawing.Point(12, 15);
            this.Label_Input.Name = "Label_Input";
            this.Label_Input.Size = new System.Drawing.Size(66, 13);
            this.Label_Input.TabIndex = 1;
            this.Label_Input.Text = "Binary Input:";
            // 
            // Label_Output
            // 
            this.Label_Output.AutoSize = true;
            this.Label_Output.Location = new System.Drawing.Point(12, 41);
            this.Label_Output.Name = "Label_Output";
            this.Label_Output.Size = new System.Drawing.Size(70, 13);
            this.Label_Output.TabIndex = 1;
            this.Label_Output.Text = "ASCII output:";
            // 
            // TB_Output
            // 
            this.TB_Output.Location = new System.Drawing.Point(88, 38);
            this.TB_Output.Name = "TB_Output";
            this.TB_Output.Size = new System.Drawing.Size(471, 20);
            this.TB_Output.TabIndex = 0;
            // 
            // Button_Exit
            // 
            this.Button_Exit.Location = new System.Drawing.Point(12, 201);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(66, 26);
            this.Button_Exit.TabIndex = 2;
            this.Button_Exit.Text = "exit";
            this.Button_Exit.UseVisualStyleBackColor = true;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // ButtonConvert
            // 
            this.ButtonConvert.Enabled = false;
            this.ButtonConvert.Location = new System.Drawing.Point(348, 64);
            this.ButtonConvert.Name = "ButtonConvert";
            this.ButtonConvert.Size = new System.Drawing.Size(124, 26);
            this.ButtonConvert.TabIndex = 2;
            this.ButtonConvert.Text = "convert";
            this.ButtonConvert.UseVisualStyleBackColor = true;
            this.ButtonConvert.Click += new System.EventHandler(this.Button_Convert_Click);
            // 
            // FileExplorer
            // 
            this.FileExplorer.FileName = "openFileDialog1";
            // 
            // ButtonOpenFile
            // 
            this.ButtonOpenFile.Location = new System.Drawing.Point(218, 64);
            this.ButtonOpenFile.Name = "ButtonOpenFile";
            this.ButtonOpenFile.Size = new System.Drawing.Size(124, 26);
            this.ButtonOpenFile.TabIndex = 2;
            this.ButtonOpenFile.Text = "open a file";
            this.ButtonOpenFile.UseVisualStyleBackColor = true;
            this.ButtonOpenFile.Click += new System.EventHandler(this.ButtonOpenFile_Click);
            // 
            // ButtonDownloadBinFiles
            // 
            this.ButtonDownloadBinFiles.Location = new System.Drawing.Point(88, 64);
            this.ButtonDownloadBinFiles.Name = "ButtonDownloadBinFiles";
            this.ButtonDownloadBinFiles.Size = new System.Drawing.Size(124, 26);
            this.ButtonDownloadBinFiles.TabIndex = 2;
            this.ButtonDownloadBinFiles.Text = "download bin files";
            this.ButtonDownloadBinFiles.UseVisualStyleBackColor = true;
            this.ButtonDownloadBinFiles.Click += new System.EventHandler(this.ButtonDownloadBinFiles_Click);
            // 
            // TbOutput
            // 
            this.TbOutput.AutoSize = true;
            this.TbOutput.Location = new System.Drawing.Point(85, 208);
            this.TbOutput.Name = "TbOutput";
            this.TbOutput.Size = new System.Drawing.Size(0, 13);
            this.TbOutput.TabIndex = 3;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 239);
            this.Controls.Add(this.TbOutput);
            this.Controls.Add(this.ButtonDownloadBinFiles);
            this.Controls.Add(this.ButtonOpenFile);
            this.Controls.Add(this.ButtonConvert);
            this.Controls.Add(this.Button_Exit);
            this.Controls.Add(this.Label_Output);
            this.Controls.Add(this.Label_Input);
            this.Controls.Add(this.TB_Output);
            this.Controls.Add(this.TB_Input);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form3_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form3_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Input;
        private System.Windows.Forms.Label Label_Input;
        private System.Windows.Forms.Label Label_Output;
        private System.Windows.Forms.TextBox TB_Output;
        private System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.Button ButtonConvert;
        private System.Windows.Forms.ToolTip ToolTipConverting;
        private System.Windows.Forms.OpenFileDialog FileExplorer;
        private System.Windows.Forms.Button ButtonOpenFile;
        private System.Windows.Forms.Button ButtonDownloadBinFiles;
        private System.Windows.Forms.Label TbOutput;
    }
}