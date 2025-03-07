using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.HUD
{
    internal class Hearts : IBlock
    {
        private Sprite blockSprite;
        private double x, y;
        public int Height { get { return this.Height; } }
        public int Width { get { return this.Width; } }

        public Hearts(Texture2D texture, int x, int y)
        {
            blockSprite = new Sprite(texture, 0, 0, 300, 300);

            this.x = x;
            this.y = y;
        }

        public bool IsCollidable()
        {
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 0.15f;
            blockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }

        public void Update(GameTime gametime)
        {
            blockSprite.Update(gametime);
        }
    }
}
