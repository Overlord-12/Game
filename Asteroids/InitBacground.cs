using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class InitBakground
    {
        public static void Bakground(int Width, int Height)
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.fon, Width, Height);
        }
        
    }
}
