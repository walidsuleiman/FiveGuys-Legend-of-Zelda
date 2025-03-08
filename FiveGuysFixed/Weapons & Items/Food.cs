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
    internal class Food : IItem
    {
        private Sprite foodSprite;
        private double x, y;
        private int currentTime;
        private float scale;

        public Food(Texture2D texture, int x, int y)
        {
            foodSprite = new Sprite(texture, 300, 185, 8, 16);

            this.x = x;
            this.y = y;
            currentTime = 0;
            scale = 2.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foodSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            foodSprite.Update(gametime);
        }

        public Vector2 Position { get; set; }

        public void Use()
        {
            GameState.PlayerState.health++;
        }
    }
}
