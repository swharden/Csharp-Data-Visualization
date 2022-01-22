namespace SplineInterpolationViewer
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
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.tbSmoothness = new System.Windows.Forms.TrackBar();
            this.lblSmoothness = new System.Windows.Forms.Label();
            this.cbShowPoints = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbSmoothness)).BeginInit();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlot1.Location = new System.Drawing.Point(13, 56);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(616, 410);
            this.formsPlot1.TabIndex = 4;
            // 
            // tbSmoothness
            // 
            this.tbSmoothness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSmoothness.Location = new System.Drawing.Point(12, 27);
            this.tbSmoothness.Maximum = 500;
            this.tbSmoothness.Name = "tbSmoothness";
            this.tbSmoothness.Size = new System.Drawing.Size(618, 45);
            this.tbSmoothness.TabIndex = 5;
            this.tbSmoothness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSmoothness.Value = 100;
            this.tbSmoothness.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblSmoothness
            // 
            this.lblSmoothness.AutoSize = true;
            this.lblSmoothness.Location = new System.Drawing.Point(13, 9);
            this.lblSmoothness.Name = "lblSmoothness";
            this.lblSmoothness.Size = new System.Drawing.Size(53, 15);
            this.lblSmoothness.TabIndex = 6;
            this.lblSmoothness.Text = "message";
            // 
            // cbShowPoints
            // 
            this.cbShowPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShowPoints.AutoSize = true;
            this.cbShowPoints.Checked = true;
            this.cbShowPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowPoints.Location = new System.Drawing.Point(538, 8);
            this.cbShowPoints.Name = "cbShowPoints";
            this.cbShowPoints.Size = new System.Drawing.Size(91, 19);
            this.cbShowPoints.TabIndex = 7;
            this.cbShowPoints.Text = "Show Points";
            this.cbShowPoints.UseVisualStyleBackColor = true;
            this.cbShowPoints.CheckedChanged += new System.EventHandler(this.cbShowPoints_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 478);
            this.Controls.Add(this.cbShowPoints);
            this.Controls.Add(this.lblSmoothness);
            this.Controls.Add(this.tbSmoothness);
            this.Controls.Add(this.formsPlot1);
            this.Name = "Form1";
            this.Text = "Cubic Interpolation";
            ((System.ComponentModel.ISupportInitialize)(this.tbSmoothness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ScottPlot.FormsPlot formsPlot1;
        private TrackBar tbSmoothness;
        private Label lblSmoothness;
        private CheckBox cbShowPoints;
    }
}