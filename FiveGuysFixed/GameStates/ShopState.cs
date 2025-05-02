using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using FiveGuysFixed.GUI;
using FiveGuysFixed.HUD;
using FiveGuysFixed.Weapons___Items;
using Microsoft.Xna.Framework.Content;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.GameStates
{
    public class ShopState : IGameState
    {
        private Game1 game;
        private List<ShopItem> shopItems;
        private SpriteFont font;
        private Texture2D whitePixel;
        private int selectedIndex;
        private KeyboardState oldState;
        private string statusMessage = "";
        private double messageTimer = 0;


        public ShopState(Game1 game)
        {
            this.game = game;
            this.oldState = Keyboard.GetState();
            LoadItems();
        }

        private void LoadItems()
        {
            shopItems = new List<ShopItem>
        {
            new ShopItem("Bomb", 2, "Green", GameState.contentLoader.bombTexture),
            new ShopItem("Food", 1, "Green", GameState.contentLoader.foodTexture),
            new ShopItem("Wood Sword", 4, "Red", GameState.contentLoader.weaponTexture),
            new ShopItem("White Sword", 6, "Red", GameState.contentLoader.weaponTexture)
        };
        }

        public void LoadContent(ContentManager content)
        {
            font = GameState.contentLoader.DefaultFont;
            whitePixel = new Texture2D(game.GraphicsDevice, 1, 1);
            whitePixel.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            if (game.IsKeyPress(Keys.Up))
                selectedIndex = (selectedIndex - 1 + shopItems.Count) % shopItems.Count;

            if (game.IsKeyPress(Keys.Down))
                selectedIndex = (selectedIndex + 1) % shopItems.Count;

            if (messageTimer > 0)
            {
                messageTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (messageTimer < 0) messageTimer = 0;
            }

            if (game.IsKeyPress(Keys.Enter))
                BuyItem(shopItems[selectedIndex]);

            if (game.IsKeyPress(Keys.X))
                GameStateManager.SetState(new GamePlayState(game));
        }

        private void BuyItem(ShopItem item)
        {
            var ps = GameState.PlayerState;
            if (ps.greenRupees >= item.Price)
            {
                ps.greenRupees -= item.Price;
                if (item.Name == "Bomb") ps.bombCount++;
                if (item.Name == "Food") ps.foodCount++;
                statusMessage = "Item bought!";
            }
            else
            {
                statusMessage = "Insufficient Funds!";
            }

            if(ps.redRupees >= item.Price)
            {
                ps.redRupees -= item.Price;
                if (item.Name == "Wood Sword") ps.heldWeapon = WeaponType.WOODSWORD;
                if (item.Name == "White Sword") ps.heldWeapon = WeaponType.WHITESWORD;
                statusMessage = "Item bought!";
            }
            else
            {
                statusMessage = "Insufficient Funds!";
            }
            messageTimer = 2.0;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(whitePixel, new Rectangle(0, 0, GameState.WindowWidth, GameState.WindowHeight + 280), Color.Black);
            spriteBatch.DrawString(font, "SHOP (Press [Enter] to buy, [X] to exit)", new Vector2(100, 50), Color.White);


            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                var pos = new Vector2(100, 100 + i * 60);
                spriteBatch.DrawString(font, $"{item.Name} - {item.Price} {item.Currency} Rupees", pos, i == selectedIndex ? Color.Yellow : Color.White);
                if (item.Name == "Bomb")
                {
                    Bomb bombIcon = new Bomb(GameState.contentLoader.bombTexture, 0, 0);
                    spriteBatch.Draw(GameState.contentLoader.bombTexture, new Rectangle(43, 80 + i * 60, 30, 60), new Rectangle(129, 185, 8, 16), Color.White);
                }
                else if (item.Name == "Food")
                {
                    Food foodIcon = new Food(GameState.contentLoader.foodTexture, 0, 0);
                    spriteBatch.Draw(GameState.contentLoader.foodTexture, new Rectangle(43, 83 + i * 60, 30, 60), new Rectangle(299, 185, 8, 16), Color.White);
                }
                else if (item.Name == "Wood Sword")
                {
                    WoodSword woodSwordIcon = new WoodSword(GameState.contentLoader.weaponTexture, 0, 0);
                    spriteBatch.Draw(GameState.contentLoader.bombTexture, new Rectangle(44, 87 + i * 60, 30, 60), new Rectangle(1, 154, 8, 16), Color.White);
                }
                else if (item.Name == "White Sword")
                {
                    WhiteSword whiteSwordIcon = new WhiteSword(GameState.contentLoader.weaponTexture, 0, 0);
                    spriteBatch.Draw(GameState.contentLoader.bombTexture, new Rectangle(44, 91 + i * 60, 30, 60), new Rectangle(36, 154, 8, 16), Color.White);
                }
            }

            if (messageTimer > 0 && !string.IsNullOrEmpty(statusMessage))
            {
                Vector2 msgSize = font.MeasureString(statusMessage);
                Vector2 msgPos = new Vector2(
                    (GameState.WindowWidth - msgSize.X) / 2,
                    GameState.WindowHeight - 100
                );
                spriteBatch.DrawString(font, statusMessage, msgPos, Color.Yellow);
            }

        }

    }
}
