using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Enemies
{
    public class EnemySprite : Sprite
    {
        public EnemySprite(Texture2D texture, int sourceX, int sourceY, int width, int height, int frames = 1)
            : base(texture, sourceX, sourceY, width, height, frames)
        { }
    }
}