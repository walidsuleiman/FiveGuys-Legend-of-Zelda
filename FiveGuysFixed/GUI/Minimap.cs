using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FiveGuysFixed.GameStates;
using System;
using System.Collections.Generic;

namespace FiveGuysFixed.GUI
{
    public class MiniMap
    {
        private Texture2D mapTexture;        // minimap background
        private Texture2D linkDotTexture;    // red dot for Link's position
        private Texture2D borderTexture;     // border texture
        private Vector2 mapPosition;         // position of the minimap on screen
        private int mapWidth, mapHeight;     // minimap dimensions
        private GraphicsDevice graphicsDevice;


        private const int DOT_SIZE = 6;      // size of Link's position indicator
        private const int BORDER_THICKNESS = 2;

        // dictionary to store room positions on the minimap
        private Dictionary<int, Vector2> roomPositions;

        // set of visited rooms
        private HashSet<int> visitedRooms;

        private float roomWidth = 1280;
        private float roomHeight = 720;

        public MiniMap(GraphicsDevice graphicsDevice, Vector2 mapPosition, int mapWidth, int mapHeight)
        {
            this.graphicsDevice = graphicsDevice;
            this.mapPosition = mapPosition;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;

            // a white pixel texture for borders and the link dot
            borderTexture = new Texture2D(graphicsDevice, 1, 1);
            borderTexture.SetData(new[] { Color.White });

            linkDotTexture = new Texture2D(graphicsDevice, 1, 1);
            linkDotTexture.SetData(new[] { Color.Red });


            visitedRooms = new HashSet<int>();
            visitedRooms.Add(GameState.currentRoomID); 

            InitializeRoomPositions();
        }

        public void LoadContent(ContentManager content)
        {
            mapTexture = content.Load<Texture2D>("Minimap1");
        }
        public void LoadContent(Texture2D texture)
        {
            mapTexture = texture;
        }

        private void InitializeRoomPositions()
        {
            // these values are relative to the minimap position
            roomPositions = new Dictionary<int, Vector2>();

            // we can adjust these positions based on our actual dungeon layout
            int roomSize = 20; // size of a room on the minimap
            int centerX = mapWidth / 2 - roomSize / 2;
            int centerY = mapHeight / 2 - roomSize / 2;

            // center room (usually starting room)
            roomPositions[1] = new Vector2(centerX, centerY);

            // adjacent rooms
            roomPositions[2] = new Vector2(centerX + roomSize, centerY); // right
            roomPositions[3] = new Vector2(centerX, centerY - roomSize); // up
            roomPositions[4] = new Vector2(centerX - roomSize, centerY); // left
            roomPositions[5] = new Vector2(centerX, centerY + roomSize); // down

            // Add more rooms as needed based on your dungeon layout
            roomPositions[6] = new Vector2(centerX + roomSize, centerY - roomSize); // up-right
            roomPositions[7] = new Vector2(centerX - roomSize, centerY - roomSize); // up-left
            roomPositions[8] = new Vector2(centerX - roomSize, centerY + roomSize); // down-left
            roomPositions[9] = new Vector2(centerX + roomSize, centerY + roomSize); // down-right

            // add more rooms as needed
        }

        public void Update(GameTime gameTime)
        {
            // mark current room as visited
            if (!visitedRooms.Contains(GameState.currentRoomID))
            {
                visitedRooms.Add(GameState.currentRoomID);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mapTexture == null)
            {
                // a default texture if mapTexture is not loaded
                mapTexture = new Texture2D(graphicsDevice, 1, 1);
                mapTexture.SetData(new[] { Color.DarkGray });
            }
            spriteBatch.Draw(mapTexture, new Rectangle(
                (int)mapPosition.X,
                (int)mapPosition.Y,
                mapWidth,
                mapHeight),
                Color.White);


            //Link's position on the minimap
            if (roomPositions.ContainsKey(GameState.currentRoomID))
            {
                Vector2 roomPos = roomPositions[GameState.currentRoomID];


                float relativeX = (GameState.PlayerState.position.X) / roomWidth;
                float relativeY = (GameState.PlayerState.position.Y) / roomHeight;

                relativeX = MathHelper.Clamp(relativeX, 0.1f, 0.9f);
                relativeY = MathHelper.Clamp(relativeY, 0.1f, 0.9f);

                Vector2 dotPosition = mapPosition + roomPos + new Vector2(
                    relativeX * 20, // size of rooms on the minimap
                    relativeY * 20
                );

                // dot representing Link
                spriteBatch.Draw(linkDotTexture, new Rectangle(
                    (int)dotPosition.X - DOT_SIZE / 2,
                    (int)dotPosition.Y - DOT_SIZE / 2,
                    DOT_SIZE,
                    DOT_SIZE),
                    Color.Red);

            }

            DrawBorders(spriteBatch);
        }

        private void DrawBorders(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(borderTexture, new Rectangle(
                (int)mapPosition.X,
                (int)mapPosition.Y,
                mapWidth,
                BORDER_THICKNESS),
                Color.Black);

  
            spriteBatch.Draw(borderTexture, new Rectangle(
                (int)mapPosition.X,
                (int)mapPosition.Y + mapHeight - BORDER_THICKNESS,
                mapWidth,
                BORDER_THICKNESS),
                Color.Black);


            spriteBatch.Draw(borderTexture, new Rectangle(
                (int)mapPosition.X,
                (int)mapPosition.Y,
                BORDER_THICKNESS,
                mapHeight),
                Color.Black);


            spriteBatch.Draw(borderTexture, new Rectangle(
                (int)mapPosition.X + mapWidth - BORDER_THICKNESS,
                (int)mapPosition.Y,
                BORDER_THICKNESS,
                mapHeight),
                Color.Black);
        }

        // call when switching rooms
        public void RoomChanged(int newRoomId)
        {
            visitedRooms.Add(newRoomId);
        }

        public void Dispose()
        {
            if (borderTexture != null)
                borderTexture.Dispose();
            if (linkDotTexture != null)
                linkDotTexture.Dispose();
        }
    }
}