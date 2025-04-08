﻿using System;
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
using Microsoft.Xna.Framework.Media;

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
            Keys[] movementKeys = { Keys.W, Keys.A, Keys.S, Keys.D, Keys.Up, Keys.Down, Keys.Left, Keys.Right };
            Keys[] itemKeys = { Keys.U, Keys.I };
            Keys[] weaponKeys = { Keys.D1, Keys.D2, Keys.D3 };
            Keys[] enemyKeys = { Keys.O, Keys.P };
            Keys[] gameKeys = { Keys.Q, Keys.R, Keys.Enter, Keys.C };
            Keys[] blockKeys = { Keys.T, Keys.Y };
            Keys[] combatKeys = { Keys.N, Keys.E };
            Keys[] audioKeys = { Keys.B };
            Keys[] boomerangKeys = { Keys.Z }; // New array for boomerang key

            //// Boomerang key handling
            //foreach (Keys bKey in boomerangKeys)
            //{
            //    bool currentDown = currentState.IsKeyDown(bKey);
            //    bool previousDown = previousState.IsKeyDown(bKey);

            //    // Check for key press (not release)
            //    if (currentDown && !previousDown)
            //    {
            //        // Call boomerang specific method
            //        ThrowBoomerang();
            //    }
            //}

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
                            // Only use for sword attack
                            if (GameState.PlayerState.heldWeapon == WeaponType.WOODSWORD ||
                                GameState.PlayerState.heldWeapon == WeaponType.WHITESWORD)
                            {
                                GameState.Player.Attack();
                            }
                            break;
                        case Keys.E:
                            GameState.Player.TakeDamage(1);
                            break;
                    }
                }
            }

            foreach (Keys gKey in gameKeys)
            {
                if (currentState.IsKeyDown(gKey) && !previousState.IsKeyDown(gKey))
                {
                    switch (gKey)
                    {
                        case Keys.Q:
                            game.Exit();
                            break;
                        case Keys.R:
                            GameState.Player.Reset();
                            break;
                        case Keys.Enter:
                            if (GameStateManager.CurrentState is GamePlayState)
                                GameStateManager.SetState(new PauseState(game));
                            else if (GameStateManager.CurrentState is PauseState)
                                GameStateManager.SetState(new GamePlayState(game));
                            break;
                        case Keys.C:
                            if (GameStateManager.CurrentState is GamePlayState)
                            {
                                GameStateManager.SetState(new Inventory(game));
                            }
                            else if (GameStateManager.CurrentState is Inventory)
                            {
                                GameStateManager.SetState(new GamePlayState(game));
                            }
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
                        case Keys.Up:
                            new MovementCommand(game, Dir.UP, isKeyDown).Execute();
                            break;
                        case Keys.A:
                        case Keys.Left:
                            new MovementCommand(game, Dir.LEFT, isKeyDown).Execute();
                            break;
                        case Keys.S:
                        case Keys.Down:
                            new MovementCommand(game, Dir.DOWN, isKeyDown).Execute();
                            break;
                        case Keys.D:
                        case Keys.Right:
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

            foreach (Keys audioKey in audioKeys)
            {
                bool currentDown = currentState.IsKeyDown(audioKey);
                bool previousDown = previousState.IsKeyDown(audioKey);

                if (currentDown && !previousDown)
                {
                    switch (audioKey)
                    {
                        case Keys.B:
                            game.isMuted = !game.isMuted;
                            if (game.isMuted)
                            {
                                //mute
                                MediaPlayer.Volume = 0f;
                            }
                            else
                            {
                                MediaPlayer.Volume = 0.5f;
                                if (MediaPlayer.State != MediaState.Playing)
                                {
                                    MediaPlayer.Play(game.backgroundMusic);
                                }
                            }
                            break;
                    }
                }
            }

            previousState = currentState;
        }

        private void ThrowBoomerang()
        {
            // Create boomerang velocity based on direction
            Vector2 velocity = Vector2.Zero;
            Vector2 startPos = GameState.PlayerState.position;

            switch (GameState.PlayerState.direction)
            {
                case Dir.UP:
                    velocity = new Vector2(0, -5);
                    startPos.Y -= 20;
                    break;
                case Dir.DOWN:
                    velocity = new Vector2(0, 5);
                    startPos.Y += 20;
                    break;
                case Dir.LEFT:
                    velocity = new Vector2(-5, 0);
                    startPos.X -= 20;
                    break;
                case Dir.RIGHT:
                    velocity = new Vector2(5, 0);
                    startPos.X += 20;
                    break;
            }

            // Check if there's already a boomerang
            bool boomerangExists = false;
            foreach (var projectile in GameState.currentRoomContents.Projectiles)
            {
                if (projectile is FiveGuysFixed.Projectiles.Boomerang)
                {
                    boomerangExists = true;
                    break;
                }
            }

            // Only throw if no boomerang exists
            if (!boomerangExists)
            {
                // Create and add boomerang
                try
                {
                    Texture2D weaponTexture = game.Content.Load<Texture2D>("linkSprite");
                    GameState.currentRoomContents.Projectiles.Add(
                        new FiveGuysFixed.Projectiles.Boomerang(
                            weaponTexture,
                            startPos.X,
                            startPos.Y,
                            velocity
                        )
                    );
                }
                catch (Exception ex)
                {
                    // Handle exception (texture not found, etc.)
                    System.Diagnostics.Debug.WriteLine("Boomerang creation error: " + ex.Message);
                }
            }
        }
    }
}