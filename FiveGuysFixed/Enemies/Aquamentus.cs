using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Enemies
{
    public class Aquamentus : Enemy
    {
        private Sprite aquamentusSprite;
        private Sprite aquamentusAttackSprite;
        private Sprite currentAquamentus;

        private double x, y;
        private int currentTime;
        private const int flightTime = 15;
        private const int stillTime = 30;
        private double xAdjust, yAdjust;

        private List<IProjectile> projectiles;// stores fireball attacks
        private Texture2D fireballTexture;  

        public Aquamentus(Texture2D texture, int x, int y, List<IProjectile> projectiles)
        {
            aquamentusSprite = new Sprite(texture, 64, 0, 32, 32, frames: 2);
            aquamentusAttackSprite = new Sprite(texture, 0, 0, 32, 32, frames: 2);

            currentAquamentus = aquamentusSprite;
            this.x = x;
            this.y = y;
            this.currentTime = 0;

            this.projectiles = projectiles;// reference to projectiles list in game1
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
                setAI();
            }

            currentTime++;
            currentAquamentus.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentAquamentus.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
        }

        private void setAI()
        {
            Random rnd = new Random();
            int decide = rnd.Next(1, 4);
            switch (decide)
            {
                case 1:
                    xAdjust = 0;
                    yAdjust = 1;
                    currentAquamentus = aquamentusSprite;
                    break;
                case 2:
                    xAdjust = 0;
                    yAdjust = -1;
                    currentAquamentus = aquamentusSprite;
                    break;
                case 3:
                    xAdjust = 0;
                    yAdjust = 0;
                    currentAquamentus = aquamentusAttackSprite;// switch to attack mode
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            //spawn 3 Fireballs moving left at different y offsets
            projectiles.Add(new Fireball(
     
                aquamentusAttackSprite.Texture,
                (float)x, (float)(y - 20),
                new Vector2(-2, 0)));

            projectiles.Add(new Fireball(
                aquamentusAttackSprite.Texture,
                (float)x, (float)y,
                new Vector2(-2, 0)));

            projectiles.Add(new Fireball(
                aquamentusAttackSprite.Texture,
                (float)x, (float)(y + 20),
                new Vector2(-2, 0)));
        }
    }
}
