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
    internal class WhiteBlock : IBlock, ICollidable
    {
        private Sprite whiteBlockSprite;
        private double x, y;
        private int currentTime;

        public int Height { get { return this.Height; } }
        public int Width { get { return this.Width; } }

        public double Rad { get { return Math.Max(whiteBlockSprite.Height, whiteBlockSprite.Width); } }

        public Vector2 position => this.position;

        public CollisionType type => CollisionType.BLOCK;

        public WhiteBlock(Texture2D texture, int x, int y)
        {
            whiteBlockSprite = new Sprite(texture, 579, 210, 16, 16);

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
            whiteBlockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            whiteBlockSprite.Update(gametime);
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            
        }
    }
}
