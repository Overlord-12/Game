using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroids
{
    class GameForm
    {
        public static void Draw()
        {
            var form = new Form();


            try
            {
                form.MinimumSize = new System.Drawing.Size(700, 600);
                form.MaximumSize = new System.Drawing.Size(700, 600);
                int width = Convert.ToInt32(form.Size.Width);
                int height = Convert.ToInt32(form.Size.Height);
                if (width >= 1000 || height >= 1000) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("ArgumentOutOfRangeException");
                return;
            }


            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Asteroids";


            Game.Init(form);
            form.Show();
            Game.Draw();
            form.Close();
        }


    }
}

