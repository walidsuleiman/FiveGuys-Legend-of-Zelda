using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Sprites
{
    public class LinkSwordAnimation : Sprite
    {

        public LinkSwordAnimation() 
        {
            
        }

        public Rectangle woodSwordAttack(Dir dir)
        {
            switch (dir) {

                case Dir.UP:
                    return new Rectangle();
                case Dir.LEFT:
                    return new Rectangle();
                case Dir.DOWN:
                    return new Rectangle();
                case Dir.RIGHT:
                    return new Rectangle();
                default:
                    return new Rectangle();

            }
        }

    }
}
