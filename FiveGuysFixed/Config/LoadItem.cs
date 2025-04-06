using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Config
{
    // stores info for drawing a sprite from a texture
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

    // loads and organizes enemy and item sprite data
    public class LoadItems
    {
        // projectiles
        public ItemData bomb;
        public ItemData fireball;
        public ItemData boomerang;

        // enemies
        public ItemData keese;
        public ItemData stalfos;
        public ItemData gel;

        public ItemData leftMoblin;
        public ItemData rightMoblin;
        public ItemData upMoblin;
        public ItemData downMoblin;

        public ItemData leftGoriya;
        public ItemData rightGoriya;
        public ItemData downGoriya;
        public ItemData upGoriya;

        // boss
        public ItemData Aquamentus;
        public ItemData AquamentusAttack;

        public LoadItems(Texture2D texture, Texture2D enemy, Texture2D bosses)
        {
            InitializeProjectiles(texture);
            InitializeBasicEnemies(enemy);
            InitializeDirectionalEnemies(enemy);
            InitializeBosses(bosses);
        }

        // creates a new sprite from item data
        public ISprite getNewItem(ItemData item)
        {
            return new Sprite(
                item.Texture,
                item.X,
                item.Y,
                item.Dimensions,
                item.Dimensions,
                item.Frames
            );
        }

 
        private void InitializeProjectiles(Texture2D texture)
        {
            fireball = new ItemData(texture, 16, 80, 16, 2);
            boomerang = new ItemData(texture, 16, 48, 16, 3);
        }


        private void InitializeBasicEnemies(Texture2D enemy)
        {
            keese = new ItemData(enemy, 16, 32, 16, 2);
            stalfos = new ItemData(enemy, 16, 96, 16, 2);
            gel = new ItemData(enemy, 16, 0, 16, 2);
        }

        private void InitializeDirectionalEnemies(Texture2D enemy)
        {
            leftMoblin = new ItemData(enemy, 80, 112, 16, 2);
            rightMoblin = new ItemData(enemy, 48, 112, 16, 2);
            downMoblin = new ItemData(enemy, 16, 112, 16, 2);
            upMoblin = new ItemData(enemy, 112, 112, 16, 2);

            leftGoriya = new ItemData(enemy, 80, 48, 16, 2);
            rightGoriya = new ItemData(enemy, 48, 48, 16, 2);
            downGoriya = new ItemData(enemy, 16, 48, 16, 2);
            upGoriya = new ItemData(enemy, 112, 48, 16, 2);
        }


        private void InitializeBosses(Texture2D bosses)
        {
            Aquamentus = new ItemData(bosses, 64, 0, 32, 4);
            AquamentusAttack = new ItemData(bosses, 0, 0, 32, 2);
        }
    }
}
