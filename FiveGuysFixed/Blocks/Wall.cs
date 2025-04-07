using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Blocks
{
    internal class Wall : IBlock
    {
        private Sprite blockSprite;
        private double x, y;
        private int currentTime;

        public Wall(Texture2D texture, int x, int y)
        {
            blockSprite = new Sprite(texture, 423, 224, 16, 16);

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
            spriteBatch.Draw(GameState.contentLoader.blockTexture, new Rectangle(0, 0, GameState.WindowWidth, GameState.WindowHeight), new Rectangle(521, 11, 256, 176), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            float scale = 2;
            var drawPos = new System.Numerics.Vector2((float)x, (float)y) + new System.Numerics.Vector2(offset.X, offset.Y);
            spriteBatch.Draw(GameState.contentLoader.blockTexture, new Rectangle(0, 0, GameState.WindowWidth, GameState.WindowHeight), new Rectangle(521, 11, 256, 176), Color.White);
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
                return new Rectangle((int)x, (int)y, 0, 0);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }
    }
}