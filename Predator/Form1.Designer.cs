namespace PredatorCV
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.source = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.detector = new System.Windows.Forms.ComboBox();
            this.capturedImageBox = new Emgu.CV.UI.ImageBox();
            this.fps = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 565);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(897, 20);
            this.textBox1.TabIndex = 2;
            // 
            // source
            // 
            this.source.FormattingEnabled = true;
            this.source.Items.AddRange(new object[] {
            "Camera",
            "Desktop",
            "Image",
            "iOS",
            "Android"});
            this.source.Location = new System.Drawing.Point(12, 2);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(204, 21);
            this.source.TabIndex = 5;
            this.source.SelectedIndexChanged += new System.EventHandler(this.SourceSelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.capturedImageBox);
            this.panel1.Location = new System.Drawing.Point(15, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 530);
            this.panel1.TabIndex = 6;
            // 
            // detector
            // 
            this.detector.FormattingEnabled = true;
            this.detector.Items.AddRange(new object[] {
            "Circle",
            "Contour",
            "Eigen",
            "Face",
            "Surf",
            "Text",
            "Triangle",
            "2D"});
            this.detector.Location = new System.Drawing.Point(222, 2);
            this.detector.Name = "detector";
            this.detector.Size = new System.Drawing.Size(204, 21);
            this.detector.TabIndex = 7;
            this.detector.SelectedIndexChanged += new System.EventHandler(this.detector_SelectedIndexChanged);
            // 
            // capturedImageBox
            // 
            this.capturedImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.capturedImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capturedImageBox.Location = new System.Drawing.Point(0, 0);
            this.capturedImageBox.Name = "capturedImageBox";
            this.capturedImageBox.Size = new System.Drawing.Size(894, 530);
            this.capturedImageBox.TabIndex = 2;
            this.capturedImageBox.TabStop = false;
            // 
            // fps
            // 
            this.fps.Location = new System.Drawing.Point(433, 2);
            this.fps.Name = "fps";
            this.fps.Size = new System.Drawing.Size(100, 20);
            this.fps.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 588);
            this.Controls.Add(this.fps);
            this.Controls.Add(this.detector);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.source);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox source;
        private System.Windows.Forms.Panel panel1;
        private Emgu.CV.UI.ImageBox capturedImageBox;
        private System.Windows.Forms.ComboBox detector;
        private System.Windows.Forms.TextBox fps;

    }
}

