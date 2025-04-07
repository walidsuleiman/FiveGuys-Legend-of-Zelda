using FiveGuysFixed.GameStates;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.RoomHandling
{
    public class TransitionManager
    {
        private const float TRANSITION_DURATION = 1.5f; // Duration in seconds
        private float timer = 0f;
        private bool active = false;
        private int destinationRoomId;
        private Dir direction;

        public void Start(int destinationRoomId, Dir dir)
        {
            this.destinationRoomId = destinationRoomId;
            this.direction = dir;
            timer = 0f;
            active = true;

            GameState.IsTransitioning = true;
            GameState.transitionX = 0f;
            GameState.transitionDir = dir;

            // Copy current room contents into "previous"
            GameState.previousRoomContents.Clear();
            GameState.previousRoomContents.Blocks.AddRange(GameState.currentRoomContents.Blocks);
            GameState.previousRoomContents.Enemies.AddRange(GameState.currentRoomContents.Enemies);
            GameState.previousRoomContents.Items.AddRange(GameState.currentRoomContents.Items);

            // Load next room into currentRoomContents
            GameState.roomManager.SwitchRoom(destinationRoomId);
        }

        public void Update(GameTime gameTime)
        {
            if (!active) return;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameState.transitionX = MathHelper.Clamp(timer / TRANSITION_DURATION, 0f, 1f);

            if (GameState.transitionX >= 1f)
            {
                EndTransition();
            }
        }

        private void EndTransition()
        {
            GameState.IsTransitioning = false;
            GameState.transitionX = 0f;
            active = false;

            GameState.currentRoomID = destinationRoomId;
            GameState.PlayerState.position += GameState.PlayerState.transitionOffset;
            GameState.PlayerState.transitionOffset = Vector2.Zero;

        }

    }
}