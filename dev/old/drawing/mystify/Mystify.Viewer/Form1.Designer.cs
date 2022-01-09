namespace Mystify.Viewer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
            this.nudPolygons = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGraphics = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCorners = new System.Windows.Forms.NumericUpDown();
            this.nudHistory = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRainbow = new System.Windows.Forms.CheckBox();
            this.cbFade = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.nudSpacing = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.timerRender = new System.Windows.Forms.Timer(this.components);
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPolygons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCorners)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistory)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Purple;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.skglControl1);
            this.panel1.Location = new System.Drawing.Point(12, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 446);
            this.panel1.TabIndex = 0;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Navy;
            this.pictureBox1.Location = new System.Drawing.Point(421, 118);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(327, 238);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // skglControl1
            // 
            this.skglControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.skglControl1.Location = new System.Drawing.Point(92, 118);
            this.skglControl1.Name = "skglControl1";
            this.skglControl1.Size = new System.Drawing.Size(266, 238);
            this.skglControl1.TabIndex = 0;
            this.skglControl1.VSync = true;
            this.skglControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl1_PaintSurface);
            // 
            // nudPolygons
            // 
            this.nudPolygons.Location = new System.Drawing.Point(167, 34);
            this.nudPolygons.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPolygons.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPolygons.Name = "nudPolygons";
            this.nudPolygons.Size = new System.Drawing.Size(50, 20);
            this.nudPolygons.TabIndex = 1;
            this.nudPolygons.ThousandsSeparator = true;
            this.nudPolygons.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudPolygons.ValueChanged += new System.EventHandler(this.nudPolygons_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Polygons:";
            // 
            // cbGraphics
            // 
            this.cbGraphics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGraphics.FormattingEnabled = true;
            this.cbGraphics.Items.AddRange(new object[] {
            "SkiaSharp with OpenGL",
            "System.Drawing"});
            this.cbGraphics.Location = new System.Drawing.Point(12, 34);
            this.cbGraphics.Name = "cbGraphics";
            this.cbGraphics.Size = new System.Drawing.Size(146, 21);
            this.cbGraphics.TabIndex = 4;
            this.cbGraphics.SelectedIndexChanged += new System.EventHandler(this.cbGraphics_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Graphics System:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Corners:";
            // 
            // nudCorners
            // 
            this.nudCorners.Location = new System.Drawing.Point(223, 34);
            this.nudCorners.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCorners.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudCorners.Name = "nudCorners";
            this.nudCorners.Size = new System.Drawing.Size(43, 20);
            this.nudCorners.TabIndex = 7;
            this.nudCorners.ThousandsSeparator = true;
            this.nudCorners.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudCorners.ValueChanged += new System.EventHandler(this.nudCorners_ValueChanged);
            // 
            // nudHistory
            // 
            this.nudHistory.Location = new System.Drawing.Point(272, 34);
            this.nudHistory.Name = "nudHistory";
            this.nudHistory.Size = new System.Drawing.Size(48, 20);
            this.nudHistory.TabIndex = 8;
            this.nudHistory.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudHistory.ValueChanged += new System.EventHandler(this.nudHistory_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "History:";
            // 
            // cbRainbow
            // 
            this.cbRainbow.AutoSize = true;
            this.cbRainbow.Location = new System.Drawing.Point(495, 37);
            this.cbRainbow.Name = "cbRainbow";
            this.cbRainbow.Size = new System.Drawing.Size(68, 17);
            this.cbRainbow.TabIndex = 10;
            this.cbRainbow.Text = "Rainbow";
            this.cbRainbow.UseVisualStyleBackColor = true;
            // 
            // cbFade
            // 
            this.cbFade.AutoSize = true;
            this.cbFade.Location = new System.Drawing.Point(569, 37);
            this.cbFade.Name = "cbFade";
            this.cbFade.Size = new System.Drawing.Size(50, 17);
            this.cbFade.TabIndex = 11;
            this.cbFade.Text = "Fade";
            this.cbFade.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(625, 33);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(855, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // nudSpacing
            // 
            this.nudSpacing.Location = new System.Drawing.Point(326, 34);
            this.nudSpacing.Name = "nudSpacing";
            this.nudSpacing.Size = new System.Drawing.Size(46, 20);
            this.nudSpacing.TabIndex = 15;
            this.nudSpacing.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSpacing.ValueChanged += new System.EventHandler(this.nudSpacing_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(323, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Spacing:";
            // 
            // nudSpeed
            // 
            this.nudSpeed.Location = new System.Drawing.Point(378, 34);
            this.nudSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(51, 20);
            this.nudSpeed.TabIndex = 17;
            this.nudSpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(375, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Speed:";
            // 
            // timerRender
            // 
            this.timerRender.Enabled = true;
            this.timerRender.Interval = 1;
            this.timerRender.Tick += new System.EventHandler(this.timerRender_Tick);
            // 
            // nudWidth
            // 
            this.nudWidth.DecimalPlaces = 1;
            this.nudWidth.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nudWidth.Location = new System.Drawing.Point(435, 35);
            this.nudWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(54, 20);
            this.nudWidth.TabIndex = 19;
            this.nudWidth.Value = new decimal(new int[] {
            14,
            0,
            0,
            65536});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(432, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Width:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 543);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.nudSpeed);
            this.Controls.Add(this.nudSpacing);
            this.Controls.Add(this.nudHistory);
            this.Controls.Add(this.nudCorners);
            this.Controls.Add(this.nudPolygons);
            this.Controls.Add(this.cbFade);
            this.Controls.Add(this.cbRainbow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbGraphics);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Mystify";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPolygons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCorners)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistory)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudPolygons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGraphics;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudCorners;
        private System.Windows.Forms.NumericUpDown nudHistory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbRainbow;
        private System.Windows.Forms.CheckBox cbFade;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.NumericUpDown nudSpacing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timerRender;
        private System.Windows.Forms.PictureBox pictureBox1;
        private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label7;
    }
}

