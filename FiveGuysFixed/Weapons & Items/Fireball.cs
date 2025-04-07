using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Projectiles;    
using FiveGuysFixed.Animation;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Projectiles
{
    public class Fireball : IProjectile
    {
        private Sprite fireballSprite;
        private Vector2 position;
        private Vector2 velocity;
        private bool isFinished;
        private int currentTime;

        private const int flightTime = 70; // move for 70 frames
        private const int stillTime = 20; // wait 20 frames, then vanish

        public Fireball(Texture2D texture, float startX, float startY, Vector2 velocity)
        {
            fireballSprite = new Sprite(texture, 32,32, 16, 16);
            this.position = new Vector2(startX, startY);
            this.velocity = velocity;
            this.isFinished = false;
            this.currentTime = 0; 
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


            fireballSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            fireballSprite.Draw(spriteBatch, position, null);
        }

        public bool IsFinished()
        {
            return isFinished;
        }
    }
}
