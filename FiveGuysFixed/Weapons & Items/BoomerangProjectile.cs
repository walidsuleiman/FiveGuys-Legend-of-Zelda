using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Enemies;
using System;

namespace FiveGuysFixed.Projectiles
{
    public class Boomerang : IProjectile
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 originalVelocity;
        private ISprite sprite;
        private bool isFinished;
        private Goriya owner;

        // Boomerang flight parameters
        private const float MaxDistance = 100f;
        private const float ReturnSpeed = 1.2f;
        private Vector2 startPosition;
        private float distanceTraveled;
        private bool isReturning = false;

        public Boomerang(Texture2D texture, float x, float y, Vector2 velocity, Goriya owner)
        {
            this.texture = texture;
            this.position = new Vector2(x, y);
            this.velocity = velocity;
            this.originalVelocity = velocity;
            this.owner = owner;
            this.startPosition = position;

            this.sprite = new Sprite(texture, 16, 48, 16, 16, 3);

            isFinished = false;
            distanceTraveled = 0f;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            position += velocity;


            distanceTraveled = Vector2.Distance(startPosition, position);


            if (!isReturning && distanceTraveled >= MaxDistance)
            {

                isReturning = true;
                velocity = -velocity;
            }


            if (isReturning)
            {

                Vector2 ownerPosition = new Vector2();

                try
                {
                    //  reflection to access the private x and y fields
                    var xField = owner.GetType().GetField("x", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var yField = owner.GetType().GetField("y", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                    if (xField != null && yField != null)
                    {
                        double x = (double)xField.GetValue(owner);
                        double y = (double)yField.GetValue(owner);
                        ownerPosition = new Vector2((float)x, (float)y);
                    }
                    else
                    {
                        // fallback to start position if we can't get the owner position
                        ownerPosition = startPosition;
                    }
                }
                catch
                {
                    // fallback in case of any exception
                    ownerPosition = startPosition;
                }

                // calculate direction to owner
                Vector2 directionToOwner = ownerPosition - position;
                if (directionToOwner.Length() > 0)
                    directionToOwner.Normalize();


                velocity = directionToOwner * Math.Abs(originalVelocity.Length()) * ReturnSpeed;

                // check if boomerang has returned to owner
                if (Vector2.Distance(position, ownerPosition) < 10)
                {
                    isFinished = true;
                }
            }

            // animate the boomerang
            sprite.Update(gameTime);

            // check if the projectile is outside the screen bounds (safeguard)
            if (position.X < -50 || position.X > 1300 ||
                position.Y < -50 || position.Y > 750)
            {
                isFinished = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, null);
        }

        public bool IsFinished()
        {
            return isFinished;
        }
    }
}