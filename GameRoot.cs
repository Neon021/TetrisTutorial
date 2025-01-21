using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TetrisTutorial.Assets;
using TetrisTutorial.Utils;

namespace TetrisTutorial
{
    public class GameRoot : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera _camera;
        private BasicEffect _shader;
        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            _shader = new BasicEffect(GraphicsDevice)
            {
                View = _camera.View,
                Projection = _camera.Projection
            };

            _shader.EnableDefaultLighting();
            _shader.PreferPerPixelLighting = true;
            _shader.SpecularPower = 16f;
            #endregion
            
            _spriteBatch.Begin();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach(ModelMesh modelMesh in Models.CubeObject.Meshes)
            {
                // This is generic- eventhough the cube only has one meshpart, 
                // Let's keep the code so you can experiment with different models.
                foreach (ModelMeshPart modelMeshPart in modelMesh.MeshParts)
                {
                    modelMeshPart.Effect = _shader;
                    _shader.World = Matrix.CreateScale(10) * Matrix.CreateRotationY(0.5f) * Matrix.CreateTranslation(0, 0, -3f);
                    _shader.DiffuseColor = Color.Red.ToVector3();
                }
                modelMesh.Draw();
            }
            base.Draw(gameTime);
        }
    }
}
