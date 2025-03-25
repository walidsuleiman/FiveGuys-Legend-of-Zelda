using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    public interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
