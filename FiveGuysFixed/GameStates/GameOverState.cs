using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace FiveGuysFixed.GameStates
{
    public class GameOverState : IGameState
    {
        private Game1 game;

        public GameOverState(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content) { }

        public void Update(GameTime gameTime)
        {
            if (game.IsKeyPress(Keys.R))
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
                "        Game Over!\nPress R to Restart",
                new Vector2(450, 150),
                Color.Red
            );
        }
    }
}
