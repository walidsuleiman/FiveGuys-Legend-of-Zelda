using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Common;
using FiveGuysFixed.Enemies;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace FiveGuysFixed.Sprites
{
    public class AttackSprites
    {
        private WeaponType currentSword;
        private Dir currentDir;

        public AttackSprites(Dir dir, WeaponType equipment) 
        {
            this.currentDir = dir;

            if (equipment == WeaponType.WOODSWORD)
            { 
                this.currentSword = WeaponType.WOODSWORD;
            }
            else if (equipment == WeaponType.WHITESWORD)
            {
                this.currentSword = WeaponType.WHITESWORD;
            }
            //else if (currentSword.Equals("magic"))
            //{
            //    this.currentSword = swordType.magicalSword;
            //}
            //else if (currentSword.Equals("magicRod"))
            //{
            //    this.currentSword = swordType.magicalRod;
            //}
            //this.currentDir = dir;
        }

        public List<Rectangle> GetWoodSwordAttackSprites()
        {
            var spritesReturnable = new List<Rectangle>();
            if (currentDir == Dir.RIGHT)
            {
                spritesReturnable.Add(new Rectangle(1, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70, 77, 19, 17));
                return spritesReturnable;
            }
            else if (currentDir == Dir.LEFT)
            {
                spritesReturnable.Add(new Rectangle(1, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70, 77, 19, 17));
                return spritesReturnable;
            }
            else if (currentDir == Dir.UP)
            {
                spritesReturnable.Add(new Rectangle(1, 109, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 97, 16, 28));
                spritesReturnable.Add(new Rectangle(35, 98, 16, 27));
                spritesReturnable.Add(new Rectangle(52, 106, 16, 19));
                return spritesReturnable;
            }
            else if (currentDir == Dir.DOWN)
            {
                spritesReturnable.Add(new Rectangle(1, 47, 16, 16));
                spritesReturnable.Add(new Rectangle(18, 47, 16, 27));
                spritesReturnable.Add(new Rectangle(35, 47, 16, 23));
                spritesReturnable.Add(new Rectangle(52, 47, 16, 19));
                return spritesReturnable;
            }
            return spritesReturnable;
        }

        public List<Rectangle> GetWhiteSwordAttackSprites()
        {
            var spritesReturnable = new List<Rectangle>();
            if (currentDir == Dir.RIGHT)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46 + 93, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70 + 93, 77, 19, 17));
                return spritesReturnable;
            }
            else if (currentDir == Dir.LEFT)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 77, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 77, 27, 17));
                spritesReturnable.Add(new Rectangle(46 + 93, 77, 23, 17));
                spritesReturnable.Add(new Rectangle(70 + 93, 77, 19, 17));
                return spritesReturnable;
            }
            else if (currentDir == Dir.UP)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 109, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 97, 16, 28));
                spritesReturnable.Add(new Rectangle(35 + 93, 98, 16, 27));
                spritesReturnable.Add(new Rectangle(52 + 93, 106, 16, 19));
                return spritesReturnable;
            }
            else if (currentDir == Dir.DOWN)
            {
                spritesReturnable.Add(new Rectangle(1 + 93, 47, 16, 16));
                spritesReturnable.Add(new Rectangle(18 + 93, 47, 16, 27));
                spritesReturnable.Add(new Rectangle(35 + 93, 47, 16, 23));
                spritesReturnable.Add(new Rectangle(52 + 93, 47, 16, 19));
                return spritesReturnable;
            }
            return spritesReturnable;
        }

    }
}
