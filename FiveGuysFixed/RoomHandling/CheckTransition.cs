using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.RoomHandling
{
    public class CheckTransition
    {
        public static void CheckRoomExit()
        {
            if (GameState.IsTransitioning) return;

            int roomWidth = GameState.WindowWidth;
            int roomHeight = GameState.WindowHeight;
            const int buffer = 5;

            Vector2 pos = GameState.PlayerState.position;
            int currentRoomId = GameState.currentRoomID;

            if (pos.X <= buffer)
            {
                if (GameState.roomManager.TryGetNeighborRoomID(currentRoomId, Dir.LEFT, out int neighborId))
                {
                    //GameState.PlayerState.position = new Vector2(roomWidth - buffer - 1, pos.Y);
                    GameState.transitionManager.Start(neighborId, Dir.LEFT);
                }
            }
            else if (pos.X >= roomWidth - buffer)
            {
                if (GameState.roomManager.TryGetNeighborRoomID(currentRoomId, Dir.RIGHT, out int neighborId))
                {
                    //GameState.PlayerState.position = new Vector2(buffer + 1, pos.Y);
                    GameState.transitionManager.Start(neighborId, Dir.RIGHT);
                }
            }
            else if (pos.Y <= buffer)
            {
                if (GameState.roomManager.TryGetNeighborRoomID(currentRoomId, Dir.UP, out int neighborId))
                {
                    //GameState.PlayerState.position = new Vector2(pos.X, roomHeight - buffer - 1);
                    GameState.transitionManager.Start(neighborId, Dir.UP);
                }
            }
            else if (pos.Y >= roomHeight - buffer)
            {
                if (GameState.roomManager.TryGetNeighborRoomID(currentRoomId, Dir.DOWN, out int neighborId))
                {
                    //GameState.PlayerState.position = new Vector2(pos.X, buffer + 1);
                    GameState.transitionManager.Start(neighborId, Dir.DOWN);
                }
            }
        }

    }
}
