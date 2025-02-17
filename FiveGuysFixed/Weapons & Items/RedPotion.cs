using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.Weapons___Items
{
    internal class RedPotion : IItem
    {
        private Sprite redPotionSprite;
        private double x, y;
        private int currentTime;

        public RedPotion(Texture2D texture, int x, int y)
        {
            redPotionSprite = new Sprite(texture, 0, 0, 22, 37);

            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            redPotionSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            redPotionSprite.Update(gametime);
        }

        public Vector2 Position { get; set; }

        public void Use()
        {
            int heal = -10;
        }
    }
}
