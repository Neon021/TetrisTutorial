using Microsoft.Xna.Framework;
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

        public static void DrawCube(Matrix world, Color color)
        {
            foreach(ModelMesh mesh in CubeObject.Meshes)
            {
                foreach(ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = GameRoot.BasicShader;
                    GameRoot.BasicShader.World = world;
                    GameRoot.BasicShader.DiffuseColor = color.ToVector3();
                }
                mesh.Draw();
            }
        }
    }
}
