namespace ChessCS
{
    partial class MainForm
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
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.getFENBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(531, 21);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(319, 241);
            this.logTextBox.TabIndex = 0;
            // 
            // getFENBtn
            // 
            this.getFENBtn.Location = new System.Drawing.Point(775, 278);
            this.getFENBtn.Name = "getFENBtn";
            this.getFENBtn.Size = new System.Drawing.Size(75, 23);
            this.getFENBtn.TabIndex = 1;
            this.getFENBtn.Text = "getFEN";
            this.getFENBtn.UseVisualStyleBackColor = true;
            this.getFENBtn.Click += new System.EventHandler(this.getFENBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 515);
            this.Controls.Add(this.getFENBtn);
            this.Controls.Add(this.logTextBox);
            this.Name = "MainForm";
            this.Text = "Chess CS v1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Button getFENBtn;
    }
}

