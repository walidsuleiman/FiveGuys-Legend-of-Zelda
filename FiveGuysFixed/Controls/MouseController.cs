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
            //add mouse controls if needed
        }
    }
}
