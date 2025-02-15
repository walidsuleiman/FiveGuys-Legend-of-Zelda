using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface ISprite
{
    Texture2D Texture { get; }  // capitalized property
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, Vector2 position);
}
