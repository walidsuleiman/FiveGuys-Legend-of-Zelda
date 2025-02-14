using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Animation
{
    public class LinkWalkAnimation : Sprite
    {

        private Rectangle sourceRect;
        private Dir dir;

        public new void Draw(SpriteBatch _spriteBatch, Vector2 position)
        {

            Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, width, height);
            sourceRect = new Rectangle(spriteLocationX + gap * (currentFrame + 1) + width * currentFrame, spriteLocationY, width, height);


            if (facLeft)
            {
                _spriteBatch.Draw(texture, destRect, sourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(texture, destRect, sourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        public void pickUp()
        {
            spriteLocationX = 212;
        }

        public void facingDirection(Dir dir) 
        {
            switch (dir)
            {
                case Dir.UP:
                    spriteLocationX = 68;
                    break;
                case Dir.DOWN:
                    spriteLocationX = 0;
                    break;
                case Dir.LEFT:
                    spriteLocationX = 34;
                    facLeft = true;
                    break;
                case Dir.RIGHT:
                    spriteLocationX = 34;
                    facLeft = false;
                    break;
                default:
                    // Optional: handle unexpected values.
                    break;
            }
        }

        public LinkWalkAnimation()
        {
            frameTime = 0.4;
            totalFrames = 2;
            width = 16;
            height = 16;
            gap = 1;
            spriteLocationX = 0;
            spriteLocationY = 11;
            facLeft = false;
        }
    }
}
