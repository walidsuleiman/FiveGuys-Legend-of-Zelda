using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed
{
  public interface IItem
  {
        void Use();
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
        Vector2 Position { get; set; }

    }
}
