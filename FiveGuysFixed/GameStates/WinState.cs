using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FiveGuysFixed.GameStates
{
    public class WinState : IGameState
    {
        private Game1 game;
        private int frameIndex = 0;
        private List<Rectangle> linkAnimationFrames;
        private float animationTimer = 0f;
        private const float animationSpeed = 0.2f;
        private Texture2D triforceTexture;
        private Texture2D linkSpriteTexture;
        private const float playerScale = 8.0f;
        private const float triforceScale = 5.0f;

        public WinState(Game1 game)
        {
            this.game = game;
            linkAnimationFrames = new List<Rectangle>
        {
            new Rectangle(214, 11, 13, 16),
            new Rectangle(231, 11, 14, 16)
        };
        }

        public void LoadContent(ContentManager content)
        {
            triforceTexture = content.Load<Texture2D>("Triforce");
            linkSpriteTexture = content.Load<Texture2D>("linkSprite");

            ContentLoader.victorySound.Play();
        }

        public void Update(GameTime gameTime)
        {
            // Animation frame update
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer >= animationSpeed)
            {
                frameIndex = (frameIndex + 1) % linkAnimationFrames.Count;
                animationTimer = 0f;
            }

            if (game.IsKeyPress(Keys.R))
            {
                GameState.currentRoomID = 1;
                GameStateManager.SetState(new GamePlayState(GameState.Game));
            }

            if (game.IsKeyPress(Keys.Q))
            {
                game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameState.contentLoader.DefaultFont, "YOU WIN!", new Vector2(600, 200), Color.Red);
            Vector2 centerPosition = new Vector2(GameState.WindowWidth / 2 + 40, GameState.WindowHeight / 2 + 150);

            spriteBatch.Draw(linkSpriteTexture, centerPosition, linkAnimationFrames[frameIndex], Color.White, 0f, new Vector2(8, 8), playerScale, SpriteEffects.None, 0f);

            Vector2 triforcePosition = new Vector2(centerPosition.X - 240, centerPosition.Y - 300);
            spriteBatch.Draw(triforceTexture, triforcePosition, null, Color.White, 0f, Vector2.Zero, triforceScale, SpriteEffects.None, 0f);

            spriteBatch.DrawString(GameState.contentLoader.DefaultFont, "Press Q to Quit | Press R to Restart", new Vector2(400, GameState.WindowHeight - 50), Color.Black);
        }
    }
}

