using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    public class GamePlayState : IGameState
    {
        private Game1 game;

        public GamePlayState(Game1 game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            game.GameUpdateLogic(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            game.GameDrawLogic(spriteBatch);
        }
    }
}
