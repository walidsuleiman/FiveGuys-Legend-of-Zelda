using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace FiveGuysFixed.Animation
{
    public class LinkWalkAnimation : Sprite
    {

        private Rectangle sourceRect;
        private Dir dir;
        private double dmgTTL;
        private double healTTL;

        public void Draw(SpriteBatch _spriteBatch, Vector2? OverridePosition)
        {
            this.sourceRect = new Rectangle(spriteLocationX + gap * (currentFrame + 1) + width * currentFrame, spriteLocationY, width, height);

            Color color = Color.White;
            if (dmgTTL > 0) { color = Color.Red; }
            else if (healTTL > 0) { color = Color.SkyBlue; }

            Vector2 position = GameState.PlayerState.position;
            if (OverridePosition.HasValue)
            {
                position = OverridePosition.Value;
            }

            if (facLeft)
            {
                _spriteBatch.Draw(texture, position, sourceRect, color, 0, new Vector2(width / 2, height / 2), 2, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(texture, position, sourceRect, color, 0, new Vector2(width / 2, height / 2), 2, SpriteEffects.None, 0f);
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
            if (dmgTTL > 0)
            {
                dmgTTL -= gt.ElapsedGameTime.TotalSeconds;
            }
            if (healTTL > 0)
            {
                healTTL -= gt.ElapsedGameTime.TotalSeconds;
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
        public void takeDamage() 
        {
            dmgTTL = 0.5;
        }

        public void heal()
        {
            healTTL = 0.5;
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
            this.dmgTTL = 0;
        }
    }
}
