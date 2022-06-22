namespace FileUploader
{
    partial class FileUploader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileUploader));
            this.FilePathtextBox1 = new System.Windows.Forms.TextBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.PortcomboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IPtextBox2 = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.percentage = new System.Windows.Forms.Label();
            this.note = new System.Windows.Forms.Label();
            this.sendbtn = new System.Windows.Forms.Button();
            this.Filebtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.WithoutAdbbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.AdbcheckBox = new System.Windows.Forms.CheckBox();
            this.filecheckBox = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.WithAdbbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // FilePathtextBox1
            // 
            this.FilePathtextBox1.AllowDrop = true;
            this.FilePathtextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePathtextBox1.Location = new System.Drawing.Point(182, 61);
            this.FilePathtextBox1.MinimumSize = new System.Drawing.Size(4, 30);
            this.FilePathtextBox1.Name = "FilePathtextBox1";
            this.FilePathtextBox1.Size = new System.Drawing.Size(281, 30);
            this.FilePathtextBox1.TabIndex = 1;
            this.FilePathtextBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FilePathtextBox1_DragDrop);
            this.FilePathtextBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.FilePathtextBox1_DragOver);
            //
            // cancelbtn
            // 
            this.cancelbtn.BackgroundImage = global::FileUploader.Properties.Resources.FileSelect;
            this.cancelbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelbtn.FlatAppearance.BorderSize = 0;
            this.cancelbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbtn.Location = new System.Drawing.Point(318, 371);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(89, 33);
            this.cancelbtn.TabIndex = 5;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // PortcomboBox1
            // 
            this.PortcomboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortcomboBox1.FormattingEnabled = true;
            this.PortcomboBox1.Items.AddRange(new object[] {
            "8080",
            "8081",
            "8082",
            "55052",
            "55053"});
            this.PortcomboBox1.Location = new System.Drawing.Point(182, 187);
            this.PortcomboBox1.Name = "PortcomboBox1";
            this.PortcomboBox1.Size = new System.Drawing.Size(169, 28);
            this.PortcomboBox1.TabIndex = 3;
            this.PortcomboBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ComboBox1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 126);
            this.label2.MinimumSize = new System.Drawing.Size(50, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "IP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IPtextBox2
            // 
            this.IPtextBox2.AutoCompleteCustomSource.AddRange(new string[] {
            "192.168.1.180",
            "172.30.144.1",
            "192.168.210.251",
            "192.168.183.165"});
            this.IPtextBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.IPtextBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.IPtextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPtextBox2.Location = new System.Drawing.Point(182, 118);
            this.IPtextBox2.MinimumSize = new System.Drawing.Size(4, 30);
            this.IPtextBox2.Name = "IPtextBox2";
            this.IPtextBox2.Size = new System.Drawing.Size(281, 30);
            this.IPtextBox2.TabIndex = 2;
            this.IPtextBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Textbox2_MouseClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(57, 266);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(405, 27);
            this.progressBar1.TabIndex = 8;
            // 
            // percentage
            // 
            this.percentage.AutoSize = true;
            this.percentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentage.Location = new System.Drawing.Point(468, 266);
            this.percentage.Margin = new System.Windows.Forms.Padding(3);
            this.percentage.MinimumSize = new System.Drawing.Size(50, 23);
            this.percentage.Name = "percentage";
            this.percentage.Size = new System.Drawing.Size(50, 23);
            this.percentage.TabIndex = 9;
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(102, 307);
            this.note.Margin = new System.Windows.Forms.Padding(1);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(324, 24);
            this.note.TabIndex = 10;
            // 
            // sendbtn
            // 
            this.sendbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sendbtn.BackgroundImage")));
            this.sendbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sendbtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.sendbtn.FlatAppearance.BorderSize = 0;
            this.sendbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sendbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sendbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendbtn.Location = new System.Drawing.Point(54, 371);
            this.sendbtn.Name = "sendbtn";
            this.sendbtn.Size = new System.Drawing.Size(86, 33);
            this.sendbtn.TabIndex = 4;
            this.sendbtn.Text = "Send";
            this.sendbtn.UseVisualStyleBackColor = true;
            this.sendbtn.Click += new System.EventHandler(this.Sendbtn_Click);
            // 
            // Filebtn
            // 
            this.Filebtn.BackgroundImage = global::FileUploader.Properties.Resources.FileSelect;
            this.Filebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Filebtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Filebtn.FlatAppearance.BorderSize = 0;
            this.Filebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Filebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Filebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Filebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filebtn.Location = new System.Drawing.Point(57, 61);
            this.Filebtn.Name = "Filebtn";
            this.Filebtn.Size = new System.Drawing.Size(83, 30);
            this.Filebtn.TabIndex = 0;
            this.Filebtn.Text = "File";
            this.Filebtn.UseVisualStyleBackColor = true;
            this.Filebtn.Click += new System.EventHandler(this.Filebtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Progress";
            // 
            // WithoutAdbbackgroundWorker
            // 
            this.WithoutAdbbackgroundWorker.WorkerReportsProgress = true;
            this.WithoutAdbbackgroundWorker.WorkerSupportsCancellation = true;
            this.WithoutAdbbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WithoutAdbbackgroundWorker_DoWork);
            this.WithoutAdbbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.WithoutAdbbackgroundWorker_ProgressChanged);
            this.WithoutAdbbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WithoutAdbbackgroundWorker_RunWorkerCompleted);
            // 
            // AdbcheckBox
            // 
            this.AdbcheckBox.AutoSize = true;
            this.AdbcheckBox.Location = new System.Drawing.Point(158, 382);
            this.AdbcheckBox.Name = "AdbcheckBox";
            this.AdbcheckBox.Size = new System.Drawing.Size(48, 17);
            this.AdbcheckBox.TabIndex = 12;
            this.AdbcheckBox.Text = "ADB";
            this.AdbcheckBox.UseVisualStyleBackColor = true;
            // 
            // filecheckBox
            // 
            this.filecheckBox.AutoSize = true;
            this.filecheckBox.Location = new System.Drawing.Point(60, 432);
            this.filecheckBox.Name = "filecheckBox";
            this.filecheckBox.Size = new System.Drawing.Size(72, 17);
            this.filecheckBox.TabIndex = 13;
            this.filecheckBox.Text = "File Install";
            this.filecheckBox.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "192.168.1.180",
            "172.30.144.1",
            "192.168.183.165"});
            this.comboBox2.Location = new System.Drawing.Point(182, 154);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(281, 28);
            this.comboBox2.TabIndex = 14;
            this.comboBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox2_KeyPress);
            this.comboBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ComboBox2_MouseClick);
            // 
            // WithAdbbackgroundWorker
            // 
            this.WithAdbbackgroundWorker.WorkerReportsProgress = true;
            this.WithAdbbackgroundWorker.WorkerSupportsCancellation = true;
            this.WithAdbbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WithAdbBackgroundWorker_DoWork);
            this.WithAdbbackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.WithAdbBackgroundWorker_ProgressChanged);
            this.WithAdbbackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WithAdbBackgroundWorker_RunWorkerCompleted);
            // 
            // FileUploader
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(547, 472);
            this.Controls.Add(this.filecheckBox);
            this.Controls.Add(this.AdbcheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.note);
            this.Controls.Add(this.percentage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.IPtextBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortcomboBox1);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.sendbtn);
            this.Controls.Add(this.FilePathtextBox1);
            this.Controls.Add(this.Filebtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FileUploader";
            this.Text = "FileUploader 1.0";
            this.Load += new System.EventHandler(this.FileUploader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Filebtn;
        private System.Windows.Forms.TextBox FilePathtextBox1;
        private System.Windows.Forms.Button sendbtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.ComboBox PortcomboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IPtextBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label percentage;
        private System.Windows.Forms.Label note;
        private System.Windows.Forms.Label label3;
        public System.ComponentModel.BackgroundWorker WithoutAdbbackgroundWorker;
        private System.Windows.Forms.CheckBox AdbcheckBox;
        private System.Windows.Forms.CheckBox filecheckBox;
        private System.Windows.Forms.ComboBox comboBox2;
        public System.ComponentModel.BackgroundWorker WithAdbbackgroundWorker;
    }
}

