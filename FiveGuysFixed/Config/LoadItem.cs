using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Config
{
    public class ItemData
    {
        public Texture2D Texture { get; }
        public int X { get; }
        public int Y { get; }
        public int Dimensions { get; }
        public int Frames { get; }

        public ItemData(Texture2D texture, int x, int y, int dimensions, int frames)
        {
            Texture = texture;
            X = x;
            Y = y;
            Dimensions = dimensions;
            Frames = frames;
        }
    }

    public class LoadItems
    {
        // ――― projectiles
        public ItemData bomb;
        public ItemData fireball;
        public ItemData boomerang;

        // ――― basic enemies
        public ItemData keese;
        public ItemData stalfos;
        public ItemData gel;
        public ItemData tektike;                   

        // ――― directional mobs
        public ItemData leftMoblin, rightMoblin, upMoblin, downMoblin;
        public ItemData leftGoriya, rightGoriya, upGoriya, downGoriya;
        public ItemData octorokLeft, octorokRight, octorokUp, octorokDown; 

        // ――― boss
        public ItemData Aquamentus;
        public ItemData AquamentusAttack;

        public LoadItems(Texture2D projTex, Texture2D enemyTex, Texture2D bossTex)
        {
            InitializeProjectiles(projTex);
            InitializeBasicEnemies(enemyTex);
            InitializeDirectionalEnemies(enemyTex);
            InitializeOctorok(enemyTex);          
            InitializeBosses(bossTex);
        }

        public ISprite getNewItem(ItemData data) =>
            new Sprite(data.Texture, data.X, data.Y, data.Dimensions, data.Dimensions, data.Frames);

        // ────────────────────────────
        private void InitializeProjectiles(Texture2D tex)
        {
            fireball = new ItemData(tex, 16, 80, 16, 2);
            boomerang = new ItemData(tex, 16, 48, 16, 3);
        }

        private void InitializeBasicEnemies(Texture2D tex)
        {
            keese = new ItemData(tex, 16, 32, 16, 2);
            stalfos = new ItemData(tex, 16, 96, 16, 2);
            gel = new ItemData(tex, 16, 0, 16, 2);
            tektike = new ItemData(tex, 16, 80, 16, 2);    
        }

        private void InitializeDirectionalEnemies(Texture2D tex)
        {
            leftMoblin = new ItemData(tex, 80, 112, 16, 2);
            rightMoblin = new ItemData(tex, 48, 112, 16, 2);
            downMoblin = new ItemData(tex, 16, 112, 16, 2);
            upMoblin = new ItemData(tex, 112, 112, 16, 2);

            leftGoriya = new ItemData(tex, 80, 48, 16, 2);
            rightGoriya = new ItemData(tex, 48, 48, 16, 2);
            downGoriya = new ItemData(tex, 16, 48, 16, 2);
            upGoriya = new ItemData(tex, 112, 48, 16, 2);
        }

        private void InitializeOctorok(Texture2D tex)         
        {
            octorokDown = new ItemData(tex, 16, 304, 16, 2);
            octorokUp = new ItemData(tex, 112, 304, 16, 2);
            octorokRight = new ItemData(tex, 80, 304, 16, 2);
            octorokLeft = new ItemData(tex, 48, 304, 16, 2);
        }

        private void InitializeBosses(Texture2D tex)
        {
            Aquamentus = new ItemData(tex, 64, 0, 32, 4);
            AquamentusAttack = new ItemData(tex, 0, 0, 32, 2);
        }
    }
}
