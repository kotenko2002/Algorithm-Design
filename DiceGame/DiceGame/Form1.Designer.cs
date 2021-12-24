
namespace DiceGame
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
            this.EnoughButton = new System.Windows.Forms.Button();
            this.MoreButton = new System.Windows.Forms.Button();
            this.ComputerButton = new System.Windows.Forms.Button();
            this.RestartButton = new System.Windows.Forms.Button();
            this.playerPointsLabel = new System.Windows.Forms.Label();
            this.ComputerPointsLabel = new System.Windows.Forms.Label();
            this.CubeBox1 = new System.Windows.Forms.PictureBox();
            this.CubeBox2 = new System.Windows.Forms.PictureBox();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.WinLabel = new System.Windows.Forms.Label();
            this.LoseLabel = new System.Windows.Forms.Label();
            this.DrawLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CubeBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CubeBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // EnoughButton
            // 
            this.EnoughButton.Font = new System.Drawing.Font("Wide Latin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EnoughButton.Location = new System.Drawing.Point(449, 670);
            this.EnoughButton.Name = "EnoughButton";
            this.EnoughButton.Size = new System.Drawing.Size(148, 57);
            this.EnoughButton.TabIndex = 0;
            this.EnoughButton.Text = "Хватит";
            this.EnoughButton.UseVisualStyleBackColor = true;
            this.EnoughButton.Click += new System.EventHandler(this.EnoughButton_Click);
            // 
            // MoreButton
            // 
            this.MoreButton.Font = new System.Drawing.Font("Wide Latin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MoreButton.Location = new System.Drawing.Point(449, 607);
            this.MoreButton.Name = "MoreButton";
            this.MoreButton.Size = new System.Drawing.Size(148, 57);
            this.MoreButton.TabIndex = 1;
            this.MoreButton.Text = "Бросить ещё раз";
            this.MoreButton.UseVisualStyleBackColor = true;
            this.MoreButton.Click += new System.EventHandler(this.MoreButton_Click);
            // 
            // ComputerButton
            // 
            this.ComputerButton.Font = new System.Drawing.Font("Wide Latin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComputerButton.Location = new System.Drawing.Point(449, 544);
            this.ComputerButton.Name = "ComputerButton";
            this.ComputerButton.Size = new System.Drawing.Size(148, 57);
            this.ComputerButton.TabIndex = 2;
            this.ComputerButton.Text = "Отдать кубике дедуле";
            this.ComputerButton.UseVisualStyleBackColor = true;
            this.ComputerButton.Click += new System.EventHandler(this.ComputerButton_Click);
            // 
            // RestartButton
            // 
            this.RestartButton.Enabled = false;
            this.RestartButton.Font = new System.Drawing.Font("Wide Latin", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RestartButton.Location = new System.Drawing.Point(449, 481);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(148, 57);
            this.RestartButton.TabIndex = 3;
            this.RestartButton.Text = "Играем";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // playerPointsLabel
            // 
            this.playerPointsLabel.AutoEllipsis = true;
            this.playerPointsLabel.AutoSize = true;
            this.playerPointsLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerPointsLabel.Location = new System.Drawing.Point(13, 670);
            this.playerPointsLabel.Name = "playerPointsLabel";
            this.playerPointsLabel.Size = new System.Drawing.Size(140, 25);
            this.playerPointsLabel.TabIndex = 4;
            this.playerPointsLabel.Text = "Очки игрока: 0";
            // 
            // ComputerPointsLabel
            // 
            this.ComputerPointsLabel.AutoSize = true;
            this.ComputerPointsLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComputerPointsLabel.Location = new System.Drawing.Point(12, 702);
            this.ComputerPointsLabel.Name = "ComputerPointsLabel";
            this.ComputerPointsLabel.Size = new System.Drawing.Size(141, 25);
            this.ComputerPointsLabel.TabIndex = 5;
            this.ComputerPointsLabel.Text = "Очки дедули: 0";
            // 
            // CubeBox1
            // 
            this.CubeBox1.Location = new System.Drawing.Point(78, 517);
            this.CubeBox1.Name = "CubeBox1";
            this.CubeBox1.Size = new System.Drawing.Size(147, 147);
            this.CubeBox1.TabIndex = 6;
            this.CubeBox1.TabStop = false;
            // 
            // CubeBox2
            // 
            this.CubeBox2.Location = new System.Drawing.Point(231, 517);
            this.CubeBox2.Name = "CubeBox2";
            this.CubeBox2.Size = new System.Drawing.Size(147, 147);
            this.CubeBox2.TabIndex = 7;
            this.CubeBox2.TabStop = false;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoEllipsis = true;
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ScoreLabel.Location = new System.Drawing.Point(414, 9);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(183, 45);
            this.ScoreLabel.TabIndex = 8;
            this.ScoreLabel.Text = "(И) 0 : 0 (Д)";
            this.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // WinLabel
            // 
            this.WinLabel.AutoSize = true;
            this.WinLabel.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WinLabel.ForeColor = System.Drawing.Color.Green;
            this.WinLabel.Location = new System.Drawing.Point(70, 423);
            this.WinLabel.Name = "WinLabel";
            this.WinLabel.Size = new System.Drawing.Size(318, 89);
            this.WinLabel.TabIndex = 9;
            this.WinLabel.Text = "YOU WIN";
            this.WinLabel.Visible = false;
            // 
            // LoseLabel
            // 
            this.LoseLabel.AutoSize = true;
            this.LoseLabel.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoseLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LoseLabel.Location = new System.Drawing.Point(61, 423);
            this.LoseLabel.Name = "LoseLabel";
            this.LoseLabel.Size = new System.Drawing.Size(338, 89);
            this.LoseLabel.TabIndex = 10;
            this.LoseLabel.Text = "YOU LOSE";
            this.LoseLabel.Visible = false;
            // 
            // DrawLabel
            // 
            this.DrawLabel.AutoSize = true;
            this.DrawLabel.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DrawLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.DrawLabel.Location = new System.Drawing.Point(115, 423);
            this.DrawLabel.Name = "DrawLabel";
            this.DrawLabel.Size = new System.Drawing.Size(226, 89);
            this.DrawLabel.TabIndex = 11;
            this.DrawLabel.Text = "DRAW";
            this.DrawLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 739);
            this.Controls.Add(this.DrawLabel);
            this.Controls.Add(this.LoseLabel);
            this.Controls.Add(this.WinLabel);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.CubeBox2);
            this.Controls.Add(this.CubeBox1);
            this.Controls.Add(this.ComputerPointsLabel);
            this.Controls.Add(this.playerPointsLabel);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.ComputerButton);
            this.Controls.Add(this.MoreButton);
            this.Controls.Add(this.EnoughButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.CubeBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CubeBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EnoughButton;
        private System.Windows.Forms.Button MoreButton;
        private System.Windows.Forms.Button ComputerButton;
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.Label playerPointsLabel;
        private System.Windows.Forms.Label ComputerPointsLabel;
        private System.Windows.Forms.PictureBox CubeBox1;
        private System.Windows.Forms.PictureBox CubeBox2;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label WinLabel;
        private System.Windows.Forms.Label LoseLabel;
        private System.Windows.Forms.Label DrawLabel;
    }
}

