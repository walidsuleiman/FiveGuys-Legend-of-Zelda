using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Projectiles;
using System.Collections.Generic;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Enemies
{
    public class Goriya : IEnemy, ICollidable
    {
        private ISprite currentSprite;
        private ISprite leftGoriyaSprite;
        private ISprite rightGoriyaSprite;
        private ISprite upGoriyaSprite;
        private ISprite downGoriyaSprite;
        private double x, y;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;
        private Direction currentDirection;
        private List<IProjectile> projectiles;
        private int attackCooldown;
        private const int attackCooldownMax = 120; // 2 seconds at 60 fps
        private Random rnd;
        private Texture2D linkTexture; // Reference to the texture with boomerang sprite

        public double Rad { get { return Math.Max(currentSprite.Height, currentSprite.Width); } }

        public Vector2 position { get { return new Vector2((float)x, (float)y); } }

        CollisionType ICollidable.type => CollisionType.ENEMY;

        private enum Direction
        {
            Down,
            Up,
            Right,
            Left
        }

        public Goriya(LoadItems items, int x, int y, List<IProjectile> projectiles = null)
        {
            downGoriyaSprite = items.getNewItem(items.downGoriya);
            upGoriyaSprite = items.getNewItem(items.upGoriya);
            rightGoriyaSprite = items.getNewItem(items.rightGoriya);
            leftGoriyaSprite = items.getNewItem(items.leftGoriya);

            // Store reference to the texture with boomerang sprite
            linkTexture = items.boomerang.Texture;

            currentSprite = downGoriyaSprite;
            currentDirection = Direction.Down;

            this.x = x;
            this.y = y;
            this.projectiles = projectiles;
            this.currentTime = 0;
            this.attackCooldown = 0;
            this.rnd = new Random();

            SetAI(); // Initialize movement on creation
        }

        public void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
            {
                x += xAdjust;
                y += yAdjust;
            }
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;
                SetAI();
            }

            currentTime++;

            // Handle attack cooldown
            if (attackCooldown > 0)
                attackCooldown--;

            // Maybe randomly attack if cooldown is ready
            if (attackCooldown <= 0 && projectiles != null && rnd.Next(100) < 1) // 1% chance per frame
            {
                Attack();
                attackCooldown = attackCooldownMax;
            }

            currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
        }

        private void SetAI()
        {
            int decide = rnd.Next(1, 5);

            switch (decide)
            {
                case 1:
                    xAdjust = 0; yAdjust = 1;
                    currentDirection = Direction.Down;
                    currentSprite = downGoriyaSprite;
                    break;
                case 2:
                    xAdjust = 0; yAdjust = -1;
                    currentDirection = Direction.Up;
                    currentSprite = upGoriyaSprite;
                    break;
                case 3:
                    xAdjust = 1; yAdjust = 0;
                    currentDirection = Direction.Right;
                    currentSprite = rightGoriyaSprite;
                    break;
                case 4:
                    xAdjust = -1; yAdjust = 0;
                    currentDirection = Direction.Left;
                    currentSprite = leftGoriyaSprite;
                    break;
            }
        }

        public void Attack()
        {
            // Only attack if we have a projectiles list
            if (projectiles == null || linkTexture == null)
                return;

            Vector2 projectileVelocity = Vector2.Zero;

            // Set projectile direction based on Goriya's facing direction
            switch (currentDirection)
            {
                case Direction.Down:
                    projectileVelocity = new Vector2(0, 3);
                    break;
                case Direction.Up:
                    projectileVelocity = new Vector2(0, -3);
                    break;
                case Direction.Right:
                    projectileVelocity = new Vector2(3, 0);
                    break;
                case Direction.Left:
                    projectileVelocity = new Vector2(-3, 0);
                    break;
            }

            // Create a boomerang projectile using the correct sprite from linkTexture
            projectiles.Add(new Boomerang(linkTexture, (float)x, (float)y, projectileVelocity, this));
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            
        }
    }
}