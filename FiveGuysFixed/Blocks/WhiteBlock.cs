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
    internal class WhiteBlock : IBlock
    {
        private Sprite whiteBlockSprite;
        private double x, y;
        private int currentTime;

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
    }
}
