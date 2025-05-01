using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Weapons___Items
{
    internal class Map : Item
    {

        public Map(Texture2D texture, int x, int y) : base(texture, 601, 156, x, y, 8, 16)
        {
        }
        public override void Use()
        {
            GameState.PlayerState.mapHeld = true;
        }

    }
}
