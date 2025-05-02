using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.HUD
{
    public sealed class Hearts : IHUDElement
    {
        
        private static readonly Rectangle Full = new Rectangle(645, 117, 8, 8);
        private static readonly Rectangle Half = new Rectangle(636, 117, 8, 8);
        private static readonly Rectangle Empty = new Rectangle(627, 117, 8, 8);

        
        private const int Scale = 9;
        private const int BaseX = 935;
        private const int BaseY = 880 + 168;
        private const int HeartStrideX = 8 * Scale;

        
        private static readonly Vector2[] Positions;

        static Hearts()
        {
            Positions = new Vector2[3];
            for (int i = 0; i < Positions.Length; i++)
            {
                Positions[i] = new Vector2(BaseX + HeartStrideX * i, BaseY);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int hp = GameState.PlayerState.health;
            if (hp < 0) hp = 0;
            else if (hp > 6) hp = 6;

            for (int i = 0; i < 3; i++)
            {
                int remaining = hp - i * 2;
                Rectangle src = remaining switch
                {
                    >= 2 => Full,
                    1 => Half,
                    _ => Empty
                };

                spriteBatch.Draw(GameState.contentLoader.HudTexture,
                                 Positions[i], src,
                                 Color.White, 0f, Vector2.Zero,
                                 Scale, SpriteEffects.None, 0f);
            }
        }

        public void Update(GameTime gameTime) 
        { 
            //nothing to update for now                                  
        }
    }
}
