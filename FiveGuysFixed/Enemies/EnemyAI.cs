using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.Enemies
{
    public static class EnemyAI
    {
        private static Random random = new Random();

        public static Vector2 GetMovementDirection(Vector2 enemyPosition)
        {
            // get player position
            Vector2 playerPosition = GameState.PlayerState.position;

            // check if we should track the player based on difficulty
            if (DifficultyManager.Instance.ShouldEnemiesTrackPlayer())
            {
                // calculate direction to player
                Vector2 direction = playerPosition - enemyPosition;
                if (direction != Vector2.Zero)
                    direction.Normalize();

                if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hard)
                {
                    // Add slight randomness to direction (80% towards player, 20% random)
                    direction = direction * 0.8f + new Vector2(
                        (float)(random.NextDouble() * 2 - 1),
                        (float)(random.NextDouble() * 2 - 1)
                    ) * 0.2f;

                    if (direction != Vector2.Zero)
                        direction.Normalize();
                }

                return direction;
            }
            else
            {
                // easy mode - just move randomly
                return new Vector2(
                    (float)(random.NextDouble() * 2 - 1),
                    (float)(random.NextDouble() * 2 - 1)
                );
            }
        }

        public static float GetEnemySpeed()
        {
            // base speed adjusted by difficulty
            float baseSpeed = 1.0f;
            return baseSpeed * DifficultyManager.Instance.GetEnemySpeedMultiplier();
        }
    }
}