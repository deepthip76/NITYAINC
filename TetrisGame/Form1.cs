using System;
using System.Windows.Forms;

namespace TetrisGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyDown += Form1_KeyEvent;
            timer1 = new Timer();
            timer1.Tick += new EventHandler(Timer1_Tick);
            timer1.Interval = 250;
            timer1.Enabled = true;
            SetCoordinates();
            occupied = new bool[ClientSize.Width+1, ClientSize.Height+1];

            shapeGeneration = new Random();
            shape = shapeGeneration.Next(0, 5);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            rotate = 0;
        }

        public void SetCoordinates()
        {
            x = (ClientSize.Width)/2;
            y = 0;
            blockMovementCoordinate = ClientSize.Height / 20;
        }
    }
}
