using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Weapons___Items
{
    internal class RedRupee : IItem
    {
        private Sprite redRupeeSprite;
        private double x, y;
        private int currentTime;
        private float scale;

        public RedRupee(Texture2D texture, int x, int y)
        {
            redRupeeSprite = new Sprite(texture, 35, 58, 14, 26);

            this.x = x;
            this.y = y;
            currentTime = 0;
            scale = 1.5f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var drawPos = new System.Numerics.Vector2((float)x + offset.X, (float)y + offset.Y);
            redRupeeSprite.Draw(spriteBatch, drawPos, null);
        }


        public void Update(GameTime gametime)
        {
            currentTime++;
            redRupeeSprite.Update(gametime);
        }

        public Vector2 Position { get; set; }

        public void Use()
        {
            GameState.PlayerState.redRupees++;
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