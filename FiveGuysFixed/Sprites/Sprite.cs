using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Sprites;
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
        protected bool facLeft;
        protected bool isAnimated;
        public float Scale { get; set; } = 1.0f;
        public Texture2D texture { get; private set; }
        public Vector2 origin;
        public Texture2D Texture => texture;
        Rectangle sourceRect;

        public Sprite(Texture2D texture, int x, int y, int width, int height, int frames = 1)
        {
            this.texture = texture;
            this.spriteLocationX = x;
            this.spriteLocationY = y;
            this.width = width;
            this.height = height;
            this.totalFrames = frames;
            this.currentFrame = 0;
            this.timeElapsed = 0;
            this.frameTime = 0.1;
            this.isAnimated = frames > 1;
            this.sourceRect = new Rectangle(spriteLocationX, spriteLocationY, width, height);
        }

        public Sprite() 
        {
            isAnimated = false;
            timeElapsed = 0;
            currentFrame = 0;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("linkSheet");
        }

        public void setPosition(Vector2 newPos)
        {
            GameState.PlayerState.position = newPos;
        }

        public void updateSourceRect(Rectangle newSourceRect)
        {
            sourceRect = newSourceRect;
        }

        public void Update(GameTime gt)
        {
            if (isAnimated)
            {
                timeElapsed += gt.ElapsedGameTime.TotalSeconds;
                if (timeElapsed >= frameTime)
                {
                    timeElapsed -= frameTime;
                    currentFrame = (currentFrame + 1) % totalFrames;

                    sourceRect.X = spriteLocationX + (width * currentFrame);
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, Vector2 position, Vector2? origin)
        {
            if (!origin.HasValue)
            {
                origin = new Vector2(width / 2, height / 2);
            }

            if (facLeft)
            {
                _spriteBatch.Draw(
                    texture,
                    position,
                    sourceRect,
                    Color.White,
                    0f,
                    origin.Value,
                    Scale,
                    SpriteEffects.FlipHorizontally,
                    0f
                );
            }
            else
            {
                _spriteBatch.Draw(
                    texture,
                    position,
                    sourceRect,
                    Color.White,
                    0f,
                    origin.Value,
                    Scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}