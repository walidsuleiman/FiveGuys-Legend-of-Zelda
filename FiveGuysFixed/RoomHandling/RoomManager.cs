﻿using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.Weapons___Items;
using FiveGuysFixed.Blocks;
using System;
using FiveGuysFixed.Common;
using FiveGuysFixed.Projectiles;

namespace FiveGuysFixed.RoomHandling
{
    public class RoomManager
    {
        private Dictionary<int, ParsedRoom> roomData = new();
        public Aquamentus aquamentus;

        public class ParsedRoom
    {
        public XmlNode RoomNode;
        public Dictionary<Dir, int> Neighbors = new();
    }

        public void LoadRoomsFromXML(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNodeList roomNodes = xmlDoc.SelectNodes("//Room");

            foreach (XmlNode roomNode in roomNodes)
            {
                int roomId = int.Parse(roomNode.Attributes["id"].Value);
                ParsedRoom parsedRoom = new ParsedRoom { RoomNode = roomNode };

                XmlNodeList neighborNodes = roomNode.SelectNodes("Neighbor");
                foreach (XmlNode neighbor in neighborNodes)
                {
                    string dirStr = neighbor.Attributes["direction"].Value;
                    int neighborId = int.Parse(neighbor.Attributes["id"].Value);

                    if (Enum.TryParse(dirStr, true, out Dir dir))
                    {
                        parsedRoom.Neighbors[dir] = neighborId;
                    }
                }

                roomData[roomId] = parsedRoom;
            }
        }

        public void SwitchRoom(int newRoomID)
        {
            if (!roomData.ContainsKey(newRoomID)) return;
            XmlNode roomNode = roomData[newRoomID].RoomNode;
            GameState.currentRoomContents.Clear();

            XmlNodeList itemNodes = roomNode.SelectNodes("Objects/Item");
            foreach (XmlNode itemNode in itemNodes)
            {

                string type = itemNode.Attributes["type"].Value;
                int x = int.Parse(itemNode.Attributes["x"].Value);
                int y = int.Parse(itemNode.Attributes["y"].Value);

                if (type == "BluePotion")
                {
                    GameState.currentRoomContents.Items.Add(
                        new BluePotion(GameState.contentLoader.bluePotionTexture, x, y)
                    );
                }

                if (type == "Bomb")
                {
                    GameState.currentRoomContents.Items.Add(
                        new Bomb(GameState.contentLoader.bombTexture, x, y)
                    );
                }

                if (type == "Food")
                {
                    GameState.currentRoomContents.Items.Add(
                        new Food(GameState.contentLoader.foodTexture, x, y)
                    );
                }

                if (type == "GreenRupee")
                {
                    GameState.currentRoomContents.Items.Add(
                        new GreenRupee(GameState.contentLoader.rupeeTexture, x, y)
                    );
                }

                if (type == "RedPotion")
                {
                    GameState.currentRoomContents.Items.Add(
                        new RedPotion(GameState.contentLoader.redPotionTexture, x, y)
                    );
                }

                if (type == "RedRupee")
                {
                    GameState.currentRoomContents.Items.Add(
                        new RedRupee(GameState.contentLoader.rupeeTexture, x, y)
                    );
                }
            }


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
                    aquamentus = new Aquamentus(
                        new Vector2(x, y),
                        new EnemySprite(GameState.contentLoader.BossTexture, 0, 0, 32, 32, 2), // Example sprite
                        new EnemySprite(GameState.contentLoader.BossTexture, 32, 0, 32, 32, 2), // Example attack sprite
                        new List<IProjectile>()  // Empty projectile list for now
                    );
                    GameState.currentRoomContents.Enemies.Add(aquamentus);
                }
                if (type == "Gel")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Gel(
                            new Vector2(x, y),
                            GameState.contentLoader.enemyTexture // Pass texture directly
                        )
                    );
                }


                if (type == "Goriya")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Goriya(
                            new Vector2(x, y),
                            GameState.contentLoader.enemyTexture, // Uses the same enemy texture as Gel
                            GameState.contentLoader.rupeeTexture, // Boomerang attack texture
                            new List<IProjectile>() // Empty projectile list for now
                        )
                    );
                }
                if (type == "Keese")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Keese(
                            new Vector2(x, y),
                            GameState.contentLoader.enemyTexture // Uses the enemy sprite sheet
                        )
                    );
                }
                if (type == "Moblin")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Moblin(
                            new Vector2(x, y),
                            GameState.contentLoader.enemyTexture // Uses the enemy sprite sheet
                        )
                    );
                }
                if (type == "Octorok")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Octorok(
                            new Vector2(x, y),
                            GameState.contentLoader.enemyTexture // Uses the enemy sprite sheet
                        )
                    );
                }
                if (type == "Stalfos")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Stalfos(
                            new Vector2(x, y),
                            GameState.contentLoader.enemyTexture // Uses the enemy sprite sheet
                        )
                    );
                }
                if (type == "Tektike")
                {
                    GameState.currentRoomContents.Enemies.Add(
                        new Tektike(
                            new Vector2(x, y),
                            GameState.contentLoader.enemyTexture // Uses the enemy sprite sheet
                        )
                    );
                }
            }
        }

        public void TrySwitchRoom(Dir dir)
        {
            int currentId = GameState.currentRoomID;
            if (!roomData.ContainsKey(currentId)) return;

            ParsedRoom currentRoom = roomData[currentId];
            if (currentRoom.Neighbors.TryGetValue(dir, out int neighborId))
            {
                SwitchRoom(neighborId);
                GameState.currentRoomID = neighborId;
            }
        }
        public bool TryGetNeighborRoomID(int currentRoomID, Dir dir, out int neighborRoomID)
        {
            neighborRoomID = -1;

            if (roomData.ContainsKey(currentRoomID) &&
                roomData[currentRoomID].Neighbors.TryGetValue(dir, out neighborRoomID))
            {
                return true;
            }

            return false;
        }

    }
}