using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisGame
{
    partial class Form1
    {
        Rectangle square;
        int x;
        int y;
        bool[,] occupied;
        int blockMovementCoordinate;

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
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            this.BackColor = Color.Black;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Draw and erase shape
        /// </summary>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.White);
            Pen clearPen = new Pen(Color.Black);
            // To avoid erasing the settled block
            if (!occupied[square.X, square.Y])
            {
                if (square.Y != ClientSize.Height - blockMovementCoordinate)
                    g.DrawRectangle(clearPen, square);
            }
            // To avoid overlapping of blocks
            if (!occupied[x, y + blockMovementCoordinate])
            {
                y += blockMovementCoordinate;
                square = new Rectangle(x, y, 40, 40);
                g.DrawRectangle(p, square);
            }
            // Settling the shape
            if (occupied[x, y + blockMovementCoordinate] || y >= ClientSize.Height - blockMovementCoordinate)
            {
                try
                {
                    occupied[x, y] = true;
                    occupied[x + 39, y] = true;
                    occupied[x + 39, y - 39] = true;
                    occupied[x, y - 39] = true;
                }
                catch (IndexOutOfRangeException) { }
                g.DrawRectangle(p, square);
                SetCoordinates();
            }
            // Game over condition
            if (occupied[x, 0] == true)
            {
                timer1.Stop();
                MessageBox.Show("Game over");
            }
        }
        /// <summary>
        /// Block left and right movement
        /// </summary>
        private void Form1_KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (x > 0)
                        x -= 40;
                    break;

                case Keys.Right:
                    if (x < ClientSize.Width)
                        x += 40;
                    break;
            }
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Timer timer1;
    }
}

