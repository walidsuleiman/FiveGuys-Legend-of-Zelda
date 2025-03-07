using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Blocks
{
    internal class YellowBlock : IBlock
    {
        private Sprite yellowBlockSprite;
        private double x, y;
        private int currentTime;

        public YellowBlock(Texture2D texture, int x, int y)
        {
            yellowBlockSprite = new Sprite(texture, 354, 81, 16, 16);

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
            yellowBlockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            yellowBlockSprite.Update(gametime);
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)x, (int)y, 32, 32);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }
    }
}