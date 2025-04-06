using System;
using Microsoft.Xna.Framework;
using FiveGuysFixed.Common;
using FiveGuysFixed.Items;

namespace FiveGuysFixed.LinkPlayer
{
    // tracks player state like position, health, inventory, etc.
    public class PlayerState
    {
        public Vector2 position;
        public bool isMoving;
        public Dir direction;
        public Vector2 transitionOffset;

        public WeaponType heldWeapon;
        public bool isAttacking;
        public int health;

        public int greenRupees;
        public int redRupees;
        public int bombCount;
        public int foodCount;

        public PlayerState(Vector2 position)
        {
            InitializeMovementState(position);
            InitializeCombatState();
            InitializeInventory();
        }

        private void InitializeMovementState(Vector2 startPosition)
        {
            this.position = startPosition;
            isMoving = false;
            direction = Dir.DOWN;
            transitionOffset = Vector2.Zero;
        }

        private void InitializeCombatState()
        {
            isAttacking = false;
            heldWeapon = WeaponType.NONE;
            health = 6; // max health = 6
        }

        private void InitializeInventory()
        {
            greenRupees = 0;
            redRupees = 0;
            bombCount = 0;
            foodCount = 0;
        }
    }
}
