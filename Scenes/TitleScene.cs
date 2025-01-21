using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisTutorial.Assets.Tetrimino;
using TetrisTutorial.Factories;
using TetrisTutorial.Utils;

namespace TetrisTutorial.Scenes
{
    internal class TitleScene : IScene
    {
        private float _angle;
        private Matrix _world;
        private Tetrimino _tetrimino;

        public TitleScene()
        {
            TetriminoFactory factory = new();
            _tetrimino = factory.Generate(Enums.Tetriminoes.J);
        }

        public void Update(GameTime gameTime)
        {
            _angle += 0.75f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _angle %= MathHelper.TwoPi;

            _world = Matrix.CreateScale(5) * Matrix.CreateRotationY(_angle) * Matrix.CreateTranslation(0, 0, -3f);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _tetrimino.Draw(_world);
        }
    }
}
