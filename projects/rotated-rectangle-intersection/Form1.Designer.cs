namespace RotatedRectangleDemo;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trackbarAngle = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarAngle)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // skglControl1
            // 
            this.skglControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skglControl1.BackColor = System.Drawing.Color.Black;
            this.skglControl1.Location = new System.Drawing.Point(12, 77);
            this.skglControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.skglControl1.Name = "skglControl1";
            this.skglControl1.Size = new System.Drawing.Size(837, 531);
            this.skglControl1.TabIndex = 0;
            this.skglControl1.VSync = true;
            this.skglControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl1_PaintSurface);
            this.skglControl1.SizeChanged += new System.EventHandler(this.skglControl1_SizeChanged);
            this.skglControl1.MouseLeave += new System.EventHandler(this.skglControl1_MouseLeave);
            this.skglControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.skglControl1_MouseMove);
            // 
            // nudX
            // 
            this.nudX.Location = new System.Drawing.Point(36, 22);
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(73, 23);
            this.nudX.TabIndex = 1;
            this.nudX.ValueChanged += new System.EventHandler(this.nudX_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 59);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotation Center";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y =";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "X =";
            // 
            // nudY
            // 
            this.nudY.Location = new System.Drawing.Point(159, 22);
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(73, 23);
            this.nudY.TabIndex = 3;
            this.nudY.ValueChanged += new System.EventHandler(this.nudY_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.trackbarAngle);
            this.groupBox2.Location = new System.Drawing.Point(385, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 59);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rotation Angle";
            // 
            // trackbarAngle
            // 
            this.trackbarAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackbarAngle.Location = new System.Drawing.Point(3, 19);
            this.trackbarAngle.Maximum = 100;
            this.trackbarAngle.Name = "trackbarAngle";
            this.trackbarAngle.Size = new System.Drawing.Size(458, 37);
            this.trackbarAngle.TabIndex = 0;
            this.trackbarAngle.Scroll += new System.EventHandler(this.trackbarAngle_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbText);
            this.groupBox3.Location = new System.Drawing.Point(266, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(113, 59);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(6, 22);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(100, 23);
            this.tbText.TabIndex = 0;
            this.tbText.Text = "Test";
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 620);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.skglControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarAngle)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
    private NumericUpDown nudX;
    private GroupBox groupBox1;
    private Label label2;
    private NumericUpDown nudY;
    private Label label1;
    private GroupBox groupBox2;
    private TrackBar trackbarAngle;
    private GroupBox groupBox3;
    private TextBox tbText;
}
