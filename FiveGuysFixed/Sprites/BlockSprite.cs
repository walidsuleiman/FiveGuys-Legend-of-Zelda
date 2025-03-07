using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Blocks;  // or whichever namespace you prefer

namespace FiveGuysFixed.Blocks
{
    public class BlockSprite : IBlock
    {
        public Texture2D Texture { get; set; }

        // Source rectangle portion
        private int spriteX, spriteY, srcWidth, srcHeight;

        // On-screen position
        private int posX, posY;
        public int Height { get { return this.Height; } }
        public int Width { get { return this.Width; } }

        public Rectangle BoundingBox { get; set; }

        public BlockSprite(Texture2D texture,
                           int x, int y,        // source x,y on the sprite sheet
                           int w, int h,        // source width,height
                           int px, int py)      // position on screen
        {
            Texture = texture;
            spriteX = x;
            spriteY = y;
            srcWidth = w;
            srcHeight = h;
            posX = px;
            posY = py;
            BoundingBox = new Rectangle(posX, posY, srcWidth, srcHeight);
        }

        public void Update(GameTime gameTime)
        {
            // If blocks don't animate, do nothing
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int scale = 5;
            // Build the source rectangle
            Rectangle sourceRect = new Rectangle(spriteX, spriteY, srcWidth, srcHeight);

            // Build the destination rectangle - same size as source, but placed at posX,posY
            Rectangle destRect = new Rectangle(posX, posY, srcWidth, srcHeight);

            spriteBatch.Draw(Texture, destRect, sourceRect, Color.White);
        }

        public bool IsCollidable()
        {
            throw new System.NotImplementedException();
        }

        // If you want the old "CycleBlockForward/Backward" logic, copy those here, too
        // ...
    }
}
