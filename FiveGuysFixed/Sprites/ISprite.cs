using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FiveGuysFixed.Sprites
{
    public interface ISprite
    {
        void Draw(SpriteBatch _spriteBatch, Vector2 position, Vector2? origin);
        void Update(GameTime gt);
        void LoadContent(ContentManager content);
    }
}
