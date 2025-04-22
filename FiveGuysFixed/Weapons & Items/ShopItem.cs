using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Weapons___Items
{
    public class ShopItem
    {
        public string Name { get; }
        public int Price { get; }
        public Texture2D Icon { get; }

        public ShopItem(string name, int price, Texture2D icon)
        {
            Name = name;
            Price = price;
            Icon = icon;
        }
    }

}
