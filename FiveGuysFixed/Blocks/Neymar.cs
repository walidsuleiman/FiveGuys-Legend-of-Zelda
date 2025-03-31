//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FiveGuysFixed.Animation;
//using FiveGuysFixed.Blocks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace FiveGuysFixed.Blocks
//{
//    internal class Neymar : IBlock
//    {
//        private Sprite blockSprite;
//        private double x, y;
//        private int currentTime;

//        public Neymar(Texture2D texture, int x, int y)
//        {
//            blockSprite = new Sprite(texture, 320, 0, 1320, 1080);

//            this.x = x;
//            this.y = y;
//            currentTime = 0;
//        }

//        public bool IsCollidable()
//        {
//            return true;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            float scale = 2;
//            blockSprite.Draw(spriteBatch, new System.Numerics.Vector2((float)x, (float)y), null, scale);
//        }

//        public void Update(GameTime gametime)
//        {
//            currentTime++;
//            blockSprite.Update(gametime);
//        }

//        public Rectangle BoundingBox
//        {
//            get
//            {
//                return new Rectangle((int)x, (int)y, 32, 32);
//            }
//            set
//            {
//                x = value.X;
//                y = value.Y;
//            }
//        }
//    }
//}