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
    internal class RedRupee : IItem
    {
        private Sprite redRupeeSprite;
        private double x, y;
        private int currentTime;

        public RedRupee(Texture2D texture, int x, int y)
        {
            redRupeeSprite = new Sprite(texture, 35, 58, 14, 26);

            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            redRupeeSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            redRupeeSprite.Update(gametime);
        }

        public Vector2 Position { get; set; }

        public void Use()
        {
            int rupee = 2;
        }
    }
}