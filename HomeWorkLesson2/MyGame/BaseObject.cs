using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    abstract class BaseObject
    {
        public delegate void Message();

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        protected BaseObject(Point pos, Point dir, Size size)
        {

            Pos = pos;
            Dir = dir;
            Size = size;

        }

        public abstract void Draw();


        public abstract void Update();

        public bool Collision(BaseObject o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(Pos, Size);

    }
}
