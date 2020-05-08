namespace Starfield.WinFormsSkiaSharp
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
            this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
            this.rb500 = new System.Windows.Forms.RadioButton();
            this.rb100k = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAlpha = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // skglControl1
            // 
            this.skglControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skglControl1.BackColor = System.Drawing.Color.Black;
            this.skglControl1.Location = new System.Drawing.Point(12, 58);
            this.skglControl1.Name = "skglControl1";
            this.skglControl1.Size = new System.Drawing.Size(600, 400);
            this.skglControl1.TabIndex = 0;
            this.skglControl1.VSync = false;
            this.skglControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl1_PaintSurface);
            // 
            // rb500
            // 
            this.rb500.AutoSize = true;
            this.rb500.Checked = true;
            this.rb500.Location = new System.Drawing.Point(12, 12);
            this.rb500.Name = "rb500";
            this.rb500.Size = new System.Drawing.Size(68, 17);
            this.rb500.TabIndex = 1;
            this.rb500.TabStop = true;
            this.rb500.Text = "500 stars";
            this.rb500.UseVisualStyleBackColor = true;
            this.rb500.CheckedChanged += new System.EventHandler(this.rb500_CheckedChanged);
            // 
            // rb100k
            // 
            this.rb100k.AutoSize = true;
            this.rb100k.Location = new System.Drawing.Point(12, 35);
            this.rb100k.Name = "rb100k";
            this.rb100k.Size = new System.Drawing.Size(89, 17);
            this.rb100k.TabIndex = 2;
            this.rb100k.Text = "100,000 stars";
            this.rb100k.UseVisualStyleBackColor = true;
            this.rb100k.CheckedChanged += new System.EventHandler(this.rb100k_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(555, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Alpha";
            // 
            // lblAlpha
            // 
            this.lblAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlpha.AutoSize = true;
            this.lblAlpha.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlpha.Location = new System.Drawing.Point(546, 22);
            this.lblAlpha.Name = "lblAlpha";
            this.lblAlpha.Size = new System.Drawing.Size(74, 32);
            this.lblAlpha.TabIndex = 4;
            this.lblAlpha.Text = "100%";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(107, 7);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(433, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 469);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAlpha);
            this.Controls.Add(this.rb100k);
            this.Controls.Add(this.rb500);
            this.Controls.Add(this.skglControl1);
            this.Name = "Form1";
            this.Text = "Starfield with SkiaSharp and OpenGL";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
        private System.Windows.Forms.RadioButton rb500;
        private System.Windows.Forms.RadioButton rb100k;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAlpha;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Timer timer1;
    }
}

