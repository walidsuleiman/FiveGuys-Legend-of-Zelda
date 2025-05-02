using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Config
{
    public class ItemData
    {
        public Texture2D Texture { get; }
        public int X { get; }
        public int Y { get; }
        public int Dimensions { get; }
        public int Frames { get; }

        public ItemData(Texture2D texture, int x, int y, int dimensions, int frames)
        {
            Texture = texture;
            X = x;
            Y = y;
            Dimensions = dimensions;
            Frames = frames;
        }
    }
}
