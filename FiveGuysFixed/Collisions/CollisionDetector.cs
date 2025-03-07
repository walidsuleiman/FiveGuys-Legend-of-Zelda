using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Collisions
{
    internal class CollisionDetector
    {
        public bool IsColliding(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }

    }
}
