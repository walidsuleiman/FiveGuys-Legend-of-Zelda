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
    internal class Bomb : IItem
    {
        private Sprite bombSprite;
        private double x, y;
        private int currentTime;
        private float scale;

        public Bomb(Texture2D texture, int x, int y)
        {
            bombSprite = new Sprite(texture, 129, 185, 8, 16);

            this.x = x;
            this.y = y;
            currentTime = 0;
            scale = 2.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var drawPos = new System.Numerics.Vector2((float)x + offset.X, (float)y + offset.Y);
            bombSprite.Draw(spriteBatch, drawPos, null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            bombSprite.Update(gametime);
        }

        public Vector2 Position { get; set; }

        public void Use()
        {
            GameState.PlayerState.health--;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)x, (int)y, 16, 32);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }

    }
}
