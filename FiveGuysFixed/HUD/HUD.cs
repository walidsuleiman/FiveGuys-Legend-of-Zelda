using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Weapons___Items;

namespace FiveGuysFixed.HUD
{
    public class HUD
    {
        private Hearts hearts;
        private RupeeCount rupees;
        private MiniMap miniMap;
        private HeldItems heldItems;
        Texture2D blackPixel;

        public HUD()
        {
            hearts = new Hearts();
            rupees = new RupeeCount();
            heldItems = new HeldItems();
        }

        public void Update(GameTime gameTime)
        {
            hearts.Update(gameTime);
            rupees.Update(gameTime);
            miniMap?.Update(gameTime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            this.blackPixel = new Texture2D(spritebatch.GraphicsDevice, 1, 1);
            this.blackPixel.SetData(new[] { Color.Black });
            spritebatch.Draw(blackPixel, new Rectangle(0, GameState.WindowHeight, 1280, 280), Color.Black);
            spritebatch.Draw(GameState.contentLoader.HudTexture, new Rectangle(0, GameState.WindowHeight, 1280, 280), new Rectangle(258, 11, 256, 55), Color.White);

            hearts.Draw(spritebatch);
            rupees.Draw(spritebatch);
            heldItems.Draw(spritebatch);

            Vector2 slotPos = new Vector2(560, GameState.WindowHeight + 95);
            Rectangle slotRect = new Rectangle((int)slotPos.X, (int)slotPos.Y, 100, 120);

            if (GameState.EquippedB != null)
            {
                string name = GameState.EquippedB.Name;
                int count = GameState.EquippedB.GetCount();
                Vector2 textPos = new Vector2(slotPos.X + 85, slotPos.Y + 83);
                Vector2 offset = new Vector2(-37, -65);
                Vector2 drawPos = slotPos + offset;

                if (name == "Bomb")
                {
                    Bomb b = new Bomb(GameState.contentLoader.bombTexture, 0, 0);
                    b.Draw(spritebatch, drawPos);
                }
                else if (name == "Food")
                {
                    Food f = new Food(GameState.contentLoader.foodTexture, 0, 0);
                    f.Draw(spritebatch, drawPos);
                }

                spritebatch.DrawString(GameState.contentLoader.DefaultFont, $"x{count}", textPos, Color.White);
            }

            if (miniMap == null)
            {
                miniMap = new MiniMap(
                    spritebatch.GraphicsDevice,
                    new Vector2(110, GameState.WindowHeight + 33), // position in bottom-left
                    220, // width
                    220  // height
                );


                miniMap.LoadContent(GameState.contentLoader.miniMapTexture);
            }

            Vector2 boomerangPos = slotPos + new Vector2(200, 44);
            Rectangle boomerangFrame = new Rectangle(80, 48, 6, 8);
            spritebatch.Draw(GameState.contentLoader.weaponTexture, boomerangPos, boomerangFrame, Color.White, 0f, Vector2.Zero, 6f, SpriteEffects.None, 0f);


            miniMap.Draw(spritebatch);
        }
    }
}