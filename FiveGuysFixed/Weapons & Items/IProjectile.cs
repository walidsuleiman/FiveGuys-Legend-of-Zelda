using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Projectiles
{
    public interface IProjectile
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        bool IsFinished();
        bool isEnemyProjectile();
        Rectangle BoundingBox { get; set; }
    }
}
