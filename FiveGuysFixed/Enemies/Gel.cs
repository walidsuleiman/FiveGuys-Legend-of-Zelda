using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public class Gel : Enemy
    {
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private Random rnd;

        private int movementType = 0;
        private bool isHopping = false;

        public Gel(Vector2 position)
            : base(position, EnemySpriteFactory.Instance.CreateGelSprite(), 3.5f)
        {
            currentTime = 0;
            rnd = new Random();

            movementType = rnd.Next(3); 

            SetAI();
        }


        public override void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
            {
                switch (movementType)
                {
                    case 0: 
                        Position += velocity;
                        break;

                    case 1: 
                        float zigzag = (float)Math.Sin(currentTime * 0.2f) * 0.5f;
                        Vector2 zigzagDir = new Vector2(-velocity.Y, velocity.X);
                        if (zigzagDir != Vector2.Zero)
                            zigzagDir.Normalize();
                        Position += velocity + zigzagDir * zigzag;
                        break;

                    case 2: 
                        if (currentTime % 15 < 5) 
                        {
                            Position += velocity * 1.5f; 
                            isHopping = true;
                        }
                        else
                        {
                            Position += velocity * 0.7f; 
                            isHopping = false;
                        }
                        break;
                }
            }
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;


                if (rnd.Next(100) < 30) 
                {
                    movementType = rnd.Next(3);
                }

                SetAI();
            }

            currentTime++;
            x = (int)Position.X;
            y = (int)Position.Y;
            sprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float currentScale = scale;
            if (movementType == 2 && isHopping)
            {
                currentScale = scale * 1.2f; 
            }

            sprite.Draw(spriteBatch, Position, null, currentScale);
        }

        private void SetAI()
        {
            // use the difficulty-based AI system
            if (DifficultyManager.Instance.ShouldEnemiesTrackPlayer())
            {
                // calculate direction to player for Hard/Hell mode
                Vector2 direction = EnemyAI.GetMovementDirection(Position);
                float speed = EnemyAI.GetEnemySpeed();
                // set velocity based on direction and speed
                velocity = direction * speed;
            }
            else
            {
                // easy mode - original random movement
                int decide = rnd.Next(1, 5);
                float speed = EnemyAI.GetEnemySpeed();
                switch (decide)
                {
                    case 1: velocity = new Vector2(0, 1) * speed; break;
                    case 2: velocity = new Vector2(0, -1) * speed; break;
                    case 3: velocity = new Vector2(1, 0) * speed; break;
                    case 4: velocity = new Vector2(-1, 0) * speed; break;
                }
            }
        }
    }
}