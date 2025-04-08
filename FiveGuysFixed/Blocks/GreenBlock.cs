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
    internal class GreenBlock : Block
    {
        public GreenBlock(Texture2D texture, int x, int y) : base(texture, 322, 65, x, y)
        {
        }
    }
}
