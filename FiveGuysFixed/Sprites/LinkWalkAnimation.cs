using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Animation
{
    public class LinkWalkAnimation : Sprite
    {

        private Rectangle sourceRect;
        private Dir dir;

        public void Draw(SpriteBatch _spriteBatch, Vector2? origin)
        {

            Rectangle destRect = new Rectangle((int)GameState.PlayerState.position.X, (int)GameState.PlayerState.position.Y, width, height);

           
                sourceRect = new Rectangle(spriteLocationX + gap * (currentFrame + 1) + width * currentFrame, spriteLocationY, width, height);


            //if (!origin.HasValue)
            //{
            //    origin = new Vector2(spriteLocationX + 8, spriteLocationY + 8);
            //}

            if (facLeft)
            {
                _spriteBatch.Draw(texture, GameState.PlayerState.position, sourceRect, Color.White, 0, new Vector2(width/2, height/2), 1, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(texture, GameState.PlayerState.position, sourceRect, Color.White, 0, new Vector2(width / 2, height / 2), 1, SpriteEffects.None, 0f);
            }
        }

        public new void Update(GameTime gt)
        {
            if (isAnimated)
            {
                    timeElapsed += gt.ElapsedGameTime.TotalSeconds;
                    if (timeElapsed >= frameTime)
                    {
                        timeElapsed -= frameTime;
                        currentFrame++;

                        if (currentFrame >= totalFrames)
                            currentFrame = 0;

                        // shift the sourceRect according to currentFrame
                        sourceRect.X = spriteLocationX + (width * currentFrame);
                    }
            }
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

        public void idle() 
        {
            isAnimated = false;
        }
        public void animate()
        {
            isAnimated = true;
        }

        public LinkWalkAnimation()
        {
            frameTime = 0.2;
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
