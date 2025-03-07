using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Blocks
{
    internal class TreeBlock : IBlock, ICollidable
    {
        private Sprite treeBlockSprite;
        private double x, y;
        private int currentTime;

        public int Height { get { return this.Height; } }
        public int Width { get { return this.Width; } }

        public double Rad { get { return Math.Max(treeBlockSprite.Height, treeBlockSprite.Width); } }

        public Vector2 position => this.position;

        public CollisionType type => CollisionType.BLOCK;

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

        public void Update(GameTime gametime)
        {
            currentTime++;
            treeBlockSprite.Update(gametime);
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            
        }
    }
}
