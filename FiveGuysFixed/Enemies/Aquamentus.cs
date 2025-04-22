using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Config;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.HUD;

namespace FiveGuysFixed.Enemies
{
    public class Aquamentus : Enemy
    {
        private ISprite aquamentusAttackSprite;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;
        private List<IProjectile> projectiles;

        public Aquamentus(Vector2 position, ISprite sprite, ISprite attackSprite, List<IProjectile> projectiles)
            : base(position, sprite, 5f)
        {
            this.health = 10;
            this.aquamentusAttackSprite = attackSprite;
            this.projectiles = projectiles;
            this.currentTime = 0;

        }

        public override void Update(GameTime gameTime)
        {
            if (currentTime < flightTime)
            {
                Position += new Vector2((float)xAdjust, (float)yAdjust);
            }
            else if (currentTime > flightTime + stillTime)
            {
                currentTime = -1;
                SetAI();
            }

            currentTime++;
            x = (int)Position.X;
            y = (int)Position.Y;
            sprite.Update(gameTime);
        }



        private void SetAI()
        {

            var rnd = new System.Random();
            int decide = rnd.Next(1, 4);
            switch (decide)
            {
                case 1:
                    xAdjust = 0; yAdjust = 1;
                    break;
                case 2:
                    xAdjust = 0; yAdjust = -1;
                    break;
                case 3:
                    xAdjust = 0; yAdjust = 0;
                    Attack();
                    break;
            }
        }
        private void Attack()
        {
            Fireball top = new Fireball(aquamentusAttackSprite.Texture, Position.X, Position.Y - 70, new Vector2(-2, 0));
            Fireball mid = new Fireball(aquamentusAttackSprite.Texture, Position.X, Position.Y, new Vector2(-2, 0));
            Fireball bot = new Fireball(aquamentusAttackSprite.Texture, Position.X, Position.Y + 70, new Vector2(-2, 0));

            projectiles.Add(top);
            projectiles.Add(mid);
            projectiles.Add(bot);
        }

        public override Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(Position.X - 32*2.5), (int)(Position.Y - 32*2.5), (int)(32*scale), (int)(32*scale));
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }

    }
}