using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuys.Controls;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Commands;
using FiveGuysFixed.Common;
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

            // Array of keys we care about.
            Keys[] movementKeys = { Keys.W, Keys.A, Keys.S, Keys.D };

            // Process movement keys using a switch statement.
            foreach (Keys key in movementKeys)
            {
                bool currentDown = currentState.IsKeyDown(key);
                bool previousDown = previousState.IsKeyDown(key);

                // Determine if the key was just pressed or just released.
                if (currentDown && !previousDown || !currentDown && previousDown)
                {
                    bool isKeyDown = currentDown; // true if just pressed, false if just released
                    switch (key)
                    {
                        case Keys.W:
                            new MovementCommand(game, Dir.UP, isKeyDown).Execute();
                            break;
                        case Keys.A:
                            new MovementCommand(game, Dir.LEFT, isKeyDown).Execute();
                            break;
                        case Keys.S:
                            new MovementCommand(game, Dir.DOWN, isKeyDown).Execute();
                            break;
                        case Keys.D:
                            new MovementCommand(game, Dir.RIGHT, isKeyDown).Execute();
                            break;
                    }
                }
            }
            if (currentState.IsKeyDown(Keys.N))
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
                game.Player.Reset();
            }


            previousState = currentState;
        }


    }
}
