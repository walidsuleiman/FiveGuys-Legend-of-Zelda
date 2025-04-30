using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.HUD
{
    public class RupeeCount : IHUDElement
    {
        
        private static readonly Rectangle GreenRupeeSrc = new Rectangle(35, 2, 14, 26);
        private static readonly Rectangle RedRupeeSrc = new Rectangle(35, 58, 14, 26);
        private static readonly Rectangle XSrc = new Rectangle(519, 117, 8, 8);


        private static readonly Rectangle[] DigitSrc;

        static RupeeCount()
        {
            DigitSrc = new Rectangle[10];
            for (int i = 0; i < 10; i++)
            {
                // 0 is the 2nd sprite in the row, so shift by (i + 1)
                DigitSrc[i] = new Rectangle(519 + 9 * (i + 1), 117, 8, 8);
            }
        }
        
        private const int BaseX = 430;
        private const int GreenBaseY = 880 + 115;
        private const int RedBaseY = 880 + 170;

        private readonly Vector2 _greenRupeePos = new Vector2(BaseX, GreenBaseY);
        private readonly Vector2 _greenXPos = new Vector2(BaseX + 28, GreenBaseY + 3);
        private readonly Vector2 _greenDigitPos = new Vector2(BaseX + 60, GreenBaseY + 3);

        private readonly Vector2 _redRupeePos = new Vector2(BaseX, RedBaseY);
        private readonly Vector2 _redXPos = new Vector2(BaseX + 28, RedBaseY + 3);
        private readonly Vector2 _redDigitPos = new Vector2(BaseX + 60, RedBaseY + 3);

        
        private const float RupeeScale = 1.5f;
        private const float HudScale = 4f;

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawSingle(spriteBatch, _greenRupeePos, _greenXPos, _greenDigitPos,
                       GameState.PlayerState.greenRupees, GreenRupeeSrc);

            DrawSingle(spriteBatch, _redRupeePos, _redXPos, _redDigitPos,
                       GameState.PlayerState.redRupees, RedRupeeSrc);
        }

        public void Update(GameTime gameTime) 
        {
            //nothing to update for now
        }

        
        private static void DrawSingle(SpriteBatch spritebatch, Vector2 rupeePos, Vector2 xPos, Vector2 digitPos, int count, Rectangle rupeeSrc)
        {
            int digit = Math.Clamp(count, 0, 9);

            spritebatch.Draw(GameState.contentLoader.rupeeTexture,
                       rupeePos, rupeeSrc, Color.White, 0f, Vector2.Zero,
                       RupeeScale, SpriteEffects.None, 0f);

            spritebatch.Draw(GameState.contentLoader.HudTexture,
                       xPos, XSrc, Color.White, 0f, Vector2.Zero,
                       HudScale, SpriteEffects.None, 0f);

            spritebatch.Draw(GameState.contentLoader.HudTexture,
                       digitPos, DigitSrc[digit], Color.White, 0f, Vector2.Zero,
                       HudScale, SpriteEffects.None, 0f);
        }
    }
}
