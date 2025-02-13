using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Animation
{
    public class LinkWalkAnimation : Sprite
    {

        private Rectangle sourceRect;

        public new void Draw(SpriteBatch _spriteBatch, Vector2 position)
        {

            Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, width, height);
            sourceRect = new Rectangle(spriteLocationX + gap * (currentFrame + 1) + width * currentFrame, spriteLocationY, width, height);


            if (flipDirection)
            {
                _spriteBatch.Draw(texture, destRect, sourceRect, Color.White, 0, new Vector2(spriteLocationX, spriteLocationY), SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(texture, destRect, sourceRect, Color.White, 0, new Vector2(spriteLocationX, spriteLocationY), SpriteEffects.None, 0f);
            }
        }

        public void pickUp()
        {
            spriteLocationX = 212;
        }

        public void right()
        {
            spriteLocationX = 34;
        }

        public void up()
        {
            spriteLocationX = 68;
        }

        public void left()
        {
            spriteLocationX = 34;
            flipDirection = true;
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
            flipDirection = false;
        }
    }
}
