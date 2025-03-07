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
    internal class GreenBlock : IBlock, ICollidable
    {
        private Sprite greenBlockSprite;
        private double x, y;
        private int currentTime;

        public int Height { get { return this.Height; } }
        public int Width { get { return this.Width; } }

        public double Rad { get { return Math.Max(greenBlockSprite.Height, greenBlockSprite.Width); } }

        public Vector2 position => this.position;

        public CollisionType type => CollisionType.BLOCK;

        public GreenBlock(Texture2D texture, int x, int y)
        {
            greenBlockSprite = new Sprite(texture, 322, 65, 16, 16);

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
            float scale = 2;
            greenBlockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            greenBlockSprite.Update(gametime);
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            
        }
    }
}
