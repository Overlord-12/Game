using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Laser:BaseObject
    {
        public Laser(Point pos,Point dir,Size size): base(pos, dir, size) { }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.laser, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {

            Pos.X = Pos.X + 15;    
        }
    }
}
