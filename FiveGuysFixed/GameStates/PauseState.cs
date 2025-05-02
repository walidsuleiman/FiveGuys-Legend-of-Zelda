using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace FiveGuysFixed.GameStates
{
    public class PauseState : IGameState
    {
        private Game1 game;

        public PauseState(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            // Load any content needed for the Pause state here
        }

        public void Update(GameTime gameTime)
        {
            if (game.IsKeyPress(Keys.Enter))
            {
                GameStateManager.SetState(new GamePlayState(game));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            game.GameDrawLogic(spriteBatch);
            spriteBatch.DrawString(
                GameState.contentLoader.DefaultFont,
                "       Game Paused\nPress Enter to Resume",
                new Vector2(450, 150),
                Color.Red
            );
        }
    }
}

