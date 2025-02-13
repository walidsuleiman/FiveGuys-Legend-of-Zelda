using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace FiveGuys.Controls
{
    public class MouseController : IController
    {
        private Game1 game;
        private MouseState previousState;
        private MouseState currentState;

        private int windowWidth;
        private int windowHeight;

        public MouseController(Game1 game)
        {
            this.game = game;
            this.currentState = Mouse.GetState();
            this.previousState = Mouse.GetState();
        }

        public void Update()
        {
            currentState = Mouse.GetState();

            if (currentState.LeftButton == ButtonState.Pressed)
            {
                // Method call to handle left click
            }

            else if (currentState.RightButton == ButtonState.Pressed)
            {
                // Method call to handle right click
                //This is a test to see if mouse input works:
                Console.WriteLine("Right Click");
                game.Exit();
            }

            previousState = currentState;

        }


    }
}
