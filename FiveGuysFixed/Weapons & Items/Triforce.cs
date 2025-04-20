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
    internal class Triforce : Item
    {
        public Triforce(Texture2D texture, int x, int y) : base(texture, 0, 0, x, y, 96, 48, 2f)
        {

        }
        public override void Use()
        {
            GameStateManager.SetState(new WinState(GameState.Game));
        }
    }
}