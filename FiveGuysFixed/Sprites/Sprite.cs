using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace FiveGuysFixed.Animation
{
    public class Sprite : ISprite
    {
        protected int currentFrame;
        protected int totalFrames;
        protected int height;
        protected int width;
        protected int spriteLocationX;
        protected int spriteLocationY;
        protected int gap;
        protected double frameTime;
        protected double timeElapsed;
        protected bool flipDirection;
        public Texture2D texture { get; private set; }
        public Vector2 position { get; private set; }
        public Vector2 origin;
        Rectangle sourceRect;

        public void setPosition(Vector2 newPos)
        {
            position = newPos;
        }

        public void updateSourceRect(Rectangle newSourceRect) 
        { 
            sourceRect = newSourceRect;
        }

        public void Update(GameTime gt)
        {

            timeElapsed += gt.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= frameTime)
            {
                timeElapsed -= frameTime;
                currentFrame++;

                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
            }

        }

        public void Draw(SpriteBatch _spriteBatch, Vector2 position)
        {

            Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, width, height);
            //sourceRect = new Rectangle();


            if (flipDirection)
            {
                _spriteBatch.Draw(texture, destRect, sourceRect, Color.White, 0, new Vector2(spriteLocationX, spriteLocationY), SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(texture, destRect, sourceRect, Color.White, 0, new Vector2(spriteLocationX, spriteLocationY), SpriteEffects.None, 0f);
            }
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("linkSheet");
        }

        public Sprite()
        {

            timeElapsed = 0;
            currentFrame = 0;
        }
    }
}
