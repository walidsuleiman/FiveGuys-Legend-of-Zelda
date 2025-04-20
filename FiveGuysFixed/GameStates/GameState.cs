using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using FiveGuysFixed.Items;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.RoomHandling;
using FiveGuysFixed.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    public class GameState
    {
        public static PlayerState PlayerState;
        //add game screen state (paused, titlescreen ect)
        //add enemy state
        //add block state
        public static Player Player;
        public static int WindowHeight;
        public static int WindowWidth;
        public static int currentRoomID = 1;
        public static RoomManager roomManager;
        public static int RoomID;
        //public static RoomContents currentRoomContents;  // Global RoomContents
        public static ContentLoader contentLoader;  // Global Loader

        public static bool IsTransitioning = false;
        public static float transitionX = 0f;
        public static Dir transitionDir;
        public static RoomContents previousRoomContents = new();
        public static TransitionManager transitionManager;

        public static bool ShouldSwitchRoom = false; // Default: No switch
        public static List<IItem> itemsToRemove = new List<IItem>();
        public static HashSet<IItem> collectedItems = new HashSet<IItem>();

        public static HUD.HUD HUD = new HUD.HUD(); // Global HUD

    }
}