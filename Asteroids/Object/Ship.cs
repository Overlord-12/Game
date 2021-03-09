using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject
    {
        public static event EventHandler DieShip;
        protected int HP = 100;
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        public int Energy
        {
            get { return HP; }
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Properties.Resources.ship, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
           throw new Exception();
        }
        public void HP_Plus(int heal)
        {
            HP += heal;
        }
        public void HP_Minus(int damage)
        {
            HP -= damage;
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;

        }
        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }
        public void Right()
        {
            if (Pos.X < (Game.Width/2)) Pos.X = Pos.X + Dir.X;
        }
        public void Die()
        {
            if ( DieShip != null)
            {
                DieShip.Invoke(this, new EventArgs());
            }
        }
    }
}
