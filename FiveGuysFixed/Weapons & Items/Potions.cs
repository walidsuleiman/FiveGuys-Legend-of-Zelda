//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;

//namespace FiveGuysFixed.Items
//{
//    public class Potion : IItem
//    {
//        private Texture2D texture;
//        public Vector2 Position { get; set; }
//        private bool isUsed;

//        public Potion(Texture2D texture, Vector2 position)
//        {
//            this.texture = texture;
//            Position = position;
//            isUsed = false;
//        }

//        public void Use()
//        {
//            if (!isUsed)
//            {
//                // How to restore health
//                isUsed = true;
//            }
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            if (!isUsed)
//            {
//                spriteBatch.Draw(texture, Position, Color.White);
//            }
//        }

//        public void Update(GameTime gameTime)
//        {
//            // Update
//        }
//    }
//}
