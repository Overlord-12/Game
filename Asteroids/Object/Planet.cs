using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Asteroids
{
    class Planet : BaseObject
    {
        private int index;
        private static Random rand = new Random();
        
        public Planet(Point pos, Point dir, Size size) : base(pos, dir, size) {
            index = rand.Next(1, 3);

        }
        public override void Draw()
        {
            switch (index)
            {
                case 1: Game.Buffer.Graphics.DrawImage(Properties.Resources.planet1, Pos.X, Pos.Y, Size.Width, Size.Height);
                    break;
                case 2: Game.Buffer.Graphics.DrawImage(Properties.Resources.planet2, Pos.X, Pos.Y, Size.Width, Size.Height);
                    break;
            }
        }

    }
}
