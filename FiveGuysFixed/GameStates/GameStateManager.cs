using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    public static class GameStateManager
    {
        private static IGameState currentState;
        private static ContentManager content;

        public static IGameState CurrentState => currentState;

        public static void Initialize(ContentManager contentManager)
        {
            content = contentManager;
        }
        public static void SetState(IGameState state)
        {
            currentState = state;
            currentState.LoadContent(content);
        }

        public static void SetState(IGameState state, ContentManager contentManager)
        {
            content = contentManager;
            SetState(state);
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

