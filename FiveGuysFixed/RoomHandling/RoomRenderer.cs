using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace FiveGuysFixed.RoomHandling
{
    public class RoomRenderer
    {
        private static CollisionHandler collisionHandler = new CollisionHandler();

        public static void Draw(SpriteBatch spriteBatch)
        {
            // Draw Blocks
            foreach (var block in GameState.currentRoomContents.Blocks)
            {
                block.Draw(spriteBatch);
                //collisionHandler.HandlePlayerBlockCollision(GameState.Player, block);
            }

            // Draw Enemies
            foreach (var enemy in GameState.currentRoomContents.Enemies)
            {
                enemy.Draw(spriteBatch);
                //collisionHandler.HandlePlayerEnemyCollision(GameState.Player, enemy);
            }

            // Draw Item
            foreach (var item in GameState.currentRoomContents.Items.ToList())
            {
                item.Draw(spriteBatch);
                //collisionHandler.HandlePlayerItemCollision(GameState.Player, item);
            }
        }

        public static void Update(GameTime gameTime)
        {
            //Update Blocks
            foreach (var block in GameState.currentRoomContents.Blocks)
            {
                block.Update(gameTime);
                collisionHandler.HandlePlayerBlockCollision(GameState.Player, block);
            }

            // Update Enemies
            for (int i = GameState.currentRoomContents.Enemies.Count - 1; i >= 0; i--)
            {
                IEnemy enemy = GameState.currentRoomContents.Enemies[i];
                enemy.Update(gameTime);
                collisionHandler.HandlePlayerEnemyCollision(GameState.Player, enemy);
            }

            //Update Items
            for (int i = GameState.currentRoomContents.Items.Count - 1; i >= 0; i--)
            {
                IItem item = GameState.currentRoomContents.Items[i];
                item.Update(gameTime);
                collisionHandler.HandlePlayerItemCollision(GameState.Player, item);
            }

            // Remove collected items **AFTER** checking all items
            foreach (var item in GameState.itemsToRemove)
            {
                GameState.currentRoomContents.Items.Remove(item);
            }
            GameState.itemsToRemove.Clear();




        }
    }
}
