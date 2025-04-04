﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Items;

namespace FiveGuysFixed.LinkPlayer
{


    public class PlayerState
    {
        public Vector2 position;
        public bool isMoving;
        public WeaponType heldWeapon;
        public Dir direction;
        public bool isAttacking;
        public int health;
        public int greenRupees;
        public int redRupees;
        public int bombCount;
        public int foodCount;

        public Vector2 transitionOffset;

        public PlayerState(Vector2 position) 
        {
            this.position = position;
            this.isMoving = false;
            this.isAttacking = false;
            this.heldWeapon = WeaponType.NONE;
            this.direction = Dir.DOWN;
            this.health = 6;
            this.greenRupees = 0;
            this.redRupees = 0;

            this.transitionOffset = Vector2.Zero;
            this.bombCount = 0;
            this.foodCount = 0;
        }
    }
}
