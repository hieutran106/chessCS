﻿namespace ChessCS
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
            this.infoLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coordinateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugMODEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.thinkLabel = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.moveHistoryTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(531, 58);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(319, 68);
            this.logTextBox.TabIndex = 0;
            // 
            // getFENBtn
            // 
            this.getFENBtn.Location = new System.Drawing.Point(775, 142);
            this.getFENBtn.Name = "getFENBtn";
            this.getFENBtn.Size = new System.Drawing.Size(75, 23);
            this.getFENBtn.TabIndex = 1;
            this.getFENBtn.Text = "getFEN";
            this.getFENBtn.UseVisualStyleBackColor = true;
            this.getFENBtn.Click += new System.EventHandler(this.getFENBtn_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(6, 197);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(40, 13);
            this.infoLabel.TabIndex = 2;
            this.infoLabel.Text = "Match:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.displayToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(862, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.coordinateToolStripMenuItem,
            this.enableAIToolStripMenuItem,
            this.debugMODEToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.displayToolStripMenuItem.Text = "Config";
            // 
            // coordinateToolStripMenuItem
            // 
            this.coordinateToolStripMenuItem.CheckOnClick = true;
            this.coordinateToolStripMenuItem.Name = "coordinateToolStripMenuItem";
            this.coordinateToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.coordinateToolStripMenuItem.Text = "Show Coordinate";
            this.coordinateToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_CheckedChanged);
            // 
            // enableAIToolStripMenuItem
            // 
            this.enableAIToolStripMenuItem.Checked = true;
            this.enableAIToolStripMenuItem.CheckOnClick = true;
            this.enableAIToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableAIToolStripMenuItem.Name = "enableAIToolStripMenuItem";
            this.enableAIToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.enableAIToolStripMenuItem.Text = "Enable AI";
            this.enableAIToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_CheckedChanged);
            // 
            // debugMODEToolStripMenuItem
            // 
            this.debugMODEToolStripMenuItem.CheckOnClick = true;
            this.debugMODEToolStripMenuItem.Name = "debugMODEToolStripMenuItem";
            this.debugMODEToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.debugMODEToolStripMenuItem.Text = "Debug MODE";
            this.debugMODEToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem_CheckedChanged);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.thinkLabel);
            this.groupBox1.Controls.Add(this.backBtn);
            this.groupBox1.Controls.Add(this.moveHistoryTextBox);
            this.groupBox1.Controls.Add(this.infoLabel);
            this.groupBox1.Location = new System.Drawing.Point(531, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 250);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Moves";
            // 
            // thinkLabel
            // 
            this.thinkLabel.AutoSize = true;
            this.thinkLabel.Location = new System.Drawing.Point(9, 214);
            this.thinkLabel.Name = "thinkLabel";
            this.thinkLabel.Size = new System.Drawing.Size(0, 13);
            this.thinkLabel.TabIndex = 4;
            // 
            // backBtn
            // 
            this.backBtn.Location = new System.Drawing.Point(7, 161);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 23);
            this.backBtn.TabIndex = 3;
            this.backBtn.Text = "Back";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // moveHistoryTextBox
            // 
            this.moveHistoryTextBox.Location = new System.Drawing.Point(7, 20);
            this.moveHistoryTextBox.Name = "moveHistoryTextBox";
            this.moveHistoryTextBox.Size = new System.Drawing.Size(306, 134);
            this.moveHistoryTextBox.TabIndex = 0;
            this.moveHistoryTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(862, 515);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.getFENBtn);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Chess CS v1.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Button getFENBtn;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coordinateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox moveHistoryTextBox;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.Label thinkLabel;
        private System.Windows.Forms.ToolStripMenuItem enableAIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugMODEToolStripMenuItem;
    }
}

