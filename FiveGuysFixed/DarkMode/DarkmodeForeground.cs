using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.DarkMode
{
    internal class DarkmodeForeground
    {



        public DarkmodeForeground() { }

        public void update()
        {
            // Update logic for dark mode
        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(GameState.contentLoader.darkModeTexture, new Vector2(GameState.PlayerState.position.X - 4000, GameState.PlayerState.position.Y - 4000), Color.Black);

        }
    }
}
