using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    class Ship : BaseObject
    {

        public static event Message MessageDie;

        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n)
        {

            _energy -= n;

        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {

        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }
        /// <summary>
        /// лечение корабля
        /// </summary>
        public void Health()
        {
            if (Game.health > 0)
            {
                _energy += 30;
                if (_energy > 100) _energy = 100;
                Game.health = Game.health - 1;
            }
        }

    }
}
