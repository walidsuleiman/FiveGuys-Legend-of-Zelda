using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FiveGuys.Controls
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
                //move up
            }
            else if (currentState.IsKeyDown(Keys.A))
            {
                //move left
            }
            else if (currentState.IsKeyDown(Keys.S))
            {
                //move down
            }
            else if (currentState.IsKeyDown(Keys.D))
            {
                //move right
            }
            else if (currentState.IsKeyDown(Keys.Space))
            {
                //attack
            }
           //else if (currentState.IsKeyDown(Keys.Enter))
           // {
           //     //pause or menu
           // }
            else if (currentState.IsKeyDown(Keys.Q))
            {
                Console.WriteLine("Exiting Game");
                game.Exit();
            }
            else if (currentState.IsKeyDown(Keys.R))
            {
                //reset or restart
            }
            else if (currentState.IsKeyDown(Keys.E))
            {
                //interact or equip
            }


            previousState = currentState;
        }


    }
}
