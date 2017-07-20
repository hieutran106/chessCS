namespace ChessCS
{
    partial class AddPieceForm
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
            this.addBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pawnRBtn = new System.Windows.Forms.RadioButton();
            this.rookRBtn = new System.Windows.Forms.RadioButton();
            this.knightRBtn = new System.Windows.Forms.RadioButton();
            this.bishopRBtn = new System.Windows.Forms.RadioButton();
            this.queenRBtn = new System.Windows.Forms.RadioButton();
            this.kingRBtn = new System.Windows.Forms.RadioButton();
            this.blackRBtn = new System.Windows.Forms.RadioButton();
            this.whiteRBtn = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(210, 288);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 0;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(303, 288);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kingRBtn);
            this.groupBox1.Controls.Add(this.queenRBtn);
            this.groupBox1.Controls.Add(this.bishopRBtn);
            this.groupBox1.Controls.Add(this.knightRBtn);
            this.groupBox1.Controls.Add(this.rookRBtn);
            this.groupBox1.Controls.Add(this.pawnRBtn);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 238);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Piece";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.whiteRBtn);
            this.groupBox2.Controls.Add(this.blackRBtn);
            this.groupBox2.Location = new System.Drawing.Point(225, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 238);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color";
            // 
            // pawnRBtn
            // 
            this.pawnRBtn.AutoSize = true;
            this.pawnRBtn.Checked = true;
            this.pawnRBtn.Location = new System.Drawing.Point(16, 20);
            this.pawnRBtn.Name = "pawnRBtn";
            this.pawnRBtn.Size = new System.Drawing.Size(52, 17);
            this.pawnRBtn.TabIndex = 0;
            this.pawnRBtn.TabStop = true;
            this.pawnRBtn.Text = "Pawn";
            this.pawnRBtn.UseVisualStyleBackColor = true;
            this.pawnRBtn.CheckedChanged += new System.EventHandler(this.pieceType_CheckedChanged);
            // 
            // rookRBtn
            // 
            this.rookRBtn.AutoSize = true;
            this.rookRBtn.Location = new System.Drawing.Point(16, 43);
            this.rookRBtn.Name = "rookRBtn";
            this.rookRBtn.Size = new System.Drawing.Size(51, 17);
            this.rookRBtn.TabIndex = 1;
            this.rookRBtn.Text = "Rook";
            this.rookRBtn.UseVisualStyleBackColor = true;
            this.rookRBtn.CheckedChanged += new System.EventHandler(this.pieceType_CheckedChanged);
            // 
            // knightRBtn
            // 
            this.knightRBtn.AutoSize = true;
            this.knightRBtn.Location = new System.Drawing.Point(16, 66);
            this.knightRBtn.Name = "knightRBtn";
            this.knightRBtn.Size = new System.Drawing.Size(55, 17);
            this.knightRBtn.TabIndex = 2;
            this.knightRBtn.Text = "Knight";
            this.knightRBtn.UseVisualStyleBackColor = true;
            this.knightRBtn.CheckedChanged += new System.EventHandler(this.pieceType_CheckedChanged);
            // 
            // bishopRBtn
            // 
            this.bishopRBtn.AutoSize = true;
            this.bishopRBtn.Location = new System.Drawing.Point(16, 89);
            this.bishopRBtn.Name = "bishopRBtn";
            this.bishopRBtn.Size = new System.Drawing.Size(57, 17);
            this.bishopRBtn.TabIndex = 3;
            this.bishopRBtn.Text = "Bishop";
            this.bishopRBtn.UseVisualStyleBackColor = true;
            this.bishopRBtn.CheckedChanged += new System.EventHandler(this.pieceType_CheckedChanged);
            // 
            // queenRBtn
            // 
            this.queenRBtn.AutoSize = true;
            this.queenRBtn.Location = new System.Drawing.Point(16, 112);
            this.queenRBtn.Name = "queenRBtn";
            this.queenRBtn.Size = new System.Drawing.Size(57, 17);
            this.queenRBtn.TabIndex = 4;
            this.queenRBtn.Text = "Queen";
            this.queenRBtn.UseVisualStyleBackColor = true;
            this.queenRBtn.CheckedChanged += new System.EventHandler(this.pieceType_CheckedChanged);
            // 
            // kingRBtn
            // 
            this.kingRBtn.AutoSize = true;
            this.kingRBtn.Location = new System.Drawing.Point(16, 135);
            this.kingRBtn.Name = "kingRBtn";
            this.kingRBtn.Size = new System.Drawing.Size(46, 17);
            this.kingRBtn.TabIndex = 5;
            this.kingRBtn.Text = "King";
            this.kingRBtn.UseVisualStyleBackColor = true;
            this.kingRBtn.CheckedChanged += new System.EventHandler(this.pieceType_CheckedChanged);
            // 
            // blackRBtn
            // 
            this.blackRBtn.AutoSize = true;
            this.blackRBtn.Checked = true;
            this.blackRBtn.Location = new System.Drawing.Point(25, 19);
            this.blackRBtn.Name = "blackRBtn";
            this.blackRBtn.Size = new System.Drawing.Size(52, 17);
            this.blackRBtn.TabIndex = 6;
            this.blackRBtn.TabStop = true;
            this.blackRBtn.Text = "Black";
            this.blackRBtn.UseVisualStyleBackColor = true;
            this.blackRBtn.CheckedChanged += new System.EventHandler(this.color_CheckedChanged);
            // 
            // whiteRBtn
            // 
            this.whiteRBtn.AutoSize = true;
            this.whiteRBtn.Location = new System.Drawing.Point(25, 43);
            this.whiteRBtn.Name = "whiteRBtn";
            this.whiteRBtn.Size = new System.Drawing.Size(53, 17);
            this.whiteRBtn.TabIndex = 7;
            this.whiteRBtn.Text = "White";
            this.whiteRBtn.UseVisualStyleBackColor = true;
            this.whiteRBtn.CheckedChanged += new System.EventHandler(this.color_CheckedChanged);
            // 
            // AddPieceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 323);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.addBtn);
            this.Name = "AddPieceForm";
            this.Text = "Add a chess piece";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton pawnRBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton kingRBtn;
        private System.Windows.Forms.RadioButton queenRBtn;
        private System.Windows.Forms.RadioButton bishopRBtn;
        private System.Windows.Forms.RadioButton knightRBtn;
        private System.Windows.Forms.RadioButton rookRBtn;
        private System.Windows.Forms.RadioButton whiteRBtn;
        private System.Windows.Forms.RadioButton blackRBtn;
    }
}