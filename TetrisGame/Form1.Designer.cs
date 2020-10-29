using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace TetrisGame
{
    partial class Form1
    {
        int x;
        int y;
        bool[,] occupied;
        int blockMovementCoordinate;
        Random shapeGeneration;
        int shape;
        int x1, x2, x3, x4, y1, y2, y3, y4;
        DataGridView grid = new DataGridView();
        SolidBrush brush;

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
            this.ClientSize = new System.Drawing.Size(800, 800);
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
            try
            {
                if (!occupied[x1, y1] && !occupied[x2, y2] && !occupied[x3, y3] && !occupied[x4, y4])
                {
                    if (y1 != ClientSize.Height - blockMovementCoordinate && y2 != ClientSize.Height - blockMovementCoordinate && y3 != ClientSize.Height - blockMovementCoordinate
                        && y4 != ClientSize.Height - blockMovementCoordinate)
                        DrawRandomShape(shape, true);
                }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                // To avoid overlapping of blocks
                if (!occupied[x1 + 1, y1 + blockMovementCoordinate - 1] && !occupied[x2 + 1, y2 + blockMovementCoordinate - 1] && !occupied[x3 + 1, y3 - 1 + +blockMovementCoordinate]
                && !occupied[x4 + 1, y4 + blockMovementCoordinate - 1])
                {
                    y += blockMovementCoordinate;
                    DrawRandomShape(shape);
                }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                // Settling the shape
                if (occupied[x1, y1 + blockMovementCoordinate] || y1 >= ClientSize.Height || occupied[x2, y2 + blockMovementCoordinate] || y2 >= ClientSize.Height - blockMovementCoordinate
                    || occupied[x3, y3 + blockMovementCoordinate] || y3 >= ClientSize.Height - blockMovementCoordinate || occupied[x4, y4 + blockMovementCoordinate] || y4 >= ClientSize.Height - blockMovementCoordinate)
                {
                    try
                    {
                        occupied[x1, y1] = true;
                        occupied[x2, y2] = true;
                        occupied[x3, y3] = true;
                        occupied[x4, y4] = true;
                    }
                    catch (IndexOutOfRangeException) { }
                    shape = shapeGeneration.Next(0, 6);
                    if (ClearUponFill())
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), 0, y4, ClientSize.Width, 40);
                        MoveBlocksDown();
                    }
                    SetCoordinates();
                    DrawRandomShape(shape);
                }
            }
            catch (IndexOutOfRangeException) { }
            // Game over condition
            if (occupied[ClientSize.Width / 2, 40] == true && occupied[ClientSize.Width / 2, 40]
                && occupied[ClientSize.Width / 2, 40] && occupied[ClientSize.Width / 2, 40])
            {
                timer1.Stop();
                MessageBox.Show("Game over");
            }
        }

        public void DrawRandomShape(int randomNumber, bool erase = false, int rotateCount = 0)
        {
            x1 = x;
            x2 = x;
            x3 = x;
            x4 = x;
            y1 = y;
            y2 = y;
            y3 = y;
            y4 = y;
            Graphics g = this.CreateGraphics();
            switch (randomNumber)
            {
                case 0:
                    // Draw L Shape
                    x4 = x + 40;
                    y2 = y + 40;
                    y3 = y + 80;
                    y4 = y + 80;
                    brush = new SolidBrush(Color.Yellow);
                    if (erase)
                    {
                        brush = new SolidBrush(Color.Black);
                    }
                    //grid.Rows[x].Cells.Add()
                    break;
                case 1:
                    if (rotateCount > 0)
                    {
                        switch (rotateCount)
                        {
                            case 1:
                                x1 = x1 - 40;
                                y1 = y1 + 40;
                                x3 = x3 + 40;
                                y3 = y3 - 40;
                                x4 = x4 + 80;
                                y4 = y - 40;
                                break;
                        }
                    }
                    else
                    {
                        // Draw I Shape
                        brush = new SolidBrush(Color.Yellow);
                        y2 = y + 40;
                        y3 = y + 80;
                        y4 = y + 120;
                    }
                    if (erase)
                    {
                        brush = new SolidBrush(Color.Black);
                    }
                    break;
                case 2:
                    // Draw S Shape
                    brush = new SolidBrush(Color.Yellow);
                    x2 = x + 40;
                    y3 = y + 40;
                    x4 = x - 40;
                    y4 = y + 40;
                    if (erase)
                    {
                        brush = new SolidBrush(Color.Black);
                    }
                    break;
                case 3:
                    // Draw Z Shape
                    x2 = x - 40;
                    y3 = y + 40;
                    x4 = x + 40;
                    y4 = y + 40;
                    brush = new SolidBrush(Color.Yellow);
                    if (erase)
                    {
                        brush = new SolidBrush(Color.Black);
                    }
                    break;
                case 4:
                    // Draw Square
                    brush = new SolidBrush(Color.Yellow);
                    x2 = x + 40;
                    y3 = y + 40;
                    x4 = x + 40;
                    y4 = y + 40;
                    if (erase)
                    {
                        brush = new SolidBrush(Color.Black);
                    }
                    break;
                case 5:
                    // T Shape
                    x3 = x + 40;
                    y2 = y + 40;
                    y4 = y + 80;
                    y3 = y + 40;
                    brush = new SolidBrush(Color.Yellow);
                    if (erase)
                    {
                        brush = new SolidBrush(Color.Black);
                    }
                    break;
            }
            g.FillRectangle(brush, new Rectangle(x1, y1, 40, 40));
            g.FillRectangle(brush, new Rectangle(x2, y2, 40, 40));
            g.FillRectangle(brush, new Rectangle(x3, y3, 40, 40));
            g.FillRectangle(brush, new Rectangle(x4, y4, 40, 40));
        }
        /// <summary>
        /// Block left and right movement
        /// </summary>
        private void Form1_KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    try
                    {
                        // Condition to avoid overlapping with the walls and other blocks
                        if (x1 >= 0 && x2 >= 0 && x3 >= 0 && x4 >= 0 && !occupied[x1 - 40, y1] && !occupied[x2 - 40, y2] && !occupied[x3 - 40, y3] && !occupied[x4 - 40, y4])
                        {
                            DrawRandomShape(shape, true);
                            x -= 40;
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                    break;

                case Keys.Right:
                    try
                    {
                        // Condition to avoid overlapping with the walls and other blocks
                        if (x1 + 40 < 800 && x2 + 40 < 800 && x3 + 40 < 800 && x4 + 40 < 800 && !occupied[x1 + 40, y1] && !occupied[x2 + 40, y2]
                            && !occupied[x3 + 40, y3] && !occupied[x4 + 40, y4])
                        {
                            DrawRandomShape(shape, true);
                            x += 40;
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                    break;
            }
        }

        public bool ClearUponFill()
        {
            for (int a = 0; a < ClientSize.Width; a += 40)
            {
                if (!occupied[a, y4])
                {
                    return false;
                }
            }

            for (int a = 0; a < ClientSize.Width; a += 40)
            {
                occupied[a, y4] = false;
            }
            return true;
        }


        public void MoveBlocksDown()
        {
            for (int a = 0; a < ClientSize.Width; a += 40)
            {
                for (int b = 0; b < ClientSize.Height; b += 40)
                    if (occupied[a, b])
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(a, b + 40, 40, 40));
                    }
            }
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Timer timer1;
    }
}

