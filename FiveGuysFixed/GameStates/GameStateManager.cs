using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    public static class GameStateManager
    {
        private static IGameState currentState;

        public static IGameState CurrentState => currentState;

        public static void SetState(IGameState state)
        {
            currentState = state;
        }

        public static void Update(GameTime gameTime)
        {
            currentState?.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            currentState?.Draw(spriteBatch);
        }

    }
}

