using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace FiveGuysFixed.GUI
{
    public class MiniMap
    {
        private Texture2D mapTexture;
        private Texture2D borderTexture;
        private Vector2 mapPosition;
        private Vector2 playerPosition;
        private int mapWidth, mapHeight;
        private int roomSize = 10;
        private GraphicsDevice graphicsDevice;

        public MiniMap(Texture2D mapTexture, Vector2 mapPosition, int mapWidth, int mapHeight, GraphicsDevice graphicsDevice)
        {
            this.mapTexture = mapTexture;
            this.mapPosition = mapPosition;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.graphicsDevice = graphicsDevice;
            // a simple white texture to draw with
            borderTexture = new Texture2D(graphicsDevice, 1, 1);
            borderTexture.SetData(new[] { Color.White });
        }

        public void Update(Vector2 playerWorldPosition, int roomWidth, int roomHeight)
        {
            // update player's position on mini-map
            playerPosition = new Vector2(
                mapPosition.X + (playerWorldPosition.X / roomWidth) * roomSize,
                mapPosition.Y + (playerWorldPosition.Y / roomHeight) * roomSize
            );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // background
            spriteBatch.Draw(mapTexture, new Rectangle((int)mapPosition.X, (int)mapPosition.Y, mapWidth, mapHeight), Color.White);
            // player on mini-map
            spriteBatch.Draw(borderTexture, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, roomSize, roomSize), Color.Red);
            // border thickness
            int borderThickness = 2;
            // borders around mini-map
            Rectangle topBorder = new Rectangle((int)mapPosition.X, (int)mapPosition.Y, mapWidth, borderThickness);
            Rectangle bottomBorder = new Rectangle((int)mapPosition.X, (int)(mapPosition.Y + mapHeight - borderThickness), mapWidth, borderThickness);
            Rectangle leftBorder = new Rectangle((int)mapPosition.X, (int)mapPosition.Y, borderThickness, mapHeight);
            Rectangle rightBorder = new Rectangle((int)(mapPosition.X + mapWidth - borderThickness), (int)mapPosition.Y, borderThickness, mapHeight);

            spriteBatch.Draw(borderTexture, topBorder, Color.Black);
            spriteBatch.Draw(borderTexture, bottomBorder, Color.Black);
            spriteBatch.Draw(borderTexture, leftBorder, Color.Black);
            spriteBatch.Draw(borderTexture, rightBorder, Color.Black);
        }

        public void Dispose()
        {
            if (borderTexture != null)
                borderTexture.Dispose();
        }
    }
}