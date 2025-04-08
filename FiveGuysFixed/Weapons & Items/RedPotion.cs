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
    internal class RedPotion : Item
    {
        public RedPotion(Texture2D texture, int x, int y) : base(texture, 0, 0, x, y, 22, 37, 2.16f)
        {
        }
        public override void Use()
        {
            GameState.Player.TakeDamage(1);
        }
    }
}
