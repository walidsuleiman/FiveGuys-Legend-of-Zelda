using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Enemies
{
    public class Goriya : Enemy
    {
        private ISprite goriyaSprite;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private Vector2 velocity;
        private List<IProjectile> projectiles;
        private int attackCooldown;
        private const int attackCooldownMax = 120;
        private Random rnd;
        private Texture2D boomerangTexture;

        public Goriya(Vector2 position, Texture2D enemyTexture, Texture2D boomerangTexture, List<IProjectile> projectiles) : base(position, new EnemySprite(enemyTexture, 16, 48, 16, 2))
        {
            goriyaSprite = new EnemySprite(enemyTexture, 16, 48, 16, 2); // Down sprite by default
            this.boomerangTexture = boomerangTexture;
            this.projectiles = projectiles;
            currentTime = 0;
            attackCooldown = 0;
            rnd = new Random();
            SetAI();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
            {
                Position += velocity;
            }
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;
                SetAI();
            }

            currentTime++;
            x = (int)Position.X;
            y = (int)Position.Y;

            if (attackCooldown > 0)
                attackCooldown--;

            if (attackCooldown <= 0 && projectiles != null && rnd.Next(100) < 1)
            {
                Attack();
                attackCooldown = attackCooldownMax;
            }

            goriyaSprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            goriyaSprite.Draw(spriteBatch, Position, null);
        }

        private void SetAI()
        {
            int decide = rnd.Next(1, 5);
            switch (decide)
            {
                case 1:
                    velocity = new Vector2(0, 1);
                    goriyaSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 16, 48, 16, 2); // Down
                    break;
                case 2:
                    velocity = new Vector2(0, -1);
                    goriyaSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 112, 48, 16, 2); // Up
                    break;
                case 3:
                    velocity = new Vector2(1, 0);
                    goriyaSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 48, 48, 16, 2); // Right
                    break;
                case 4:
                    velocity = new Vector2(-1, 0);
                    goriyaSprite = new EnemySprite(GameState.contentLoader.enemyTexture, 80, 48, 16, 2); // Left
                    break;
            }
        }

        private void Attack()
        {
            if (projectiles == null || boomerangTexture == null)
                return;

            //projectiles.Add(new Boomerang(boomerangTexture, Position.X, Position.Y, new Vector2(-3, 0), this));
        }
    }
}
