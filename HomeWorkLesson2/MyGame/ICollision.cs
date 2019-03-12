using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    interface ICollision
    {

        bool Collision(BaseObject obj);
        Rectangle Rect { get; }

    }
}
