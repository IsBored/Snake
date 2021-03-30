using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Cerc> Snake = new List<Cerc>(); 
        private Cerc mar = new Cerc(); 
        public Form1()
        {
            InitializeComponent();
            new Setari(); 

            timer.Interval = 1000 / Setari.Viteza; 
            timer.Tick += updateSreen; 
            timer.Start(); 

            start(); 
        }

        private void updateSreen(object sender, EventArgs e)
        {
            

            if (Setari.GameOver == true)
            {

                

                if (Input.KeyPress(Keys.Enter))
                {
                    start();
                }
            }
            else
            {
                

                if (Input.KeyPress(Keys.Right) && Setari.directie != Directii.Stanga)
                {
                    Setari.directie = Directii.Dreapta;
                }
                else if (Input.KeyPress(Keys.Left) && Setari.directie != Directii.Dreapta)
                {
                    Setari.directie = Directii.Stanga;
                }
                else if (Input.KeyPress(Keys.Up) && Setari.directie != Directii.Jos)
                {
                    Setari.directie = Directii.Sus;
                }
                else if (Input.KeyPress(Keys.Down) && Setari.directie != Directii.Sus)
                {
                    Setari.directie = Directii.Jos;
                }

                muta(); 
            }

            pictureBox1.Invalidate(); 

        }
        private void updateGame(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics; 

            if (Setari.GameOver == false)
            {

                Brush snakeColour; 

                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                       snakeColour = Brushes.Green;
                    }
                    canvas.FillEllipse(snakeColour, new Rectangle(Snake[i].x * Setari.Latime, Snake[i].y * Setari.Inaltime, Setari.Latime, Setari.Inaltime));
                    canvas.FillEllipse(Brushes.Red, new Rectangle(mar.x * Setari.Latime,mar.y * Setari.Inaltime,Setari.Latime, Setari.Inaltime));
                }
            }
            else
            {
                

                string gameOver = "Game Over \n" + "Scor final: " + Setari.Scor + "\n Apasati Enter pentru Restart \n";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

        private void start()
        {
            label3.Visible = false; 
            new Setari(); 
            Snake.Clear(); 
            Cerc head = new Cerc { x = 10, y = 5 }; 
            Snake.Add(head); 

            label2.Text = Setari.Scor.ToString(); 

            genMancare(); 
        }

        private void muta()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            { 
                if (i == 0)
                {
                    switch (Setari.directie)
                    {
                        case Directii.Dreapta:
                            Snake[i].x++;
                            break;
                        case Directii.Stanga:
                            Snake[i].x--;
                            break;
                        case Directii.Sus:
                            Snake[i].y--;
                            break;
                        case Directii.Jos:
                            Snake[i].y++;
                            break;
                    }

                    int maxXpos = pictureBox1.Size.Width / Setari.Latime;
                    int maxYpos = pictureBox1.Size.Height / Setari.Inaltime;

                    /* if (Snake[i].x < 0 || Snake[i].y < 0 ||Snake[i].x > maxXpos || Snake[i].y > maxYpos)
                     {
                        die();
                     }
                    */
                    if (Snake[i].x < 0) Snake[i].x = maxXpos;
                    if (Snake[i].y < 0) Snake[i].y = maxYpos;
                    if (Snake[i].x > maxXpos) Snake[i].x = 0;
                    if (Snake[i].y > maxYpos) Snake[i].y = 0;

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].x == Snake[j].x && Snake[i].y == Snake[j].y)
                        {
                            die();
                        }
                    }
                    if (Snake[0].x == mar.x && Snake[0].y == mar.y)
                    { 
                        eat();
                    }

                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }
            }
        }

        private void genMancare()
        {
            int maxXpos = pictureBox1.Size.Width / Setari.Latime;
            int maxYpos = pictureBox1.Size.Height / Setari.Inaltime;
            Random rnd = new Random();
            mar = new Cerc { x = rnd.Next(0, maxXpos), y = rnd.Next(0, maxYpos) };
            
        }
        private void eat()
        {
           Cerc body = new Cerc
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y

            };

            Snake.Add(body); 
            Setari.Scor += Setari.Puncte; 
            label2.Text = Setari.Scor.ToString(); 
            genMancare(); 
        }
        private void die()
        {
            Setari.GameOver = true;
        }
    }
}
