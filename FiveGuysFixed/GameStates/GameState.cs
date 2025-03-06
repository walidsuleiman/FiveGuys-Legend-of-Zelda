using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.RoomHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    public class GameState
    {
        public static PlayerState PlayerState;
        //add game screen state (paused, titlescreen ect)
        //add enemy state
        //add block state
        public static int WindowHeight;
        public static int WindowWidth;
        public static RoomManager roomManager;
        public static int RoomID;
        public static CurrentRoomContents currentRoomContents;  // Global RoomContents
        public static ContentLoader contentLoader;  // Global Loader

    }
}
