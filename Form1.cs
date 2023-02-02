using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_game
{
    public partial class Form1 : Form
    {
        bool goleft = false;
        bool goright = false;
        bool jumping = false;
        bool gameOver = false;

        int jumpSpeed = 5;
        int force = 6;
        int score = 0;
        int health = 2;
        int playerSpeed = 5;
        int horizonSpeed = 3;
        int verticalSpeed = 4;
        int enemyOnespeed = 2;
        int enemyTwospeed = 2;
        int checkgem = 0;
        bool checkdie = true;
        public Form1()
        {
            InitializeComponent();
        }
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            player.Image = Image.FromFile("D:/Net/Basic game/Resources/idle.gif");
            player.Top += jumpSpeed;
            if (goleft == true && player.Location.X >0)
            {
                player.Left -= playerSpeed;
                player.Image = Image.FromFile("D:/Net/Basic game/Resources/idle1.gif");
            }    
            if (goright == true && player.Location.X < (this.ClientSize.Width - player.Width))
                player.Left += playerSpeed;
            if (jumping == true & force < 0)
                jumping = false;
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
                jumpSpeed = 5 ;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "gem" && score != 24)
                        x.Visible = false;
                    if ((string)x.Tag == "gem" && score == 24 && checkgem == 0)
                        x.Visible = true;
                    if ((string)x.Tag == "gem" && x.Visible == true)
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = false;
                            checkgem++;
                        }

                    }

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 6;
                            player.Top = x.Top - player.Height;
                            if ((string)x.Name == "horizon" & goleft == false || (string)x.Name == "horizon" & goright == false)
                                player.Left -= horizonSpeed;
                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag == "enemy" & checkdie == true)
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            player.Image = Image.FromFile("D:/Net/Basic game/Resources/hurt.gif");
                            gameTimer.Stop();
                            gameOver = true;
                            txtScore.Text = "Score: "+score + Environment.NewLine + "You Died!!";
                            DialogResult result = MessageBox.Show("Do you want restart game?","",MessageBoxButtons.YesNo);
                            if(result == DialogResult.Yes)
                                Restartgame();
                            if (result != DialogResult.Yes)
                                Application.Exit();

                        }    
                    }
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = false;
                            score++;
                        }

                    }

                }
            }
            horizon.Left -= horizonSpeed;
            if(horizon.Left < 0 || horizon.Left +horizon.Width > this.ClientSize.Width)
                horizonSpeed = -horizonSpeed;
            verticalPlatform.Top += verticalSpeed;

            if(verticalPlatform.Top > 506 || verticalPlatform.Top < 217)
                verticalSpeed = -verticalSpeed;

            enemyOne.Left -= enemyOnespeed;
            if (enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Width + pictureBox5.Left)
                enemyOnespeed = -enemyOnespeed;

            enemyTwo.Left -= enemyTwospeed;
            if(enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyOne.Width > pictureBox2.Width +pictureBox2.Left)
                enemyTwospeed = -enemyTwospeed;

            if(player.Top +player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                gameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You Died!!";
                DialogResult result = MessageBox.Show("Do you want restart game?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Restartgame();
                else
                    Application.Exit();
            }
            if(checkgem == 1 & score == 24)
            {
                door.Image = Image.FromFile("D:/Net/Basic game/Resources/door-opened.png");
            }    
            else
                door.Image = Image.FromFile("D:/Net/Basic game/Resources/door.png");

            if (player.Bounds.IntersectsWith(door.Bounds) & score == 24 & checkgem == 1)
            {
                player.Visible = false;
                gameTimer.Stop();
                gameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You Win!!";

            }
            if (player.Bounds.IntersectsWith(door.Bounds) & score != 24) 
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "Please Collect All The Coin !!";
            }
            if (player.Bounds.IntersectsWith(door.Bounds) & score == 24 & checkgem != 1)
                txtScore.Text = "Score: " + score + Environment.NewLine + "Please Collect Gem!!";


        }       
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (jumping)
            {
                jumping = false;
            }
            if(e.KeyCode == Keys.Enter & gameOver == true)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn chơi lại không !!","Lưu ý",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Restartgame();
                else
                    Application.Exit();
            }
        }
        private void Restartgame()
        {
            goleft = false;
            goright = false;
            jumping = false;
            gameOver = false;
            score = 0;
            checkgem = 0;
            health = 2;
            txtScore.Text = "Score: " + score;
            foreach(Control x in this.Controls)
            {
                if (x is PictureBox & x.Visible == false)
                    x.Visible = true;
            }    
            player.Top = 627;
            player.Left = 44;
            enemyOne.Left = 439;
            enemyOne.Top = 308;
            enemyTwo.Left = 522;
            enemyTwo.Top = 562;
            player.Image = Image.FromFile("D:/Net/Basic game/Resources/idle.gif");
            gameTimer.Start();
        }

        private void keyisdowm(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.Space )
            {
                jumping = true;
            }
        }

        //hack nhặt coin
        private void hackcoin_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox )
                {
                    if ((string)x.Tag == "coin")
                    {
                        x.Visible = false;
                        score = 24;
                    }
                    
                }       
            }
        }
        //Bất tử
        private void label2_Click(object sender, EventArgs e)
        {
            checkdie = false;
            label2.ForeColor = Color.Red;
        }

        private void tele_Click(object sender, EventArgs e)
        {
            player.Top = 11;
            player.Left = 98;
        }

        private void enemyOne_Click(object sender, EventArgs e)
        {

        }
    }
}
