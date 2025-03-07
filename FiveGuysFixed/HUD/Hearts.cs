using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.GameStates;
using System.Diagnostics;

namespace FiveGuysFixed.HUD
{
    public class Hearts : IHUD
    {
        private Rectangle fullHeartRectangle = new Rectangle(645, 117, 8, 8);
        private Rectangle halfHeartRectangle = new Rectangle(636, 117, 8, 8);
        private Rectangle emptyHeartRectangle = new Rectangle(627, 117, 8, 8);
        Vector2 heart1Pos = new Vector2(1200, 10);
        Vector2 heart2Pos = new Vector2(1216, 10);
        Vector2 heart3Pos = new Vector2(1232, 10);

        public Hearts() { }

        public void Draw(SpriteBatch spriteBatch)
        {

            switch (GameState.PlayerState.health)
            {
                case 6: // Full health
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart1Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart2Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart3Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    break;
                case 5: // 5 health
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart1Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart2Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart3Pos, halfHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    break;
                case 4: // 4 health
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart1Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart2Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart3Pos, emptyHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    break;
                case 3: // 3 health
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart1Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart2Pos, halfHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart3Pos, emptyHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    break;
                case 2: // 2 health
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart1Pos, fullHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart2Pos, emptyHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart3Pos, emptyHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    break;
                case 1: // 1 health
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart1Pos, halfHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart2Pos, emptyHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    spriteBatch.Draw(GameState.contentLoader.HudTexture, heart3Pos, emptyHeartRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
                    break;

            }

        }

        public void Update(GameTime gametime)
        {
            // No need to update anything for now.
        }
    }
}
