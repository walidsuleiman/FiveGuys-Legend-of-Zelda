using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using FiveGuysFixed.GUI;
using FiveGuysFixed.HUD;
using FiveGuysFixed.Weapons___Items;
using Microsoft.Xna.Framework.Content;

namespace FiveGuysFixed.GameStates
{
    public class Inventory : IGameState
    {
        private Game1 game;
        private Texture2D whitePixel;
        private SpriteFont font;
        private Hearts hearts;
        private RupeeCount rupees;
        private MiniMap miniMap;
        private Dictionary<string, int> itemCounts;
        private List<KeyValuePair<string, int>> itemList;
        private int selectedIndex = 0;
        private int columns = 2;
        private int slotWidth = 250;
        private int slotHeight = 150;
        private int startX = 100;
        private int startY = 200;
        private Texture2D blackPixel;
        private KeyboardState oldState;
        private Vector2 cachedPlayerPos;
        private int cachedRoomID;
        public Inventory(Game1 game)
        {
            this.game = game;
            hearts = new Hearts();
            rupees = new RupeeCount();
            oldState = Keyboard.GetState();

            var ps = GameState.PlayerState;
            if (ps != null)
            {
                cachedPlayerPos = ps.position;
                cachedRoomID = GameState.currentRoomID;
            }
        }

        public void LoadContent(ContentManager content) { }

        public void Update(GameTime gameTime)
        {

            if (game.IsKeyPress(Keys.C))
            {
                GameStateManager.SetState(new GamePlayState(game));
                return;
            }

            if (game.IsKeyPress(Keys.C))
            {
                UseSelectedItem();
                GameStateManager.SetState(new GamePlayState(game));
                return;
            }

            HandleNavigation();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnsureResourcesInitialized(spriteBatch.GraphicsDevice);
            DrawBackground(spriteBatch);
            DrawHUD(spriteBatch);
            DrawInventoryItems(spriteBatch);
            DrawInstructions(spriteBatch);
            miniMap.Draw(spriteBatch, cachedPlayerPos, cachedRoomID);
        }

        private void HandleNavigation()
        {
            if (game.IsKeyPress(Keys.W) || game.IsKeyPress(Keys.Up))
            {
                selectedIndex -= columns;
                if (selectedIndex < 0) selectedIndex += itemList.Count;
            }
            if (game.IsKeyPress(Keys.S) || game.IsKeyPress(Keys.Down))
            {
                selectedIndex += columns;
                if (selectedIndex >= itemList.Count) selectedIndex -= itemList.Count;
            }
            if (game.IsKeyPress(Keys.A) || game.IsKeyPress(Keys.Left))
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = itemList.Count - 1;
            }
            if (game.IsKeyPress(Keys.D) || game.IsKeyPress(Keys.Right))
            {
                selectedIndex++;
                if (selectedIndex >= itemList.Count) selectedIndex = 0;
            }
        }

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                whitePixel,
                new Rectangle(0, 0, GameState.WindowWidth, GameState.WindowHeight),
                Color.Black
            );
            spriteBatch.DrawString(font, "Inventory", new Vector2(50, 50), Color.White);
        }

        private void DrawHUD(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blackPixel, new Rectangle(0, 880, 1280, 280), Color.Black);
            spriteBatch.Draw(GameState.contentLoader.HudTexture, new Rectangle(0, 880, 1280, 280), new Rectangle(258, 11, 256, 55), Color.White);
            hearts.Draw(spriteBatch);
            rupees.Draw(spriteBatch);
        }

        private void DrawInventoryItems(SpriteBatch spriteBatch)
        {
            BuildItemList();

            if (itemList.Count == 0)
            {
                spriteBatch.DrawString(font, "No Items", new Vector2(startX, startY), Color.White);
                return;
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                string itemName = itemList[i].Key;
                int count = itemList[i].Value;

                int row = i / columns;
                int col = i % columns;
                int x = startX + col * slotWidth;
                int y = startY + row * slotHeight;

                Rectangle slotRect = new Rectangle(x, y, slotWidth - 10, slotHeight - 10);
                Vector2 iconPos = new Vector2(slotRect.Right - 200, slotRect.Top - 40);

                spriteBatch.Draw(whitePixel, slotRect, Color.Gray);

                if (i == selectedIndex)
                {
                    DrawRectBorder(spriteBatch, slotRect, 2, Color.Red);
                }

                spriteBatch.DrawString(font, itemName, new Vector2(x + 10, y + 5), Color.White);
                spriteBatch.DrawString(font, $"x {count}", new Vector2(x + 10, y + 30), Color.Yellow);

                if (itemName == "Bomb")
                {
                    new Bomb(GameState.contentLoader.bombTexture, 0, 0).Draw(spriteBatch, iconPos);
                }
                else if (itemName == "Food")
                {
                    new Food(GameState.contentLoader.foodTexture, 0, 0).Draw(spriteBatch, iconPos);
                }
            }
        }

        private void DrawInstructions(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "[WASD/Arrows] Move | [N] Use | [C] Back", new Vector2(50, GameState.WindowHeight - 40), Color.White);
        }

        private void BuildItemList()
        {
            itemCounts = new Dictionary<string, int>();
            var ps = GameState.PlayerState;
            if (ps != null)
            {
                if (ps.bombCount > 0) itemCounts["Bomb"] = ps.bombCount;
                if (ps.foodCount > 0) itemCounts["Food"] = ps.foodCount;
            }
            itemList = itemCounts.ToList();

            if (selectedIndex >= itemList.Count) selectedIndex = itemList.Count - 1;
            if (selectedIndex < 0) selectedIndex = 0;
        }

        private void UseSelectedItem()
        {
            if (itemList.Count == 0) return;
            var ps = GameState.PlayerState;
            string itemName = itemList[selectedIndex].Key;
            int count = itemList[selectedIndex].Value;
            if (count <= 0) return;

            if (itemName == "Bomb")
            {
                GameState.EquippedB = new EquippedItemSlot("Bomb", () => ps.bombCount, c => ps.bombCount = c);
            }
            else if (itemName == "Food")
            {
                GameState.EquippedB = new EquippedItemSlot("Food", () => ps.foodCount, c => ps.foodCount = c);
            }

            if (count <= 0)
            {
                itemCounts.Remove(itemName);
                itemList = itemCounts.ToList();
                if (selectedIndex >= itemList.Count) selectedIndex = itemList.Count - 1;
            }
        }

        private void DrawRectBorder(SpriteBatch spriteBatch, Rectangle rect, int thickness, Color color)
        {
            spriteBatch.Draw(whitePixel, new Rectangle(rect.X, rect.Y, rect.Width, thickness), color);
            spriteBatch.Draw(whitePixel, new Rectangle(rect.X, rect.Y + rect.Height - thickness, rect.Width, thickness), color);
            spriteBatch.Draw(whitePixel, new Rectangle(rect.X, rect.Y, thickness, rect.Height), color);
            spriteBatch.Draw(whitePixel, new Rectangle(rect.X + rect.Width - thickness, rect.Y, thickness, rect.Height), color);
        }

        private void EnsureResourcesInitialized(GraphicsDevice graphicsDevice)
        {
            if (whitePixel == null)
            {
                whitePixel = new Texture2D(graphicsDevice, 1, 1);
                whitePixel.SetData(new[] { Color.White });
            }
            if (blackPixel == null)
            {
                blackPixel = new Texture2D(graphicsDevice, 1, 1);
                blackPixel.SetData(new[] { Color.Black });
            }
            if (font == null)
            {
                font = GameState.contentLoader.DefaultFont;
            }
            if (miniMap == null)
            {
                miniMap = new MiniMap(graphicsDevice, new Vector2(GameState.WindowWidth - 480, (GameState.WindowHeight - 440) / 2), 440, 440, true);
                miniMap.LoadContent(GameState.contentLoader.miniMapTexture);
            }
        }
    }

}
