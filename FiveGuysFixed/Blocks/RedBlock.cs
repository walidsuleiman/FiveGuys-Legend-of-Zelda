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
    internal class RedBlock : IBlock
    {
        private Sprite redBlockSprite;
        private double x, y;
        private int currentTime;

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

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            float scale = 2;
            var drawPos = new System.Numerics.Vector2((float)x + offset.X, (float)y + offset.Y);
            redBlockSprite.Draw(spriteBatch, drawPos, null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            redBlockSprite.Update(gametime);
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
