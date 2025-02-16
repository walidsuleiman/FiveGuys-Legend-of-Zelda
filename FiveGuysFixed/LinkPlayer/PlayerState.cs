using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.LinkPlayer
{


    public class PlayerState
    {
        public Vector2 position;
        public bool isMoving;
        public WeaponType heldWeapon;
        public Dir direction;

        public PlayerState(Vector2 position) 
        {
            this.position = position;
            this.isMoving = false;
            this.heldWeapon = WeaponType.NONE;
            this.direction = Dir.DOWN;
        }
    }
}
