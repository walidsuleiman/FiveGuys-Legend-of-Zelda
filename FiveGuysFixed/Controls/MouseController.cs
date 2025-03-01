using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuys.Controls;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.Controls
{
    public class MouseController : IController
    {
        private Game1 game;
        private MouseState previousState;
        private MouseState currentState;

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
                //Go to the previous room
            }

            else if (currentState.RightButton == ButtonState.Pressed)
            {
                // Go to the next room
                //This is a test to see if mouse input works:
                Console.WriteLine("Right Click");
                game.Exit();
            }

            previousState = currentState;

        }


    }
}
