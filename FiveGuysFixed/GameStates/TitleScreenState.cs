using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.GameStates
{
    internal class TitleScreenState : IGameState
    {
        private Game1 game;
        private Texture2D backgroundTexture;
        private SpriteFont font;
        private Rectangle screen = new Rectangle(0, 0, 1280, 1000);

        // Menu options
        private List<string> menuOptions = new List<string>
        {
            "Start Game",
            "Select Difficulty",
            "Exit"
        };

        private int selectedIndex = 0;
        private KeyboardState previousKeyboardState;
        private Color selectedColor = Color.Yellow;
        private Color normalColor = Color.OrangeRed;

        public TitleScreenState(Game1 game)
        {
            this.game = game;
            previousKeyboardState = Keyboard.GetState();
        }

        public void LoadContent(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("LOZTitle");

            // Check if the contentLoader is initialized and has the DefaultFont
            if (GameState.contentLoader != null && GameState.contentLoader.DefaultFont != null)
            {
                font = GameState.contentLoader.DefaultFont;
            }
            else
            {
                // Load a default font directly if contentLoader.DefaultFont is not available
                try
                {
                    font = content.Load<SpriteFont>("DefaultFont");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Failed to load font: " + ex.Message);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            // Legacy support - Space to start with default difficulty
            if (currentKeyboard.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
            {
                GameStateManager.SetState(new GamePlayState(game));
            }

            // Handle menu navigation
            if (currentKeyboard.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
            {
                selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : menuOptions.Count - 1;
            }
            else if (currentKeyboard.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
            {
                selectedIndex = (selectedIndex < menuOptions.Count - 1) ? selectedIndex + 1 : 0;
            }

            // Handle selection
            if (currentKeyboard.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                switch (selectedIndex)
                {
                    case 0: // Start Game with current difficulty
                        GameStateManager.SetState(new GamePlayState(game));
                        break;
                    case 1: // Select Difficulty
                        GameStateManager.SetState(new DifficultySelectState(game));
                        break;
                    case 2: // Exit
                        game.Exit();
                        break;
                }
            }

            previousKeyboardState = currentKeyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw background
            spriteBatch.Draw(backgroundTexture, screen, Color.White);

            // Only draw text if font is loaded
            if (font != null)
            {
                // Draw title
                spriteBatch.DrawString(font, "Team FiveGuys", new Vector2(25, 40), Color.OrangeRed);

                // Draw menu options
                for (int i = 0; i < menuOptions.Count; i++)
                {
                    Color textColor = (i == selectedIndex) ? selectedColor : normalColor;
                    spriteBatch.DrawString(font, menuOptions[i], new Vector2(25, 100 + i * 40), textColor);
                }

                // Draw instructions
                spriteBatch.DrawString(font, "Use Arrow Keys to select, Enter to confirm", new Vector2(25, 250), Color.White);
                spriteBatch.DrawString(font, "Spacebar to Play with current settings!", new Vector2(25, 290), Color.White);

                // Display current difficulty
                string difficultyText = "Current Difficulty: " + DifficultyManager.Instance.CurrentDifficulty.ToString();
                spriteBatch.DrawString(font, difficultyText, new Vector2(25, 330), Color.White);
            }
        }
    }
}