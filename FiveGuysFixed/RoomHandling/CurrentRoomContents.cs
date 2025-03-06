using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.Items;

namespace FiveGuysFixed.RoomHandling
{
    public class CurrentRoomContents
    {
        //public List<GameObject> Blocks { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        //public List<Item> Items { get; private set; }

        public CurrentRoomContents()
        {
            //Blocks = new List<GameObject>();
            Enemies = new List<Enemy>();
            //Items = new List<Item>();
        }

        public void Clear()
        {
            //Blocks.Clear();
            Enemies.Clear();
            //Items.Clear();
        }
    }

}
