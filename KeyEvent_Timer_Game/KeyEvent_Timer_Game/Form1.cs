using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyEvent_Timer_Game
{
    public partial class Form1 : Form
    {
        Graphics graph;

        Rectangle player;
        Color plyerColor = Color.Brown;
        Keys playerDirection = Keys.None;

        Rectangle ball;
        Color ballColor = Color.Green;
        Point ballSpeed;

        public Form1()
        {
            InitializeComponent();

            //From documentation:
            //Gets or sets a value indicating whether the form will receive key events
            //before the event is passed to the control that has focus.
            this.KeyPreview = true;

            pictureBoxGame.Image = new Bitmap(pictureBoxGame.Width, pictureBoxGame.Height);
            graph = Graphics.FromImage(pictureBoxGame.Image);

            player = new Rectangle(10, 10, 30, 100);
            ball = new Rectangle(pictureBoxGame.Width / 2, pictureBoxGame.Height / 2, 30, 30);
            ballSpeed = new Point(10, 5);

            drawGame();
        }

        private void drawGame()
        {
            graph.Clear(Color.Yellow);

            graph.FillEllipse(new SolidBrush(ballColor), ball);

            graph.FillRectangle(new SolidBrush(plyerColor), player);


            //pause
            if (!timer.Enabled)
            {
                graph.DrawString("PAUSE",
                                 new Font(Font.FontFamily, 100),
                                 new SolidBrush(Color.Red),
                                 new Point(10, 10));
            }

            pictureBoxGame.Refresh();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (playerDirection == Keys.Up && player.Y > 10)
            {
                player.Y -= 10;
            }
            else if (playerDirection == Keys.Down && player.Y + player.Height + 10 < pictureBoxGame.Height)
            {
                player.Y += 10;
            }
            //right border or player
            if (ball.X + ball.Width > pictureBoxGame.Width ||
               player.IntersectsWith(ball))
            {
                ballSpeed.X = -ballSpeed.X;
            }
            //top/bottom border
            if (ball.Y < 0 ||
                ball.Y + ball.Height > pictureBoxGame.Height)
            {
                ballSpeed.Y = -ballSpeed.Y;
            }

            //left border - end game
            if (ball.X < 0)
            {
                timer.Stop();
                MessageBox.Show("End game!");
                Close();
            }

            ball.X += ballSpeed.X;
            ball.Y += ballSpeed.Y;

            drawGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                playerDirection = e.KeyCode;
            }
            else if (e.KeyCode == Keys.Space)
            {
                timer.Enabled = !timer.Enabled;
                drawGame();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                playerDirection = Keys.None;
            }
        }
    }
}
