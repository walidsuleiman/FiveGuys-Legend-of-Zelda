using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Sprites
{
    public interface ISprite
    {
        Texture2D Texture { get; }
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2? origin);
        void Draw(SpriteBatch _spriteBatch, Vector2 position, Vector2? origin, float scale);
        int Height { get; }
        int Width { get; }
    }
}