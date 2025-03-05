using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public class Aquamentus : IEnemy
    {
        private ISprite aquamentusSprite;
        private ISprite aquamentusAttackSprite;
        private ISprite currentSprite;

        private double x, y;
        private int currentTime;
        private const int flightTime = 15, stillTime = 30;
        private double xAdjust, yAdjust;
        private List<IProjectile> projectiles;

        public Aquamentus(LoadItems items, int x, int y, List<IProjectile> projectiles)
        {
            aquamentusSprite = items.getNewItem(items.Aquamentus);
            aquamentusAttackSprite = items.getNewItem(items.AquamentusAttack);
            currentSprite = aquamentusSprite;

            this.x = x;
            this.y = y;
            this.projectiles = projectiles;
            this.currentTime = 0;
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
            currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch, new Vector2((float)x, (float)y), null);
        }

        private void SetAI()
        {
            var rnd = new System.Random();
            int decide = rnd.Next(1, 4);
            switch (decide)
            {
                case 1:
                    xAdjust = 0; yAdjust = 1;
                    currentSprite = aquamentusSprite;
                    break;
                case 2:
                    xAdjust = 0; yAdjust = -1;
                    currentSprite = aquamentusSprite;
                    break;
                case 3:
                    xAdjust = 0; yAdjust = 0;
                    currentSprite = aquamentusAttackSprite;
                    Attack();
                    break;
            }
        }

        private void Attack()
        {
            // Must ensure aquamentusAttackSprite.Texture is valid
            projectiles.Add(new Fireball(aquamentusAttackSprite.Texture, (float)x, (float)y - 20, new Vector2(-2, 0)));
            projectiles.Add(new Fireball(aquamentusAttackSprite.Texture, (float)x, (float)y, new Vector2(-2, 0)));
            projectiles.Add(new Fireball(aquamentusAttackSprite.Texture, (float)x, (float)y + 20, new Vector2(-2, 0)));
        }
    }
}
