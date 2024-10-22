using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Apocalypto
{
    public class LeaderboardEntry
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public static class Leaderboard
    {
        private const string FilePath = "leaderboard.json";

        public static List<LeaderboardEntry> GetLeaderboard()
        {
            if (!File.Exists(FilePath))
            {
                return new List<LeaderboardEntry>();
            }

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<LeaderboardEntry>>(json);
        }

        public static void AddScoreToLeaderboard(string name, int score)
        {
            List<LeaderboardEntry> leaderboard = GetLeaderboard();
            leaderboard.Add(new LeaderboardEntry { Name = name, Score = score });
            leaderboard.Sort((x, y) => y.Score.CompareTo(x.Score)); // Sort by score descending

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(leaderboard));
        }
    }

    public partial class Form1 : Form
    {
        // Global Variables
        bool goUp; // Variable for making the Player go Up
        bool goDown; // Variable for making the Player go Down
        bool goLeft; // Variable for making the Player go Left
        bool goRight; // Variable for making the Player go Right
        bool neutral; // Variable for the rest state. This will change with power-ups down the road.
        string facing = "up"; // Used to guide the bullets
        double playerHealth = 100; // Player's Health in the game
        int speed = 10; // Speed of the Player
        int ammo = 10; // Ammo for the Player
        int zombieSpeed = 3; // Speed of the Zombies
        int score = 0; // Score of the Game
        bool gameOver = false; // The game is not over in the beginning. Things can change this boolean and end the game.
        Random rnd = new Random(); // An Instance of the Random class

        // Starting the game
        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            // If the Game's over, do nothing in here and return
            if (gameOver) return;

            // If the Left key is pressed, move the Player left, mark it as left and use the left Image from Resources
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }

            // If the Right key is pressed, move the Player right, mark it as right and use the right Image from Resources
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }

            // If the Up key is pressed, move the Player Up, mark it as Up and use the Up Image from Resources
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }

            // If the Down key is pressed, move the Player Down, mark it as Down and use the Down Image from Resources
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            // If the Game's over, do nothing in here and return
            if (gameOver) return;

            // Logic for what occurs once the Left Key has been lifted. Stop moving Left.
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            // Logic for what occurs once the Right Key has been lifted. Stop moving Right.
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            // Logic for what occurs once the Up Key has been lifted. Stop moving Up.
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            // Logic for what occurs once the Down Key has been lifted. Stop moving Down.
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

            // If the ammo is more than 0 and the Space Bar is lifted, remove 1 from Ammo and shoot in the direction facing.
            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                shoot(facing);

                // If Ammo is less than 1, Drop Ammo
                if(ammo < 1)
                {
                    DropAmmo();
                }
            }
        }

        private void gameEngine(object sender, EventArgs e)
        {
            // If the Player's Health is above 1, show the progress bar to the Player's Health Number
            if (playerHealth > 1)
            {
                progressBar1.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                // If the Player's Health is below 1, turn the Image to Dead, stop the timer and set gameOver to true
                player.Image = Properties.Resources.dead;
                timer1.Stop();
                gameOver = true;

                // Add player's score to the leaderboard
                string playerName = "CurrentPlayer"; // Replace with actual player name if available
                Leaderboard.AddScoreToLeaderboard(playerName, score);
            }

            // Labels for the Ammo and Kill Score
            label1.Text = "Ammo: " + ammo;
            label2.Text = "Kills: " + score;

            // If the Player's Health reaches below 20, turn the Progress Bar Red
            if (playerHealth < 20)
            {
                progressBar1.ForeColor = System.Drawing.Color.Red;
            }

            // If moving Left is true and Player is facing the left, move the Player to the Left
            if (goLeft && player.Left > 0)
            {
                player.Left -= speed;
            }

            // If moving Right is true and Player Left + PLayer width is less than 930 pixels, move the Player Right
            if (goRight && player.Left + player.Width < 930)
            {
                player.Left += speed;
            }

            // If moving Up is true and the Player is 60 pixels away from the top, move the Player Up
            if (goUp && player.Top > 60)
            {
                player.Top -= speed;
            }

            // If moving Down is true and the Player + the Player's height is less than 700 pixels, move the Player Down
            if (goDown && player.Top + player.Height < 700)
            {
                player.Top += speed;
            }

            // Logic for searching for Controls
            foreach (Control x in this.Controls)
            {
                // If the PictureBox has a tag of ammo 
                if (x is PictureBox && x.Tag == "ammo")
                {
                    // Check if its hitting the Player
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        // Remove the Ammo's Picture once Picked Up and Adds 5 ammo to the Player
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                // If the PictureBox has a tag of bullet and hits a border, Remove the Image
                if (x is PictureBox && x.Tag == "bullet")
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x));
                        ((PictureBox)x).Dispose();
                    }
                }

                // If the PictureBox has a tag of zombie and hits a player, Remove 1 from the Player's Health
                if (x is PictureBox && x.Tag == "zombie")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    // Moves the Zombies to the Left of the Player & Changes the Image to the Left Zombie
                    if (((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }

                    // Moves the Zombies to the Right of the Player & Changes the Image to the Right Zombie
                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }

                    // Moves the Zombies Up to the Player & Changes the Image to the Up Zombie
                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }

                    // Moves the Zombies Down to the Player & Changes the Image to the Down Zombie
                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }

                // Logic to determine Bullet and Zombie Interaction
                foreach (Control j in this.Controls)
                {
                    // If the Bullet an Zombie Picture Boxes...
                    if ((j is PictureBox && j.Tag == "bullet") && (x is PictureBox && x.Tag == "zombie"))
                    {
                        // ..Intersect, Increment the score, remove them both, make a new zombie for each killed zombie
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                            makeZombies();
                        }
                    }
                }
            }
        }

        private void DropAmmo()
        {
            //Creates the Image for the ammo and uses the Random Class to shuffle it through the map. Bring both images to the front.
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = rnd.Next(10, 890);
            ammo.Top = rnd.Next(50, 600);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);
            ammo.BringToFront();
            player.BringToFront();
        }

        private void shoot(string direct)
        {
            // Creates a new bullet, assigns the direction, Places the bullet to the Top Left of the Player. Make a new bullet from the bullet class.
            bullet shoot = new bullet();
            shoot.direction = direct;
            shoot.bulletLeft = player.Left + (player.Width / 2);
            shoot.bulletTop = player.Top + (player.Height / 2);
            shoot.mkBullet(this);
        }

        private void makeZombies()
        {
            // Logic for Making new Zombies. New Picturebox with a class of zombie. 
            // Use the Zombie down image and populate it Randomly. Bring it to the front. 
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = rnd.Next(0, 900);
            zombie.Top = rnd.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(zombie);
            player.BringToFront();
        }
    }
}
