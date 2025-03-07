using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        public Texture2D bossTexture;
        public Texture2D enemyTexture;
        public Texture2D blockTexture;
        public Texture2D yellowBlockTexture;
        public Texture2D treeBlockTexture;
        public Texture2D whiteBlockTexture;
        public Texture2D greenBlockTexture;
        public Texture2D redPotionTexture;
        public Texture2D bluePotionTexture;
        public Texture2D bombTexture;
        public Texture2D foodTexture;
        public Texture2D rupeeTexture;
        public Texture2D HudTexture;
        public void LoadContent(ContentManager content)
        {
            //LinkTexture = content.Load<Texture2D>("linkSheet");
            BossTexture = content.Load<Texture2D>("Boss_SpriteSheet");

            enemyTexture = content.Load<Texture2D>("Enemy_SpriteSheet");
            blockTexture = content.Load<Texture2D>("BlockSprite");
            yellowBlockTexture = content.Load<Texture2D>("YellowBlockSprite");
            treeBlockTexture = content.Load<Texture2D>("TreeBlockSprite");
            whiteBlockTexture = content.Load<Texture2D>("WhiteBlockSprite");
            greenBlockTexture = content.Load<Texture2D>("GreenBlockSprite");


            redPotionTexture = content.Load<Texture2D>("RedPotionSprite");
            bluePotionTexture = content.Load<Texture2D>("BluePotionSprite");
            bombTexture = content.Load<Texture2D>("linkSprite");
            foodTexture = content.Load<Texture2D>("linkSprite");
            rupeeTexture = content.Load<Texture2D>("rupeeSprite");

            //HUD
            HudTexture = content.Load<Texture2D>("HUD");
        }

        public ContentLoader() { }

    }
}