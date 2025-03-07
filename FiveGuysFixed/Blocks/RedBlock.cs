using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Blocks
{
    internal class RedBlock : IBlock, ICollidable
    {
        private Sprite redBlockSprite;
        private double x, y;
        private int currentTime;

        public int Height { get { return this.Height; } }
        public int Width { get { return this.Width; } }

        public double Rad { get { return Math.Max(redBlockSprite.Height, redBlockSprite.Width); } }

        public Vector2 position => this.position;

        public CollisionType type => CollisionType.BLOCK;

        public RedBlock(Texture2D texture, int x, int y)
        {
            redBlockSprite = new Sprite(texture, 578, 1287, 18, 18);

            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public bool IsCollidable()
        {
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            redBlockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            redBlockSprite.Update(gametime);
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            
        }
    }
}
