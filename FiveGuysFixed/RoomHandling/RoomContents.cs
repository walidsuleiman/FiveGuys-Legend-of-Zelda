using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Common;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Items;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Weapons___Items;
using Microsoft.Xna.Framework;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.RoomHandling
{
    public class RoomContents
    {
        public List<IBlock> Blocks { get; private set; }
        public List<IEnemy> Enemies { get; private set; }
        public List<IItem> Items { get; set; }
        public List<IProjectile> Projectiles { get; set; }

        public Dictionary<Dir, int> Neighbors;

        private Dictionary<string, Func<int, int, IItem>> ItemMap = new()
        {
            { "BluePotion", (x, y) => new BluePotion(GameState.contentLoader.bluePotionTexture, x, y) },
            { "Bomb",       (x, y) => new Bomb(GameState.contentLoader.bombTexture, x, y) },
            { "Food",       (x, y) => new Food(GameState.contentLoader.foodTexture, x, y) },
            { "GreenRupee", (x, y) => new GreenRupee(GameState.contentLoader.rupeeTexture, x, y) },
            { "RedPotion",  (x, y) => new RedPotion(GameState.contentLoader.redPotionTexture, x, y) },
            { "RedRupee",   (x, y) => new RedRupee(GameState.contentLoader.rupeeTexture, x, y) },
            { "Triforce", (x, y) => new Triforce(GameState.contentLoader.triforceTexture, x, y) },
            { "WoodSword", (x, y) => new WoodSword(GameState.contentLoader.swordTexture, x, y) },
            { "WhiteSword", (x, y) => new WhiteSword(GameState.contentLoader.swordTexture, x, y) }
        };

        private Dictionary<string, Func<int, int, IBlock>> BlockMap = new()
        {
            { "Wall",             (x, y) => new Wall(GameState.contentLoader.blockTexture, x, y) },
            { "Floor",            (x, y) => new Floor(GameState.contentLoader.blockTexture, x, y) },
            { "TopDoorOpen",      (x, y) => new TopDoorOpen(GameState.contentLoader.blockTexture, x, y) },
            { "TopDoorClose",     (x, y) => new TopDoorClose(GameState.contentLoader.blockTexture, x, y) },
            { "LeftDoorOpen",     (x, y) => new LeftDoorOpen(GameState.contentLoader.blockTexture, x, y) },
            { "LeftDoorClose",    (x, y) => new LeftDoorClose(GameState.contentLoader.blockTexture, x, y) },
            { "RightDoorOpen",    (x, y) => new RightDoorOpen(GameState.contentLoader.blockTexture, x, y) },
            { "RightDoorClose",   (x, y) => new RightDoorClose(GameState.contentLoader.blockTexture, x, y) },
            { "BottomDoorOpen",   (x, y) => new BottomDoorOpen(GameState.contentLoader.blockTexture, x, y) },
            { "BottomDoorClose",  (x, y) => new BottomDoorClose(GameState.contentLoader.blockTexture, x, y) },
            { "ClearBlock",       (x, y) => new ClearBlock(GameState.contentLoader.blockTexture, x, y) },
            { "BlueBlock",        (x, y) => new BlueBlock(GameState.contentLoader.blockTexture, x, y) },
            { "GreenBlock",       (x, y) => new GreenBlock(GameState.contentLoader.greenBlockTexture, x, y) },
            { "TreeBlock",        (x, y) => new TreeBlock(GameState.contentLoader.treeBlockTexture, x, y) },
            { "WhiteBlock",       (x, y) => new WhiteBlock(GameState.contentLoader.whiteBlockTexture, x, y) },
            { "YellowBlock",      (x, y) => new YellowBlock(GameState.contentLoader.yellowBlockTexture, x, y) }
        };


        public RoomContents()
        {
            Blocks = new List<IBlock>();
            Enemies = new List<IEnemy>();
            Items = new List<IItem>();
            Projectiles = new List<IProjectile>();
        }

        public RoomContents(XmlNode roomNode, Dictionary<Dir, int> neighbors)
        {
            Neighbors = neighbors;
            Blocks = new List<IBlock>();
            Enemies = new List<IEnemy>();
            Items = new List<IItem>();
            Projectiles = new List<IProjectile>();

            LoadItems(roomNode.SelectNodes("Objects/Item"));
            LoadBlocks(roomNode.SelectNodes("Objects/Block"));
            LoadEnemies(roomNode.SelectNodes("Objects/Enemy"));
        }

        private void LoadItems(XmlNodeList itemNodes)
        {


            foreach (XmlNode itemNode in itemNodes)
            {
                string type = itemNode.Attributes["type"].Value;
                int x = int.Parse(itemNode.Attributes["x"].Value);
                int y = int.Parse(itemNode.Attributes["y"].Value);

                if (ItemMap.TryGetValue(type, out var createItem))
                {
                    Items.Add(createItem(x, y));
                }
            }
        }

        private void LoadBlocks(XmlNodeList blockNodes)
        {

            foreach (XmlNode blockNode in blockNodes)
            {
                string type = blockNode.Attributes["type"].Value;
                int x = int.Parse(blockNode.Attributes["x"].Value);
                int y = int.Parse(blockNode.Attributes["y"].Value);

                if (BlockMap.TryGetValue(type, out var createBlock))
                {
                    Blocks.Add(createBlock(x, y));
                }
            }
        }

        private void LoadEnemies(XmlNodeList enemyNodes)
        {
            foreach (XmlNode enemyNode in enemyNodes)
            {
                string type = enemyNode.Attributes["type"].Value;
                int x = int.Parse(enemyNode.Attributes["x"].Value);
                int y = int.Parse(enemyNode.Attributes["y"].Value);
                var position = new Vector2(x, y);

                switch (type)
                {
                    case "Aquamentus":
                        Enemies.Add(new Aquamentus(
                            position,
                            EnemySpriteFactory.Instance.CreateAquamentusSprite(),
                            EnemySpriteFactory.Instance.CreateAquamentusFireballSprite(),
                            Projectiles
                        ));
                        break;

                    case "Gel":
                        Enemies.Add(new Gel(position));
                        break;

                    case "Goriya":
                        Enemies.Add(new Goriya(
                            position,
                            GameState.contentLoader.weaponTexture,
                            Projectiles));
                        break;

                    case "Keese":
                        Enemies.Add(new Keese(position));
                        break;

                    case "Moblin":
                        Enemies.Add(new Moblin(position));
                        break;

                    case "Octorok":
                        Enemies.Add(new Octorok(position));
                        break;

                    case "Stalfos":
                        Enemies.Add(new Stalfos(position));
                        break;

                    case "Tektike":
                        Enemies.Add(new Tektike(position));
                        break;
                }
            }
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
