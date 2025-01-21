using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisTutorial.Assets
{
    internal static class Models
    {
        public static Model CubeObject;

        public static void Initialize(ContentManager contentManager)
        {
            CubeObject = contentManager.Load<Model>("CubeObject");
        }
    }
}
