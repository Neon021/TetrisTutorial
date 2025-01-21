using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisTutorial.Assets.Tetrimino;
using TetrisTutorial.Factories;
using TetrisTutorial.Grid;
using TetrisTutorial.Utils;

namespace TetrisTutorial.Scenes
{
    internal class GameScene : IScene
    {
        private Playfield _playfield;

        public GameScene()
        {
            // The playfield's origin is topleft:
            // The playfield is 2 units wide and 4 units high; so -1,2,0 puts the playfield in the center of our view. 
            _playfield = new Playfield(new Vector3(-1f, 2f, 0));

            TetriminoFactory factory = new();

            Tetrimino t1 = factory.Generate(Enums.Tetriminoes.O);
            Tetrimino t2 = factory.Generate(Enums.Tetriminoes.L);
            Tetrimino t3 = factory.Generate(Enums.Tetriminoes.J);
            Tetrimino t4 = factory.Generate(Enums.Tetriminoes.O);

            _playfield.LockInPlace(t1, 0, 18);
            _playfield.LockInPlace(t2, 2, 18);
            _playfield.LockInPlace(t3, 5, 18);
            _playfield.LockInPlace(t4, 8, 18);

            int lines = _playfield.ValidateField();
            _playfield.ClearLines();
        }
      
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _playfield.Draw();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
