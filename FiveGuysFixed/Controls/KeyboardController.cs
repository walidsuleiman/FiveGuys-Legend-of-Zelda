using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FiveGuys.Controls;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Commands;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.LinkPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            Keys[] itemKeys = { Keys.U, Keys.I };
            Keys[] weaponKeys = { Keys.D1, Keys.D2, Keys.D3 };
            Keys[] enemyKeys = { Keys.O, Keys.P };
            Keys[] gameKeys = { Keys.Q, Keys.R, Keys.Enter };
            Keys[] blockKeys = { Keys.T, Keys.Y };
            Keys[] combatKeys = { Keys.N, Keys.E };



            foreach (Keys cKey in combatKeys)
            {
                bool currentDown = currentState.IsKeyDown(cKey);
                bool previousDown = previousState.IsKeyDown(cKey);

                // Determine if the key was just pressed or just released.
                if (!currentDown && previousDown)
                {
                    bool isKeyDown = currentDown;

                    switch (cKey)
                    {
                        case Keys.N:
                            game.Player.attack(GameState.PlayerState.heldWeapon);
                            break;
                        case Keys.E:
                            game.Player.takeDamage(15);
                            break;
                    }
                }
            }
            
            foreach (Keys gKey in gameKeys)
            {
                if (currentState.IsKeyDown(gKey))
                {
                    switch (gKey)
                    {
                        case Keys.Q:
                            game.Exit();
                            break;
                        case Keys.R:
                            game.Reset();
                            game.Player.Reset();
                            //game.Player.idle();
                            //game.enemies.Clear();
                            //game.projectiles.Clear();
                            //GameState.PlayerState.direction = Dir.DOWN;
                            break;
                        case Keys.Enter:
                            //Start Game
                            break;
                    }
                }
            }

            // Process movement keys using a switch statement.
            foreach (Keys mKey in movementKeys)
            {
                bool currentDown = currentState.IsKeyDown(mKey);
                bool previousDown = previousState.IsKeyDown(mKey);

                // Determine if the key was just pressed or just released.
                if (currentDown && !previousDown || !currentDown && previousDown)
                {
                    bool isKeyDown = currentDown; // true if just pressed, false if just released
                    switch (mKey)
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

            foreach (Keys eKey in enemyKeys)
            {
                bool currentDown = currentState.IsKeyDown(eKey);
                bool previousDown = previousState.IsKeyDown(eKey);

                // Determine if the key was just pressed or just released.
                if (!currentDown && previousDown)
                {
                    bool isKeyDown = currentDown; // true if just pressed, false if just released
                    switch (eKey)
                    {
                        case Keys.P:
                            new EnemySwitchCommand(game, true).Execute();
                            break;
                        case Keys.O:
                            new EnemySwitchCommand(game, false).Execute();
                            break;
                    }
                }

            }
   
            foreach (Keys iKey in itemKeys)
            {
                bool currentDown = currentState.IsKeyDown(iKey);
                bool previousDown = previousState.IsKeyDown(iKey);

                if (!currentDown && previousDown)
                {
                    bool isKeyDown = currentDown;
                    switch (iKey)
                    {
                        case Keys.U:
                            new ItemSwitchCommand(game, true).Execute();
                            break;
                        case Keys.I:
                            new ItemSwitchCommand(game, false).Execute();
                            break;
                    }
                }
            }

            foreach (Keys wKey in weaponKeys)
            {
                if (currentState.IsKeyDown(wKey))
                {
                    switch (wKey)
                    {
                        case Keys.D1:
                            new WeaponSwitchCommand(game, 1).Execute();
                            break;
                        case Keys.D2:
                            new WeaponSwitchCommand(game, 2).Execute();
                            break;
                        case Keys.D3:
                            new WeaponSwitchCommand(game, 3).Execute();
                            break;
                    }
                }
            }

            foreach (Keys bKey in blockKeys)
            {
                bool currentDown = currentState.IsKeyDown(bKey);
                bool previousDown = previousState.IsKeyDown(bKey);

                if (!currentDown && previousDown)
                {
                    bool isKeyDown = currentDown;
                    switch (bKey) 
                    {
                        case Keys.T:
                            new BlockSwitchCommand(game, true).Execute();
                            break;
                        case Keys.Y:
                            new BlockSwitchCommand(game, false).Execute();
                            break;
                    }

                }

            }
            previousState = currentState;
        }


    }
}
