namespace drawing2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.panel_dataView = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_axis_x = new System.Windows.Forms.Label();
            this.lbl_sweep = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_zoom_x_out = new System.Windows.Forms.Button();
            this.btn_zoom_x_in = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.lbl_axis_y = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_zoom_y_in = new System.Windows.Forms.Button();
            this.btn_zoom_y_out = new System.Windows.Forms.Button();
            this.pb_graph = new System.Windows.Forms.PictureBox();
            this.pb_markers = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_dataView.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_markers)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_dataView, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(939, 611);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 84);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "http://www.GitHub.com/SWHarden";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "C# Data Visualization Demo";
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.Location = new System.Drawing.Point(9, 6);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(192, 32);
            this.lbl_title.TabIndex = 3;
            this.lbl_title.Text = "Table Layout";
            // 
            // panel_dataView
            // 
            this.panel_dataView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_dataView.Controls.Add(this.tableLayoutPanel2);
            this.panel_dataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_dataView.Location = new System.Drawing.Point(3, 93);
            this.panel_dataView.Name = "panel_dataView";
            this.panel_dataView.Size = new System.Drawing.Size(933, 515);
            this.panel_dataView.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pb_markers, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(929, 511);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Controls.Add(this.hScrollBar1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 486);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(929, 25);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar1.Location = new System.Drawing.Point(75, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(829, 25);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.lbl_axis_x, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_sweep, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 461);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(929, 25);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // lbl_axis_x
            // 
            this.lbl_axis_x.AutoSize = true;
            this.lbl_axis_x.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_axis_x.Location = new System.Drawing.Point(278, 5);
            this.lbl_axis_x.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_axis_x.Name = "lbl_axis_x";
            this.lbl_axis_x.Size = new System.Drawing.Size(371, 20);
            this.lbl_axis_x.TabIndex = 0;
            this.lbl_axis_x.Text = "Time (s)";
            this.lbl_axis_x.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_sweep
            // 
            this.lbl_sweep.AutoSize = true;
            this.lbl_sweep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_sweep.Location = new System.Drawing.Point(649, 5);
            this.lbl_sweep.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lbl_sweep.Name = "lbl_sweep";
            this.lbl_sweep.Size = new System.Drawing.Size(280, 20);
            this.lbl_sweep.TabIndex = 1;
            this.lbl_sweep.Text = "Sweep: 10 Visisble: 1 of 16";
            this.lbl_sweep.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_zoom_x_out);
            this.flowLayoutPanel1.Controls.Add(this.btn_zoom_x_in);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(278, 25);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn_zoom_x_out
            // 
            this.btn_zoom_x_out.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_zoom_x_out.Font = new System.Drawing.Font("Wingdings 3", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_zoom_x_out.Location = new System.Drawing.Point(0, 0);
            this.btn_zoom_x_out.Margin = new System.Windows.Forms.Padding(0);
            this.btn_zoom_x_out.Name = "btn_zoom_x_out";
            this.btn_zoom_x_out.Size = new System.Drawing.Size(25, 25);
            this.btn_zoom_x_out.TabIndex = 8;
            this.btn_zoom_x_out.Text = "t";
            this.btn_zoom_x_out.UseVisualStyleBackColor = true;
            // 
            // btn_zoom_x_in
            // 
            this.btn_zoom_x_in.Font = new System.Drawing.Font("Wingdings 3", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_zoom_x_in.Location = new System.Drawing.Point(25, 0);
            this.btn_zoom_x_in.Margin = new System.Windows.Forms.Padding(0);
            this.btn_zoom_x_in.Name = "btn_zoom_x_in";
            this.btn_zoom_x_in.Size = new System.Drawing.Size(25, 25);
            this.btn_zoom_x_in.TabIndex = 10;
            this.btn_zoom_x_in.Text = "u";
            this.btn_zoom_x_in.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.Controls.Add(this.vScrollBar1, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbl_axis_y, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.pb_graph, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(929, 436);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar1.Location = new System.Drawing.Point(904, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(25, 436);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // lbl_axis_y
            // 
            this.lbl_axis_y.AutoSize = true;
            this.lbl_axis_y.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbl_axis_y.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_axis_y.Location = new System.Drawing.Point(0, 0);
            this.lbl_axis_y.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_axis_y.Name = "lbl_axis_y";
            this.lbl_axis_y.Size = new System.Drawing.Size(50, 436);
            this.lbl_axis_y.TabIndex = 1;
            this.lbl_axis_y.Text = "pA";
            this.lbl_axis_y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.btn_zoom_y_in, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btn_zoom_y_out, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(50, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(25, 436);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // btn_zoom_y_in
            // 
            this.btn_zoom_y_in.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_zoom_y_in.Font = new System.Drawing.Font("Wingdings 3", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_zoom_y_in.Location = new System.Drawing.Point(0, 193);
            this.btn_zoom_y_in.Margin = new System.Windows.Forms.Padding(0);
            this.btn_zoom_y_in.Name = "btn_zoom_y_in";
            this.btn_zoom_y_in.Size = new System.Drawing.Size(25, 25);
            this.btn_zoom_y_in.TabIndex = 6;
            this.btn_zoom_y_in.Text = "p";
            this.btn_zoom_y_in.UseVisualStyleBackColor = true;
            // 
            // btn_zoom_y_out
            // 
            this.btn_zoom_y_out.Font = new System.Drawing.Font("Wingdings 3", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btn_zoom_y_out.Location = new System.Drawing.Point(0, 218);
            this.btn_zoom_y_out.Margin = new System.Windows.Forms.Padding(0);
            this.btn_zoom_y_out.Name = "btn_zoom_y_out";
            this.btn_zoom_y_out.Size = new System.Drawing.Size(25, 25);
            this.btn_zoom_y_out.TabIndex = 7;
            this.btn_zoom_y_out.Text = "q";
            this.btn_zoom_y_out.UseVisualStyleBackColor = true;
            // 
            // pb_graph
            // 
            this.pb_graph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pb_graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_graph.Location = new System.Drawing.Point(75, 0);
            this.pb_graph.Margin = new System.Windows.Forms.Padding(0);
            this.pb_graph.Name = "pb_graph";
            this.pb_graph.Size = new System.Drawing.Size(829, 436);
            this.pb_graph.TabIndex = 3;
            this.pb_graph.TabStop = false;
            // 
            // pb_markers
            // 
            this.pb_markers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pb_markers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_markers.Location = new System.Drawing.Point(0, 0);
            this.pb_markers.Margin = new System.Windows.Forms.Padding(0);
            this.pb_markers.Name = "pb_markers";
            this.pb_markers.Size = new System.Drawing.Size(929, 25);
            this.pb_markers.TabIndex = 3;
            this.pb_markers.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 611);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_dataView.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_markers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_zoom_x_in;
        private System.Windows.Forms.Button btn_zoom_x_out;
        private System.Windows.Forms.Button btn_zoom_y_out;
        private System.Windows.Forms.Button btn_zoom_y_in;
        private System.Windows.Forms.Panel panel_dataView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lbl_axis_x;
        private System.Windows.Forms.Label lbl_sweep;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Label lbl_axis_y;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.PictureBox pb_graph;
        private System.Windows.Forms.PictureBox pb_markers;
    }
}

