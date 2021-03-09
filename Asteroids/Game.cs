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
        private static BaseObject[] _asteroids;  
        private static BaseObject[] _stars;
        private static BaseObject[] _planets;
        private static Laser _laser;
        private static List<Medicine> medicines = new List<Medicine>();
        private static Medicine med;

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
                _laser = new Laser(new Point(ship.Rect.X + 10, ship.Rect.Y + 10), new Point(5, 0), new Size(40, 30));
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

          
            if (ship.Energy <= 50 && medicines.Count <= 1)
            {
                if (medicines != null)
                {
                    foreach (var med in medicines)
                        med.Draw();
                }
            }





            foreach (var asteroid in _asteroids)
                if (asteroid != null)
                {
                    asteroid.Draw();
                }
               

            foreach (var star in _stars)
                if (star != null)
                {
                    star.Draw(); // Переопределенный метод Draw
                }
               

            foreach (var planet in _planets)
                planet.Draw();
            if (_laser != null)
            {
                _laser.Draw();
            }
           
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

            if (ship.Energy <=0)
            {
                ship.Die();
            }
            
            for (int i = 0; i < _asteroids.Length; i++)
            {
                var asteroids = _asteroids[i];
               
                if (_asteroids[i] == null)
                {
                    continue;
                }
                _asteroids[i].Update();
                if (_laser != null && _asteroids[i].Collision(_laser))
                {
                    _asteroids[i] = null;
                    _laser = null;
                    score += 30;
                    continue;
                }
                if (ship != null && _asteroids[i].Collision(ship))
                {
                    _asteroids[i] = null;
                    ship.HP_Minus(10);
                  
                    continue;
                }
               
                if (medicines != null && ship.Collision(med) && med != null)
                {
                    medicines.Remove(med);
                    ship.HP_Plus(30);
                    med = null;
                    continue;
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
            if (_laser != null)
            {
                _laser.Update();
            }
           

        }

        public static void Load()
        {
            var random = new Random();
            _asteroids = new BaseObject[15];
            for (int i = 0; i < _asteroids.Length; i++)
            {
                var size = random.Next(10, 20);
                _asteroids[i] = new Asteroid(new Point(300, (i+1) *20), new Point(-i, -i), new Size(size, size));
            }
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
  
            var position = new Random();
            var x = position.Next(0, Width / 2);
            var y = position.Next(0, Height / 2);
            med = new Medicine(new Point(x, y), new Point(1, 1), new Size(40, 40));
            medicines.Add(med);

        }

    }
}
