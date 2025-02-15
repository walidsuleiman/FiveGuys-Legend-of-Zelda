using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Controls;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.LinkPlayer;



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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.LoadContent(Content);
        }
        

        protected override void Update(GameTime gameTime)
        {
            //mouseController.Update();

            keyboardController.Update();
            
            Player.Update(gameTime);

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

            base.Draw(gameTime);
        }
    }
}
