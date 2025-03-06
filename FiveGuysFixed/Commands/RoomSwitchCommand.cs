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
            Debug.WriteLine("Switched to next");
            GameState.roomManager.SwitchRoom(2); // Move to Room 2
        }

        public void Previous()
        {
            GameState.roomManager.SwitchRoom(1); // Move to Room 1
        }

    }
}
