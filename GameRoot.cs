using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TetrisTutorial.Assets;
using TetrisTutorial.Scenes;
using TetrisTutorial.Utils;

namespace TetrisTutorial
{
    public class GameRoot : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static SceneManager SceneManager;
        public static BasicEffect BasicShader;
        private Camera _camera;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            SceneManager = new SceneManager();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
#if DEBUG
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = false;
#else
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
#endif
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Models.Initialize(Content);
            _camera = new Camera(new Vector3(0, 0, 5), new Vector3(0, 0, -5), _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, MathHelper.ToRadians(60));

            #region Shader
            BasicShader = new BasicEffect(GraphicsDevice)
            {
                View = _camera.View,
                Projection = _camera.Projection
            };

            BasicShader.EnableDefaultLighting();
            BasicShader.PreferPerPixelLighting = true;
            BasicShader.SpecularPower = 16f;
            #endregion
            
            SceneManager.PushScene(new TitleScreen());
            _spriteBatch.Begin();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManager.Draw(_spriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}
