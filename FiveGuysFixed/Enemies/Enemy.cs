using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.GameStates;


namespace FiveGuysFixed.Enemies
{
    public abstract class Enemy : IEnemy
    {
        public Vector2 Position { get; set; }
        protected ISprite sprite;
        public int x, y;
        int health = 1;

        public void TakeDamage(int damage)
        {
            health = health - damage;

            if (health < 0)
            {
                GameState.currentRoomContents.Enemies.Remove(this);
            }
        }

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
            Draw(spriteBatch, Vector2.Zero);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Vector2 drawPos = Position + offset;
            sprite.Draw(spriteBatch, drawPos, null);
        }

        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int) x, (int) y, 24, 24);
            }
            set
            {
                x = value.X;
                y = value.Y;
            }
        }


    }
}