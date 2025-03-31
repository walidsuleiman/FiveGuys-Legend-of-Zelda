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
    public class TransitionTracker
    {
        public static Vector2 GetTransitionOffset()
        {
            int screenWidth = GameState.WindowWidth;
            int screenHeight = GameState.WindowHeight;
            float x = GameState.transitionX;

            return GameState.transitionDir switch
            {
                Dir.LEFT => new Vector2(-x * screenWidth, 0),
                Dir.RIGHT => new Vector2(x * screenWidth, 0),
                Dir.UP => new Vector2(0, -x * screenHeight),
                Dir.DOWN => new Vector2(0, x * screenHeight),
                _ => Vector2.Zero
            };
        }
    }
}
