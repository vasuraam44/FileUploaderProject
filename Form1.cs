using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.Win32;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace FileUploader
{
    public partial class FileUploader : Form
    {
        AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        static AutoCompleteStringCollection list = new AutoCompleteStringCollection();
        List<String> listip = new List<String>();
        //AutoComplete autoComplete1 = new AutoComplete();
        public FileUploader()
        {
            InitializeComponent();

            // SETTING ENVIRONMENT VARIABLE
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            String adbEnvpath = string.Format(@"{0}Resources\platform-tools", Path.Combine(RunningPath));
            //MessageBox.Show("ADBPATH:"+adbEnvpath);
            var name = "Path";
            var scope = EnvironmentVariableTarget.User; // or User
            String oldValue = Environment.GetEnvironmentVariable(name, scope);
            //MessageBox.Show(oldValue);
            List<String> listStrLineElements;
            bool adflag=true;
            listStrLineElements = oldValue.Split(';').ToList();
            foreach(String value in listStrLineElements)
            {
                if (value.Contains(adbEnvpath))
                {
                   //MessageBox.Show("Already Exist Environment variable");
                    adflag = false;
                    break;
                }
            }
            //MessageBox.Show("FlagsAttribute:"+adflag);
            if (adflag)
            {
                var newValue = oldValue + adbEnvpath+";";
                Environment.SetEnvironmentVariable("Path", newValue, scope);

            }


            /*
                        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MylistData");
                        //storing the values  
                        key.SetValue("Mylist", string.Join(",", listip));
                        key.Close();
            */





        }

        private void FileUploader_Load(object sender, EventArgs e)
        {


            cancelbtn.Enabled = false;
            try
            {
                String[] args = Environment.GetCommandLineArgs();
                //MessageBox.Show(args[1]);
                textBox1.Text = args[1];


            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            


            //opening the subkey  
            RegistryKey keys = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MylistData");
            //if it does exist, retrieve the stored values  
            if (keys != null)
            {
                String iplist = keys.GetValue("Mylist").ToString();
                listip = iplist.Split(',').ToList();
                keys.Close();
            }
            list.AddRange(listip.ToArray());
           
            /*
            for (int i = 0; i < 4; i++)
            {
                listip.Add(list[i]);
            }
            */

            
            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = list;

            
           

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
                CheckFileExists = true,
                CheckPathExists = true,  
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


            bool adbchbtn;
            CheckBox chk = AdbcheckBox;
            if (chk.Checked)
            {
                adbchbtn = false;
            }
            else if (comboBox1.Text == "")
            {
                adbchbtn = true;
            }
            else
            {
                adbchbtn = false;
            }

         


            if (textBox1.Text == "" || textBox2.Text == "" || adbchbtn)
            {
                string message = "Please Fill required fields..";
                MessageBox.Show(message);

            }
            else
            {
                // IP address validation
                if (IsvalidIp())
                {
                    // validation port number free
                    if (IsValidPortNumber() && IsInteger(comboBox1.Text))
                    {

                        //string newsuggestion = textBox2.Text;
                        //Settings.Default.autoComplete.Add(newsuggestion);

                        if (autoComplete.Contains(textBox2.Text) == false)
                        {
                            autoComplete.Add(textBox2.Text);
                            
                        }

              
                        if (list.Contains(textBox2.Text) == false)
                        {
                            if (list.Count >= 4)
                            {
                                for (int i=4; i<=list.Count;i++)
                                {
                                    list.RemoveAt(i);
                                }     
                            }
                            List<String> listOfIp = new List<String>();
                            list.Insert(0, textBox2.Text);
                            foreach (String value in list)
                            {
                                listOfIp.Add(value);
                            }
                            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MylistData");
                            //storing the values  
                            key.SetValue("Mylist", string.Join(",", listOfIp));
                            key.Close();
                           

                        }
                
                        //---------------------------------------------------------------

                        //CheckBox chk = AdbcheckBox;
                        //MessageBox.Show("You " + chk.Checked);
                        if (chk.Checked)
                        {
                            WithAdbbackgroundWorker.RunWorkerAsync();
                        }
                        else
                        {
                            WithoutAdbbackgroundWorker.RunWorkerAsync();

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
                WithAdbbackgroundWorker.CancelAsync();
                cancelbtn.Enabled = false;
                sendbtn.Enabled = true;
                progressBar1.Value = 0;
                note.Text = "Process Cancelled..";
                percentage.Text = "";
            }
            else
            {
                WithoutAdbbackgroundWorker.CancelAsync();

                cancelbtn.Enabled = false;
                sendbtn.Enabled = true;
                progressBar1.Value = 0;
                note.Text = "Process Cancelled..";
                percentage.Text = "";
            }

        }


        private bool IsvalidIp()
        {
            return Regex.IsMatch(textBox2.Text, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$");

        }
        private bool IsValidPortNumber()
        {
            CheckBox chk = AdbcheckBox;
            if (chk.Checked)
            {
                return true;
            }
            else
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

        }
        private bool IsInteger(String port)
        {
            try
            {
                CheckBox chk = AdbcheckBox;
                if (chk.Checked)
                {
                    return true;
                }

                int.Parse(port);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private void WithAdbBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            cancelbtn.Enabled = true;
            sendbtn.Enabled = false;
            //MessageBox.Show("You selected: " + chk.Text);

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;

            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            // string.Format("{0}Resources\\file.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            startInfo.FileName = string.Format("{0}Resources\\platform-tools\\adb.exe", Path.Combine(RunningPath));
           
            String MyIp = textBox2.Text;
            startInfo.Arguments = "connect " + MyIp + ":5555";
            process.StartInfo = startInfo;
            try
            {
                process.Start();
                process.WaitForExit();
                string output1 = process.StandardOutput.ReadToEnd();
                //MessageBox.Show(output1);
                if (output1 == "")
                {
                    MessageBox.Show("Please connect device..");
                    sendbtn.Enabled = true;
                }
                else if (output1.Contains("connected"))
                {
                    note.Text = "";
                    process.Close();
                    String filePath = textBox1.Text;
                    String filenametostore = Path.GetFileName(filePath);

                    //cancelbtn.Enabled = true;
                    sendbtn.Enabled = true;
                   
                    //ExecuteBatFile(command);
                    ExecuteCommand(filePath);
                    
                    sendbtn.Enabled = true;
                    cancelbtn.Enabled = false;
                }
                else
                {
                    note.Text = "Failed to connect Adb.. Restart Wifi Debug";
                    cancelbtn.Enabled = false;
                    sendbtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cancelbtn.Enabled = false;
                sendbtn.Enabled = true;
            }

        }
        

        static void ExecuteCommand(String filePath)
        {
            
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            String commandbatfile = string.Format(@"""{0}pushcmd.bat""", Path.Combine(RunningPath));
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName =commandbatfile,
                    Arguments = "\""+filePath+"\""

                }
            };
            proc.Start();
            proc.WaitForExit();
          
        }


        private void WithAdbBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBar1.Value = e.ProgressPercentage;
        }

        private void WithAdbBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                note.Text = "Process Cancelled";
                progressBar1.Value = 0;
            }
        }

        private void WithoutAdbbackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            cancelbtn.Enabled = true;
            sendbtn.Enabled = false;
            try
            {

                IPAddress ipAddress = IPAddress.Parse(textBox2.Text);
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
                //int bufferSize = 1024;
                byte[] conmsg = new byte[1024];
                int c= netStream.Read(conmsg, 0, conmsg.Length);
                String connectedmsg = System.Text.Encoding.ASCII.GetString(conmsg, 0, c);
                //MessageBox.Show(connectedmsg);
                if (connectedmsg!= "CONNECTED")
                {
                    sendbtn.Enabled = true;
                    cancelbtn.Enabled = false;
                    note.Text = "Connection Faild..";
                }



                // Read bytes from file
                byte[] data = File.ReadAllBytes(textBox1.Text);
                /*string message = data.Length.ToString();
                MessageBox.Show(message);*/
                int dataLength = data.Length;
                int datakb = dataLength / 1024;

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


                CheckBox chk = AdbcheckBox;
                FileInfo fi = new FileInfo(textBox1.Text);
                //JSON REQUEST
                var myRequestData = new
                {
                    time_stamp = DateTime.Now.ToString(),
                    request_type = "File_download",
                    System_action = new
                    {
                        Device_action = "Restart",
                        Settime = "True"
                    },
                    File_info = new
                    {
                        File_size = fi.Length,
                        File_name = fi.Name
                    },
                    DLD_info = new
                    {
                        ADB = chk.Checked
                    }
                };


               // MessageBox.Show(fi.Length+" AND "+ dataLength);

                //Tranform it to Json object
                string jsonData = JsonConvert.SerializeObject(myRequestData);

                byte[] jsonbytes = Encoding.ASCII.GetBytes(jsonData);
                netStream.Write(jsonbytes, 0, jsonbytes.Length);
               // MessageBox.Show("jsonData Send Success..");

                byte[] messageReceived = new byte[1024];
                String dataconnected;
                int k;
                k = netStream.Read(messageReceived, 0, messageReceived.Length);
                dataconnected = System.Text.Encoding.ASCII.GetString(messageReceived, 0, k);
                //MessageBox.Show(dataconnected);
                //if(dataconnected == "")

                // netStream.read(messageReceived, 0, data.Length);

                for (int i = 0; i <= data.Length; i++)
                {
                    if (WithoutAdbbackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    progress = i;
                    
                    WithoutAdbbackgroundWorker.ReportProgress(progress);
                    int percentSend = (progress * 100) / dataLength;
                    percentage.Text = percentSend.ToString() + "%";

                }
                if (progress == data.Length)
                {
                    byte[] datasize = BitConverter.GetBytes(data.Length);
                    netStream.Write(data, 0, data.Length);

                    byte[] ackReceivedjson = new byte[1024];
                    String acknowledge;
                    int m;
                    m = netStream.Read(ackReceivedjson, 0, ackReceivedjson.Length);
                    acknowledge = System.Text.Encoding.ASCII.GetString(ackReceivedjson, 0, m);
                   // MessageBox.Show(acknowledge);

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




        private void WithoutAdbbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

        }

        private void WithoutAdbbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void AdbcheckBox_CheckedChanged(object sender, EventArgs e)
        {



        }


    }

}
