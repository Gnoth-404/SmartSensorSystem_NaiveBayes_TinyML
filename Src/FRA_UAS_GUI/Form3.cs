using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSCP;

namespace UDP_Client
{
    public partial class Form3 : Form
    {
        readonly String ProgDirRemote = "/root/iic";
        internal static Form1 FM1 = null;
        private string IpAddress;
        private int DownloadedFiles = 0;

        public Form3()
        {
            InitializeComponent();
        }



        private void Form3_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;

            TbOutput.Text = "First download files, then open bin-files and just convert them";

            ToolTipConverting.SetToolTip(ButtonConvert, "Click to convert");
            ToolTipConverting.AutoPopDelay = 5000;
            ToolTipConverting.InitialDelay = 1000;
            ToolTipConverting.ReshowDelay = 500;
            ToolTipConverting.ShowAlways = true;
            FM1 = new Form1();
            IpAddress = "192.168.128.1";

        }


        
        private void Form3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }



        private void Form3_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList;

            FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            TB_Input.Text = FileList[0];
        }



        private void Button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void Button_Convert_Click(object sender, EventArgs e)
        {
            string OutputPath = "";
            string Output = "";
            int HeaderLength, DataLength;
            UInt64 ByteCount;
            int Samples = (int)(Math.Pow(2, 14));
            BinaryReader BR = null;
            Button Butt_In = (Button)(sender);


            Butt_In.Enabled = false;
            if ( File.Exists(TB_Input.Text) )
            {
                try
                {
                    BR = new BinaryReader(new FileStream(TB_Input.Text, FileMode.Open));
                    OutputPath = TB_Output.Text;
                    ByteCount = (UInt64)BR.BaseStream.Length;

                    do
                    {
                        HeaderLength = ((int)BR.ReadSingle());
                        DataLength = ((int)BR.ReadSingle());
                        if( (HeaderLength > 8) & (DataLength > 0) )
                        {
                            Output = (HeaderLength / 4).ToString("0") + "\t" + (DataLength / 2).ToString("0") + "\t";
                            for (int ix = 2; ix < (HeaderLength / 4); ix++)
                            {
                                Output += BR.ReadSingle().ToString() + "\t";
                            }

                            for (int ix = 0; ix < (DataLength / 2); ix++)
                            {
                                Output += BR.ReadInt16().ToString() + "\t";
                            }
                            ByteCount -= (UInt64)(HeaderLength + DataLength);
                            File.AppendAllText(OutputPath, Output + "\n");
                        }
                        else
                        {
                            ByteCount = 0;
                        }
                        
                    } while (ByteCount > 0);
                }
                catch
                {}


                if(BR != null)
                {
                    BR.Close();
                }
                Butt_In.Enabled = true;

                TbOutput.Text = "The .txt file should be ready!";

            }
        }



        private void TB_Input_TextChanged(object sender, EventArgs e)
        {
            bool Checked = File.Exists(TB_Input.Text);

            ButtonConvert.Enabled = Checked;
            TB_Output.Text = (Checked) ? (TB_Input.Text.Substring(0, TB_Input.Text.Length - 4) + ".txt") : ("---");
        }



        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            String Tmp = Directory.GetCurrentDirectory();

            if(Directory.Exists(Tmp + "\\save"))
            {
                Tmp += "\\save";
            }

            openFileDialog.Filter = "Binfiles (*.bin)|*.bin";
            openFileDialog.InitialDirectory = Tmp;
            openFileDialog.ShowDialog();

            TbOutput.Text = "Now you can convert it!";
            
            TB_Input.Text = openFileDialog.FileName;

            return;
        }



        private void ButtonDownloadBinFiles_Click(object sender, EventArgs e)
        {
            WinSCP.Session SCP_Client = new WinSCP.Session();
            WinSCP.SessionOptions ScpSensorOptions;
            WinSCP.TransferOptions transferOptions = new TransferOptions();
            WinSCP.TransferOperationResult Result = null;

            TbOutput.Text = "downloading via SCP protocol. Please wait!";

            transferOptions.FileMask = "*.bin";
            ScpSensorOptions = new WinSCP.SessionOptions { Protocol = Protocol.Scp, HostName = IpAddress, UserName = "root", Password = "root", GiveUpSecurityAndAcceptAnySshHostKey = true, PortNumber = 22, };
            
            SCP_Client.Open(ScpSensorOptions);
            if (SCP_Client.FileExists(ProgDirRemote))
            {
                Result = SCP_Client.GetFilesToDirectory(ProgDirRemote, Directory.GetCurrentDirectory() + "\\save","*.bin",true);
            }
            else
            {
                TbOutput.Text = "Error - the folder does not exist!";
            }
            SCP_Client.Close();
            DownloadedFiles = Result.Transfers.Count;
            TbOutput.Text = (DownloadedFiles > 0)?(DownloadedFiles.ToString() + " new file(s)") :("No Data available!");
            ButtonConvert.Enabled = (DownloadedFiles > 0);
        }
    }
}
