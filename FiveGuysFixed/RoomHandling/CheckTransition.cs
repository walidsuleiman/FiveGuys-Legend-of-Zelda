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

            int neighborId;

            if (pos.X <= buffer && (neighborId = GameState.roomManager.TryGetNeighborRoomID(Dir.LEFT)) > -1)
            {
                GameState.transitionManager.Start(neighborId, Dir.LEFT);
            }
            else if (pos.X >= roomWidth - buffer && (neighborId = GameState.roomManager.TryGetNeighborRoomID(Dir.RIGHT)) > -1)
            {
                GameState.transitionManager.Start(neighborId, Dir.RIGHT);
            }
            else if (pos.Y <= buffer && (neighborId = GameState.roomManager.TryGetNeighborRoomID(Dir.UP)) > -1)
            {
                GameState.transitionManager.Start(neighborId, Dir.UP);
            }
            else if (pos.Y >= roomHeight - buffer && (neighborId = GameState.roomManager.TryGetNeighborRoomID(Dir.DOWN)) > -1)
            {
                GameState.transitionManager.Start(neighborId, Dir.DOWN);
            }
        }

    }
}