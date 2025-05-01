using System;
using FiveGuysFixed.Config;
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.Enemies
{
    public static class EnemyAI
    {
        private static readonly Random random = new Random();

        private static readonly Vector2[] directions8 =
        {
            new Vector2( 1, 0), new Vector2(-1, 0),
            new Vector2( 0, 1), new Vector2( 0,-1),
            new Vector2( 1, 1), new Vector2(-1, 1),
            new Vector2( 1,-1), new Vector2(-1,-1)
        };

        public static Vector2 GetRandomDirection(bool allowDiagonal = true)
        {
            var dir = directions8[random.Next(allowDiagonal ? 8 : 4)];
            dir.Normalize();
            return dir;
        }

        public static float JitterSpeed(float baseSpeed) =>
            baseSpeed * (0.95f + (float)random.NextDouble() * 0.1f);

        public static Vector2 GetOrbitDirection(Vector2 enemyPos, bool clockwise = true)
        {
            Vector2 toPlayer = GameState.PlayerState.position - enemyPos;
            if (toPlayer == Vector2.Zero) toPlayer = new Vector2(1, 0);
            toPlayer.Normalize();
            return clockwise ? new Vector2(toPlayer.Y, -toPlayer.X)
                             : new Vector2(-toPlayer.Y, toPlayer.X);
        }

        public static Vector2 GetMovementDirection(Vector2 enemyPosition)
        {
            Vector2 playerPosition = GameState.PlayerState.position;

            if (DifficultyManager.Instance.ShouldEnemiesTrackPlayer())
            {
                Vector2 direction = playerPosition - enemyPosition;
                if (direction != Vector2.Zero) direction.Normalize();

                if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hard)
                {
                    direction = direction * 0.8f + GetRandomDirection() * 0.2f;
                    if (direction != Vector2.Zero) direction.Normalize();
                }
                return direction;
            }
            return GetRandomDirection();
        }

        public static float GetEnemySpeed()
        {
            float baseSpeed = 1.0f;
            float speed = baseSpeed * DifficultyManager.Instance.GetEnemySpeedMultiplier();
            return JitterSpeed(speed);          
        }
    }
}
