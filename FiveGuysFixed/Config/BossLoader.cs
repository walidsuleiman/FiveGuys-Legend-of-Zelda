using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Config
{
    public class BossLoader
    {
        private readonly Texture2D _texture;

        public BossLoader(Texture2D texture)
            => _texture = texture;

        public ItemData Aquamentus => new ItemData(_texture, 64, 0, 32, 4);
        public ItemData AquamentusAttack => new ItemData(_texture, 0, 0, 32, 2);
    }
}
