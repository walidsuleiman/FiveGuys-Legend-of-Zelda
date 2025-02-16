using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.GameBlocks;
using FiveGuysFixed.GameBlocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameBlocks
{
    internal class Block : IBlock
    {
        private Sprite blockSprite;
        private double x, y;
        private int currentTime;

        public Block(Texture2D texture, int x, int y)
        {
            blockSprite = new Sprite(texture, 423, 223, 16, 16);

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
            blockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            blockSprite.Update(gametime);
        }
    }
}
