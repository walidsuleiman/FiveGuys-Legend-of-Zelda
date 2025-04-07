using System;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.RoomHandling
{
    public class TransitionManager
    {
        // Speed of transition (higher = faster)
        private const float TRANSITION_SPEED = 2.0f;

        // Update the transition effect
        public void Update(GameTime gameTime)
        {
            if (!GameState.IsTransitioning) return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Progress the transition
            GameState.transitionX += TRANSITION_SPEED * deltaTime;

            // Check if transition is complete
            if (GameState.transitionX >= 1.0f)
            {
                CompleteTransition();
            }
        }

        private void CompleteTransition()
        {
            // Finalize transition
            GameState.transitionX = 0.0f;
            GameState.IsTransitioning = false;
            GameState.PlayerState.transitionOffset = Vector2.Zero;

            // Make sure the player is properly positioned
            ClampPlayerPosition();
        }

        private void ClampPlayerPosition()
        {
            // Ensure player stays within the room boundaries
            float playerWidth = 16;  // Adjust as needed
            float playerHeight = 16; // Adjust as needed

            Vector2 position = GameState.PlayerState.position;

            position.X = MathHelper.Clamp(position.X,
                                          playerWidth + 200,
                                          GameState.WindowWidth - playerWidth - 200);
            position.Y = MathHelper.Clamp(position.Y,
                                          playerHeight + 82,
                                          GameState.WindowHeight - playerHeight - 82);

            GameState.PlayerState.position = position;
        }

        // Start a transition in the specified direction
        public void StartTransition(Dir direction)
        {
            GameState.transitionDir = direction;
            GameState.transitionX = 0.0f;
            GameState.IsTransitioning = true;
        }
    }
}