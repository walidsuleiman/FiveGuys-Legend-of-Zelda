using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.Weapons___Items;
using FiveGuysFixed.Blocks;
using System;
using FiveGuysFixed.Common;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.RoomHandling
{
    public class RoomManager
    {
        public Dictionary<int, RoomContents> rooms = new();

        public RoomContents getCurrentRoom() { return rooms[GameState.currentRoomID]; }

        public void SwitchRoomWithDifficulty(int newRoomID)
        {
            if (!rooms.ContainsKey(newRoomID)) return;

            // Clear projectiles from current room
            rooms[GameState.currentRoomID].Projectiles.Clear();

            // Set the new room ID
            GameState.currentRoomID = newRoomID;

            // Apply Hell Mode if enabled
            if (DifficultyManager.Instance.CurrentDifficulty == GameDifficulty.Hell)
            {
                // Store original enemy positions before replacing
                List<Vector2> enemyPositions = new List<Vector2>();
                foreach (var enemy in rooms[newRoomID].Enemies)
                {
                    enemyPositions.Add(enemy.Position);
                }

                // Clear current enemies
                rooms[newRoomID].Enemies.Clear();

                // Create Aquamentus at each position
                foreach (var position in enemyPositions)
                {
                    // Use the proper Animation namespace instead of Sprites
                    var aquamentusSprite = new FiveGuysFixed.Animation.Sprite(
                        GameState.contentLoader.BossTexture,
                        64, 0, 32, 32, 4
                    );

                    var aquamentusAttackSprite = new FiveGuysFixed.Animation.Sprite(
                        GameState.contentLoader.BossTexture,
                        0, 0, 32, 32, 2
                    );

                    var aquamentus = new FiveGuysFixed.Enemies.Aquamentus(
                        position,
                        aquamentusSprite,
                        aquamentusAttackSprite,
                        rooms[newRoomID].Projectiles
                    );

                    rooms[newRoomID].Enemies.Add(aquamentus);
                }
            }
        }

        public void LoadRoomsFromXML(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNodeList roomNodes = xmlDoc.SelectNodes("//Room");

            foreach (XmlNode roomNode in roomNodes)
            {
                int roomId = int.Parse(roomNode.Attributes["id"].Value);
                Dictionary<Dir, int> Neighbors = new();

                XmlNodeList neighborNodes = roomNode.SelectNodes("Neighbor");
                foreach (XmlNode neighbor in neighborNodes)
                {
                    string dirStr = neighbor.Attributes["direction"].Value;
                    int neighborId = int.Parse(neighbor.Attributes["id"].Value);

                    if (Enum.TryParse(dirStr, true, out Dir dir))
                    {
                        Neighbors[dir] = neighborId;
                    }
                }
                rooms[roomId] = new RoomContents(roomNode, Neighbors);
            }
        }

        public void SwitchRoom(int newRoomID)
        {
            if (!rooms.ContainsKey(newRoomID)) return;
            rooms[GameState.currentRoomID].Projectiles.Clear();
            GameState.currentRoomID = newRoomID;
        }

        public int TryGetNeighborRoomID(Dir dir)
        {
            int neighborRoomID = -1;
            rooms[GameState.currentRoomID].Neighbors.TryGetValue(dir, out neighborRoomID);
            return neighborRoomID;
        }

    }
}