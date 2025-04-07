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
        private int slotWidth = 200;
        private int slotHeight = 100;
        private int startX = 100;
        private int startY = 200;
        private KeyboardState oldState;
        public Inventory(Game1 game)
        {
            this.game = game;

            hearts = new Hearts();
            rupees = new RupeeCount();
            oldState = Keyboard.GetState();
        }

        public void LoadContent(ContentManager content)
        {
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (IsKeyPressed(ks, Keys.C))
            {
                GameStateManager.SetState(new GamePlayState(game));
                return;
            }

            if (IsKeyPressed(ks, Keys.N))
            {
                UseSelectedItem();
                GameStateManager.SetState(new GamePlayState(game));
                return;
            }

            if (IsKeyPressed(ks, Keys.W) || IsKeyPressed(ks, Keys.Up))
            {
                selectedIndex -= columns;
                if (selectedIndex < 0) selectedIndex += itemList.Count;
            }
            if (IsKeyPressed(ks, Keys.S) || IsKeyPressed(ks, Keys.Down))
            {
                selectedIndex += columns;
                if (selectedIndex >= itemList.Count) selectedIndex -= itemList.Count;
            }
            if (IsKeyPressed(ks, Keys.A) || IsKeyPressed(ks, Keys.Left))
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = itemList.Count - 1;
            }
            if (IsKeyPressed(ks, Keys.D) || IsKeyPressed(ks, Keys.Right))
            {
                selectedIndex++;
                if (selectedIndex >= itemList.Count) selectedIndex = 0;
            }

            oldState = ks;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (whitePixel == null)
            {
                whitePixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                whitePixel.SetData(new[] { Color.White });
            }
            spriteBatch.Draw(
                whitePixel,
                new Rectangle(0, 0, GameState.WindowWidth, GameState.WindowHeight),
                Color.Black
            );
            if (font == null)
            {
                font = GameState.contentLoader.DefaultFont;
            }
            spriteBatch.DrawString(font, "Inventory", new Vector2(50, 50), Color.White);

            hearts.Draw(spriteBatch);
            rupees.Draw(spriteBatch);
            if (miniMap == null)
            {
                miniMap = new MiniMap(
    spriteBatch.GraphicsDevice,
    new Vector2(GameState.WindowWidth - 260, 320),
    160,
    160
                );
            }
            miniMap.Draw(spriteBatch);

            BuildItemList();

            if (itemList.Count == 0)
            {
                spriteBatch.DrawString(font, "No Items", new Vector2(startX, startY), Color.White);
            }
            else
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    string itemName = itemList[i].Key;
                    int count = itemList[i].Value;

                    int row = i / columns;
                    int col = i % columns;
                    int x = startX + col * slotWidth;
                    int y = startY + row * slotHeight;

                    Rectangle slotRect = new Rectangle(x, y, slotWidth - 10, slotHeight - 10);
                    spriteBatch.Draw(whitePixel, slotRect, Color.Gray);

                    if (i == selectedIndex)
                    {
                        DrawRectBorder(spriteBatch, slotRect, 2, Color.Red);
                    }

                    spriteBatch.DrawString(font, itemName, new Vector2(x + 10, y + 5), Color.White);
                    spriteBatch.DrawString(font, $"x {count}", new Vector2(x + 10, y + 30), Color.Yellow);
                    if (itemName == "Bomb")
                    {
                        Bomb bombIcon = new Bomb(
                            GameState.contentLoader.bombTexture,
                            0, 0
                        );
                        bombIcon.Draw(spriteBatch, new Vector2(x + slotWidth - 40, y + 10));
                    }
                    else if (itemName == "Food")
                    {
                        Food foodIcon = new Food(
                            GameState.contentLoader.foodTexture,
                            0, 0
                        );
                        foodIcon.Draw(spriteBatch, new Vector2(x + slotWidth - 40, y + 10));
                    }
                }
            }
            spriteBatch.DrawString(
                font,
                "[WASD/Arrows] Move | [N] Use | [C] Back",
                new Vector2(50, GameState.WindowHeight - 40),
                Color.White
            );
        }
        private void BuildItemList()
        {
            itemCounts = new Dictionary<string, int>();

            var ps = GameState.PlayerState;
            if (ps != null)
            {
                if (ps.bombCount > 0)
                    itemCounts["Bomb"] = ps.bombCount;

                if (ps.foodCount > 0)
                    itemCounts["Food"] = ps.foodCount;
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

            count--;

            if (itemName == "Bomb")
            {
                ps.bombCount = count;
                ps.health--;
            }
            else if (itemName == "Food")
            {
                ps.foodCount = count;
                ps.health++;
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

        private bool IsKeyPressed(KeyboardState ks, Keys key)
        {
            bool pressed = ks.IsKeyDown(key) && !oldState.IsKeyDown(key);
            return pressed;
        }
    }
}
