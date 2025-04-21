using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.GameStates;
using System.Diagnostics;

namespace FiveGuysFixed.HUD
{
    public class HeldItems : IHUDElement
    {
        private Rectangle woodSprite = new Rectangle(1, 154, 8, 16);
        private Rectangle whiteSprite = new Rectangle(36, 154, 8, 16);
        private Rectangle bowSprite = new Rectangle(1, 185, 8, 16);
        private Rectangle boomerangSprite = new Rectangle(64, 185, 8, 16);

        private Vector2 aItemPos = new Vector2(762, 1005);
        private Vector2 bItemPos = new Vector2(10, GameState.WindowHeight - 20);

        public HeldItems() { }

        public void Draw(SpriteBatch spriteBatch)
        {

            switch (GameState.PlayerState.heldWeapon)
            {
                case Common.WeaponType.WOODSWORD: 
                    spriteBatch.Draw(GameState.contentLoader.swordTexture, aItemPos, woodSprite, Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0f);
                    break;
                case Common.WeaponType.WHITESWORD: 
                    spriteBatch.Draw(GameState.contentLoader.swordTexture, aItemPos, whiteSprite, Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0f);
                    break;
                case Common.WeaponType.BOW: 
                    spriteBatch.Draw(GameState.contentLoader.swordTexture, aItemPos, bowSprite, Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0f);
                    break;
                case Common.WeaponType.BOOMERANG: 
                    spriteBatch.Draw(GameState.contentLoader.swordTexture, aItemPos, boomerangSprite, Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0f);
                    break;
            }

        }

        public void Update(GameTime gametime)
        {
            // No need to update anything for now.
        }
    }
}