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
    public class GamepadController : IController
    {
        private GamePadState previousState;
        private GamePadState currentState;
        private Game1 game;

        public GamepadController(Game1 game)
        {
            this.game = game;
            this.currentState = GamePad.GetState(PlayerIndex.One);
            this.previousState = GamePad.GetState(PlayerIndex.One);
        }

        public void Update()
        {
            currentState = GamePad.GetState(PlayerIndex.One);

            HandleMovement();
            HandleCombat();
            HandleItems();
            HandleGameControls();
            HandleAudioToggle();
            HandleBoomerang();
            HandleDifficultyToggle();

            previousState = currentState;
        }

        private void HandleMovement()
        {
            Vector2 thumbstick = currentState.ThumbSticks.Left;
            thumbstick.Y *= -1; // Invert Y axis for typical movement

            if (thumbstick.Length() > 0.5f)
            {
                if (Math.Abs(thumbstick.X) > Math.Abs(thumbstick.Y))
                {
                    if (thumbstick.X > 0)
                        new MovementCommand(game, Dir.RIGHT, true).Execute();
                    else
                        new MovementCommand(game, Dir.LEFT, true).Execute();
                }
                else
                {
                    if (thumbstick.Y > 0)
                        new MovementCommand(game, Dir.DOWN, true).Execute();
                    else
                        new MovementCommand(game, Dir.UP, true).Execute();
                }
            }
        }

        private void HandleCombat()
        {
            if (Pressed(Buttons.A))
            {
                if (GameState.PlayerState.heldWeapon == WeaponType.WOODSWORD ||
                    GameState.PlayerState.heldWeapon == WeaponType.WHITESWORD)
                {
                    GameState.Player.Attack();
                }
            }
        }

        private void HandleItems()
        {
            if (Pressed(Buttons.B))
            {
                var slot = GameState.EquippedB;
                if (slot == null) return;

                int count = slot.GetCount();
                if (count <= 0)
                {
                    GameState.EquippedB = null;
                    return;
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
            }
        }

        private void HandleGameControls()
        {
            if (Pressed(Buttons.Start))
            {
                if (GameStateManager.CurrentState is GamePlayState)
                    GameStateManager.SetState(new PauseState(game));
                else if (GameStateManager.CurrentState is PauseState)
                    GameStateManager.SetState(new GamePlayState(game));
            }

            if (Pressed(Buttons.Back))
            {
                game.Exit();
            }

            if (Pressed(Buttons.LeftShoulder))
            {
                if (GameStateManager.CurrentState is GamePlayState)
                    GameStateManager.SetState(new Inventory(game));
                else if (GameStateManager.CurrentState is Inventory)
                    GameStateManager.SetState(new GamePlayState(game));
            }

            if (Pressed(Buttons.RightShoulder))
            {
                if (GameStateManager.CurrentState is GamePlayState)
                    GameStateManager.SetState(new ShopState(game));
                else if (GameStateManager.CurrentState is ShopState)
                    GameStateManager.SetState(new GamePlayState(game));
            }
        }

        private void HandleAudioToggle()
        {
            if (Pressed(Buttons.Y))
            {
                game.isMuted = !game.isMuted;
                MediaPlayer.Volume = game.isMuted ? 0f : 0.5f;

                if (!game.isMuted && MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.Play(game.backgroundMusic);
                }
            }
        }

        private void HandleBoomerang()
        {
            if (Pressed(Buttons.X))
            {
                ThrowBoomerang();
            }
        }

        private void HandleDifficultyToggle()
        {
            if (Pressed(Buttons.RightTrigger))
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

        private bool Pressed(Buttons button)
        {
            return currentState.IsButtonDown(button) && !previousState.IsButtonDown(button);
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
