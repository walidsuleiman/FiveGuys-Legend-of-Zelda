using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.Items;

namespace FiveGuysFixed.RoomHandling
{
    public class CurrentRoomContents
    {
        public List<IBlock> Blocks { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<IItem> Items { get; set; }

        public CurrentRoomContents()
        {
            Blocks = new List<IBlock>();
            Enemies = new List<Enemy>();
            Items = new List<IItem>();
        }

        public void Clear()
        {
            Blocks.Clear();
            Enemies.Clear();
            Items.Clear();
        }
    }

}
