using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.GameStates;
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

            // Draw Enemies
            foreach (var item in GameState.currentRoomContents.Items)
            {
                item.Draw(spriteBatch);
            }

            // Draw Items
            //foreach (var item in gameState.CurrentRoomContents.Items)
            //{
            //    spriteBatch.Draw(item.Texture, new Vector2(item.X, item.Y), Color.White);
            //}

        }

        public static void update(GameTime gameTime)
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
