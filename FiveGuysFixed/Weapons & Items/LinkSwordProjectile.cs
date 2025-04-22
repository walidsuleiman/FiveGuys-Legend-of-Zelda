using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Animation;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Projectiles
{
    public class LinkSwordProjectile : IProjectile
    {
        private Sprite sprite;
        private Vector2 position;
        private Vector2 velocity;
        private bool isFinished;
        private int currentTime;
        private Dir dir;

        private const int flightTime = 70; // move for 70 frames
        private const int stillTime = 20; // wait 20 frames, then vanish

        public LinkSwordProjectile(Texture2D texture, float startX, float startY, Dir dir)
        {
            this.position = new Vector2(startX, startY);
            this.isFinished = false;
            this.currentTime = 0;
            this.dir = dir;

            // Initialize sprite based on direction
            switch(dir)
            {
                case Dir.UP:
                    sprite = new Sprite(texture, 1, 154, 8, 16, 1); // Adjust sprite location for UP
                    velocity = new Vector2(0, -5); // Move up
                    break;
                case Dir.DOWN:
                    sprite = new Sprite(texture, 1, 154, 8, 16, 1); // but flip it vertically somehow
                    velocity = new Vector2(0, 5); // Move down
                    break;
                case Dir.LEFT:
                    sprite = new Sprite(texture, 10, 154, 16, 16, 1); // but flip it horizontally somehow
                    velocity = new Vector2(-5, 0); // Move left
                    break;
                case Dir.RIGHT:
                    sprite = new Sprite(texture, 10, 154, 16, 16, 1); // Adjust sprite location for RIGHT
                    velocity = new Vector2(5, 0); // Move right
                    break;
            }

        }

        public bool isEnemyProjectile()
        {
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
            {
                position += velocity;
            }
            else if (currentTime >= flightTime + stillTime)
            {
                isFinished = true;// mark fireball for removal
            }
            currentTime++;

            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (dir == Dir.LEFT) 
            {
                sprite.Draw(spriteBatch, position, null, 5.0f, SpriteEffects.FlipHorizontally);
            }
            else if (dir == Dir.DOWN) 
            {
                sprite.Draw(spriteBatch, position, null, 5.0f, SpriteEffects.FlipVertically);
            } else 
            { 
                sprite.Draw(spriteBatch, position, null, 5.0f);
            }
        }

        public bool IsFinished()
        {
            return isFinished;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            }
            set
            {
                position.X = value.X;
                position.Y = value.Y;
            }
        }
    }
}
