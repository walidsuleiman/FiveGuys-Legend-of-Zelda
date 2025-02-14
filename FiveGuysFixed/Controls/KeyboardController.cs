using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuys.Controls;
using FiveGuysFixed.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.Controls
{
    public class KeyboardController : IController
    {
        private KeyboardState previousState;
        private KeyboardState currentState;
        private Game1 game;

        public KeyboardController(Game1 game)
        {
            this.game = game;
            this.currentState = Keyboard.GetState();
            this.previousState = Keyboard.GetState();

        }

        public void Update()
        {
         
            currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.W))
            {

            }
            else if (currentState.IsKeyDown(Keys.A))
            {
                //linkMoveLeft();
            }
            else if (currentState.IsKeyDown(Keys.S))
            {
                //linkMoveDown();
            }
            else if (currentState.IsKeyDown(Keys.D))
            {
                //linkMoveRight();
            }
            else if (currentState.IsKeyDown(Keys.N))
            {
                //useCurrentItem();
            }
            else if (currentState.IsKeyDown(Keys.M))
            {
                //switchItem();
            }
            else if (currentState.IsKeyDown(Keys.D0) || currentState.IsKeyDown(Keys.NumPad0))
            {
                Console.WriteLine("Exiting Game");
                game.Exit();
            }
            else if (currentState.IsKeyDown(Keys.R))
            {
                //game.Reset();
            }


            previousState = currentState;
        }


    }
}
