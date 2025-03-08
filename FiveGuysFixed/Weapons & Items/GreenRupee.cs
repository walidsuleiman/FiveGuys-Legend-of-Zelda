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
    internal class GreenRupee : IItem
    {
        private Sprite greenRupeeSprite;
        private double x, y;
        private int currentTime;
        private float scale;

        public GreenRupee(Texture2D texture, int x, int y)
        {
            greenRupeeSprite = new Sprite(texture, 35, 2, 14, 26);

            this.x = x;
            this.y = y;
            currentTime = 0;
            scale = 1.5f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            greenRupeeSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            greenRupeeSprite.Update(gametime);
        }

        public Vector2 Position { get; set; }

        public void Use()
        {
            int rupee = 1;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)x, (int)y, 21, 39);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }

    }
}