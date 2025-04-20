using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FiveGuysFixed.GameStates
{
    // loads and stores all textures and fonts for the game
    public class ContentLoader
    {
        #region Texture Properties

        // link texture
        public Texture2D LinkTexture;

        // enemy textures
        public Texture2D BossTexture;
        public Texture2D bossTexture; // probably duplicate, can clean up later
        public Texture2D enemyTexture;

        // block textures
        public Texture2D blockTexture;
        public Texture2D yellowBlockTexture;
        public Texture2D treeBlockTexture;
        public Texture2D whiteBlockTexture;
        public Texture2D greenBlockTexture;

        // item textures
        public Texture2D redPotionTexture;
        public Texture2D bluePotionTexture;
        public Texture2D bombTexture;
        public Texture2D foodTexture;
        public Texture2D rupeeTexture;
        public Texture2D weaponTexture;
        public Texture2D triforceTexture;

        public Texture2D HudTexture;
        public Texture2D miniMapTexture;

        // game font
        public SpriteFont DefaultFont;
        #endregion

        public ContentLoader() { }

        // main method to load everything using ContentManager
        public void LoadContent(ContentManager content)
        {
            LoadEnemyTextures(content);
            LoadBlockTextures(content);
            LoadItemTextures(content);
            LoadUITextures(content);

            DefaultFont = content.Load<SpriteFont>("DefaultFont");
        }

        #region Private Loading Methods

        private void LoadEnemyTextures(ContentManager content)
        {

            BossTexture = content.Load<Texture2D>("Boss_SpriteSheet");
            enemyTexture = content.Load<Texture2D>("Enemy_SpriteSheet");
        }

        //block/environment textures
        private void LoadBlockTextures(ContentManager content)
        {
            blockTexture = content.Load<Texture2D>("BlockSprite");
            yellowBlockTexture = content.Load<Texture2D>("YellowBlockSprite");
            treeBlockTexture = content.Load<Texture2D>("TreeBlockSprite");
            whiteBlockTexture = content.Load<Texture2D>("WhiteBlockSprite");
            greenBlockTexture = content.Load<Texture2D>("GreenBlockSprite");
        }

        //item textures like potions and rupees
        private void LoadItemTextures(ContentManager content)
        {
            redPotionTexture = content.Load<Texture2D>("RedPotionSprite");
            bluePotionTexture = content.Load<Texture2D>("BluePotionSprite");
            bombTexture = content.Load<Texture2D>("linkSprite"); // placeholder?
            foodTexture = content.Load<Texture2D>("linkSprite"); // same here
            rupeeTexture = content.Load<Texture2D>("rupeeSprite");
            weaponTexture = content.Load<Texture2D>("Weapon");
            triforceTexture = content.Load<Texture2D>("Triforce");
        }

        //HUD and minimap textures
        private void LoadUITextures(ContentManager content)
        {
            HudTexture = content.Load<Texture2D>("HUDClean");
            miniMapTexture = content.Load<Texture2D>("Minimap1");
        }

        #endregion
    }
}