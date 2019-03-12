using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    static class Game
    {
        public static BaseObject[] _objs;

        public static Bullet _bullet;
        public static Asteroid[] _asteroids;
        public static Planet _planet;

        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Timer _timer = new Timer();
        public static Random rnd = new Random();

        public static int health = 3;
        public static int userPoint = 0;

        static Game()
        {

        }

        

        public static void Init(Form form)
        {
            _timer.Start();

            //log = Convert.ToString(DateTime.Now);
            //Console.WriteLine($"{log}: Начало инициализации.");

            Graphics g;

            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            if (Width >= 1000 || Height >= 1000 || Width < 0 || Height < 0)
            {
                Exception e = new ArgumentOutOfRangeException();
                throw new Exception("Ошибка ", e);
            }



            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            
            
            _timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;
            
            
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.X) _ship.Health();
        }

        public static void Draw()
        {

            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in _objs)
                obj.Draw();

            _planet.Draw();

            Random rnd = new Random();
            var r = rnd.Next(5, 50);
            foreach (BaseObject obj in _asteroids)
            {
                obj?.Draw();                
            }
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);

            Buffer.Graphics.DrawString("Аптечки(Х): " + health, SystemFonts.DefaultFont, Brushes.White, 60, 0);

            Buffer.Graphics.DrawString("Астероидов сбито: " + userPoint, SystemFonts.DefaultFont, Brushes.White, 140, 0);

            Buffer.Render();

        }

        public static void Update()
        {

            foreach (BaseObject obj in _objs)
                obj.Update();

            _planet.Update();

            for (int i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    string events1 = "Попадание по астероиду.";
                    LogUpdate(events1);
                    userPoint++;
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                var rnd = new Random();
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Hand.Play();
                string events = "Столкновение астероида и корабля.";
                LogUpdate(events);
                if (_ship.Energy <= 0) Finish();
            }

            //foreach (BaseObject obj in _asteroids)
            //    obj.Update();

            _bullet?.Update();
        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            _planet = new Planet(new Point(-199, 250), new Point(5, 0), new Size(50, 50));
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(100, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));

            }


        }

        private static void Timer_Tick(object sender, EventArgs e)
        {

            Update();
            Draw();

        }      

        public static void Finish()
        {

            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
            string events = "Конец игры.";
            LogUpdate(events);
        }
        /// <summary>
        /// Обновляет лог консоли
        /// </summary>
        /// <param name="events">событие для обновления</param>
        public static void LogUpdate(String events)
        {

            string log = Convert.ToString(DateTime.Now);
            Console.WriteLine($"{log} {events}");

        }
    }
}
