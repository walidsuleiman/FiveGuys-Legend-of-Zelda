using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.Items
{
    public class WhiteSword
    {
        private Texture2D texture;
        public Vector2 Position { get; set; }
        private Rectangle sourceRect;

        public WhiteSword(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            Position = position;
            sourceRect = new Rectangle(0, 0, 0, 0);
        }

        public void Use()
        {
            int damage = 15;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, sourceRect, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            // update
        }
    }
}