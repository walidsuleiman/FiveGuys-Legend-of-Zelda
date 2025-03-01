using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Collisions
{
    public interface ICollidable
    {
        double Rad { get; set; }
        Vector2 position { get; set; }
        void onCollision(ICollidable a, ICollidable b);
    }
}
