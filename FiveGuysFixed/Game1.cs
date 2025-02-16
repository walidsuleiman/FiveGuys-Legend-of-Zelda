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




//using FiveGuys.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FiveGuysFixed
{
    public class Game1 : Game
    {
        //private MouseController mouseController;
        private KeyboardController keyboardController;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Vector2 position;
        public List<IBlock> blocks;
        public List<IEnemy> enemies;
        public List<IProjectile> projectiles;// stores all active projectiles
        private Texture2D bossTexture;
        private Texture2D enemyTexture;
        private Texture2D blockTexture;


        public int activeEnemyIndex;


        private GameState gameState;

        public Player Player { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this._graphics.PreferredBackBufferHeight = 720;
            this._graphics.PreferredBackBufferWidth = 1280;
        }

        protected override void Initialize()
        {

            GameState.WindowWidth = GraphicsDevice.Viewport.Width;
            GameState.WindowHeight = GraphicsDevice.Viewport.Height;
            GameState.PlayerState = new PlayerState(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));

            Player = new Player();
            keyboardController = new KeyboardController(this);

            enemies = new List<IEnemy>();
            activeEnemyIndex = 0;
            projectiles = new List<IProjectile>();
            blocks = new List<IBlock>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.LoadContent(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.LoadContent(Content);

            enemyTexture = Content.Load<Texture2D>("Enemy_SpriteSheet");
            Texture2D bossTexture = Content.Load<Texture2D>("Boss_SpriteSheet");

            blockTexture = Content.Load<Texture2D>("BlockSprite");


            // initialize enemies after texture is loaded
            enemies.Add(new Keese(enemyTexture, 100, 100));
            enemies.Add(new Moblin(enemyTexture, 300, 200));
            enemies.Add(new Gel(enemyTexture, 500, 300));
            enemies.Add(new Aquamentus(bossTexture, 600, 500, projectiles));// pass projectile list

            blocks.Add(new Block(blockTexture, 100, 200));


            for (int i = 0; i < 64; i = i + 16)
            {
                int x = 400 + i;
                int y = 600;
                blocks.Add(new Block(blockTexture, x, y));

            }


        }


        protected override void Update(GameTime gameTime)
        {
            //mouseController.Update();

            keyboardController.Update();

            Player.Update(gameTime);

            //if (enemies.Count > 0)
            //{
            //    enemies[activeEnemyIndex].Update(gameTime);
            //}
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            foreach (var proj in projectiles)
            {
                proj.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void Reset()
        {
            position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGoldenrodYellow);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Player.Draw(_spriteBatch);
            _spriteBatch.End();


            _spriteBatch.Begin();

            Debug.WriteLine(activeEnemyIndex);

            //if (enemies.Count > 0)
            //{
            //    enemies[activeEnemyIndex].Draw(_spriteBatch);
            //}w

            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }

            foreach (var proj in projectiles)
            {
                proj.Draw(_spriteBatch);
            }

            //draw object
            foreach (var block in blocks)
            {
                block.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
