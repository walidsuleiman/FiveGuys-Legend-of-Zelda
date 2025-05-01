using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Config
{
    public class ProjectileLoader
    {
        private readonly Texture2D _texture;

        public ProjectileLoader(Texture2D texture)
            => _texture = texture;

        public ItemData Bomb => new ItemData(_texture, 0, 80, 16, 2);
        public ItemData Fireball => new ItemData(_texture, 16, 80, 16, 2);
        public ItemData Boomerang => new ItemData(_texture, 16, 48, 16, 3);
    }
}
