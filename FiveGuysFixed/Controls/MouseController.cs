using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuys.Controls;
using FiveGuysFixed.Commands;
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
        }

        public void Update()
        {
            RoomSwitchCommand roomSwitch = new RoomSwitchCommand();
            previousState = currentState;
            currentState = Mouse.GetState();

            Debug.WriteLine("Mouse Controller Update");

            if (currentState.LeftButton == ButtonState.Pressed)
            {
                roomSwitch.Next();
            }

            else if (currentState.RightButton == ButtonState.Pressed)
            {
                roomSwitch.Previous();
            }

        }

    }
}