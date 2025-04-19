using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
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
        private bool isLinkBoomerang;

        private const float MaxDistance = 300f;
        private const float ReturnSpeed = 1.2f;
        private Vector2 startPosition;
        private float distanceTraveled;
        private bool isReturning = false;
        private const float SCALE = 5f;

        public Boomerang(Texture2D texture, float x, float y, Vector2 velocity, Goriya owner)
        {
            this.texture = texture;
            this.position = new Vector2(x, y);
            this.velocity = velocity;
            this.originalVelocity = velocity;
            this.owner = owner;
            this.startPosition = position;
            this.isLinkBoomerang = false;

            this.sprite = new Sprite(texture, 16, 48, 16, 16, 3);

            isFinished = false;
            distanceTraveled = 0f;
        }

        // new constructor for Link's boomerang
        public Boomerang(Texture2D texture, float x, float y, Vector2 velocity)
        {
            this.texture = texture;
            this.position = new Vector2(x, y);
            this.velocity = velocity;
            this.originalVelocity = velocity;
            this.owner = null;
            this.startPosition = position;
            this.isLinkBoomerang = true;

            this.sprite = new Sprite(texture, 52, 185, 16, 16, 3);

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
                Vector2 returnTarget;

                if (isLinkBoomerang)
                {
                    //return to Link instead of Goriya
                    returnTarget = GameState.PlayerState.position;
                }
                else
                {

                    try
                    {

                        var xField = owner.GetType().GetField("x", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        var yField = owner.GetType().GetField("y", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                        if (xField != null && yField != null)
                        {
                            double x = (double)xField.GetValue(owner);
                            double y = (double)yField.GetValue(owner);
                            returnTarget = new Vector2((float)x, (float)y);
                        }
                        else
                        {

                            returnTarget = startPosition;
                        }
                    }
                    catch
                    {

                        returnTarget = startPosition;
                    }
                }


                Vector2 directionToTarget = returnTarget - position;
                if (directionToTarget.Length() > 0)
                    directionToTarget.Normalize();

                velocity = directionToTarget * Math.Abs(originalVelocity.Length()) * ReturnSpeed;


                if (Vector2.Distance(position, returnTarget) < 10)
                {
                    isFinished = true;
                }
            }


            sprite.Update(gameTime);


            if (position.X < -50 || position.X > 1300 ||
                position.Y < -50 || position.Y > 750)
            {
                isFinished = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, null, SCALE);
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
                position = new Vector2(value.X, value.Y);
            }
        }
        public Goriya Owner => owner;
        public bool IsLinkBoomerang => isLinkBoomerang;
    }
}