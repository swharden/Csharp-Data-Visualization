namespace FormsLife
{
    partial class Form2
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
            this.skControl1 = new SkiaSharp.Views.Desktop.SKControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // skControl1
            // 
            this.skControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.skControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skControl1.Location = new System.Drawing.Point(0, 0);
            this.skControl1.Name = "skControl1";
            this.skControl1.Size = new System.Drawing.Size(800, 450);
            this.skControl1.TabIndex = 0;
            this.skControl1.Text = "skControl1";
            this.skControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.skControl1_PaintSurface);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.skControl1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private SkiaSharp.Views.Desktop.SKControl skControl1;
        private System.Windows.Forms.Timer timer1;
    }
}