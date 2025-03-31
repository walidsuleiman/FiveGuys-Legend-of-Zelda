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
            int roomWidth = GameState.WindowWidth;
            int roomHeight = GameState.WindowHeight;
            const int buffer = 5;

            Vector2 pos = GameState.PlayerState.position;

            if (pos.X <= buffer)
            {
                GameState.roomManager.TrySwitchRoom(Dir.LEFT);
                GameState.PlayerState.position = new Vector2(roomWidth - buffer - 1, pos.Y);
            }
            else if (pos.X >= roomWidth - buffer)
            {
                GameState.roomManager.TrySwitchRoom(Dir.RIGHT);
                GameState.PlayerState.position = new Vector2(buffer + 1, pos.Y);
            }
            else if (pos.Y <= buffer)
            {
                GameState.roomManager.TrySwitchRoom(Dir.UP);
                GameState.PlayerState.position = new Vector2(pos.X, roomHeight - buffer - 1);
            }
            else if (pos.Y >= roomHeight - buffer)
            {
                GameState.roomManager.TrySwitchRoom(Dir.DOWN);
                GameState.PlayerState.position = new Vector2(pos.X, buffer + 1);
            }
        }


    }
}
