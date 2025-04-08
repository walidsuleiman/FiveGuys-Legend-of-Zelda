using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Weapons___Items
{
    internal abstract class Item : IItem
    {
        private readonly Vector2 globalOffset = new Vector2(160, 160);
        private Vector2 scaleOffset;
        private Sprite sprite;
        private int width, height;
        private double x, y;
        private float scale = 5;
        private int currentTime;

        public Item(Texture2D texture, int sourceX, int sourceY, int x, int y, int width, int height)
        {
            sprite = new Sprite(texture, sourceX, sourceY, width, height);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            currentTime = 0;
            scaleOffset = new Vector2((width*scale) / 2, (height*scale) / 2);
        }

        public Item(Texture2D texture, int sourceX, int sourceY, int x, int y, int width, int height, float scale)
        {
            sprite = new Sprite(texture, sourceX, sourceY, width, height);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.scale = scale;
            currentTime = 0;
            scaleOffset = new Vector2((width * scale) / 2, (height * scale) / 2);
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var drawPos = new Vector2((float)x * 80, (float)y * 80) + offset + globalOffset - scaleOffset;
            sprite.Draw(spriteBatch, drawPos, null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            sprite.Update(gametime);
        }

        public virtual void Use()
        {

            // Implement item usage logic here
        }

        public Rectangle BoundingBox
        {
            get
            {
                Vector2 pos = new Vector2((float)x * 80, (float)y * 80) + globalOffset - scaleOffset;

                return new Rectangle((int)pos.X, (int)pos.Y, (int)(width*scale), (int)(height * scale));
            }
        }
    }
}
