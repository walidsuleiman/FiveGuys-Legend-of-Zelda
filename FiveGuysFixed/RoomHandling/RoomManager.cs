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

namespace FiveGuysFixed.RoomHandling
{
    public class RoomManager
    {
        public Dictionary<int, RoomContents> rooms = new();

        public RoomContents getCurrentRoom() { return rooms[GameState.currentRoomID]; }

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