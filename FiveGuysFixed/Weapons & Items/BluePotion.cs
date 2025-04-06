using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Weapons___Items
{
    internal class BluePotion : IItem
    {
        private Sprite bluePotionSprite;
        private double x, y;
        private int currentTime;

        public BluePotion(Texture2D texture, int x, int y)
        {
            bluePotionSprite = new Sprite(texture, 0, 0, 22, 37);

            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var drawPos = new System.Numerics.Vector2((float)x + offset.X, (float)y + offset.Y);
            bluePotionSprite.Draw(spriteBatch, drawPos, null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            bluePotionSprite.Update(gametime);
        }

        public Vector2 Position { get; set; }

        public void Use()
        {
            GameState.Player.Heal(1);
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)x, (int)y, 22, 37);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }
    }
}
