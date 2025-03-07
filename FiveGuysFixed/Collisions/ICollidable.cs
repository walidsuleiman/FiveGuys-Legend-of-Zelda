using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Collisions
{
    public interface ICollidable
    {
        double Rad { get; }
        Vector2 position { get; }
        CollisionType type { get; }
        void onCollision(ICollidable a, ICollidable b);
    }
}
