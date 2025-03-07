using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    public class ContentLoader
    {
        public Texture2D LinkTexture;
        public Texture2D BossTexture;
        public void LoadContent(ContentManager content)
        {
            //LinkTexture = content.Load<Texture2D>("linkSheet");
            BossTexture = content.Load<Texture2D>("Boss_SpriteSheet");
        }

        public ContentLoader() { }

    }
}