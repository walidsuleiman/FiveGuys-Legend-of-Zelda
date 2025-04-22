using System;
using System.Collections.Generic;
using System.Diagnostics;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace FiveGuysFixed.Animation
{
    public class LinkSwordAnimation : Sprite
    {

        private Rectangle sourceRect;
        private int currentFrameIndex;

        public new void Update(GameTime gt)
        {

            timeElapsed += gt.ElapsedGameTime.TotalSeconds;
            if (timeElapsed >= frameTime)
            {

                timeElapsed -= frameTime;
                currentFrameIndex++;

                if (currentFrameIndex >= totalFrames)
                {
                    GameState.PlayerState.isAttacking = false;
                    currentFrameIndex = 0;
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            List<Rectangle> swordSprites;
            if (GameState.PlayerState.heldWeapon == WeaponType.WOODSWORD)
            {
                swordSprites = GetWoodSwordAttackSprites(GameState.PlayerState.direction);
            }
            else if (GameState.PlayerState.heldWeapon == WeaponType.WHITESWORD)
            {
                swordSprites = GetWhiteSwordAttackSprites(GameState.PlayerState.direction);
            }
            else
            {
               
                swordSprites = GetWoodSwordAttackSprites(GameState.PlayerState.direction);
                
                return;
            }

            Rectangle rectangle = swordSprites[currentFrameIndex];
            sourceRect = rectangle;
            Rectangle destRect = new Rectangle((int)GameState.PlayerState.position.X, (int)GameState.PlayerState.position.Y, rectangle.Width, rectangle.Height);
            if (GameState.PlayerState.direction == Dir.LEFT)
            {
                _spriteBatch.Draw(texture, GameState.PlayerState.position, sourceRect, Color.White, 0, new Vector2(rectangle.Width / 2, rectangle.Height / 2), 5, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(texture, GameState.PlayerState.position, sourceRect, Color.White, 0, new Vector2(rectangle.Width / 2, rectangle.Height / 2), 5, SpriteEffects.None, 0f);
            }
        }

        public List<Rectangle> GetWoodSwordAttackSprites(Dir currentDir)
        {
            var spritesReturnable = new List<Rectangle>();
            if (currentDir == Dir.RIGHT)
            {
                spritesReturnable.Add(new Rectangle(1, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70, 77, 19, 17));

            }
            else if (currentDir == Dir.LEFT)
            {
                spritesReturnable.Add(new Rectangle(1, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70, 77, 19, 17));
            }
            else if (currentDir == Dir.UP)
            {
                spritesReturnable.Add(new Rectangle(1, 109, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 97, 16, 28));
                spritesReturnable.Add(new Rectangle(35, 98, 16, 27));
                spritesReturnable.Add(new Rectangle(52, 106, 16, 19));
            }
            else if (currentDir == Dir.DOWN)
            {
                spritesReturnable.Add(new Rectangle(1, 47, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 47, 16, 27));
                spritesReturnable.Add(new Rectangle(35, 47, 16, 23));
                spritesReturnable.Add(new Rectangle(52, 47, 16, 19));
            }
            return spritesReturnable;
        }

        public List<Rectangle> GetWhiteSwordAttackSprites(Dir currentDir)
        {
            var spritesReturnable = new List<Rectangle>();
            if (currentDir == Dir.RIGHT)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46 + 93, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70 + 93, 77, 19, 17));
            }
            else if (currentDir == Dir.LEFT)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46 + 93, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70 + 93, 77, 19, 17));
            }
            else if (currentDir == Dir.UP)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 109, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 97, 16, 28));
                spritesReturnable.Add(new Rectangle(35 + 93, 98, 16, 27));
                spritesReturnable.Add(new Rectangle(52 + 93, 106, 16, 19));
            }
            else if (currentDir == Dir.DOWN)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 47, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 47, 16, 27));
                spritesReturnable.Add(new Rectangle(35 + 93, 47, 16, 23));
                spritesReturnable.Add(new Rectangle(52 + 93, 47, 16, 19));
            }
            return spritesReturnable;
        }

        public LinkSwordAnimation()
        {
            currentFrameIndex = 0;
            frameTime = 0.1;
            totalFrames = 4;
        }

        public IProjectile GetSwordProjectile(Dir dir, Vector2 position)
        {
            return new LinkSwordProjectile(GameState.contentLoader.swordTexture, position.X, position.Y, dir);
        }

    }
}
