using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisTutorial.Utils.Scene
{
    public interface IScene
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
