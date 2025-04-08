using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Weapons___Items
{
    internal class GreenRupee : Item
    {
        public GreenRupee(Texture2D texture, int x, int y) : base(texture, 35, 2, x, y, 14, 26, 2.5f)
        {
        }
        public override void Use()
        {
            GameState.PlayerState.greenRupees++;
        }

    }
}