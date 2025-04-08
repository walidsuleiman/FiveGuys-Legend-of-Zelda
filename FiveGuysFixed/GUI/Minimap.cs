using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FiveGuysFixed.GameStates;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FiveGuysFixed.GUI
{
    public class MiniMap
    {
        private Texture2D mapTexture;        // minimap background
        private Texture2D linkDotTexture;    // red dot for Link's position
        private Vector2 mapPosition;         // position of the minimap on screen
        private int mapWidth, mapHeight;     // minimap dimensions
        private GraphicsDevice graphicsDevice;

        private const int DOT_SIZE = 8;      // size of Link's position indicator
        private Vector2 mapOffset;           // offset for fine-tuning

        private float roomWidth = 720;
        private float roomHeight = 528;

        private float ROOM_WID;
        private float ROOM_HEI;

        private Dictionary<int, Vector2> roomPositions;

        private Dictionary<int, Vector2> roomWorldPositions;

        private bool isFirstDraw = true;

        public MiniMap(GraphicsDevice graphicsDevice, Vector2 mapPosition, int mapWidth, int mapHeight)
        {
            this.graphicsDevice = graphicsDevice;
            this.mapPosition = mapPosition;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;

            // calculate room size on minimap (6 rooms across)
            ROOM_WID = mapWidth / 6;
            ROOM_HEI = mapHeight / 6;

            linkDotTexture = new Texture2D(graphicsDevice, 1, 1);
            linkDotTexture.SetData(new[] { Color.Red });

            // add a small offset for fine-tuning positioning
            mapOffset = new Vector2(8, 8);

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
            roomPositions = new Dictionary<int, Vector2>
            {
                { 1, new Vector2(22, 7) },
                { 2, new Vector2(45, 2) },
                { 3, new Vector2(45, 30) },
                { 4, new Vector2(100, 30) },
                { 5, new Vector2(125, 30) },
                { 6, new Vector2(0, 58) },
                { 7, new Vector2(20, 58) },
                { 8, new Vector2(45, 58) },
                { 9, new Vector2(75, 58) },
                { 10, new Vector2(100, 58) }, 
                { 11, new Vector2(20, 83) },
                { 12, new Vector2(45, 83) },
                { 13, new Vector2(75, 83) },
                { 14, new Vector2(45, 110) },
                { 15, new Vector2(20, 140) },
                { 16, new Vector2(45, 140) },
                { 17, new Vector2(75, 140) }
            };

            // room world positions
            roomWorldPositions = new Dictionary<int, Vector2>
            {
                { 1, new Vector2(1 * ROOM_WID, 0 * ROOM_HEI) },
                { 2, new Vector2(2 * ROOM_WID, 0 * ROOM_HEI) },
                { 3, new Vector2(2 * ROOM_WID, 1 * ROOM_HEI) },
                { 4, new Vector2(4 * ROOM_WID, 1 * ROOM_HEI) },
                { 5, new Vector2(5 * ROOM_WID, 1 * ROOM_HEI) },
                { 6, new Vector2(0 * ROOM_WID, 2 * ROOM_HEI) },
                { 7, new Vector2(1 * ROOM_WID, 2 * ROOM_HEI) },
                { 8, new Vector2(2 * ROOM_WID, 2 * ROOM_HEI) },
                { 9, new Vector2(3 * ROOM_WID, 2 * ROOM_HEI) },
                { 10, new Vector2(4 * ROOM_WID, 2 * ROOM_HEI) },
                { 11, new Vector2(1 * ROOM_WID, 3 * ROOM_HEI) },
                { 12, new Vector2(2 * ROOM_WID, 3 * ROOM_HEI) },
                { 13, new Vector2(3 * ROOM_WID, 3 * ROOM_HEI) },
                { 14, new Vector2(2 * ROOM_WID, 4 * ROOM_HEI) },
                { 15, new Vector2(1 * ROOM_WID, 5 * ROOM_HEI) },
                { 16, new Vector2(2 * ROOM_WID, 5 * ROOM_HEI) },
                { 17, new Vector2(3 * ROOM_WID, 5 * ROOM_HEI) }
            };
        }

        public void Update(GameTime gameTime)
        {
          
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isFirstDraw)
            {
                GameState.currentRoomID = 1;
                isFirstDraw = false;
                Debug.WriteLine("First draw detected, setting room to 1");
            }

            if (mapTexture != null)
            {
                spriteBatch.Draw(mapTexture, new Rectangle(
                    (int)mapPosition.X,
                    (int)mapPosition.Y,
                    mapWidth,
                    mapHeight),
                    Color.White);
            }

            Debug.WriteLine($"Current Room ID: {GameState.currentRoomID}");

            if (roomPositions.TryGetValue(GameState.currentRoomID, out Vector2 roomMinimapPosition) &&
                roomWorldPositions.TryGetValue(GameState.currentRoomID, out Vector2 roomWorldPosition))
            {
                float relativeX = (GameState.PlayerState.position.X - 96) / 528;
                float relativeY = (GameState.PlayerState.position.Y - 336) / 288;

                relativeX = MathHelper.Clamp(relativeX, 0f, 1f);
                relativeY = MathHelper.Clamp(relativeY, 0f, 1f);

                Debug.WriteLine($"Player Position: X={GameState.PlayerState.position.X}, Y={GameState.PlayerState.position.Y}");
                Debug.WriteLine($"Relative Position: X={relativeX}, Y={relativeY}");

                Vector2 dotPosition = mapPosition + roomMinimapPosition + mapOffset + new Vector2(
                    relativeX * (ROOM_WID - 12),
                    relativeY * (ROOM_HEI - 12)
                );

                Debug.WriteLine($"Dot Position: X={dotPosition.X}, Y={dotPosition.Y}");

                spriteBatch.Draw(linkDotTexture, new Rectangle(
                    (int)dotPosition.X - DOT_SIZE / 2,
                    (int)dotPosition.Y - DOT_SIZE / 2,
                    DOT_SIZE,
                    DOT_SIZE),
                    Color.Red);
            }
            else
            {
                Debug.WriteLine($"Room {GameState.currentRoomID} not found in position dictionaries");
            }

            int borderThickness = 2;
            spriteBatch.Draw(linkDotTexture, new Rectangle(
                (int)mapPosition.X,
                (int)mapPosition.Y,
                mapWidth,
                borderThickness),
                Color.Black);

            spriteBatch.Draw(linkDotTexture, new Rectangle(
                (int)mapPosition.X,
                (int)mapPosition.Y + mapHeight - borderThickness,
                mapWidth,
                borderThickness),
                Color.Black);

            spriteBatch.Draw(linkDotTexture, new Rectangle(
                (int)mapPosition.X,
                (int)mapPosition.Y,
                borderThickness,
                mapHeight),
                Color.Black);

            spriteBatch.Draw(linkDotTexture, new Rectangle(
                (int)mapPosition.X + mapWidth - borderThickness,
                (int)mapPosition.Y,
                borderThickness,
                mapHeight),
                Color.Black);
        }

        public void Dispose()
        {
            linkDotTexture?.Dispose();
        }
    }
}