namespace AudioMonitor
{
    partial class MenuForm
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
            this.btnLaunchWaveform = new System.Windows.Forms.Button();
            this.btnLaunchFFT = new System.Windows.Forms.Button();
            this.lbDevice = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLaunchWaveform
            // 
            this.btnLaunchWaveform.Location = new System.Drawing.Point(15, 365);
            this.btnLaunchWaveform.Name = "btnLaunchWaveform";
            this.btnLaunchWaveform.Size = new System.Drawing.Size(117, 54);
            this.btnLaunchWaveform.TabIndex = 0;
            this.btnLaunchWaveform.Text = "Audio Waveform";
            this.btnLaunchWaveform.UseVisualStyleBackColor = true;
            this.btnLaunchWaveform.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLaunchFFT
            // 
            this.btnLaunchFFT.Location = new System.Drawing.Point(158, 365);
            this.btnLaunchFFT.Name = "btnLaunchFFT";
            this.btnLaunchFFT.Size = new System.Drawing.Size(117, 54);
            this.btnLaunchFFT.TabIndex = 1;
            this.btnLaunchFFT.Text = "Audio FFT";
            this.btnLaunchFFT.UseVisualStyleBackColor = true;
            this.btnLaunchFFT.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbDevice
            // 
            this.lbDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDevice.FormattingEnabled = true;
            this.lbDevice.ItemHeight = 15;
            this.lbDevice.Location = new System.Drawing.Point(3, 19);
            this.lbDevice.Name = "lbDevice";
            this.lbDevice.Size = new System.Drawing.Size(394, 315);
            this.lbDevice.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbDevice);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 337);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device";
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 439);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLaunchFFT);
            this.Controls.Add(this.btnLaunchWaveform);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio Monitor";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnLaunchWaveform;
        private Button btnLaunchFFT;
        private ListBox lbDevice;
        private GroupBox groupBox1;
    }
}