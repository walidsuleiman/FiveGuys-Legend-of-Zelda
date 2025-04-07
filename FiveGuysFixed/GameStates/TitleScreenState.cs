using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.GameStates
{
    internal class TitleScreenState : IGameState
    {
        private Game1 game;
        private Texture2D backgroundTexture;
        private SpriteFont font;
        private string titleText = "Five Guys: Legend of Zelda";
        private string startText = "Press the Spacebar to Start";
    public TitleScreenState(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("TitleScreenBG");
            font = content.Load<SpriteFont>("DefaultFont");


        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
               GameStateManager.SetState(new GamePlayState(game)); // Transition to the GamePlayState when space is pressed
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            if(backgroundTexture == null)
            {
                return;
            }
            // Draw the background
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);

            // Draw the title text
            Vector2 titlePosition = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 3);
            Vector2 titleOrigin = font.MeasureString(titleText) / 2;
            _spriteBatch.DrawString(font, titleText, titlePosition, Color.Black, 0, titleOrigin, 1.5f, SpriteEffects.None, 0.5f);

            // Draw the start text
            Vector2 startPosition = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            Vector2 startOrigin = font.MeasureString(startText) / 2;
            _spriteBatch.DrawString(font, startText, startPosition, Color.Black, 0, startOrigin, 1.0f, SpriteEffects.None, 0.5f);

        }

    }
    }
