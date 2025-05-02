using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.LinkPlayer;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.Collisions
{
    public static class BoundingBoxExtension
    {
        public static Rectangle GetBoundingBox(this IPlayer player, int width, int height)
        {
            // Example using a player's position from GameState:
            return new Rectangle(
                (int)GameState.PlayerState.position.X,
                (int)GameState.PlayerState.position.Y,
                width,
                height
            );
        }
    }
}
