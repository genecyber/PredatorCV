namespace _ExperimentCV
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
            this.desktop = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.capturedImageBox = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.desktop)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 367);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(597, 20);
            this.textBox1.TabIndex = 2;
            // 
            // desktop
            // 
            this.desktop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.desktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.desktop.Location = new System.Drawing.Point(0, 0);
            this.desktop.Name = "desktop";
            this.desktop.Size = new System.Drawing.Size(401, 361);
            this.desktop.TabIndex = 3;
            this.desktop.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.capturedImageBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.desktop);
            this.splitContainer1.Size = new System.Drawing.Size(621, 361);
            this.splitContainer1.SplitterDistance = 216;
            this.splitContainer1.TabIndex = 4;
            // 
            // capturedImageBox
            // 
            this.capturedImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.capturedImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capturedImageBox.Location = new System.Drawing.Point(0, 0);
            this.capturedImageBox.Name = "capturedImageBox";
            this.capturedImageBox.Size = new System.Drawing.Size(216, 361);
            this.capturedImageBox.TabIndex = 1;
            this.capturedImageBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 399);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.desktop)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.capturedImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox capturedImageBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox desktop;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}

