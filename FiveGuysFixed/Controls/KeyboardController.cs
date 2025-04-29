using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Commands;
using FiveGuysFixed.Common;
using FiveGuysFixed.Config;
using FiveGuysFixed.GameStates;
using FiveGuys.Controls;
using System.Linq;

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

            HandleMovementKeys();
            HandleCombatKeys();
            HandleItemKeys();
            HandleEnemyKeys();
            HandleGameKeys();
            HandleAudioKeys();
            HandleBoomerangKeys();
            HandleBlockKeys();
            HandleDifficultyToggle();

            previousState = currentState;
        }

        private void HandleMovementKeys()
        {
            Keys[] movementKeys = { Keys.W, Keys.A, Keys.S, Keys.D, Keys.Up, Keys.Down, Keys.Left, Keys.Right };

            foreach (Keys mKey in movementKeys)
            {
                bool currentDown = currentState.IsKeyDown(mKey);
                bool previousDown = previousState.IsKeyDown(mKey);

                if (currentDown != previousDown)
                {
                    bool isKeyDown = currentDown;
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
        }

        private void HandleCombatKeys()
        {
            Keys[] combatKeys = { Keys.OemComma, Keys.E };
            foreach (Keys cKey in combatKeys)
            {
                bool currentDown = currentState.IsKeyDown(cKey);
                bool previousDown = previousState.IsKeyDown(cKey);

                if (!currentDown && previousDown)
                {
                    switch (cKey)
                    {
                        case Keys.OemComma:
                            if (GameState.PlayerState.heldWeapon == WeaponType.WOODSWORD ||
                                GameState.PlayerState.heldWeapon == WeaponType.WHITESWORD)
                            {
                                GameState.Player.Attack();
                            }
                            break;
                        case Keys.E:
                            break;
                    }
                }
            }
        }

        private void HandleItemKeys()
        {
            Keys[] itemKeys = { Keys.U, Keys.I, Keys.B };
            foreach (Keys iKey in itemKeys)
            {
                bool currentDown = currentState.IsKeyDown(iKey);
                bool previousDown = previousState.IsKeyDown(iKey);

                if (!currentDown && previousDown)
                {
                    switch (iKey)
                    {
                        case Keys.B:
                            var slot = GameState.EquippedB;
                            if (slot == null) break;

                            int count = slot.GetCount();
                            if (count <= 0)
                            {
                                GameState.EquippedB = null;
                                break;
                            }
                            switch (slot.Name)
                            {
                                case "Bomb":
                                    GameState.PendingBomb = true;
                                    GameState.PendingPos = GameState.PlayerState.position;
                                    GameState.PlayerState.health--;
                                    break;
                                case "Food":
                                    GameState.PlayerState.health++;
                                    break;
                            }
                            count--;
                            slot.SetCount(count);
                            if (count <= 0) GameState.EquippedB = null;
                            break;
                    }
                }
            }
        }

        private void HandleEnemyKeys()
        {
            Keys[] enemyKeys = { Keys.O, Keys.P };
            foreach (Keys eKey in enemyKeys)
            {
                bool currentDown = currentState.IsKeyDown(eKey);
                bool previousDown = previousState.IsKeyDown(eKey);

                if (!currentDown && previousDown)
                {
                    // Old sprint2 feature commented out
                }
            }
        }

        private void HandleGameKeys()
        {
            Keys[] gameKeys = { Keys.Q, Keys.R, Keys.Enter, Keys.C, Keys.L, Keys.X };
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
                                GameStateManager.SetState(new Inventory(game));
                            else if (GameStateManager.CurrentState is Inventory)
                                GameStateManager.SetState(new GamePlayState(game));
                            break;
                        case Keys.X:
                            if (GameStateManager.CurrentState is GamePlayState)
                                GameStateManager.SetState(new ShopState(game));
                            else if (GameStateManager.CurrentState is ShopState)
                                GameStateManager.SetState(new GamePlayState(game));
                            break;
                        case Keys.L:
                            GameState.darkMode = !GameState.darkMode;
                            Debug.WriteLine("Dark mode toggled: " + GameState.darkMode);
                            break;
                    }
                }
            }
        }

        private void HandleAudioKeys()
        {
            if (currentState.IsKeyDown(Keys.M) && !previousState.IsKeyDown(Keys.M))
            {
                game.isMuted = !game.isMuted;
                MediaPlayer.Volume = game.isMuted ? 0f : 0.5f;

                if (!game.isMuted && MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.Play(game.backgroundMusic);
                }
            }
        }

        private void HandleBoomerangKeys()
        {
            if (currentState.IsKeyDown(Keys.OemPeriod) && !previousState.IsKeyDown(Keys.OemPeriod))
            {
                ThrowBoomerang();
            }
        }

        private void HandleBlockKeys()
        {
            Keys[] blockKeys = { Keys.T, Keys.Y };
            foreach (Keys bKey in blockKeys)
            {
                bool currentDown = currentState.IsKeyDown(bKey);
                bool previousDown = previousState.IsKeyDown(bKey);

                if (!currentDown && previousDown)
                {
                    // Old sprint2 feature commented out
                }
            }
        }

        private void HandleDifficultyToggle()
        {
            if (currentState.IsKeyDown(Keys.F1) && !previousState.IsKeyDown(Keys.F1))
            {
                var dm = DifficultyManager.Instance;
                if (dm.CurrentDifficulty == GameDifficulty.Easy)
                {
                    dm.SetDifficulty(GameDifficulty.Hard);
                }
                else if (dm.CurrentDifficulty == GameDifficulty.Hard)
                {
                    dm.SetDifficulty(GameDifficulty.Hell);
                }
                else
                {
                    dm.SetDifficulty(GameDifficulty.Easy);
                }
                Debug.WriteLine("Difficulty set to: " + dm.CurrentDifficulty);
            }
        }

        private void ThrowBoomerang()
        {
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

            bool boomerangExists = GameState.roomManager.getCurrentRoom().Projectiles
                .Any(p => p is FiveGuysFixed.Projectiles.Boomerang);

            if (!boomerangExists)
            {
                try
                {
                    Texture2D weaponTexture = game.Content.Load<Texture2D>("linkSprite");
                    GameState.roomManager.getCurrentRoom().Projectiles.Add(
                        new FiveGuysFixed.Projectiles.Boomerang(weaponTexture, startPos.X, startPos.Y, velocity));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Boomerang creation error: " + ex.Message);
                }
            }
        }
    }
}
