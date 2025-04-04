﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Blocks
{
    internal class Block : IBlock
    {
        private Sprite blockSprite;
        private double x, y;
        private int currentTime;

        public Block(Texture2D texture, int x, int y)
        {
            blockSprite = new Sprite(texture, 423, 224, 16, 16);

            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public bool IsCollidable()
        {
            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 2;
            blockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            float scale = 2;
            var drawPos = new System.Numerics.Vector2((float)x, (float)y) + new System.Numerics.Vector2(offset.X, offset.Y);
            blockSprite.Draw(spriteBatch, drawPos, null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            blockSprite.Update(gametime);
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)x, (int)y, 32, 32);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }
    }
}
