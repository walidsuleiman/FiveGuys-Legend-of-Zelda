using System;
using Microsoft.Xna.Framework;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Config
{
    public enum GameDifficulty
    {
        Easy,
        Hard,
        Hell
    }

    public class DifficultyManager
    {
        private static DifficultyManager instance;

        public static DifficultyManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DifficultyManager();
                return instance;
            }
        }

        public GameDifficulty CurrentDifficulty { get; private set; } = GameDifficulty.Easy;

        private DifficultyManager() { }

        public void SetDifficulty(GameDifficulty difficulty)
        {
            CurrentDifficulty = difficulty;

            // Apply difficulty settings
            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    ApplyEasyMode();
                    break;
                case GameDifficulty.Hard:
                    ApplyHardMode();
                    break;
                case GameDifficulty.Hell:
                    ApplyHellMode();
                    break;
            }
        }

        private void ApplyEasyMode()
        {
            if (GameState.Game != null)
            {
                GameState.Game.AquamentusModeEnabled = false;
            }
        }

        private void ApplyHardMode()
        {
            if (GameState.Game != null)
            {
                GameState.Game.AquamentusModeEnabled = false;
            }
        }

        private void ApplyHellMode()
        {
            if (GameState.Game != null)
            {
                GameState.Game.AquamentusModeEnabled = true;

                // If we're in gameplay, apply the Aquamentus transformation
                if (GameStateManager.CurrentState is GamePlayState)
                {
                    GameState.Game.ReplaceAllEnemiesWithAquamentus();
                }
            }
        }

        public float GetEnemySpeedMultiplier()
        {
            switch (CurrentDifficulty)
            {
                case GameDifficulty.Easy:
                    return 0.7f;
                case GameDifficulty.Hard:
                    return 1.3f;
                case GameDifficulty.Hell:
                    return 1.5f;
                default:
                    return 1.0f;
            }
        }

        public bool ShouldEnemiesTrackPlayer()
        {
            return CurrentDifficulty != GameDifficulty.Easy;
        }
    }
}