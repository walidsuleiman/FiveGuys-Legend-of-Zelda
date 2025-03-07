using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FiveGuysFixed.Weapons___Items;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Items;

namespace FiveGuysFixed.Collisions
{
    internal class CollisionHandler
    {
        public void HandleCollision(ICollidable a, ICollidable b)
        {

            if (a is IPlayer && b is IEnemy)
            {
            
                HandleEnemyPlayerCollision((IEnemy)b, (IPlayer)a);

            }
            else if (a is IPlayer && b is IBlock)
            {
                HandleBlockPlayerCollision((IBlock)b, (IPlayer)a);
            }

            else if (a is IPlayer && b is IItem)
            {
                HandleItemPlayerCollision((IItem)b, (IPlayer)a);
            }
            // Add more collision handling as needed
        }

        private void HandleEnemyPlayerCollision(IEnemy enemy, IPlayer player)
        {
            // Link taking damage
        }

        private void HandleBlockPlayerCollision(IBlock block, IPlayer player)
        {
            //Link Stop Moving
        }

        private void HandleItemPlayerCollision(IItem item, IPlayer player)
        {
            //Link picking up item
        }
    }
}