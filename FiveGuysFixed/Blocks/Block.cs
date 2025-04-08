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
    internal class Block : IBlock
    {
        private readonly Vector2 globalOffset = new Vector2(160, 160);
        private Vector2 scaleOffset;
        private Sprite blockSprite;
        private int width = 80, height = 80;
        private double x, y;
        private readonly float scale = 5;
        private int currentTime;

        public Block(Texture2D texture, int sourceX, int sourceY, int x, int y)
        {
            blockSprite = new Sprite(texture, sourceX, sourceY, 16, 16);
            this.x = x;
            this.y = y;
            currentTime = 0;
            scaleOffset = new Vector2(width / 2, height / 2);
        }

        public bool IsCollidable()
        {
            return true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Vector2.Zero);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var drawPos = new Vector2((float)x * width, (float)y * height) + offset + globalOffset - scaleOffset;
            blockSprite.Draw(spriteBatch, drawPos, null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            blockSprite.Update(gametime);
        }

        public Rectangle BoundingBox
        {
            get
            {
                Vector2 pos = new Vector2((float)x * width, (float)y * height) + globalOffset - scaleOffset;

                return new Rectangle((int)pos.X, (int)pos.Y, 80, 80);
            }
        }
    }
}
