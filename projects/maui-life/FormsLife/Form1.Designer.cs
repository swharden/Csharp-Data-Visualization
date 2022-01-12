namespace FormsLife
{
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
            this.components = new System.ComponentModel.Container();
            this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnFaster = new System.Windows.Forms.Button();
            this.btnSlower = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAddRandom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skglControl1
            // 
            this.skglControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skglControl1.BackColor = System.Drawing.Color.Black;
            this.skglControl1.Location = new System.Drawing.Point(13, 41);
            this.skglControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.skglControl1.Name = "skglControl1";
            this.skglControl1.Size = new System.Drawing.Size(774, 397);
            this.skglControl1.TabIndex = 0;
            this.skglControl1.VSync = true;
            this.skglControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl1_PaintSurface);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(183, 14);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(47, 19);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Run";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 48;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnFaster
            // 
            this.btnFaster.Location = new System.Drawing.Point(236, 11);
            this.btnFaster.Name = "btnFaster";
            this.btnFaster.Size = new System.Drawing.Size(54, 23);
            this.btnFaster.TabIndex = 4;
            this.btnFaster.Text = "Faster";
            this.btnFaster.UseVisualStyleBackColor = true;
            this.btnFaster.Click += new System.EventHandler(this.btnFaster_Click);
            // 
            // btnSlower
            // 
            this.btnSlower.Location = new System.Drawing.Point(296, 11);
            this.btnSlower.Name = "btnSlower";
            this.btnSlower.Size = new System.Drawing.Size(54, 23);
            this.btnSlower.TabIndex = 5;
            this.btnSlower.Text = "Slower";
            this.btnSlower.UseVisualStyleBackColor = true;
            this.btnSlower.Click += new System.EventHandler(this.btnSlower_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(13, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(51, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAddRandom
            // 
            this.btnAddRandom.Location = new System.Drawing.Point(70, 11);
            this.btnAddRandom.Name = "btnAddRandom";
            this.btnAddRandom.Size = new System.Drawing.Size(107, 23);
            this.btnAddRandom.TabIndex = 7;
            this.btnAddRandom.Text = "Add Random";
            this.btnAddRandom.UseVisualStyleBackColor = true;
            this.btnAddRandom.Click += new System.EventHandler(this.btnAddRandom_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(419, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(369, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Conway\'s Game of Life with Maui.Graphics";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddRandom);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSlower);
            this.Controls.Add(this.btnFaster);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.skglControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
        private CheckBox checkBox1;
        private System.Windows.Forms.Timer timer1;
        private Button btnFaster;
        private Button btnSlower;
        private Button btnClear;
        private Button btnAddRandom;
        private Label label1;
    }
}