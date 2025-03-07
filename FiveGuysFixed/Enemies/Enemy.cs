using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Sprites;

namespace FiveGuysFixed.Enemies
{
    public abstract class Enemy : IEnemy
    {
        public Vector2 Position { get; protected set; }
        protected ISprite sprite;

        public Enemy(Vector2 position, ISprite sprite)
        {
            this.Position = position;
            this.sprite = sprite;
        }

        public virtual void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position, null);
        }
    }
}