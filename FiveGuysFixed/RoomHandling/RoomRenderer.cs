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
            //Debug.WriteLine(GameState.currentRoomContents.Enemies.Count);
            // Draw Blocks

            foreach (var block in GameState.currentRoomContents.Blocks)
            {
                block.Draw(spriteBatch);
                collisionHandler.HandlePlayerBlockCollision(GameState.Player, block);
            }

            // Draw Enemies
            foreach (var enemy in GameState.currentRoomContents.Enemies)
            {
                enemy.Draw(spriteBatch);
                collisionHandler.HandlePlayerEnemyCollision(GameState.Player, enemy);
            }

            // Draw Item
            foreach (var item in GameState.currentRoomContents.Items.ToList())
            {
                item.Draw(spriteBatch);
                collisionHandler.HandlePlayerItemCollision(GameState.Player, item);
            }

            foreach (var item in GameState.itemsToRemove)
            {
                if (GameState.currentRoomContents.Items.Contains(item))
                {
                    Debug.WriteLine("Item found and removed.");
                    GameState.currentRoomContents.Items.Remove(item);
                }
                else
                {
                    Debug.WriteLine("Item not found.");
                }
            }
            GameState.itemsToRemove.Clear();
        }
            // Draw Items
            //foreach (var item in gameState.CurrentRoomContents.Items)
            //{
            //    spriteBatch.Draw(item.Texture, new Vector2(item.X, item.Y), Color.White);
            //}

        public static void Update(GameTime gameTime)
        {
            // Update Enemies
            foreach (var enemy in GameState.currentRoomContents.Enemies)
            {
                enemy.Update(gameTime);
            }

            foreach (var item in GameState.currentRoomContents.Items)
            {
                item.Update(gameTime);
            }

            // Update Items
            //foreach (var item in gameState.CurrentRoomContents.Items)
            //{
            //    item.Update(gameTime);
            //}
        }
    }
}
