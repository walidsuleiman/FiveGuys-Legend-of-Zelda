using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.Config
{
    public class EnemyLoader
    {
        private readonly Texture2D _texture;

        public EnemyLoader(Texture2D texture)
            => _texture = texture;

        public ItemData Keese => new ItemData(_texture, 16, 32, 16, 2);
        public ItemData Stalfos => new ItemData(_texture, 16, 96, 16, 2);
        public ItemData Gel => new ItemData(_texture, 16, 0, 16, 2);

        public ItemData LeftMoblin => new ItemData(_texture, 80, 112, 16, 2);
        public ItemData RightMoblin => new ItemData(_texture, 48, 112, 16, 2);
        public ItemData DownMoblin => new ItemData(_texture, 16, 112, 16, 2);
        public ItemData UpMoblin => new ItemData(_texture, 112, 112, 16, 2);
        public ItemData LeftGoriya => new ItemData(_texture, 80, 48, 16, 2);
        public ItemData RightGoriya => new ItemData(_texture, 48, 48, 16, 2);
        public ItemData DownGoriya => new ItemData(_texture, 16, 48, 16, 2);
        public ItemData UpGoriya => new ItemData(_texture, 112, 48, 16, 2);

        // EnemyLoader.cs
        public ItemData LeftOctorok => new ItemData(_texture, 48, 160, 16, 2);
        public ItemData RightOctorok => new ItemData(_texture, 16, 160, 16, 2);
        public ItemData DownOctorok => new ItemData(_texture, 32, 160, 16, 2);
        public ItemData UpOctorok => new ItemData(_texture, 80, 160, 16, 2);


        public ItemData Tektike => new ItemData(_texture, 16, 160, 16, 2);

    }
}
