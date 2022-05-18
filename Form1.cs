using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;


namespace FileUploader
{
    public partial class FileUploader : Form
    {
        //AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        static AutoCompleteStringCollection List = new AutoCompleteStringCollection();

        //AutoComplete autoComplete1 = new AutoComplete();
        public FileUploader()
        {
            InitializeComponent();
        }

        private void FileUploader_Load(object sender, EventArgs e)
        {


            cancelbtn.Enabled = false;
            try
            {
                String[] args = Environment.GetCommandLineArgs();
                textBox1.Text = args[1];

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            //List.Add("");
            List.Add("192.168.1.180");
            List.Add("172.30.144.1");
            List.Add("192.168.183.165");
            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = List;

        }


        private void Textbox2_MouseClick(object sender, MouseEventArgs e)
        {
            //textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;



        }

        /* private AutoCompleteStringCollection IPList()
         {
             //List.Add(null);
             List.Add("192.168.1.180");
             List.Add("172.30.144.1");
             List.Add("192.168.183.165");
             return List;

         }*/


        private void Filebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"",
                //Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true,
                //RestoreDirectory = true,
                //ReadOnlyChecked = true,
                //ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }
        private void TextBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;

        }

        private void TextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
                textBox1.Text = files.First(); //select the first one  
        }

        //BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private void Sendbtn_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                string message = "Please Fill required fields..";
                MessageBox.Show(message);

            }
            else
            {
                //textBox2.AutoCompleteCustomSource.Add(textBox2.Text);
                /*List.Add(textBox2.Text.ToString());
                textBox2.AutoCompleteCustomSource = List;*/

                /* String[] arr = new string[comboBox1.Text.Length];
                 List.AddRange(arr);*/

                // IP address validation
                if (IsvalidIp())
                {
                    // validation port number free
                    if (IsValidPortNumber() && IsInteger(comboBox1.Text))
                    {


                        if (List.Contains(textBox2.Text) == false)
                        {
                            // textBox2.AutoCompleteCustomSource.Add(textBox2.Text);
                            List.Add(textBox2.Text);
                            textBox2.AutoCompleteCustomSource = List;
                        }
                        //---------------------------------------------------------------

                        CheckBox chk = AdbcheckBox;
                        //MessageBox.Show("You " + chk.Checked);
                        if (chk.Checked)
                        {
                            //MessageBox.Show("You selected: " + chk.Text);

                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            startInfo.UseShellExecute = false;
                            startInfo.RedirectStandardOutput = true;

                            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
                            // string.Format("{0}Resources\\file.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                            //startInfo.FileName = "..\\platform-tools\\adb.exe";
                            startInfo.FileName = string.Format("{0}Resources\\platform-tools\\adb.exe",Path.Combine(RunningPath));
                            //startInfo.FileName = string.Format("platform-tools\\adb.exe");

                           // MessageBox.Show(startInfo.FileName);

                            String MyIp = textBox2.Text;
                            startInfo.Arguments = "connect " + MyIp + ":5555";
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo = startInfo;
                            try
                            {
                                process.Start();
                                process.WaitForExit();

                                string output1 = process.StandardOutput.ReadToEnd();
                                if (output1 == "")
                                {
                                    MessageBox.Show("Please connect device..");
                                }
                                else if (output1.Contains("connected"))
                                {
                                    String filePath = textBox1.Text;
                                    String filenametostore = Path.GetFileName(filePath);
                                    startInfo.Arguments = "push " + filePath + " /sdcard";
                                    process.StartInfo.CreateNoWindow = true;
                                    process.StartInfo = startInfo;

                                    process.Start();
                                    cancelbtn.Enabled = true;
                                    note.Text = "File sharing...";
                                    process.WaitForExit();
                                    
                                    string output2 = process.StandardOutput.ReadToEnd();
                                    if (output2.Contains("1 file pushed"))
                                    {
                                        note.Text = "File shared Sucessfully";
                                        cancelbtn.Enabled = false;
                                    }
                                    
                                }
                                else
                                {
                                    note.Text = "Failed to connect ADb";

                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }


                        }
                        else
                        {
                            backgroundWorker2.RunWorkerAsync();

                        }

                    }
                    else
                    {
                        string message = "Please Enter Valid port Number";
                        MessageBox.Show(message);
                    }
                }
                else
                {
                    string message = "Please Enter Valid IP";
                    MessageBox.Show(message);
                }
            }

        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {

            CheckBox chk = AdbcheckBox;
            if (chk.Checked)
            { 
                
            }
            backgroundWorker2.CancelAsync();

            cancelbtn.Enabled = false;
            sendbtn.Enabled = true;
            progressBar1.Value = 0;
            note.Text = "Process Cancelled..";
            percentage.Text = "";

        }



        private bool IsvalidIp()
        {

            return Regex.IsMatch(textBox2.Text, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$");

        }
        private bool IsValidPortNumber()
        {

            if (IsInteger(comboBox1.Text))
            {
                if (int.Parse(comboBox1.Text) > 1 && int.Parse(comboBox1.Text) < 65535)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }
        private bool IsInteger(String port)
        {
            try
            {
                int.Parse(port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void BackgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {




            /*string message = "Valid Port";
            MessageBox.Show(message);*/
            cancelbtn.Enabled = true;
            sendbtn.Enabled = false;
            try
            {

                /*// Establish the remote endpoint
                // for the socket. This example
                // uses port 11111 on the local
                // computer.
                IPHostEntry ipHost = Dns.GetHostEntry(textBox2.Text);
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr,int.Parse(comboBox1.Text));

                string message = ipAddr.ToString();
                MessageBox.Show(message);
                // Creation TCP/IP Socket using
                // Socket Class Constructor
                Socket socket = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(localEndPoint);
    */
                IPAddress ipAddress = IPAddress.Parse(textBox2.Text);
                //int bufferSize = 1024;

                TcpClient client = new TcpClient();
                NetworkStream netStream;

                // Connect to server
                try
                {
                    client.Connect(new IPEndPoint(ipAddress, int.Parse(comboBox1.Text)));

                }
                catch (Exception)
                {
                    sendbtn.Enabled = true;
                    cancelbtn.Enabled = false;
                    note.Text = "Connection Faild..";
                }

                /*TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(textBox2.Text), int.Parse(comboBox1.Text));*/
                netStream = client.GetStream();


                // Read bytes from file
                byte[] data = File.ReadAllBytes(textBox1.Text);
                /*string message = data.Length.ToString();
                MessageBox.Show(message);*/

                int dataLength = data.Length;
                int datakb = dataLength / 1024;

                /*string message = datakb.ToString() + "KB";
                MessageBox.Show(message);*/

                int progress = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dataLength;
                progressBar1.Value = progressBar1.Minimum;

                note.Text = "File Uploading..";

                // to add if new Ip address connected to show suggestion


                /*if (comboBox2.Items.Contains(textBox2.Text))
                {
                    comboBox2.Items.Add(textBox2.Text);
                }*/


                for (int i = 0; i <= data.Length; i++)
                {
                    if (backgroundWorker2.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    /*if (_stop == true)
                    {
                        note.Text = "Process Cancelled";
                        sendbtn.Enabled = true;
                        progressBar1.Value = 0;
                        break;


                    }*/

                    progress = i;
                    //netStream.WriteByte(data[i]);
                    // 
                    // progressBar1.Value = progress;
                    //Thread.Sleep(1);
                    backgroundWorker2.ReportProgress(progress);
                    int percentSend = (progress * 100) / dataLength;
                    percentage.Text = percentSend.ToString() + "%";

                }
                if (progress == data.Length)
                {
                    byte[] datasize = BitConverter.GetBytes(data.Length);
                    netStream.Write(data, 0, data.Length);
                    // netStream.Write(data,data.Length,0);
                    sendbtn.Enabled = true;
                    progressBar1.Value = 0;
                    percentage.Text = "";
                    cancelbtn.Enabled = false;
                    note.Text = "FileUploaded sucessfully..";
                    comboBox2.Update();
                    comboBox2.Refresh();

                }

            }
            catch (SocketException)
            {
                note.Text = "Connection Faild..";
                sendbtn.Enabled = true;
                cancelbtn.Enabled = false;
            }
            catch (Exception)
            {

                sendbtn.Enabled = true;
                cancelbtn.Enabled = false;
                note.Text = "Connection Faild..";
            }

        }




        private void BackgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

        }

        private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                note.Text = "Process Cancelled";
                progressBar1.Value = 0;
            }

        }



        private void ComboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.DroppedDown = true;
        }

        private void ComboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox2.DroppedDown = true;
        }

        private void ComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            comboBox2.DroppedDown = false;
        }

        /* private void AdbcheckBox_CheckedChanged(object sender, EventArgs e)
         {
             CheckBox chk = (sender as CheckBox);
             if (chk.Checked)
             {
                 MessageBox.Show("You selected: " + chk.Text);

                 System.Diagnostics.Process process = new System.Diagnostics.Process();
                 System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                 startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                 startInfo.UseShellExecute = false;
                 startInfo.RedirectStandardOutput = true;

                 string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
                 // string.Format("{0}Resources\\file.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                 //startInfo.FileName = "..\\platform-tools\\adb.exe";
                 startInfo.FileName = string.Format("{0}Resources\\platform-tools\\adb.exe", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
                 String MyIp = textBox2.Text;
                 startInfo.Arguments = "connect " + MyIp + ":5555";
                 process.StartInfo.CreateNoWindow = false;
                 process.StartInfo = startInfo;
                 try
                 {
                     process.Start();
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }

                 string output1 = process.StandardOutput.ReadToEnd();
                 if (output1 == "")
                 {
                     MessageBox.Show("Please connect device..");
                 }
                 else if (output1.Contains("connected"))
                 {
                     String filePath = textBox1.Text;
                     String filenametostore = Path.GetFileName(filePath);
                     startInfo.Arguments = "push "+ filePath+" /sdcard";
                     process.StartInfo.CreateNoWindow = false;
                     process.StartInfo = startInfo;
                     try
                     {
                         process.Start();
                         string output2 = process.StandardOutput.ReadToEnd();
                         MessageBox.Show(output2);
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message);
                     }


                 }
                 else
                 {
                     MessageBox.Show("You Solution: " + output1);
                 }
                 process.WaitForExit();

             }



         }
        */
    }

}
