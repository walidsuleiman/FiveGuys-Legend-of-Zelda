using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Items
{
    public interface IItem
    {
        Rectangle BoundingBox { get; set; }
        void Use();
        void Draw(SpriteBatch spriteBatch);
        void Draw(SpriteBatch spriteBatch, Vector2 offset);
        void Update(GameTime gameTime);
        Vector2 Position { get; set; }

    }
}
