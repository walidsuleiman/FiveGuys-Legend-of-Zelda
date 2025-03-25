using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.GameStates
{
    public class GameOverState : IGameState
    {
        private Game1 game;

        public GameOverState(Game1 game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            var currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Enter))
            {
                GameState.Player.Reset();
                GameStateManager.SetState(new GamePlayState(game));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            game.GameDrawLogic(spriteBatch);

            spriteBatch.DrawString(
                GameState.contentLoader.DefaultFont,
                "        Game Over!\nPress Enter to Restart",
                new Vector2(450, 150),
                Color.Red
            );
        }
    }
}
