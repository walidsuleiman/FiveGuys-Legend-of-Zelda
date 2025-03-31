using System;
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
            treeBlockSprite = new Sprite(texture, 820, 774, 16, 16);

            this.x = x;
            this.y = y;
            currentTime = 0;
        }

        public bool IsCollidable()
        {
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 2;
            treeBlockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            float scale = 2;
            var drawPos = new System.Numerics.Vector2((float)x + offset.X, (float)y + offset.Y);
            treeBlockSprite.Draw(spriteBatch, drawPos, null, scale);
        }

        public void Update(GameTime gametime)
        {
            currentTime++;
            treeBlockSprite.Update(gametime);
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
