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



//using FiveGuys.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed
{
    public class Game1 : Game
    {
        //private MouseController mouseController;
        private KeyboardController keyboardController;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 position;
        private List<IEnemy> enemies;
        private List<IProjectile> projectiles;// stores all active projectiles
        private Texture2D bossTexture;
        private Texture2D enemyTexture;



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
            //keyboardController = new KeyboardController(this);
            //mouseController = new MouseController(this);

            GameState.WindowWidth = GraphicsDevice.Viewport.Width;
            GameState.WindowHeight = GraphicsDevice.Viewport.Height;
            GameState.PlayerState = new PlayerState(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            
            Player = new Player();
            keyboardController = new KeyboardController(this);

            enemies = new List<IEnemy>();
            projectiles = new List<IProjectile>();

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

            // initialize enemies after texture is loaded
            enemies.Add(new Keese(enemyTexture, 100, 100));
            enemies.Add(new Moblin(enemyTexture, 300, 200));
            enemies.Add(new Gel(enemyTexture, 500, 300));
            enemies.Add(new Aquamentus(bossTexture, 600, 500, projectiles));// pass projectile list
        }
        

        protected override void Update(GameTime gameTime)
        {
            //mouseController.Update();

            keyboardController.Update();
            
            Player.Update(gameTime);

            keyboardController.Update();
            Player.Update(gameTime);

            // Update all enemies
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
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

            // draw Enemy
            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }
            // draw all projectiles
            foreach (var proj in projectiles)
            {
                proj.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
