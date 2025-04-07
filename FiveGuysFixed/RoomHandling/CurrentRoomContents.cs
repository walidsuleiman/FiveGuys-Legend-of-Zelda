using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.Items;
using FiveGuysFixed.Projectiles;

namespace FiveGuysFixed.RoomHandling
{
    public class CurrentRoomContents
    {
        public List<IBlock> Blocks { get; private set; }
        public List<IEnemy> Enemies { get; private set; }
        public List<IItem> Items { get; set; }

        public List<IProjectile> Projectiles { get; set; } = new List<IProjectile>();

        public CurrentRoomContents()
        {
            Blocks = new List<IBlock>();
            Enemies = new List<IEnemy>();
            Items = new List<IItem>();
        }

        public void Clear()
        {
            Blocks.Clear();
            Enemies.Clear();
            Items.Clear();
            Projectiles.Clear();
        }
    }

}
