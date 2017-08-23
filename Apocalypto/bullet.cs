using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Apocalypto
{
    class bullet
    {
        // Bullet Variables
        public string direction;
        public int speed = 20;
        PictureBox Bullet = new PictureBox();
        Timer tm = new Timer();

        public int bulletLeft;
        public int bulletTop;

        // Logic for Creating a new Bullet
        public void mkBullet(Form form)
        {
            // Creates size, color, tag, location and adds it to the front of the form
            Bullet.BackColor = System.Drawing.Color.White;
            Bullet.Size = new Size(5, 5);
            Bullet.Tag = "bullet";
            Bullet.Top = bulletTop;
            Bullet.BringToFront();
            form.Controls.Add(Bullet);

            // Sets the Timer to the Speed. Assigns Tick Event to the bullet to move it across the screen.******* Starts the timer.
            tm.Interval = speed;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();
        }

        public void tm_Tick(object sender, EventArgs e)
        {
            // If the Direction the Player is facing is left, fire the bullet left
            if (direction == "left")
            {
                Bullet.Left -= speed;
            }

            // If the Direction the Player is facing is right, fire the bullet right
            if (direction == "right")
            {
                Bullet.Left += speed;
            }

            // If the Direction the Player is facing is Up, fire the bullet Up
            if (direction == "up")
            {
                Bullet.Top -= speed;
            }

            // If the Direction the Player is facing is Down, fire the bullet Down
            if (direction == "down")
            {
                Bullet.Top += speed;
            }

            // If the Bullet hits the borders, stop the bullet timer, dispose of all properties of the Bullet and set it to null
            if (Bullet.Left < 16 || Bullet.Left > 860 || Bullet.Top < 10 || Bullet.Top > 616)
            {
                tm.Stop();
                tm.Dispose();
                Bullet.Dispose();
                tm = null;
                Bullet = null;
            }
        }
    }
}
