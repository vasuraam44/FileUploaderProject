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
using System.Threading;

namespace FileUploader
{
    public partial class FileUploader : Form
    {
        //AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        static AutoCompleteStringCollection list = new AutoCompleteStringCollection();
        List<String> listip = new List<String>();
        //AutoComplete autoComplete1 = new AutoComplete();
        TcpClient client = new TcpClient();
        public FileUploader()
        {
            InitializeComponent();

            /*
            //SETTING ENVIRONMENT VARIABLE for Adb Execution
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            String adbEnvpath = string.Format(@"{0}Resources\platform-tools", Path.Combine(RunningPath));
            var name = "Path";
            var scope = EnvironmentVariableTarget.User; // or Machin
            String oldValue = Environment.GetEnvironmentVariable(name, scope);
            List<String> listStrLineElements;
            bool adflag=true;
            listStrLineElements = oldValue.Split(';').ToList();
            //Checking the if exist discard otherwise it will add
            foreach(String value in listStrLineElements)
            {
                if (value.Contains(adbEnvpath))
                {
                    adflag = false;
                    break;
                }
            }
            if (adflag)
            {
                var newValue = oldValue + adbEnvpath+";";
                Environment.SetEnvironmentVariable("Path", newValue, scope);
                //Application.Restart();
                //Environment.Exit(0);
                
            }
            */

        }

        private void FileUploader_Load(object sender, EventArgs e)
        {
            cancelbtn.Enabled = false;
            try
            {
                String[] args = Environment.GetCommandLineArgs();
                FilePathtextBox1.Text = args[1];
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }


            //opening the subkey get Recent 5 Ips from registry
            RegistryKey keys = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MylistData");
            //if it does exist, retrieve the stored values  
            if (keys != null)
            {
                String iplist = keys.GetValue("Mylist").ToString();
                listip = iplist.Split(',').ToList();
                keys.Close();
            }
            list.AddRange(listip.ToArray());
                     
            IPtextBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            IPtextBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            IPtextBox2.AutoCompleteCustomSource = list;

        }


        private void Textbox2_MouseClick(object sender, MouseEventArgs e)
        {
           // IPtextBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }



        private void Filebtn_Click(object sender, EventArgs e)
        {
            //Opens Dialog Box to select Files and path will add to FilePathtextBox1
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"",
                CheckFileExists = true,
                CheckPathExists = true,  
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePathtextBox1.Text = openFileDialog1.FileName;
            }

        }
        //For drag file Over the textbox 
        private void FilePathtextBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;

        }

        //For DragDrop file Over the FilePathtextBox1 drop file over texbox the path will add to textbox
        private void FilePathtextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];  
            if (files != null && files.Any())
                FilePathtextBox1.Text = files.First(); //select the first one  
        }

        private void Sendbtn_Click(object sender, EventArgs e)
        {

            bool adbchbtn;
            //Checking aDB checkbox selected or not
            CheckBox chk = AdbcheckBox;
            if (chk.Checked)
            {
                adbchbtn = false;
            }
            else if (PortcomboBox1.Text == "")
            {
                adbchbtn = true;
            }
            else
            {
                adbchbtn = false;
            }

            //Checking all fields values exists or nots
            if (FilePathtextBox1.Text == "" || IPtextBox2.Text == "" || adbchbtn)
            {
                string message = "Please Fill required fields..";
                MessageBox.Show(message);
            }
            else
            {
                // IP address validation
                if (IsvalidIp())
                {
                    // port number validation
                    if (IsValidPortNumber() && IsInteger(PortcomboBox1.Text))
                    {

                        //If new Ip address it will add recent used list
                        if (list.Contains(IPtextBox2.Text) == false)
                        {
                            if (list.Count >= 5)
                            {
                                for (int i=5; i<=list.Count;i++)
                                {
                                    list.RemoveAt(i);
                                }     
                            }

                            List<String> listOfIp = new List<String>();
                            list.Insert(0, IPtextBox2.Text);
                            foreach (String value in list)
                            {
                                listOfIp.Add(value);
                            }
                            //Storing new recent 5 Ip address in to registry
                            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MylistData");
                            key.SetValue("Mylist", string.Join(",", listOfIp));
                            key.Close();
                           

                        }
                
                        if (chk.Checked)
                        {
                            //WithAdbbackgroundWorker.RunWorkerAsync();
                       
                            if (!WithAdbbackgroundWorker.IsBusy)
                            {
                                //If With ADB then call this
                                WithAdbbackgroundWorker.RunWorkerAsync();
                                //AdbcheckBox.Enabled = false;
                            }
                            else
                            {
                                //MessageBox.Show("Can't run the worker twice!");
                                note.Text = "Please close active window.. ";
                            }
                      
                            sendbtn.Enabled = true;

                        }
                        else
                        {
                           // WithoutAdbbackgroundWorker.RunWorkerAsync();

                            
                            if (!WithoutAdbbackgroundWorker.IsBusy)
                            {
                                //If With ADB then call this
                                WithoutAdbbackgroundWorker.RunWorkerAsync();
                                //AdbcheckBox.Enabled = false;
                            }
                            else
                            {
                                //MessageBox.Show("Can't run the worker twice.. Please wait for some time!");
                                //If Without ADB then call this
                                note.Text = "Please wait for sometime..";
                               
                                return;
                            }
                            

                            sendbtn.Enabled = true;

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
            //client.Close();
            //AdbcheckBox.Enabled = true;
           
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            startInfo.FileName = string.Format("{0}Resources\\platform-tools\\adb.exe", Path.Combine(RunningPath));
            startInfo.Arguments = "disconnect";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            string disconnectedOrNotoutput1 = process.StandardOutput.ReadToEnd();
            //MessageBox.Show("Disconnectd:" + disconnectedOrNotoutput1);

    /*
           
            CheckBox chk = AdbcheckBox;
            if (chk.Checked)
            {
                WithAdbbackgroundWorker.CancelAsync();
                note.Text = "Process Cancelled..";
                if (WithAdbbackgroundWorker.IsBusy)
                {
                    note.Text = "Please close active window ........";
                    sendbtn.Enabled = true;
                   // MessageBox.Show("Busy "); 
                }
                else
                {
                    note.Text = "free";
                    
                    
                }
            }
            else
            {
                WithoutAdbbackgroundWorker.CancelAsync();
                if (WithAdbbackgroundWorker.IsBusy)
                {
                    note.Text = "Busy with some task please wait..";
                    MessageBox.Show("Busy ");
                }
                else
                {
                    note.Text = "Busy with some task please wait 5sec..";
                    Thread.Sleep(5000);//will sleep for 5 sec.
                    sendbtn.Enabled = true;
                }
                
            }
            cancelbtn.Enabled = false;
            progressBar1.Value = 0;
            note.Text = "Process Cancelled..";
            percentage.Text = "";
            */

           
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
            return Regex.IsMatch(IPtextBox2.Text, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$");

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
                if (IsInteger(PortcomboBox1.Text))
                {
                    if (int.Parse(PortcomboBox1.Text) > 1 && int.Parse(PortcomboBox1.Text) < 65535)
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
            note.Text = "";
            cancelbtn.Enabled = false;
            sendbtn.Enabled = false;

            //Process to execute some commands to connect android device by IP address
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            startInfo.FileName = string.Format("{0}Resources\\platform-tools\\adb.exe", Path.Combine(RunningPath));         
            String MyIp = IPtextBox2.Text;
            startInfo.Arguments = "connect " + MyIp + ":5555";
            process.StartInfo = startInfo;
            try
            {
              
                process.Start();
                process.WaitForExit();
                string connectedOrNotoutput1 = process.StandardOutput.ReadToEnd();
                if (connectedOrNotoutput1 == "")
                {
                    MessageBox.Show("Please connect device..");
                  
                    cancelbtn.Enabled = false;
                    sendbtn.Enabled = true;
                }
                else if (connectedOrNotoutput1.Contains("connected"))
                {
                    note.Text = "";
                    process.Close();
                    String filePath = FilePathtextBox1.Text;
                    String filenametostore = Path.GetFileName(filePath);
                    cancelbtn.Enabled = true;
                    ExecuteCommand(filePath, cancelbtn);

                    // cancelbtn.Enabled = true;
                    sendbtn.Enabled = true;
                    
                }
                else
                {
                    note.Text = "Failed to connect Device, Restart Wifi Debugging..";
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
        
        //Execute Bat file to push file
        static void ExecuteCommand(String filePath,Button cancelbtn)
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
            cancelbtn.Enabled = false;
        }


        private void WithAdbBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void WithAdbBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            if (e.Cancelled)
            {
                //note.Text = "Process Cancelled";
                //progressBar1.Value = 0;
                
            }
        }

        private void WithoutAdbbackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            note.Text = "";
            cancelbtn.Enabled = true;
            sendbtn.Enabled = false;
            try
            {

                IPAddress ipAddress = IPAddress.Parse(IPtextBox2.Text);
               
                NetworkStream netStream;
                // Connect to server
                try
                {
                    note.Text = "Connecting..";
                    client.Connect(new IPEndPoint(ipAddress, int.Parse(PortcomboBox1.Text)));
                    
                }
                catch (Exception)
                {
                   
                    cancelbtn.Enabled = false;
                    note.Text = "Connection Faild, try again..";
                    sendbtn.Enabled = true;
                    return;
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
                   
                    cancelbtn.Enabled = false;
                    note.Text = "Connection Faild, try again..";
                    sendbtn.Enabled = true;
                }



                
                byte[] data = File.ReadAllBytes(FilePathtextBox1.Text);
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
                FileInfo fi = new FileInfo(FilePathtextBox1.Text);
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
                note.Text = "Connection Faild, Try again..";
                sendbtn.Enabled = true;
                cancelbtn.Enabled = false;
            }
            catch (Exception)
            {
                sendbtn.Enabled = true;
                cancelbtn.Enabled = false;
                note.Text = "Connection Faild, Try again...";
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
            PortcomboBox1.DroppedDown = true;
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
