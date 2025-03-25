using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.GameStates
{
    public class WinState : IGameState
    {
        private Game1 game;

        public WinState(Game1 game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Enter))
            {
                GameStateManager.SetState(new GameOverState(game));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            game.GameDrawLogic(spriteBatch);

            spriteBatch.DrawString(
                GameState.contentLoader.DefaultFont,
                "      You Win!\nPress Enter to Continue",
                new Vector2(450,150),
                Color.Green
            );
        }
    }
}

