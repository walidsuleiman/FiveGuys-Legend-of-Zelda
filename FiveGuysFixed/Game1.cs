using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Controls;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.Items;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.HUD;
using FiveGuysFixed.Collisions;


//using FiveGuys.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using FiveGuysFixed.Weapons___Items;
using FiveGuysFixed.Config;
using FiveGuysFixed.RoomHandling;
using Microsoft.Xna.Framework.Content;

namespace FiveGuysFixed
{
    public class Game1 : Game
    {
        private MouseController mouseController;
        private KeyboardController keyboardController;
        private GamepadController gamepadController;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Vector2 position;
        public Hearts hearts;
        public List<IBlock> blocks;
        public List<IEnemy> enemies;
        public List<IItem> items;
        public List<IItem> weapons;
        public List<IProjectile> projectiles;// stores all active projectiles
        private Texture2D bossTexture;
        private Texture2D enemyTexture;
        private Texture2D blockTexture;
        private Texture2D yellowBlockTexture;
        private Texture2D treeBlockTexture;
        private Texture2D whiteBlockTexture;
        private Texture2D greenBlockTexture;
        private Texture2D redPotionTexture;
        private Texture2D bluePotionTexture;
        private Texture2D bombTexture;
        private Texture2D foodTexture;
        private Texture2D rupeeTexture;
        private Texture2D heartTexture;
        private CollisionDetector collisionDetector;
        private CollisionHandler collisionHandler;


        public int activeWeaponIndex;
        public int activeItemIndex;
        public int activeEnemyIndex;
        public int activeBlockIndex;


        private GameState gameState;

        public Player Player { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this._graphics.PreferredBackBufferHeight = 720;
            this._graphics.PreferredBackBufferWidth = 1280;
            collisionDetector = new CollisionDetector();
            collisionHandler = new CollisionHandler();
        }

        protected override void Initialize()
        {

            centreScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            GameState.WindowWidth = GraphicsDevice.Viewport.Width;
            GameState.WindowHeight = GraphicsDevice.Viewport.Height;
            GameState.PlayerState = new PlayerState(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            GameState.roomManager = new RoomManager();
            GameState.currentRoomContents = new CurrentRoomContents();
            GameState.contentLoader = new ContentLoader();
            GameState.currentRoomID = 1;
            GameState.Player = new Player();

            keyboardController = new KeyboardController(this);
            mouseController = new MouseController(this);
            gamepadController = new GamepadController(this);

            enemies = new List<IEnemy>();
            activeEnemyIndex = 0;
            projectiles = new List<IProjectile>();
            blocks = new List<IBlock>();
            hearts = new Hearts();
            activeBlockIndex = 0;
            items = new List<IItem>();
            activeItemIndex = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameState.Player.LoadContent(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameState.Player.LoadContent(Content);

            enemyTexture = Content.Load<Texture2D>("Enemy_SpriteSheet");
            //bossTexture = Content.Load<Texture2D>("Boss_SpriteSheet");
            GameState.contentLoader.LoadContent(Content);
            blockTexture = Content.Load<Texture2D>("BlockSprite");
            yellowBlockTexture = Content.Load<Texture2D>("YellowBlockSprite");
            treeBlockTexture = Content.Load<Texture2D>("TreeBlockSprite");
            whiteBlockTexture = Content.Load<Texture2D>("WhiteBlockSprite");
            greenBlockTexture = Content.Load<Texture2D>("GreenBlockSprite");


            redPotionTexture = Content.Load<Texture2D>("RedPotionSprite");
            bluePotionTexture = Content.Load<Texture2D>("BluePotionSprite");
            bombTexture = Content.Load<Texture2D>("linkSheet");
            foodTexture = Content.Load<Texture2D>("linkSheet");
            rupeeTexture = Content.Load<Texture2D>("rupeeSprite");
            //heartTexture = Content.Load<Texture2D>("heart");





            // initialize enemies after texture is loaded
            //enemies.Add(new Keese(enemyTexture, 100, 100));
            //enemies.Add(new Moblin(enemyTexture, 300, 200));
            //enemies.Add(new Gel(enemyTexture, 500, 300));
            //enemies.Add(new Aquamentus(bossTexture, 600, 500, projectiles));// pass projectile list

            //enemies.Add(new Keese(loadItems, 100, 100));
            //enemies.Add(new Gel(loadItems, 500, 300));
            ////enemies.Add(new Aquamentus(loadItems, 600, 500, projectiles));
            //enemies.Add(new Goriya(loadItems, 700, 150, projectiles));
            //enemies.Add(new Octorok(loadItems, 800, 250));
            //enemies.Add(new Stalfos(loadItems, 900, 350));
            //enemies.Add(new Tektike(loadItems, 1000, 450));

            //blocks.Add(new Hearts(heartTexture, 1150, 30));
            //blocks.Add(new Hearts(heartTexture, 1100, 30));
            //blocks.Add(new Hearts(heartTexture, 1050, 30));

            //blocks.Add(new RedBlock(yellowBlockTexture, 900, 350));
            //blocks.Add(new YellowBlock(yellowBlockTexture, 500, 650));
            //blocks.Add(new Block(blockTexture, 100, 200));
            //blocks.Add(new TreeBlock(treeBlockTexture, 550, 150));
            //blocks.Add(new WhiteBlock(whiteBlockTexture, 750, 250));
            //blocks.Add(new GreenBlock(greenBlockTexture, 1050, 650));


            //items.Add(new RedPotion(redPotionTexture, 1000, 200));
            //items.Add(new BluePotion(bluePotionTexture, 200, 600));
            //items.Add(new Bomb(bombTexture, 350, 150));
            //items.Add(new Food(foodTexture, 800, 500));
            //items.Add(new GreenRupee(rupeeTexture, 100, 100));
            //items.Add(new RedRupee(rupeeTexture, 200, 300));




            GameState.roomManager.LoadRoomsFromXML("C:\\Users\\kanir\\source\\repos\\walidsuleiman\\FiveGuys-Legend-of-Zelda\\FiveGuysFixed\\RoomDirectory.xml");
            
        }


        protected override void Update(GameTime gameTime)
        {
            mouseController.Update();

            keyboardController.Update();

            //gamepadController.Update();


            GameState.Player.Update(gameTime);
            GameState.roomManager.SwitchRoom(GameState.currentRoomID);
            RoomRenderer.update(gameTime);

            if (enemies.Count > 0)
            {
                enemies[activeEnemyIndex].Update(gameTime);
            }

            if (blocks.Count > 0)
            {
                blocks[activeBlockIndex].Update(gameTime);
            }

            if (items.Count > 0)
            {
                items[activeItemIndex].Update(gameTime);
            }

            foreach (var proj in projectiles)
            {
                proj.Update(gameTime);
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(gameTime);
                if (projectiles[i].IsFinished())
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }

            //call detectCollision
            //collisionDetector.IsColliding(Player,);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGoldenrodYellow);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GameState.Player.Draw(_spriteBatch);

            RoomRenderer.Draw(_spriteBatch);
            hearts.Draw(_spriteBatch);

            //if (enemies.Count > 0)
            //{
            //    enemies[activeEnemyIndex].Draw(_spriteBatch);
            //}

            //foreach (var proj in projectiles)
            //{
            //    proj.Draw(_spriteBatch);
            //}


            if (blocks.Count > 0)
            {
                blocks[activeBlockIndex].Draw(_spriteBatch);
            }

            //if (items.Count > 0)
            //{
            //    items[activeItemIndex].Draw(_spriteBatch);
            //}

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}