using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FiveGuysFixed.Config;
namespace FiveGuysFixed.GameStates
{
    public class GamePlayState : IGameState
    {
        private Game1 game;

        public void LoadContent(ContentManager content)
        {
            // Load any content needed for the Game Play state here
        }
        public GamePlayState(Game1 game)
        {
            this.game = game;

            // Apply Hell Mode if active
            if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hell)
            {
                game.ReplaceAllEnemiesWithAquamentus();
            }
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
