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
        int rotate;
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
                        DrawRandomShape(shape, true, rotate);
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
                    DrawRandomShape(shape, false, rotate);
                }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                // Settling the shape
                if (occupied[x1, y1 + blockMovementCoordinate] || y1 >= ClientSize.Height - blockMovementCoordinate || occupied[x2, y2 + blockMovementCoordinate] || y2 >= ClientSize.Height - blockMovementCoordinate
                    || occupied[x3, y3 + blockMovementCoordinate] || y3 >= ClientSize.Height - blockMovementCoordinate || occupied[x4, y4 + blockMovementCoordinate] || y4 >= ClientSize.Height - blockMovementCoordinate
                    || y1 >= ClientSize.Height || y2 >= ClientSize.Height || y3 >= ClientSize.Height || y4 >= ClientSize.Height)
                {
                    try
                    {
                        occupied[x1, y1] = true;
                        occupied[x2, y2] = true;
                        occupied[x3, y3] = true;
                        occupied[x4, y4] = true;
                    }
                    catch (IndexOutOfRangeException) { }
                    shape = shapeGeneration.Next(0, 5);
                    if (ClearUponFill(y1))
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), 0, y1, ClientSize.Width, 40);
                        MoveBlocksDown(y1);
                    }
                    if (ClearUponFill(y2))
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), 0, y2, ClientSize.Width, 40);
                        MoveBlocksDown(y2);
                    }
                    if (ClearUponFill(y3))
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), 0, y3, ClientSize.Width, 40);
                        MoveBlocksDown(y3);
                    }
                    if (ClearUponFill(y4))
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), 0, y4, ClientSize.Width, 40);
                        MoveBlocksDown(y4);
                    }
                    // Set coordinates back to 0. 
                    SetCoordinates();

                    // Start new shape
                    DrawRandomShape(shape, false, rotate);
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

        /// <summary>
        /// Draws a random shape 
        /// </summary>
        /// <param name="randomNumber">Random number </param>
        /// <param name="erase">true if the shape needs to erased</param>
        /// <param name="rotateCount">Count of rotations 1. 0 2. 90 3. 180 4. 270</param>
        public void DrawRandomShape(int randomNumber, bool erase = false, int rotateCount = 0)
        {
            // Set coordinates
            x1 = x;
            x2 = x;
            x3 = x;
            x4 = x;
            y1 = y;
            y2 = y;
            y3 = y;
            y4 = y;
            Graphics g = this.CreateGraphics();
            brush = new SolidBrush(Color.Yellow);

            // Set brush to black to erase the existing shape
            if (erase)
            {
                brush = new SolidBrush(Color.Black);
            }
            switch (randomNumber)
            {
                case 0:
                    switch (Math.Abs(rotateCount) % 4)
                    {
                        case 0:
                            // Draw L Shape
                            x4 = x + 40;
                            y2 = y + 40;
                            y3 = y + 80;
                            y4 = y + 80;
                            break;
                        case 1:
                            // Rotate 90
                            y2 = y + 40;
                            x3 = x - 40;
                            y3 = y + 40;
                            x4 = x - 80;
                            y4 = y + 40;
                            break;
                        case 2:
                            // Rotate 180
                            y2 = y - 40;
                            y3 = y - 80;
                            y4 = y - 80;
                            x4 = x - 40;
                            break;
                        case 3:
                            // Rotate 270
                            y2 = y + 40;
                            x3 = x - 40;
                            y3 = y + 40;
                            x4 = x - 80;
                            y4 = y + 40;
                            y1 = y + 80;
                            break;
                    }
                    break;
                case 1:
                    switch (Math.Abs(rotateCount) % 2)
                    {
                        // Draw I Shape
                        case 0:
                            y2 = y + 40;
                            y3 = y + 80;
                            y4 = y + 120;
                            break;
                        case 1:
                            // Rotate 90
                            x2 = x - 40;
                            x3 = x - 80;
                            x4 = x + 40;
                            break;
                    }
                    break;
                case 2:
                    switch (Math.Abs(rotateCount) % 4)
                    {
                        case 0:
                            // Draw S Shape
                            x2 = x + 40;
                            y3 = y + 40;
                            x4 = x - 40;
                            y4 = y + 40;
                            break;
                        case 1:
                            // Rotate 90 Left
                            y2 = y + 40;
                            x3 = x + 40;
                            y3 = y + 40;
                            x4 = x + 40;
                            y4 = y + 80;
                            break;
                        case 2:
                            // Rotate 180 Left
                            x2 = x - 40;
                            y3 = y + 40;
                            x4 = x + 40;
                            y4 = y + 40;
                            break;
                        case 3:
                            // Rotate 270 Left
                            y2 = y + 40;
                            x3 = x + 40;
                            y3 = y + 40;
                            x4 = x + 40;
                            y4 = y + 80;
                            break;
                    }
                    break;
                case 3:
                    // Draw Square
                    x2 = x + 40;
                    y3 = y + 40;
                    x4 = x + 40;
                    y4 = y + 40;
                    break;
                case 4:
                    switch (Math.Abs(rotateCount) % 4)
                    {
                        case 0:
                            // T Shape
                            x3 = x + 40;
                            y2 = y + 40;
                            y4 = y + 80;
                            y3 = y + 40;
                            break;
                        case 1:
                            // Rotate 90 Left
                            y2 = y + 40;
                            x3 = x - 40;
                            y3 = y + 40;
                            x4 = x + 40;
                            y4 = y + 40;
                            break;
                        case 2:
                            // Rotate 180 Left
                            y4 = y + 40;
                            x4 = x - 40;
                            y2 = y + 40;
                            y3 = y + 80;
                            break;
                        case 3:
                            // Rotate 270
                            x2 = x - 40;
                            x3 = x + 40;
                            y4 = y + 40;
                            break;
                    }
                    break;
            }
            // To Make sure, the shape doesn't collide with the walls
            if (x1 < 0 || x2 < 0 || x3 < 0 || x4 < 0)
            {
                x1 = -40;
                x2 = -40;
                x3 = -40;
                x4 = -40;
            }
            g.FillRectangle(brush, new Rectangle(x1, y1, 40, 40));
            g.FillRectangle(brush, new Rectangle(x2, y2, 40, 40));
            g.FillRectangle(brush, new Rectangle(x3, y3, 40, 40));
            g.FillRectangle(brush, new Rectangle(x4, y4, 40, 40));
        }

        /// <summary>
        /// Block left and right movement, rotate movement
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
                            DrawRandomShape(shape, true, rotate);
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
                            DrawRandomShape(shape, true, rotate);
                            x += 40;
                        }
                    }
                    catch (IndexOutOfRangeException) { }
                    break;
                case Keys.Up:
                    // Erase the existing shape and rotate
                    DrawRandomShape(shape, true, rotate);
                    rotate++;
                    break;
                case Keys.Down:
                    // Erase the existing shape and rotate
                    DrawRandomShape(shape, true, rotate);
                    rotate--;
                    break;
            }
        }

        /// <summary>
        /// Clear the line if it is filled
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool ClearUponFill(int b)
        {
            // Check if the line is filled
            for (int a = 0; a < ClientSize.Width; a += 40)
            {
                if (!occupied[a, b])
                {
                    return false;
                }
            }

            // If the line is filled, de-occupy the coordinates to clear it.
            for (int a = 0; a < ClientSize.Width; a += 40)
            {
                occupied[a, b] = false;
            }
            return true;
        }

        /// <summary>
        /// Move blocks down to fill up the cleared coordinates
        /// </summary>
        /// <param name="yco">Y- Cordinate</param>
        public void MoveBlocksDown(int yco)
        {
            for (int a = 0; a < ClientSize.Width; a += 40)
            {
                for (int b = yco; b < ClientSize.Height && b - 40 >= 0; b -= 40)
                {
                    if (occupied[a, b])
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(a, b + 40, 40, 40));
                        occupied[a, b] = false;
                        g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(a, b, 40, 40));
                        occupied[a, b + 40] = true;
                    }
                }
            }
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Timer timer1;
    }
}

