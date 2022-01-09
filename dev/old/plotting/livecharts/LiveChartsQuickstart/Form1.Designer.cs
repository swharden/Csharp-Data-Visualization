namespace LiveChartsQuickstart
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
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.BarButton = new System.Windows.Forms.Button();
            this.ScatterButton = new System.Windows.Forms.Button();
            this.LineButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChart1.BackColor = System.Drawing.Color.White;
            this.cartesianChart1.Location = new System.Drawing.Point(12, 41);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(505, 279);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // BarButton
            // 
            this.BarButton.Location = new System.Drawing.Point(12, 12);
            this.BarButton.Name = "BarButton";
            this.BarButton.Size = new System.Drawing.Size(75, 23);
            this.BarButton.TabIndex = 1;
            this.BarButton.Text = "Bar Graph";
            this.BarButton.UseVisualStyleBackColor = true;
            this.BarButton.Click += new System.EventHandler(this.BarButton_Click);
            // 
            // ScatterButton
            // 
            this.ScatterButton.Location = new System.Drawing.Point(93, 12);
            this.ScatterButton.Name = "ScatterButton";
            this.ScatterButton.Size = new System.Drawing.Size(75, 23);
            this.ScatterButton.TabIndex = 2;
            this.ScatterButton.Text = "Scatter Plot";
            this.ScatterButton.UseVisualStyleBackColor = true;
            this.ScatterButton.Click += new System.EventHandler(this.ScatterButton_Click);
            // 
            // LineButton
            // 
            this.LineButton.Location = new System.Drawing.Point(174, 12);
            this.LineButton.Name = "LineButton";
            this.LineButton.Size = new System.Drawing.Size(75, 23);
            this.LineButton.TabIndex = 3;
            this.LineButton.Text = "Line Plot";
            this.LineButton.UseVisualStyleBackColor = true;
            this.LineButton.Click += new System.EventHandler(this.LineButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 332);
            this.Controls.Add(this.LineButton);
            this.Controls.Add(this.ScatterButton);
            this.Controls.Add(this.BarButton);
            this.Controls.Add(this.cartesianChart1);
            this.Name = "Form1";
            this.Text = "LiveCharts Quickstart";
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Button BarButton;
        private System.Windows.Forms.Button ScatterButton;
        private System.Windows.Forms.Button LineButton;
    }
}

