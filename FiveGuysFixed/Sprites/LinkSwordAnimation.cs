using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Animation
{
    public class LinkSwordAnimation : Sprite
    {

        private Rectangle sourceRect;
        private Dir dir;
        private AttackSprites attackSprites;
        private List<Rectangle> currentSprites;
        private int currentFrameIndex;

        public new void Update(GameTime gt)
        {
            if (isAnimated)
            {
                timeElapsed += gt.ElapsedGameTime.TotalSeconds;
                if (timeElapsed >= frameTime)
                {
                    timeElapsed -= frameTime;
                    currentFrameIndex++;

                    if (currentFrameIndex >= totalFrames)
                        currentFrameIndex = 0;

                }
            }
        }
        public new void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("linkSheet");
        }


        public void Draw(SpriteBatch _spriteBatch, Vector2? origin)
        {

            Rectangle destRect = new Rectangle((int)GameState.PlayerState.position.X, (int)GameState.PlayerState.position.Y, width, height);
            sourceRect = currentSprites[currentFrameIndex];

            if (!origin.HasValue)
            {
                Point position = currentSprites[currentFrameIndex].Location;
                int x = position.X; 
                int y = position.Y;
                if (dir == Dir.UP)
                {
                    origin = new Vector2(9, 117);
                }
                else
                {
                    origin = new Vector2(x+8, y+8);
                }
            }

            if (facLeft)
            {
                _spriteBatch.Draw(texture, GameState.PlayerState.position, sourceRect, Color.White, 0, new Vector2(width / 2, height / 2), 1, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(texture, GameState.PlayerState.position, sourceRect, Color.White, 0, new Vector2(width / 2, height / 2), 1, SpriteEffects.None, 0f);
            }
        }

        public LinkSwordAnimation(Dir dir, WeaponType weapon)
        {
            attackSprites = new AttackSprites(dir, weapon);
            frameTime = 0.3;

            if (weapon == WeaponType.WOODSWORD) 
            {
                currentSprites = attackSprites.GetWoodSwordAttackSprites();
            }
            if (weapon == WeaponType.WHITESWORD)
            {
                currentSprites = attackSprites.GetWhiteSwordAttackSprites();
            }

            this.dir = dir;
            totalFrames = currentSprites.Count;
            facLeft = false;
        }
    }
}
