using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.RoomHandling;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.Commands
{
    public class RoomSwitchCommand
    {
        public void Next()
        {
            if(GameState.currentRoomID < 4)
            {
                Debug.WriteLine("Switched to next");
                GameState.currentRoomID++; // Move to Room 2
            }
        }

        public void Previous()
        {
            if (GameState.currentRoomID > 1)
            {
                GameState.currentRoomID--; // Move to Room 1
            }

        }
    }
}