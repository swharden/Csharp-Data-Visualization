namespace GameOfLife
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SizeNud = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.RunCheckbox = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GridCheckbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DelayNud = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.DensityNud = new System.Windows.Forms.NumericUpDown();
            this.GliderButton = new System.Windows.Forms.Button();
            this.RowButton = new System.Windows.Forms.Button();
            this.SpaceshipButton = new System.Windows.Forms.Button();
            this.GunButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DensityNud)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(864, 405);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            // 
            // SizeNud
            // 
            this.SizeNud.Location = new System.Drawing.Point(100, 7);
            this.SizeNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SizeNud.Name = "SizeNud";
            this.SizeNud.Size = new System.Drawing.Size(53, 20);
            this.SizeNud.TabIndex = 1;
            this.SizeNud.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SizeNud.ValueChanged += new System.EventHandler(this.SizeNud_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Square size (px):";
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(545, 5);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(56, 23);
            this.ResetButton.TabIndex = 5;
            this.ResetButton.Text = "Random";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // RunCheckbox
            // 
            this.RunCheckbox.AutoSize = true;
            this.RunCheckbox.Checked = true;
            this.RunCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RunCheckbox.Location = new System.Drawing.Point(487, 10);
            this.RunCheckbox.Name = "RunCheckbox";
            this.RunCheckbox.Size = new System.Drawing.Size(46, 17);
            this.RunCheckbox.TabIndex = 6;
            this.RunCheckbox.Text = "Run";
            this.RunCheckbox.UseVisualStyleBackColor = true;
            this.RunCheckbox.CheckedChanged += new System.EventHandler(this.RunCheckbox_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GridCheckbox
            // 
            this.GridCheckbox.AutoSize = true;
            this.GridCheckbox.Checked = true;
            this.GridCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GridCheckbox.Location = new System.Drawing.Point(429, 10);
            this.GridCheckbox.Name = "GridCheckbox";
            this.GridCheckbox.Size = new System.Drawing.Size(45, 17);
            this.GridCheckbox.TabIndex = 7;
            this.GridCheckbox.Text = "Grid";
            this.GridCheckbox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Delay (ms):";
            // 
            // DelayNud
            // 
            this.DelayNud.Location = new System.Drawing.Point(231, 7);
            this.DelayNud.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.DelayNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DelayNud.Name = "DelayNud";
            this.DelayNud.Size = new System.Drawing.Size(53, 20);
            this.DelayNud.TabIndex = 9;
            this.DelayNud.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.DelayNud.ValueChanged += new System.EventHandler(this.DelayNud_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Density (%):";
            // 
            // DensityNud
            // 
            this.DensityNud.Location = new System.Drawing.Point(357, 7);
            this.DensityNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DensityNud.Name = "DensityNud";
            this.DensityNud.Size = new System.Drawing.Size(53, 20);
            this.DensityNud.TabIndex = 11;
            this.DensityNud.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.DensityNud.ValueChanged += new System.EventHandler(this.DensityNud_ValueChanged);
            // 
            // GliderButton
            // 
            this.GliderButton.Location = new System.Drawing.Point(607, 5);
            this.GliderButton.Name = "GliderButton";
            this.GliderButton.Size = new System.Drawing.Size(56, 23);
            this.GliderButton.TabIndex = 12;
            this.GliderButton.Text = "Glider";
            this.GliderButton.UseVisualStyleBackColor = true;
            this.GliderButton.Click += new System.EventHandler(this.GliderButton_Click);
            // 
            // RowButton
            // 
            this.RowButton.Location = new System.Drawing.Point(741, 5);
            this.RowButton.Name = "RowButton";
            this.RowButton.Size = new System.Drawing.Size(56, 23);
            this.RowButton.TabIndex = 12;
            this.RowButton.Text = "Row";
            this.RowButton.UseVisualStyleBackColor = true;
            this.RowButton.Click += new System.EventHandler(this.RowButton_Click);
            // 
            // SpaceshipButton
            // 
            this.SpaceshipButton.Location = new System.Drawing.Point(669, 5);
            this.SpaceshipButton.Name = "SpaceshipButton";
            this.SpaceshipButton.Size = new System.Drawing.Size(66, 23);
            this.SpaceshipButton.TabIndex = 13;
            this.SpaceshipButton.Text = "Spaceship";
            this.SpaceshipButton.UseVisualStyleBackColor = true;
            this.SpaceshipButton.Click += new System.EventHandler(this.SpaceshipButton_Click);
            // 
            // GunButton
            // 
            this.GunButton.Location = new System.Drawing.Point(803, 5);
            this.GunButton.Name = "GunButton";
            this.GunButton.Size = new System.Drawing.Size(74, 23);
            this.GunButton.TabIndex = 14;
            this.GunButton.Text = "Glider Gun";
            this.GunButton.UseVisualStyleBackColor = true;
            this.GunButton.Click += new System.EventHandler(this.GunButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 450);
            this.Controls.Add(this.GunButton);
            this.Controls.Add(this.SpaceshipButton);
            this.Controls.Add(this.RowButton);
            this.Controls.Add(this.GliderButton);
            this.Controls.Add(this.DensityNud);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DelayNud);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GridCheckbox);
            this.Controls.Add(this.RunCheckbox);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SizeNud);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game of Life - C# Data Visualization";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DensityNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown SizeNud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.CheckBox RunCheckbox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox GridCheckbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown DelayNud;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown DensityNud;
        private System.Windows.Forms.Button GliderButton;
        private System.Windows.Forms.Button RowButton;
        private System.Windows.Forms.Button SpaceshipButton;
        private System.Windows.Forms.Button GunButton;
    }
}

