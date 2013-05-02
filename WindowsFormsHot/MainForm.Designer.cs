namespace WindowsFormsHot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bnStart
            // 
            this.bnStart.Location = new System.Drawing.Point(12, 12);
            this.bnStart.Name = "bnStart";
            this.bnStart.Size = new System.Drawing.Size(463, 23);
            this.bnStart.TabIndex = 0;
            this.bnStart.Text = "Start";
            this.bnStart.UseVisualStyleBackColor = true;
            this.bnStart.Click += new System.EventHandler(this.bnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 378);
            this.Controls.Add(this.bnStart);
            this.Name = "Form1";
            this.Text = "convRse Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bnStart;
    }
}

