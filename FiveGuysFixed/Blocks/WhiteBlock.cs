using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Blocks
{
    internal class WhiteBlock : Block
    {
        public WhiteBlock(Texture2D texture, int x, int y) : base(texture, 579, 210, x, y)
        {
        }
    }
}
