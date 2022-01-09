namespace Viewer
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
            this.AttractTrackbar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.AvoidTrackbar = new System.Windows.Forms.TrackBar();
            this.AlignTrackbar = new System.Windows.Forms.TrackBar();
            this.FpsLabel = new System.Windows.Forms.Label();
            this.RangeCheckbox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.PredatorCountNud = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BoidCountNud = new System.Windows.Forms.NumericUpDown();
            this.TailsCheckbox = new System.Windows.Forms.CheckBox();
            this.SpeedTrackbar = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
            this.DirectionCheckbox = new System.Windows.Forms.CheckBox();
            this.VisionTrackbar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.RandomCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.AttractTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvoidTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlignTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PredatorCountNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoidCountNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisionTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // AttractTrackbar
            // 
            this.AttractTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AttractTrackbar.AutoSize = false;
            this.AttractTrackbar.Location = new System.Drawing.Point(232, 619);
            this.AttractTrackbar.Maximum = 20;
            this.AttractTrackbar.Name = "AttractTrackbar";
            this.AttractTrackbar.Size = new System.Drawing.Size(112, 30);
            this.AttractTrackbar.TabIndex = 1;
            this.AttractTrackbar.Value = 10;
            this.AttractTrackbar.Scroll += new System.EventHandler(this.AttractTrackbar_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(232, 596);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Attract";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetButton.Location = new System.Drawing.Point(13, 620);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(90, 29);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(350, 596);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Avoid";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AvoidTrackbar
            // 
            this.AvoidTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AvoidTrackbar.AutoSize = false;
            this.AvoidTrackbar.Location = new System.Drawing.Point(350, 619);
            this.AvoidTrackbar.Maximum = 20;
            this.AvoidTrackbar.Name = "AvoidTrackbar";
            this.AvoidTrackbar.Size = new System.Drawing.Size(112, 30);
            this.AvoidTrackbar.TabIndex = 7;
            this.AvoidTrackbar.Value = 10;
            this.AvoidTrackbar.Scroll += new System.EventHandler(this.AvoidTrackbar_Scroll);
            // 
            // AlignTrackbar
            // 
            this.AlignTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AlignTrackbar.AutoSize = false;
            this.AlignTrackbar.Location = new System.Drawing.Point(468, 619);
            this.AlignTrackbar.Maximum = 20;
            this.AlignTrackbar.Name = "AlignTrackbar";
            this.AlignTrackbar.Size = new System.Drawing.Size(112, 30);
            this.AlignTrackbar.TabIndex = 10;
            this.AlignTrackbar.Value = 10;
            this.AlignTrackbar.Scroll += new System.EventHandler(this.AlignTrackbar_Scroll);
            // 
            // FpsLabel
            // 
            this.FpsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FpsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FpsLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.FpsLabel.Location = new System.Drawing.Point(1063, 628);
            this.FpsLabel.Name = "FpsLabel";
            this.FpsLabel.Size = new System.Drawing.Size(109, 24);
            this.FpsLabel.TabIndex = 22;
            this.FpsLabel.Text = "0.00 FPS";
            this.FpsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RangeCheckbox
            // 
            this.RangeCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RangeCheckbox.AutoSize = true;
            this.RangeCheckbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RangeCheckbox.Location = new System.Drawing.Point(966, 628);
            this.RangeCheckbox.Name = "RangeCheckbox";
            this.RangeCheckbox.Size = new System.Drawing.Size(80, 25);
            this.RangeCheckbox.TabIndex = 21;
            this.RangeCheckbox.Text = "Ranges";
            this.RangeCheckbox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(698, 597);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 23);
            this.label8.TabIndex = 20;
            this.label8.Text = "Predators";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PredatorCountNud
            // 
            this.PredatorCountNud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PredatorCountNud.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PredatorCountNud.Location = new System.Drawing.Point(713, 620);
            this.PredatorCountNud.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.PredatorCountNud.Name = "PredatorCountNud";
            this.PredatorCountNud.Size = new System.Drawing.Size(61, 29);
            this.PredatorCountNud.TabIndex = 19;
            this.PredatorCountNud.ValueChanged += new System.EventHandler(this.PredatorCountNud_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(802, 596);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 23);
            this.label6.TabIndex = 18;
            this.label6.Text = "Speed";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(594, 595);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 23);
            this.label7.TabIndex = 17;
            this.label7.Text = "Boids";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BoidCountNud
            // 
            this.BoidCountNud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BoidCountNud.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoidCountNud.Location = new System.Drawing.Point(612, 620);
            this.BoidCountNud.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.BoidCountNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BoidCountNud.Name = "BoidCountNud";
            this.BoidCountNud.Size = new System.Drawing.Size(61, 29);
            this.BoidCountNud.TabIndex = 16;
            this.BoidCountNud.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.BoidCountNud.ValueChanged += new System.EventHandler(this.BoidCountNud_ValueChanged);
            // 
            // TailsCheckbox
            // 
            this.TailsCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TailsCheckbox.AutoSize = true;
            this.TailsCheckbox.Checked = true;
            this.TailsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TailsCheckbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TailsCheckbox.Location = new System.Drawing.Point(966, 604);
            this.TailsCheckbox.Name = "TailsCheckbox";
            this.TailsCheckbox.Size = new System.Drawing.Size(58, 25);
            this.TailsCheckbox.TabIndex = 15;
            this.TailsCheckbox.Text = "Tails";
            this.TailsCheckbox.UseVisualStyleBackColor = true;
            // 
            // SpeedTrackbar
            // 
            this.SpeedTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SpeedTrackbar.AutoSize = false;
            this.SpeedTrackbar.Location = new System.Drawing.Point(802, 619);
            this.SpeedTrackbar.Maximum = 25;
            this.SpeedTrackbar.Name = "SpeedTrackbar";
            this.SpeedTrackbar.Size = new System.Drawing.Size(158, 30);
            this.SpeedTrackbar.TabIndex = 14;
            this.SpeedTrackbar.Value = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(468, 596);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Align";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // skglControl1
            // 
            this.skglControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skglControl1.BackColor = System.Drawing.Color.Black;
            this.skglControl1.Location = new System.Drawing.Point(15, 12);
            this.skglControl1.Name = "skglControl1";
            this.skglControl1.Size = new System.Drawing.Size(1157, 578);
            this.skglControl1.TabIndex = 14;
            this.skglControl1.VSync = false;
            this.skglControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl1_PaintSurface);
            this.skglControl1.SizeChanged += new System.EventHandler(this.skglControl1_SizeChanged);
            // 
            // DirectionCheckbox
            // 
            this.DirectionCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DirectionCheckbox.AutoSize = true;
            this.DirectionCheckbox.Checked = true;
            this.DirectionCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DirectionCheckbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectionCheckbox.Location = new System.Drawing.Point(1067, 600);
            this.DirectionCheckbox.Name = "DirectionCheckbox";
            this.DirectionCheckbox.Size = new System.Drawing.Size(92, 25);
            this.DirectionCheckbox.TabIndex = 23;
            this.DirectionCheckbox.Text = "Direction";
            this.DirectionCheckbox.UseVisualStyleBackColor = true;
            // 
            // VisionTrackbar
            // 
            this.VisionTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VisionTrackbar.AutoSize = false;
            this.VisionTrackbar.Location = new System.Drawing.Point(114, 619);
            this.VisionTrackbar.Maximum = 22;
            this.VisionTrackbar.Minimum = 3;
            this.VisionTrackbar.Name = "VisionTrackbar";
            this.VisionTrackbar.Size = new System.Drawing.Size(112, 30);
            this.VisionTrackbar.TabIndex = 24;
            this.VisionTrackbar.Value = 10;
            this.VisionTrackbar.Scroll += new System.EventHandler(this.VisionTrackbar_Scroll);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(114, 595);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 23);
            this.label2.TabIndex = 25;
            this.label2.Text = "Vision";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RandomCheckbox
            // 
            this.RandomCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RandomCheckbox.AutoSize = true;
            this.RandomCheckbox.Checked = true;
            this.RandomCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RandomCheckbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RandomCheckbox.Location = new System.Drawing.Point(15, 595);
            this.RandomCheckbox.Name = "RandomCheckbox";
            this.RandomCheckbox.Size = new System.Drawing.Size(88, 25);
            this.RandomCheckbox.TabIndex = 26;
            this.RandomCheckbox.Text = "Random";
            this.RandomCheckbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.RandomCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.VisionTrackbar);
            this.Controls.Add(this.DirectionCheckbox);
            this.Controls.Add(this.RangeCheckbox);
            this.Controls.Add(this.FpsLabel);
            this.Controls.Add(this.TailsCheckbox);
            this.Controls.Add(this.skglControl1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SpeedTrackbar);
            this.Controls.Add(this.PredatorCountNud);
            this.Controls.Add(this.AttractTrackbar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AvoidTrackbar);
            this.Controls.Add(this.BoidCountNud);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AlignTrackbar);
            this.Controls.Add(this.label5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boids (with OpenGL) - C# Data Visualization";
            ((System.ComponentModel.ISupportInitialize)(this.AttractTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvoidTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlignTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PredatorCountNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoidCountNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VisionTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar AttractTrackbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar AvoidTrackbar;
        private System.Windows.Forms.TrackBar AlignTrackbar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown BoidCountNud;
        private System.Windows.Forms.CheckBox TailsCheckbox;
        private System.Windows.Forms.TrackBar SpeedTrackbar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown PredatorCountNud;
        private System.Windows.Forms.CheckBox RangeCheckbox;
        private System.Windows.Forms.Label FpsLabel;
        private System.Windows.Forms.CheckBox DirectionCheckbox;
        private System.Windows.Forms.TrackBar VisionTrackbar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox RandomCheckbox;
    }
}

