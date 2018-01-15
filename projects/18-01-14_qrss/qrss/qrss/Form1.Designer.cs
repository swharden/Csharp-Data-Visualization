namespace qrss
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbl_spec_width_time = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cmb_colormap = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cb_logscale = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.nud_scale_factor = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.nud_spec_width = new System.Windows.Forms.NumericUpDown();
            this.lbl_spec_size = new System.Windows.Forms.Label();
            this.lbl_resolution = new System.Windows.Forms.Label();
            this.lbl_peak_freq = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_buffer_size_points = new System.Windows.Forms.Label();
            this.lbl_fft_step_ms = new System.Windows.Forms.Label();
            this.nud_fft_step_ms = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_fft_size = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nud_fft_size = new System.Windows.Forms.NumericUpDown();
            this.lbl_unanalyzed_sec = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_buffers_captured = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nud_buffer_size = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.combo_rate = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.combo_device = new System.Windows.Forms.ComboBox();
            this.pic_level_front = new System.Windows.Forms.PictureBox();
            this.pic_level_back = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_scale_factor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_spec_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fft_step_ms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fft_size)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_buffer_size)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_level_front)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_level_back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbl_spec_width_time);
            this.splitContainer1.Panel1.Controls.Add(this.label18);
            this.splitContainer1.Panel1.Controls.Add(this.btn_save);
            this.splitContainer1.Panel1.Controls.Add(this.label17);
            this.splitContainer1.Panel1.Controls.Add(this.cmb_colormap);
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.cb_logscale);
            this.splitContainer1.Panel1.Controls.Add(this.label15);
            this.splitContainer1.Panel1.Controls.Add(this.nud_scale_factor);
            this.splitContainer1.Panel1.Controls.Add(this.label14);
            this.splitContainer1.Panel1.Controls.Add(this.nud_spec_width);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_spec_size);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_resolution);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_peak_freq);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_buffer_size_points);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_fft_step_ms);
            this.splitContainer1.Panel1.Controls.Add(this.nud_fft_step_ms);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_fft_size);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.nud_fft_size);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_unanalyzed_sec);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_buffers_captured);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.nud_buffer_size);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.combo_rate);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.combo_device);
            this.splitContainer1.Panel1.Controls.Add(this.pic_level_front);
            this.splitContainer1.Panel1.Controls.Add(this.pic_level_back);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(984, 755);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbl_spec_width_time
            // 
            this.lbl_spec_width_time.AutoSize = true;
            this.lbl_spec_width_time.Location = new System.Drawing.Point(142, 180);
            this.lbl_spec_width_time.Name = "lbl_spec_width_time";
            this.lbl_spec_width_time.Size = new System.Drawing.Size(52, 17);
            this.lbl_spec_width_time.TabIndex = 53;
            this.lbl_spec_width_time.Text = "12.324";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(320, 106);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(125, 17);
            this.label18.TabIndex = 52;
            this.label18.Text = "spectrograph size:";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(861, 159);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(111, 38);
            this.btn_save.TabIndex = 51;
            this.btn_save.Text = "save image";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(785, 133);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 17);
            this.label17.TabIndex = 50;
            this.label17.Text = "colormap:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_colormap
            // 
            this.cmb_colormap.FormattingEnabled = true;
            this.cmb_colormap.Items.AddRange(new object[] {
            "gray",
            "blue",
            "green",
            "spectrum"});
            this.cmb_colormap.Location = new System.Drawing.Point(861, 128);
            this.cmb_colormap.Name = "cmb_colormap";
            this.cmb_colormap.Size = new System.Drawing.Size(111, 24);
            this.cmb_colormap.TabIndex = 49;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(782, 102);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 17);
            this.label16.TabIndex = 48;
            this.label16.Text = "Log scale:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cb_logscale
            // 
            this.cb_logscale.AutoSize = true;
            this.cb_logscale.Location = new System.Drawing.Point(861, 104);
            this.cb_logscale.Name = "cb_logscale";
            this.cb_logscale.Size = new System.Drawing.Size(18, 17);
            this.cb_logscale.TabIndex = 47;
            this.cb_logscale.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(730, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 17);
            this.label15.TabIndex = 46;
            this.label15.Text = "scale factor (mult):";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nud_scale_factor
            // 
            this.nud_scale_factor.DecimalPlaces = 1;
            this.nud_scale_factor.Location = new System.Drawing.Point(861, 73);
            this.nud_scale_factor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_scale_factor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_scale_factor.Name = "nud_scale_factor";
            this.nud_scale_factor.Size = new System.Drawing.Size(111, 22);
            this.nud_scale_factor.TabIndex = 45;
            this.nud_scale_factor.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(369, 159);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 17);
            this.label14.TabIndex = 44;
            this.label14.Text = "Width (px):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nud_spec_width
            // 
            this.nud_spec_width.Location = new System.Drawing.Point(454, 157);
            this.nud_spec_width.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nud_spec_width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_spec_width.Name = "nud_spec_width";
            this.nud_spec_width.Size = new System.Drawing.Size(90, 22);
            this.nud_spec_width.TabIndex = 43;
            this.nud_spec_width.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nud_spec_width.ValueChanged += new System.EventHandler(this.nud_spec_width_ValueChanged);
            // 
            // lbl_spec_size
            // 
            this.lbl_spec_size.AutoSize = true;
            this.lbl_spec_size.Location = new System.Drawing.Point(135, 163);
            this.lbl_spec_size.Name = "lbl_spec_size";
            this.lbl_spec_size.Size = new System.Drawing.Size(52, 17);
            this.lbl_spec_size.TabIndex = 42;
            this.lbl_spec_size.Text = "12.324";
            // 
            // lbl_resolution
            // 
            this.lbl_resolution.AutoSize = true;
            this.lbl_resolution.Location = new System.Drawing.Point(84, 146);
            this.lbl_resolution.Name = "lbl_resolution";
            this.lbl_resolution.Size = new System.Drawing.Size(52, 17);
            this.lbl_resolution.TabIndex = 41;
            this.lbl_resolution.Text = "12.324";
            // 
            // lbl_peak_freq
            // 
            this.lbl_peak_freq.AutoSize = true;
            this.lbl_peak_freq.Location = new System.Drawing.Point(120, 129);
            this.lbl_peak_freq.Name = "lbl_peak_freq";
            this.lbl_peak_freq.Size = new System.Drawing.Size(52, 17);
            this.lbl_peak_freq.TabIndex = 40;
            this.lbl_peak_freq.Text = "12.324";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 17);
            this.label13.TabIndex = 39;
            this.label13.Text = "spectrograph size:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 146);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 17);
            this.label12.TabIndex = 38;
            this.label12.Text = "resolution:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 17);
            this.label10.TabIndex = 37;
            this.label10.Text = "peak frequency:";
            // 
            // lbl_buffer_size_points
            // 
            this.lbl_buffer_size_points.AutoSize = true;
            this.lbl_buffer_size_points.Location = new System.Drawing.Point(550, 78);
            this.lbl_buffer_size_points.Name = "lbl_buffer_size_points";
            this.lbl_buffer_size_points.Size = new System.Drawing.Size(82, 17);
            this.lbl_buffer_size_points.TabIndex = 36;
            this.lbl_buffer_size_points.Text = "1234 points";
            this.lbl_buffer_size_points.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_fft_step_ms
            // 
            this.lbl_fft_step_ms.AutoSize = true;
            this.lbl_fft_step_ms.Location = new System.Drawing.Point(550, 133);
            this.lbl_fft_step_ms.Name = "lbl_fft_step_ms";
            this.lbl_fft_step_ms.Size = new System.Drawing.Size(90, 17);
            this.lbl_fft_step_ms.TabIndex = 35;
            this.lbl_fft_step_ms.Text = "12345 points";
            this.lbl_fft_step_ms.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nud_fft_step_ms
            // 
            this.nud_fft_step_ms.Location = new System.Drawing.Point(454, 131);
            this.nud_fft_step_ms.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_fft_step_ms.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_fft_step_ms.Name = "nud_fft_step_ms";
            this.nud_fft_step_ms.Size = new System.Drawing.Size(90, 22);
            this.nud_fft_step_ms.TabIndex = 34;
            this.nud_fft_step_ms.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nud_fft_step_ms.ValueChanged += new System.EventHandler(this.nud_fft_step_ms_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(345, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 17);
            this.label11.TabIndex = 33;
            this.label11.Text = "FFT step (ms):";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_fft_size
            // 
            this.lbl_fft_size.AutoSize = true;
            this.lbl_fft_size.Location = new System.Drawing.Point(550, 106);
            this.lbl_fft_size.Name = "lbl_fft_size";
            this.lbl_fft_size.Size = new System.Drawing.Size(82, 17);
            this.lbl_fft_size.TabIndex = 32;
            this.lbl_fft_size.Text = "2048 points";
            this.lbl_fft_size.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "spectrograph width:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nud_fft_size
            // 
            this.nud_fft_size.Location = new System.Drawing.Point(454, 104);
            this.nud_fft_size.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nud_fft_size.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_fft_size.Name = "nud_fft_size";
            this.nud_fft_size.Size = new System.Drawing.Size(90, 22);
            this.nud_fft_size.TabIndex = 30;
            this.nud_fft_size.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nud_fft_size.ValueChanged += new System.EventHandler(this.nud_fft_size_ValueChanged);
            // 
            // lbl_unanalyzed_sec
            // 
            this.lbl_unanalyzed_sec.AutoSize = true;
            this.lbl_unanalyzed_sec.Location = new System.Drawing.Point(170, 112);
            this.lbl_unanalyzed_sec.Name = "lbl_unanalyzed_sec";
            this.lbl_unanalyzed_sec.Size = new System.Drawing.Size(52, 17);
            this.lbl_unanalyzed_sec.TabIndex = 29;
            this.lbl_unanalyzed_sec.Text = "12.324";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "unanalyzed audio (sec):";
            // 
            // lbl_buffers_captured
            // 
            this.lbl_buffers_captured.AutoSize = true;
            this.lbl_buffers_captured.Location = new System.Drawing.Point(126, 95);
            this.lbl_buffers_captured.Name = "lbl_buffers_captured";
            this.lbl_buffers_captured.Size = new System.Drawing.Size(48, 17);
            this.lbl_buffers_captured.TabIndex = 27;
            this.lbl_buffers_captured.Text = "12345";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "buffers captured:";
            // 
            // nud_buffer_size
            // 
            this.nud_buffer_size.Location = new System.Drawing.Point(454, 76);
            this.nud_buffer_size.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nud_buffer_size.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_buffer_size.Name = "nud_buffer_size";
            this.nud_buffer_size.Size = new System.Drawing.Size(90, 22);
            this.nud_buffer_size.TabIndex = 25;
            this.nud_buffer_size.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nud_buffer_size.ValueChanged += new System.EventHandler(this.nud_buffer_size_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(335, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 17);
            this.label6.TabIndex = 24;
            this.label6.Text = "Buffer Size (ms):";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combo_rate
            // 
            this.combo_rate.FormattingEnabled = true;
            this.combo_rate.Items.AddRange(new object[] {
            "44100",
            "32000",
            "12000",
            "8000",
            "4000"});
            this.combo_rate.Location = new System.Drawing.Point(454, 46);
            this.combo_rate.Name = "combo_rate";
            this.combo_rate.Size = new System.Drawing.Size(90, 24);
            this.combo_rate.TabIndex = 23;
            this.combo_rate.Text = "22333";
            this.combo_rate.SelectedIndexChanged += new System.EventHandler(this.combo_rate_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(324, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "Sample Rate (Hz):";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "Audio Device:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combo_device
            // 
            this.combo_device.FormattingEnabled = true;
            this.combo_device.Location = new System.Drawing.Point(454, 16);
            this.combo_device.Name = "combo_device";
            this.combo_device.Size = new System.Drawing.Size(258, 24);
            this.combo_device.TabIndex = 20;
            // 
            // pic_level_front
            // 
            this.pic_level_front.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pic_level_front.Location = new System.Drawing.Point(298, 12);
            this.pic_level_front.Name = "pic_level_front";
            this.pic_level_front.Size = new System.Drawing.Size(10, 57);
            this.pic_level_front.TabIndex = 17;
            this.pic_level_front.TabStop = false;
            // 
            // pic_level_back
            // 
            this.pic_level_back.BackColor = System.Drawing.Color.Red;
            this.pic_level_back.Location = new System.Drawing.Point(298, 12);
            this.pic_level_back.Name = "pic_level_back";
            this.pic_level_back.Size = new System.Drawing.Size(10, 176);
            this.pic_level_back.TabIndex = 16;
            this.pic_level_back.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(897, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 44);
            this.button1.TabIndex = 5;
            this.button1.Text = "start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "GitHub.com/SWHarden";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "C# Data Visualization Demo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "QRSS Spectrograph";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(331, 224);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 755);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "QRSS Spectrograph";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_scale_factor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_spec_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fft_step_ms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_fft_size)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_buffer_size)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_level_front)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_level_back)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic_level_front;
        private System.Windows.Forms.PictureBox pic_level_back;
        private System.Windows.Forms.Label lbl_buffers_captured;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nud_buffer_size;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combo_rate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox combo_device;
        private System.Windows.Forms.Label lbl_unanalyzed_sec;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown nud_fft_size;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_buffer_size_points;
        private System.Windows.Forms.Label lbl_fft_step_ms;
        private System.Windows.Forms.NumericUpDown nud_fft_step_ms;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_fft_size;
        private System.Windows.Forms.Label lbl_spec_size;
        private System.Windows.Forms.Label lbl_resolution;
        private System.Windows.Forms.Label lbl_peak_freq;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nud_spec_width;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox cb_logscale;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nud_scale_factor;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmb_colormap;
        private System.Windows.Forms.Label lbl_spec_width_time;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btn_save;
    }
}

