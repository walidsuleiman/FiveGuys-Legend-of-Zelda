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
    internal class GreenBlock : IBlock
    {
        private Sprite greenBlockSprite;
        private double x, y;
        private int currentTime;

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
