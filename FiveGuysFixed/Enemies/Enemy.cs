using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Sprites;

namespace FiveGuysFixed.Enemies
{
    public abstract class Enemy : IEnemy
    {
        public Vector2 Position { get; set; }
        protected ISprite sprite;
        public int x, y;

        public Enemy(Vector2 position, ISprite sprite)
        {
            Position = position;
            this.x = (int) position.X;
            this.y = (int) position.Y;
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

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int) x, (int) y, 32, 32);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }
    }
}