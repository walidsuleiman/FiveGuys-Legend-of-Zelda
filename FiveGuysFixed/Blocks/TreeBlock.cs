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
    internal class TreeBlock : IBlock
    {
        private Sprite treeBlockSprite;
        private double x, y;
        private int currentTime;

        public TreeBlock(Texture2D texture, int x, int y)
        {
            treeBlockSprite = new Sprite(texture, 820, 774, 17, 15);

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
            treeBlockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            treeBlockSprite.Update(gametime);
        }
    }
}
