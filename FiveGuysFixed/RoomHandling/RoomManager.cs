using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace FiveGuysFixed.RoomHandling
{
    public class RoomManager
    {
        private Dictionary<int, XmlNode> roomData; // Store XML data for each room

        public RoomManager()
        {
            roomData = new Dictionary<int, XmlNode>();
        }

        public void LoadRoomsFromXML(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNodeList roomNodes = xmlDoc.SelectNodes("//Room");

            foreach (XmlNode roomNode in roomNodes)
            {
                int roomId = int.Parse(roomNode.Attributes["id"].Value);
                roomData[roomId] = roomNode;
            }
        }

        public void SwitchRoom(int newRoomID)
        {
            if (!roomData.ContainsKey(newRoomID)) return;

            XmlNode roomNode = roomData[newRoomID];

            // Clear current room contents
            GameState.currentRoomContents.Clear();

            // Load Blocks
            XmlNodeList blockNodes = roomNode.SelectNodes("Objects/Block");
            foreach (XmlNode blockNode in blockNodes)
            {

                string type = blockNode.Attributes["type"].Value;
                int x = int.Parse(blockNode.Attributes["x"].Value);
                int y = int.Parse(blockNode.Attributes["y"].Value);

                if (type == "Block")
                {
                    GameState.currentRoomContents.Blocks.Add(
                        new Block(GameState.contentLoader.blockTexture, x, y)
                    );
                }

                if (type == "GreenBlock")
                {
                    GameState.currentRoomContents.Blocks.Add(
                        new GreenBlock(GameState.contentLoader.greenBlockTexture, x, y)
                    );
                }

                if (type == "TreeBlock")
                {
                    GameState.currentRoomContents.Blocks.Add(
                        new TreeBlock(GameState.contentLoader.treeBlockTexture, x, y)
                    );
                }

                if (type == "WhiteBlock")
                {
                    GameState.currentRoomContents.Blocks.Add(
                        new WhiteBlock(GameState.contentLoader.whiteBlockTexture, x, y)
                    );
                }

                if (type == "YellowBlock")
                {
                    GameState.currentRoomContents.Blocks.Add(
                        new YellowBlock(GameState.contentLoader.yellowBlockTexture, x, y)
                    );
                }
            }

            // Load Enemies
            XmlNodeList enemyNodes = roomNode.SelectNodes("Objects/Enemy");
            foreach (XmlNode enemyNode in enemyNodes)
            {
                string type = enemyNode.Attributes["type"].Value;
                int x = int.Parse(enemyNode.Attributes["x"].Value);
                int y = int.Parse(enemyNode.Attributes["y"].Value);

                if (type == "Aquamentus")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Aquamentus(
                            new Vector2(x, y),
                            new EnemySprite(GameState.contentLoader.BossTexture, 0, 0, 32, 32, 2), // Example sprite
                            new EnemySprite(GameState.contentLoader.BossTexture, 32, 0, 32, 32, 2), // Example attack sprite
                            new List<IProjectile>()  // Empty projectile list for now
                        )
                    );
                }
            }

            // Load Items
            //XmlNodeList itemNodes = roomNode.SelectNodes("Objects/Item");
            //foreach (XmlNode itemNode in itemNodes)
            //{
            //    string type = itemNode.Attributes["type"].Value;
            //    int x = int.Parse(itemNode.Attributes["x"].Value);
            //    int y = int.Parse(itemNode.Attributes["y"].Value);
            //    gameState.CurrentRoomContents.Items.Add(new Item(type, x, y));
            //}
        }
    }



}
