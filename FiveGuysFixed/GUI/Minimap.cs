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
        private Vector2 mapPosition;         // position of the minimap on screen
        private int mapWidth, mapHeight;     // minimap dimensions
        private GraphicsDevice graphicsDevice;

        private const int DOT_SIZE = 4;      // size of Link's position indicator

        // Room dimensions in the game world
        private float roomWidth = 1280;
        private float roomHeight = 720;

        // Store actual room layout in a 2D grid
        private int[,] roomGrid;
        private Vector2 roomGridOrigin; // Coordinates of room 1 in the grid

        // Define the corners of the minimap where the dungeon area is
        private Rectangle dungeonArea;

        public MiniMap(GraphicsDevice graphicsDevice, Vector2 mapPosition, int mapWidth, int mapHeight)
        {
            this.graphicsDevice = graphicsDevice;
            this.mapPosition = mapPosition;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;

            // Create red dot texture
            linkDotTexture = new Texture2D(graphicsDevice, 1, 1);
            linkDotTexture.SetData(new[] { Color.Red });

            // Define the area on the minimap where the dungeon is shown
            // Adjust these values based on your actual minimap texture
            dungeonArea = new Rectangle(
                (int)mapPosition.X + 10,  // Left edge of dungeon area
                (int)mapPosition.Y + 10,  // Top edge of dungeon area
                mapWidth - 20,            // Width of dungeon area
                mapHeight - 20            // Height of dungeon area
            );

            InitializeRoomGrid();
        }

        public void LoadContent(ContentManager content)
        {
            mapTexture = content.Load<Texture2D>("Minimap1");
        }

        public void LoadContent(Texture2D texture)
        {
            mapTexture = texture;
        }

        private void InitializeRoomGrid()
        {
            // Create a larger grid to ensure we have enough space
            // for all rooms with their proper relationships
            roomGrid = new int[7, 8]; // 7 rows, 8 columns

            // Initialize all to -1 (no room)
            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 8; x++)
                    roomGrid[y, x] = -1;

            // Place rooms in the grid according to their connections

            // Row 0 (top row)
            roomGrid[0, 1] = 1;  // Room 1
            roomGrid[0, 2] = 2;  // Room 2

            // Row 1
            roomGrid[1, 2] = 3;  // Room 3
            roomGrid[1, 4] = 4;  // Room 4
            roomGrid[1, 5] = 5;  // Room 5

            // Row 2
            roomGrid[2, 0] = 6;  // Room 6
            roomGrid[2, 1] = 7;  // Room 7
            roomGrid[2, 2] = 8;  // Room 8
            roomGrid[2, 3] = 9;  // Room 9
            roomGrid[2, 4] = 10; // Room 10

            // Row 3
            roomGrid[3, 1] = 11; // Room 11
            roomGrid[3, 2] = 12; // Room 12
            roomGrid[3, 3] = 13; // Room 13

            // Row 4
            roomGrid[4, 2] = 14; // Room 14

            // Row 5
            roomGrid[5, 1] = 15; // Room 15
            roomGrid[5, 2] = 16; // Room 16
            roomGrid[5, 3] = 17; // Room 17

            // Set origin to room 1 position
            roomGridOrigin = new Vector2(1, 0);
        }

        private Vector2 GetRoomCoordinates(int roomId)
        {
            // Search for the room in the grid
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (roomGrid[y, x] == roomId)
                    {
                        return new Vector2(x, y);
                    }
                }
            }

            // Return a default position if room not found
            return new Vector2(0, 0);
        }

        private Vector2 GridToMinimap(Vector2 gridPosition)
        {
            // Calculate the size of each cell in the minimap
            float cellWidth = dungeonArea.Width / 8.0f;
            float cellHeight = dungeonArea.Height / 7.0f;

            // Convert grid coordinates to pixel coordinates
            float pixelX = dungeonArea.X + gridPosition.X * cellWidth + cellWidth / 2;
            float pixelY = dungeonArea.Y + gridPosition.Y * cellHeight + cellHeight / 2;

            return new Vector2(pixelX, pixelY);
        }

        public void Update(GameTime gameTime)
        {
            // No additional update logic needed for this implementation
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the map background if available
            if (mapTexture != null)
            {
                spriteBatch.Draw(mapTexture, new Rectangle(
                    (int)mapPosition.X,
                    (int)mapPosition.Y,
                    mapWidth,
                    mapHeight),
                    Color.White);
            }

            // Get grid coordinates for the current room
            Vector2 roomGridPos = GetRoomCoordinates(GameState.currentRoomID);

            // Convert to minimap coordinates
            Vector2 roomMiniPos = GridToMinimap(roomGridPos);

            // Calculate cell size
            float cellWidth = dungeonArea.Width / 8.0f;
            float cellHeight = dungeonArea.Height / 7.0f;

            // Calculate relative position within the room
            float relativeX = GameState.PlayerState.position.X / roomWidth;
            float relativeY = GameState.PlayerState.position.Y / roomHeight;

            // Clamp to ensure dot stays within room boundaries
            relativeX = MathHelper.Clamp(relativeX, 0.1f, 0.9f);
            relativeY = MathHelper.Clamp(relativeY, 0.1f, 0.9f);

            // Map the player's position within the room to the minimap
            float offsetX = (relativeX - 0.5f) * cellWidth * 0.8f;
            float offsetY = (relativeY - 0.5f) * cellHeight * 0.8f;

            // Calculate final dot position
            Vector2 dotPosition = new Vector2(
                roomMiniPos.X + offsetX,
                roomMiniPos.Y + offsetY
            );

            // Draw the red dot for Link
            spriteBatch.Draw(linkDotTexture, new Rectangle(
                (int)dotPosition.X - DOT_SIZE / 2,
                (int)dotPosition.Y - DOT_SIZE / 2,
                DOT_SIZE,
                DOT_SIZE),
                Color.Red);

            // For debugging - draw room centers
            /*
            for (int y = 0; y < 7; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (roomGrid[y, x] != -1)
                    {
                        Vector2 pos = GridToMinimap(new Vector2(x, y));
                        spriteBatch.Draw(linkDotTexture, new Rectangle(
                            (int)pos.X - 1,
                            (int)pos.Y - 1,
                            2,
                            2),
                            Color.Blue);
                    }
                }
            }
            */
        }

        public void Dispose()
        {
            linkDotTexture?.Dispose();
        }
    }
}