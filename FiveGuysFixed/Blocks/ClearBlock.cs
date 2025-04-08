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
    internal class ClearBlock : Block
    {
        public ClearBlock(Texture2D texture, int x, int y) : base(texture, 0, 0, x, y) { 
        
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            // Do not draw anything for ClearBlock
        }
    }
}