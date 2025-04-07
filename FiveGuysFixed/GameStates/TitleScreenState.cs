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
        private Rectangle screen = new Rectangle(0, 0, 1280, 1000);
    public TitleScreenState(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("LOZTitle");
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
            _spriteBatch.Draw(backgroundTexture, screen, Color.White); // Draw the background texture

            _spriteBatch.DrawString(GameState.contentLoader.DefaultFont, "Team FiveGuys", new Vector2(25, 40), Color.OrangeRed);
            _spriteBatch.DrawString(GameState.contentLoader.DefaultFont, "Spacebar to Play!", new Vector2(25, 70), Color.OrangeRed);
        }

    }
    }
