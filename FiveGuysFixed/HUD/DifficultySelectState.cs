using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.GameStates
{
    public class DifficultySelectState : IGameState
    {
        private Game1 game;
        private SpriteFont font;
        private int selectedIndex = 0;
        private KeyboardState previousKeyboardState;
        private List<string> difficultyOptions = new List<string>
        {
            "Easy Mode: Simple enemy movement",
            "Hard Mode: Enemies hunt you down",
            "HELL MODE: Link is in the dark and all enemies are Aquamentus",
            "Back to Title"
        };

        private Color normalColor = Color.White;
        private Color selectedColor = Color.Yellow;
        private Color hellModeColor = Color.Red;
        private Texture2D backgroundTexture;

        public DifficultySelectState(Game1 game)
        {
            this.game = game;
            previousKeyboardState = Keyboard.GetState();
        }

        public void LoadContent(ContentManager content)
        {
            font = GameState.contentLoader.DefaultFont;
            backgroundTexture = content.Load<Texture2D>("LOZTitle");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            // handle menu navigation
            if (currentKeyboard.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
            {
                selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : difficultyOptions.Count - 1;
            }
            else if (currentKeyboard.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
            {
                selectedIndex = (selectedIndex < difficultyOptions.Count - 1) ? selectedIndex + 1 : 0;
            }

            // handle selection
            if (currentKeyboard.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                switch (selectedIndex)
                {
                    case 0: // Easy Mode
                        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Easy);
                        GameStateManager.SetState(new TitleScreenState(game));
                        break;
                    case 1: // Hard Mode
                        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Hard);
                        GameStateManager.SetState(new TitleScreenState(game));
                        break;
                    case 2: // Hell Mode
                        DifficultyManager.Instance.SetDifficulty(GameDifficulty.Hell);
                        GameStateManager.SetState(new TitleScreenState(game));
                        break;
                    case 3: // Back to Title
                        GameStateManager.SetState(new TitleScreenState(game));
                        break;
                }
            }

            previousKeyboardState = currentKeyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw background with darker tint
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, GameState.WindowWidth, GameState.WindowHeight), new Color(100, 100, 100));

            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            spriteBatch.Draw(pixel, new Rectangle(0, 0, GameState.WindowWidth, GameState.WindowHeight), new Color(0, 0, 0, 150));

            // title
            string title = "SELECT DIFFICULTY";
            Vector2 titleSize = font.MeasureString(title);
            spriteBatch.DrawString(font, title,
                new Vector2(GameState.WindowWidth / 2 - titleSize.X / 2, 150),
                Color.White);

            //options
            for (int i = 0; i < difficultyOptions.Count; i++)
            {
                Color textColor = (i == selectedIndex) ? selectedColor : normalColor;

                // special color for Hell Mode
                if (i == 2) // Hell Mode option
                {
                    textColor = (i == selectedIndex) ? selectedColor : hellModeColor;
                }

                Vector2 textSize = font.MeasureString(difficultyOptions[i]);
                spriteBatch.DrawString(font, difficultyOptions[i],
                    new Vector2(GameState.WindowWidth / 2 - textSize.X / 2, 250 + i * 50),
                    textColor);
            }

            // Draw description for the selected option
            string description = GetDescriptionForOption(selectedIndex);
            Vector2 descSize = font.MeasureString(description);
            spriteBatch.DrawString(font, description,
                new Vector2(GameState.WindowWidth / 2 - descSize.X / 2, 450),
                Color.LightGray);
        }

        private string GetDescriptionForOption(int optionIndex)
        {
            switch (optionIndex)
            {
                case 0:
                    return "Enemies move randomly, perfect for beginners.";
                case 1:
                    return "Enemies actively chase you. A true challenge!";
                case 2:
                    return "Nightmarish difficulty! Every enemy is the dragon Aquamentus!";
                case 3:
                    return "Return to the title screen.";
                default:
                    return "";
            }
        }
    }
}