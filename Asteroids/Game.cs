using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Asteroids
{
    public static class Game
    {
        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static List<Asteroid> asteroids = new List<Asteroid>(); 
        private static BaseObject[] _stars;
        private static BaseObject[] _planets;
        private static List<Laser> laser = new List<Laser>();
        private static List<Medicine> medicines = new List<Medicine>();
        private static Random random = new Random();

        static Ship ship = new Ship(new Point(10,400),new Point(5,5), new Size(70,70));
        static Timer timer = new Timer();
        private static int damage;
        private static int score;
      

        public static int Width { get; set; }
        public static int Height { get; set; }
      

        public static BufferedGraphics Buffer
        {
            get { return _buffer; }
        }

        static Game() { }

       
       

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

          
            timer.Interval = 100;
            timer.Start();
            timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;
            Ship.DieShip += Ship_DieShip; ;
        }

        private static void Ship_DieShip(object sender, EventArgs e)
        {
            timer.Stop();

            Buffer.Graphics.DrawString("Игра закончена", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Underline), Brushes.LightGray, 50, 50);
            Buffer.Graphics.DrawString($"Ваш счет {score}", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Regular), Brushes.LightGray, 50, 150);
            Buffer.Render();
        }


        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (laser.Count < 3)
                {
                    laser.Add(new Laser(new Point(ship.Rect.X + 10, ship.Rect.Y + 10), new Point(5, 0), new Size(40, 30)));
                }
            }
            if(e.KeyCode == Keys.W)
            {
                ship.Up();
            }
            if(e.KeyCode == Keys.S)
            {
                ship.Down();
            }
            if (e.KeyCode == Keys.D)
            {
                ship.Right();
            }
            if (e.KeyCode == Keys.A)
            {
                ship.Left();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            _buffer.Graphics.Clear(Color.Black);
            _buffer.Graphics.DrawImage(Properties.Resources.Sun1,50,50,350,300);
            ship.Draw();

            
                if (ship.Energy < 50 && medicines.Count < 1)
                {
                    var position = new Random();
                    var x = position.Next(0, Width / 2);
                    var y = position.Next(0, Height / 2);
                    medicines.Add(new Medicine(new Point(x, y), new Point(1, 1), new Size(40, 40)));            
                }

            foreach (var med in medicines)
            {
                med.Draw();
            }
           

            foreach (var asteroid in asteroids)
                    asteroid.Draw();
               
               

            foreach (var star in _stars)
                if (star != null)
                {
                    star.Draw(); // Переопределенный метод Draw
                }
               

            foreach (var planet in _planets)
                planet.Draw();
           foreach(var _laser in laser)
                _laser.Draw();
           
           
            _buffer.Render();
            if (ship != null)
            {
                ship.Draw();
                Buffer.Graphics.DrawString($"HP{ship.Energy}", SystemFonts.DefaultFont, Brushes.White, 100, 10);
                     Buffer.Graphics.DrawString($"Score{score}", SystemFonts.DefaultFont, Brushes.White, 100, 20);
                Buffer.Render();
            }
           
          
        }

        public static void Update()
        {
            if (asteroids.Count < 15)
            {
                var size = random.Next(10, 50);
                var location = random.Next(0, Height);
                asteroids.Add(new Asteroid(new Point(Width-100, location), new Point(-4, -4), new Size(size, size)));
            }
            foreach (var med in medicines)
            {
                med.Draw();
            }

            if (ship.Energy <=0)
            {
                ship.Die();
            }
           
            for (int i = 0; i < asteroids.Count; i++)
            {
                var asteroid = asteroids[i];

                for (int j= 0; j < laser.Count; j++)
                {
                    if (asteroids[i].Collision(laser[j]))
                    {
                        asteroids.RemoveAt(i);
                        laser.RemoveAt(j);
                        score += 30;
                        i--;
                        continue;
                    }
                }
               
                if (ship != null && asteroids[i].Collision(ship))
                {
                    asteroids.RemoveAt(i);
                    ship.HP_Minus(10);
                    i--;
                    continue;
                }

               

            }
            foreach(var _asteroid in asteroids)
            {
                _asteroid.Update();
            }
            for (int j = 0; j < medicines.Count; j++)
            {
                if (ship.Collision(medicines[j]))
                {
                    medicines.RemoveAt(j);
                    ship.HP_Plus(30);
                }
            }
            //if (medicines != null && ship.Collision(medicines))
            //{
            //    medicines.Remove;
            //    ship.HP_Plus(30);

            //}

            foreach (var star in _stars)
                star.Update(); // Переопределенный метод Update
            foreach (var planets in _planets)
                planets.Update();

            foreach (var _laser in laser)
            {
               
                _laser.Update();
                
            }

            for (int i = laser.Count - 1; i >= 0; i--)
            {
                if (laser[i].GetPos.X > Width)
                {
                    laser.RemoveAt(i);
                }
            }



        }

        public static void Load()
        {

            //if (asteroids.Count < 15)
            //{
               
            //    var size = random.Next(10,50);
            //    asteroids.Add(new Asteroid(new Point(300, (i + 1) * 20), new Point(-i, -i), new Size(size, size)));
            //    i++;
            //    Game.Load();
            //}

            
            _stars = new BaseObject[10];
            for (int i = 0; i < _stars.Length; i++)
            {
                _stars[i] = new Star(new Point(400, (i+1) * 30), new Point(i + 1, i + 1), new Size(3, 3));
            }
            _planets = new BaseObject[2];
            for (int i = 0; i < _planets.Length; i++)
            {
                var size = random.Next(50, 100);
                _planets[i] = new Planet(new Point(300, (i+1) * 100), new Point(-i, -i), new Size(size, size));
            }
  

        }

    }
}
