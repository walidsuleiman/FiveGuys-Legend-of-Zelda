using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.RoomHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.Commands
{
    public class RoomSwitchCommand
    {
        public void Next()
        {
            if (GameState.currentRoomID < 17)
            {
                GameState.ShouldSwitchRoom = true;
                Debug.WriteLine("Switched to next: " + GameState.currentRoomID);
                GameState.currentRoomID++; // Move to Room 2
                GameState.PlayerState.position = new Vector2(GameState.WindowWidth / 2, GameState.WindowHeight / 2);

            }
        }

        public void Previous()
        {
            if (GameState.currentRoomID > 1)
            {
                GameState.ShouldSwitchRoom = true;
                Debug.WriteLine("Switched to previous: " + GameState.currentRoomID);
                GameState.currentRoomID--; // Move to Room 1
                GameState.PlayerState.position = new Vector2(GameState.WindowWidth / 2, GameState.WindowHeight / 2);

            }

        }
    }
}