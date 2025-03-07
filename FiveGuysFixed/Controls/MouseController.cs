using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using FiveGuysFixed.Commands;
using FiveGuys.Controls;

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
            this.previousState = Mouse.GetState();
        }

        public void Update()
        {
            RoomSwitchCommand roomSwitch = new RoomSwitchCommand();
            previousState = currentState;
            currentState = Mouse.GetState();

            Debug.WriteLine("Mouse Controller Update");

            // Check for a left mouse click (debounced)
            if (previousState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed)
            {
                roomSwitch.Next();
            }

            // Check for a right mouse click (debounced)
            else if (previousState.RightButton == ButtonState.Released && currentState.RightButton == ButtonState.Pressed)
            {
                roomSwitch.Previous();
            }
        }
    }
}
