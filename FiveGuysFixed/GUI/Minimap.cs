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
        private const int DOT_SIZE = 8;               
        private const float ROOMS_ACROSS = 6f;          
        private const float ROOMS_DOWN = 6f;             
        private const float MAP_OFFSET_FACTOR = 8f;     
        private const float PLAYER_OFFSET_X = 96f;      
        private const float PLAYER_OFFSET_Y = 336f;     
        private const float PLAYER_BOUNDARY_X = 528f;  
        private const float PLAYER_BOUNDARY_Y = 288f;   
        private const float DOT_PADDING = 12f;         
        private const int BORDER_THICKNESS = 2;         
        private const int DEFAULT_ROOM_ID = 1;           
        private const float MINIMAP_BASE_SIZE = 220f;    

        // raw room pixel coordinates (centralized magic numbers)
        private static readonly Dictionary<int, Point> RawRoomPixels = new()
        {
            { 1, new(22, 3) },
            { 2, new(60, 3) },
            { 3, new(60, 40) },
            { 4, new(132, 40) },
            { 5, new(169, 40) },
            { 6, new(0, 75) },
            { 7, new(22, 75) },
            { 8, new(60, 75) },
            { 9, new(95, 75) },
            { 10, new(132, 75) },
            { 11, new(22, 115) },
            { 12, new(60, 115) },
            { 13, new(95, 115) },
            { 14, new(60, 150) },
            { 15, new(22, 185) },
            { 16, new(60, 185) },
            { 17, new(95, 185) }
        };

        private Texture2D mapTexture;                   
        private Texture2D linkDotTexture;               

        private Vector2 mapPosition;                     
        private int mapWidth, mapHeight;                
        private GraphicsDevice graphicsDevice;         
        private Vector2 mapOffset;                    
        private float roomWidth;                      
        private float roomHeight;                        
        private float scale;                           
        private bool isFirstDraw = true;              
        private bool suppressRoomInit;                

        // room positions on minimap and in world
        private Dictionary<int, Vector2> roomPositions;
        private Dictionary<int, Vector2> roomWorldPositions;

        public MiniMap(GraphicsDevice graphicsDevice, Vector2 mapPosition, int mapWidth, int mapHeight, bool suppressRoomInit = false)
        {
            this.graphicsDevice = graphicsDevice;
            this.mapPosition = mapPosition;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.suppressRoomInit = suppressRoomInit;

            scale = mapWidth / MINIMAP_BASE_SIZE;

            roomWidth = mapWidth / ROOMS_ACROSS;
            roomHeight = mapHeight / ROOMS_DOWN;

            InitializeDotTexture();

            mapOffset = new Vector2(MAP_OFFSET_FACTOR * scale, MAP_OFFSET_FACTOR * scale);

            InitializeRoomPositions();
        }

        private void InitializeDotTexture()
        {
            linkDotTexture = new Texture2D(graphicsDevice, 1, 1);
            linkDotTexture.SetData(new[] { Color.Red });
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
            // initialize room positions on minimap using the raw pixel coordinates
            roomPositions = new Dictionary<int, Vector2>();
            foreach (var (id, pt) in RawRoomPixels)
            {
                roomPositions[id] = new Vector2(pt.X, pt.Y) * scale;
            }

            InitializeRoomWorldPositions();
        }

        private void InitializeRoomWorldPositions()
        {
            roomWorldPositions = new Dictionary<int, Vector2>();

            // define room positions in the world grid
            for (int i = 1; i <= 17; i++)
            {
                int x = GetRoomWorldX(i);
                int y = GetRoomWorldY(i);
                roomWorldPositions[i] = new Vector2(x * roomWidth, y * roomHeight);
            }
        }

        private int GetRoomWorldX(int roomId)
        {
            switch (roomId)
            {
                case 1: case 7: case 11: case 15: return 1;
                case 2: case 3: case 8: case 12: case 14: case 16: return 2;
                case 9: case 13: case 17: return 3;
                case 4: case 10: return 4;
                case 5: return 5;
                case 6: return 0;
                default: return 0;
            }
        }

        private int GetRoomWorldY(int roomId)
        {
            switch (roomId)
            {
                case 1: case 2: return 0;
                case 3: case 4: case 5: return 1;
                case 6: case 7: case 8: case 9: case 10: return 2;
                case 11: case 12: case 13: return 3;
                case 14: return 4;
                case 15: case 16: case 17: return 5;
                default: return 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            // update logic (currently empty)
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, GameState.PlayerState.position, GameState.currentRoomID);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 playerPosition, int roomID)
        {
            HandleFirstDraw();
            DrawMapBackground(spriteBatch);
            DrawPlayerDot(spriteBatch, playerPosition, roomID);
            DrawMapBorder(spriteBatch);
        }

        private void HandleFirstDraw()
        {
            if (isFirstDraw && !suppressRoomInit)
            {
                GameState.currentRoomID = DEFAULT_ROOM_ID;
                isFirstDraw = false;
            }
        }

        private void DrawMapBackground(SpriteBatch spriteBatch)
        {
            if (mapTexture != null)
            {
                spriteBatch.Draw(
                    mapTexture,
                    new Rectangle((int)mapPosition.X, (int)mapPosition.Y, mapWidth, mapHeight),
                    Color.White
                );
            }
        }

        private void DrawPlayerDot(SpriteBatch spriteBatch, Vector2 playerPosition, int roomID)
        {
            if (roomPositions.TryGetValue(roomID, out Vector2 roomMinimapPosition))
            {
                float relativeX = (playerPosition.X - PLAYER_OFFSET_X) / PLAYER_BOUNDARY_X;
                float relativeY = (playerPosition.Y - PLAYER_OFFSET_Y) / PLAYER_BOUNDARY_Y;

                // clamp to ensure position is within the room boundaries
                relativeX = MathHelper.Clamp(relativeX, 0f, 1f);
                relativeY = MathHelper.Clamp(relativeY, 0f, 1f);

                Vector2 dotPosition = mapPosition + roomMinimapPosition + mapOffset + new Vector2(
                    relativeX * (roomWidth - DOT_PADDING * scale),
                    relativeY * (roomHeight - DOT_PADDING * scale)
                );

                spriteBatch.Draw(
                    linkDotTexture,
                    new Rectangle(
                        (int)dotPosition.X - DOT_SIZE / 2,
                        (int)dotPosition.Y - DOT_SIZE / 2,
                        DOT_SIZE,
                        DOT_SIZE
                    ),
                    Color.Red
                );
            }
        }

        private void DrawMapBorder(SpriteBatch spriteBatch)
        {
            Rectangle bgRect = new Rectangle((int)mapPosition.X, (int)mapPosition.Y, mapWidth, mapHeight);
            DrawBorderSide(spriteBatch, bgRect, BorderSide.Top);
            DrawBorderSide(spriteBatch, bgRect, BorderSide.Bottom);
            DrawBorderSide(spriteBatch, bgRect, BorderSide.Left);
            DrawBorderSide(spriteBatch, bgRect, BorderSide.Right);
        }

        private enum BorderSide
        {
            Top,
            Bottom,
            Left,
            Right
        }

        private void DrawBorderSide(SpriteBatch sb, Rectangle bgRect, BorderSide side)
        {
            Rectangle borderRect;

            switch (side)
            {
                case BorderSide.Top:
                    borderRect = new Rectangle(bgRect.X, bgRect.Y, bgRect.Width, BORDER_THICKNESS);
                    break;
                case BorderSide.Bottom:
                    borderRect = new Rectangle(bgRect.X, bgRect.Y + bgRect.Height - BORDER_THICKNESS, bgRect.Width, BORDER_THICKNESS);
                    break;
                case BorderSide.Left:
                    borderRect = new Rectangle(bgRect.X, bgRect.Y, BORDER_THICKNESS, bgRect.Height);
                    break;
                case BorderSide.Right:
                    borderRect = new Rectangle(bgRect.X + bgRect.Width - BORDER_THICKNESS, bgRect.Y, BORDER_THICKNESS, bgRect.Height);
                    break;
                default:
                    return;
            }

            sb.Draw(linkDotTexture, borderRect, Color.Black);
        }

        public void Dispose()
        {
            linkDotTexture?.Dispose();
        }
    }
}