using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Enemies
{
    public interface IEnemy
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Draw(SpriteBatch spriteBatch, Vector2 offset); // Add this overload
        Rectangle BoundingBox { get; }
        Vector2 Position { get; set; }
    }
}
