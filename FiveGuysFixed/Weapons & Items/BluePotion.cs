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
    internal class BluePotion : Item
    {
        public BluePotion(Texture2D texture, int x, int y) : base(texture, 0, 0, x, y, 22, 37, 2.16f)
        {
        }
        public override void Use()
        {
            GameState.Player.Heal(1);
        }
    }
}
