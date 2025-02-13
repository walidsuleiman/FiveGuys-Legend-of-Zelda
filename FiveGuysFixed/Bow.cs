using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed
{
    public class Bow : IItem
    {
        private Texture2D texture;
        public Vector2 Position { get; set; }

        public Bow(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
        }

        public void Use()
        {
            // How to use the bow
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            // Update
        }
    }
}
