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
            yellowBlockSprite = new Sprite(texture, 353, 80, 18, 18);

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
            yellowBlockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            yellowBlockSprite.Update(gametime);
        }
    }
}