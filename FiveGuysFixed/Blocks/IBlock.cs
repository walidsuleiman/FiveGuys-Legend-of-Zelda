using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Blocks
{
    public interface IBlock
    {
        Rectangle BoundingBox { get; }

        bool IsCollidable();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Draw(SpriteBatch spriteBatch, Vector2 offset);

    }
}
