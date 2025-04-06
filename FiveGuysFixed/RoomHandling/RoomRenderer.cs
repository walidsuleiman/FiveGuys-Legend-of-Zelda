using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Common;
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
            int screenWidth = GameState.WindowWidth;
            int screenHeight = GameState.WindowHeight;

            if (!GameState.IsTransitioning)
            {
                DrawRoomContents(spriteBatch, GameState.currentRoomContents, Vector2.Zero);
                return;
            }

            float x = GameState.transitionX;
            int dx = (int)(x * screenWidth);
            int dy = (int)(x * screenHeight);

            Vector2 prevPos = Vector2.Zero;
            Vector2 currPos = Vector2.Zero;

            switch (GameState.transitionDir)
            {
                case Dir.LEFT:
                    prevPos = new Vector2(dx, 0);
                    currPos = new Vector2(dx - screenWidth, 0);
                    break;

                case Dir.RIGHT:
                    prevPos = new Vector2(-dx, 0);
                    currPos = new Vector2(screenWidth - dx, 0);
                    break;

                case Dir.UP:
                    prevPos = new Vector2(0, dy);
                    currPos = new Vector2(0, dy - screenHeight);
                    break;

                case Dir.DOWN:
                    prevPos = new Vector2(0, -dy);
                    currPos = new Vector2(0, screenHeight - dy);
                    break;
            }

            DrawRoomContents(spriteBatch, GameState.previousRoomContents, prevPos);
            DrawRoomContents(spriteBatch, GameState.currentRoomContents, currPos);

            if (GameState.IsTransitioning)
            {
                float progress = GameState.transitionX;

                Vector2 offset = Vector2.Zero;

                //Rename variable
                int moveLink = 0; 

                switch (GameState.transitionDir)
                {
                    case Dir.LEFT:
                        offset = new Vector2(progress * (screenWidth - moveLink), 0);
                        break;
                    case Dir.RIGHT:
                        offset = new Vector2(-progress * (screenWidth - moveLink), 0);
                        break;
                    case Dir.UP:
                        offset = new Vector2(0, progress * (screenHeight - moveLink));
                        break;
                    case Dir.DOWN:
                        offset = new Vector2(0, -progress * (screenHeight - moveLink));
                        break;
                }

                GameState.PlayerState.transitionOffset = offset;
            }
            else
            {
                GameState.PlayerState.transitionOffset = Vector2.Zero;
            }

            GameState.Player.Draw(spriteBatch);

        }



        private static void DrawRoomContents(SpriteBatch spriteBatch, CurrentRoomContents contents, Vector2 offset)
        {
            foreach (var block in contents.Blocks)
                block.Draw(spriteBatch, offset);

            foreach (var enemy in contents.Enemies)
                enemy.Draw(spriteBatch, offset);

            foreach (var item in contents.Items)
                item.Draw(spriteBatch, offset);
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
                
                foreach (var block in GameState.currentRoomContents.Blocks)
                {
                    block.Update(gameTime);
                    collisionHandler.HandleEnemyBlockCollision(enemy, block);
                }
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
