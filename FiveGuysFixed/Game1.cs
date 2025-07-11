﻿using System;
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using FiveGuysFixed.Weapons___Items;
using FiveGuysFixed.Config;
using FiveGuysFixed.RoomHandling;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;
using FiveGuysFixed.GUI;

namespace FiveGuysFixed
{
    // main game class that sets up and runs the game loop.
    public class Game1 : Game
    {
        private MouseController mouseController;
        private KeyboardController keyboardController;
        private GamepadController gamepadController;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Vector2 centreScreen;

        //public List<IBlock> blocks;
        //public List<IEnemy> enemies;
        //public List<IItem> items;
        //public List<IItem> weapons;
        //public List<IProjectile> projectiles;

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
        private PlayerEnemyCollisionResolver playerEnemyCollisionResolver;
        private PlayerBlockCollisionResolver playerBlockCollisionResolver;
        private PlayerItemCollisionResolver playerItemCollisionResolver;
        private ProjectileCollisionResolver projectileCollisionResolver;
        private EnemyBlockCollisionResolver enemyBlockCollisionResolver;

        //public int activeWeaponIndex;
        //public int activeItemIndex;
        //public int activeEnemyIndex;
        //public int activeBlockIndex;

        private GameState gameState;
        public Player Player { get; set; }
        public Song backgroundMusic;
        public bool isMuted = false; // if true, no background music
        public bool AquamentusModeEnabled { get; set; } = false;

        private KeyboardState previousState;
        private KeyboardState currentState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // window size
            _graphics.PreferredBackBufferHeight = 1160;
            _graphics.PreferredBackBufferWidth = 1280;

            collisionDetector = new CollisionDetector();
        }

        // called once at game start and sets up states, controllers, etc
        protected override void Initialize()
        {
            centreScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            // set up shared GameState stuff
            GameState.WindowWidth = GraphicsDevice.Viewport.Width;
            GameState.WindowHeight = 880;
            GameState.PlayerState = new PlayerState(new Vector2(GameState.WindowWidth / 2, GameState.WindowHeight / 2));
            GameState.roomManager = new RoomManager();
            GameState.contentLoader = new ContentLoader();
            GameState.currentRoomID = 1;
            GameState.Player = new Player(this);
            GameState.transitionManager = new TransitionManager();
            GameState.Game = this;

            keyboardController = new KeyboardController(this);
            mouseController = new MouseController(this);
            gamepadController = new GamepadController(this);

            previousState = Keyboard.GetState();

            //InitializeEntityLists();

            base.Initialize();
        }

        // sets up lists for enemies, blocks, items, etc
        
        /*
        private void InitializeEntityLists()
        {
            enemies = new List<IEnemy>();
            activeEnemyIndex = 0;

            projectiles = new List<IProjectile>();

            blocks = new List<IBlock>();
            activeBlockIndex = 0;

            items = new List<IItem>();
            activeItemIndex = 0;
        }
        */
        

        protected override void LoadContent()
        {
            GameStateManager.Initialize(Content);
            GameStateManager.SetState(new TitleScreenState(this));

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GameState.Player.LoadContent(Content);

            LoadTextures();

            var projectileLoader = new ProjectileLoader(GameState.contentLoader.weaponTexture);
            var enemyLoader = new EnemyLoader(GameState.contentLoader.enemyTexture);
            var bossLoader = new BossLoader(GameState.contentLoader.BossTexture);

            EnemySpriteFactory.Instance.Initialize(projectileLoader, enemyLoader, bossLoader);

            LoadRoomData();

            LoadBackgroundMusic();
        }

        private void LoadTextures()
        {
            enemyTexture = Content.Load<Texture2D>("Enemy_SpriteSheet");
            GameState.contentLoader.LoadContent(Content);

            blockTexture = Content.Load<Texture2D>("BlockSprite");
            yellowBlockTexture = Content.Load<Texture2D>("YellowBlockSprite");
            treeBlockTexture = Content.Load<Texture2D>("TreeBlockSprite");
            whiteBlockTexture = Content.Load<Texture2D>("WhiteBlockSprite");
            greenBlockTexture = Content.Load<Texture2D>("GreenBlockSprite");

            redPotionTexture = Content.Load<Texture2D>("RedPotionSprite");
            bluePotionTexture = Content.Load<Texture2D>("BluePotionSprite");
            bombTexture = Content.Load<Texture2D>("linkSprite");
            foodTexture = Content.Load<Texture2D>("linkSprite");
            rupeeTexture = Content.Load<Texture2D>("rupeeSprite");
        }

        // reads room data from an XML file and picks the first room
        private void LoadRoomData()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "RoomDirectory.xml");
            GameState.roomManager.LoadRoomsFromXML(path);
            GameState.roomManager.SwitchRoom(GameState.currentRoomID);
        }

        private void LoadBackgroundMusic()
        {
            backgroundMusic = Content.Load<Song>("Zelda_Bgm");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(backgroundMusic);
        }

        // called every frame by MonoGame and updates game logic or handles transitions
        protected override void Update(GameTime gameTime)
        {
            currentState = Keyboard.GetState();

            if (GameState.IsTransitioning)
            {
                GameState.transitionManager.Update(gameTime);
                base.Update(gameTime);
                previousState = currentState;
                return;
            }

            GameStateManager.Update(gameTime);

            base.Update(gameTime);

            if (GameState.ShouldSwitchRoom)
            {
                GameState.roomManager.SwitchRoom(GameState.currentRoomID);
                GameState.ShouldSwitchRoom = false;
            }

            previousState = currentState;
        }

        // custom game logic per frame, called by the active GamePlayState

        public void ReplaceAllEnemiesWithAquamentus()
        {
            // get current room
            var currentRoom = GameState.roomManager.getCurrentRoom();

            // log starting state
            System.Diagnostics.Debug.WriteLine($"Replacing enemies with Aquamentus. Current enemy count: {currentRoom.Enemies.Count}");

            // save original enemy positions
            List<Vector2> enemyPositions = new List<Vector2>();
            foreach (var enemy in currentRoom.Enemies)
            {
                enemyPositions.Add(enemy.Position);
                System.Diagnostics.Debug.WriteLine($"Found enemy at position: {enemy.Position}");
            }

            // clear current enemies
            currentRoom.Enemies.Clear();
            System.Diagnostics.Debug.WriteLine("Cleared all enemies");

            // create Aquamentus at each position
            foreach (var position in enemyPositions)
            {
                try
                {
                    // use the proper Animation namespace instead of Sprites
                    var aquamentusSprite = new FiveGuysFixed.Animation.Sprite(
                        GameState.contentLoader.BossTexture,
                        64, 0, 32, 32, 4
                    );

                    var aquamentusAttackSprite = new FiveGuysFixed.Animation.Sprite(
                        GameState.contentLoader.BossTexture,
                        0, 0, 32, 32, 2
                    );

                    var aquamentus = new FiveGuysFixed.Enemies.Aquamentus(
                        position,
                        aquamentusSprite,
                        aquamentusAttackSprite,
                        currentRoom.Projectiles
                    );

                    currentRoom.Enemies.Add(aquamentus);
                    System.Diagnostics.Debug.WriteLine($"Added Aquamentus at position: {position}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error creating Aquamentus: {ex.Message}");
                }
            }

            System.Diagnostics.Debug.WriteLine($"Replacement complete. New enemy count: {currentRoom.Enemies.Count}");
        }
        public void GameUpdateLogic(GameTime gameTime)
        {
            if (GameState.PendingBomb)
            {
                GameState.roomManager.getCurrentRoom().Items.Add(new BombPlaced(GameState.contentLoader.bombTexture, GameState.PendingPos));
                GameState.PendingBomb = false;
            }
            
            for (int i = 0; i < GameState.roomManager.getCurrentRoom().Items.Count; i++)
            {
                var it = GameState.roomManager.getCurrentRoom().Items[i];
                it.Update(gameTime);

                if (it is BombPlaced bp && bp.IsFinished)
                    GameState.roomManager.getCurrentRoom().Items.RemoveAt(i--);
            }

            mouseController.Update();
            keyboardController.Update();

            GameState.Player.Update(gameTime);

            // if there's a transition, handle that first
            if (GameState.IsTransitioning)
            {
                GameState.transitionManager.Update(gameTime);
                return;
            }

            CheckTransition.CheckRoomExit();

            //if (GameState.ShouldSwitchRoom)
            //{
            //    GameState.roomManager.SwitchRoom(GameState.currentRoomID);
            //    GameState.ShouldSwitchRoom = false;
            //}

            RoomRenderer.Update(gameTime);

            UpdateActiveEntities(gameTime);

            UpdateProjectiles(gameTime);
        }

        // updates one enemy, block, and item each, plus any projectiles in our own list
        private void UpdateActiveEntities(GameTime gameTime)
        {
            var room = GameState.roomManager.getCurrentRoom();

            if (room.Enemies.Count > 0)
                room.Enemies[0].Update(gameTime); // or loop through a few if needed

            if (room.Blocks.Count > 0)
                room.Blocks[0].Update(gameTime);

            if (room.Items.Count > 0)
                room.Items[0].Update(gameTime);
        }

        // updates projectiles in the current room and removes finished ones
        private void UpdateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < GameState.roomManager.getCurrentRoom().Projectiles.Count; i++)
            {
                GameState.roomManager.getCurrentRoom().Projectiles[i].Update(gameTime);

                if (GameState.roomManager.getCurrentRoom().Projectiles[i].IsFinished())
                {
                    GameState.roomManager.getCurrentRoom().Projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGoldenrodYellow);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GameStateManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // called by GamePlayState to render the scene like player, room, HUD
        public void GameDrawLogic(SpriteBatch spriteBatch)
        {
            RoomRenderer.Draw(spriteBatch);


            GameState.Player.Draw(spriteBatch);

            //spriteBatch.Draw(GameState.contentLoader.blockTexture, new Vector2(1195, 397), new Rectangle(863, 77, 17, 32),  Color.White, 0, new Vector2(0,0), 5.0f, SpriteEffects.None, 0);//right
            //spriteBatch.Draw(GameState.contentLoader.blockTexture, new Rectangle(848, 44, 17, 32), new Rectangle(0, 397, 17*5, 32*5), Color.White);//left
            //spriteBatch.Draw(GameState.contentLoader.blockTexture, new Rectangle(848, 11, 32, 17), new Rectangle(537, 0, 32*5, 17*5), Color.White);//up
            //spriteBatch.Draw(GameState.contentLoader.blockTexture, new Rectangle(848, 125, 32, 17), new Rectangle(537, 795, 32 * 5, 17 * 5), Color.White);//down

            


            GameState.HUD.Draw(spriteBatch);

            
            foreach (var projectile in GameState.roomManager.getCurrentRoom().Projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            

            if (GameState.roomManager.getCurrentRoom().Blocks.Count > 0)
            {
                GameState.roomManager.getCurrentRoom().Blocks[0].Draw(spriteBatch);
            }

            //foreach (var it in GameState.roomManager.getCurrentRoom().Items)
            //    it.Draw(spriteBatch);
        }

        public bool IsKeyPress(Keys key)
        {
            return currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
        }
    }
}
