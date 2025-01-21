using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TetrisTutorial.Scenes
{
    internal class TitleScreen : IScene
    {
        private float _angle;

        public TitleScreen()
        {

        }

        public void Update(GameTime gameTime)
        {
            _angle += 0.75f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _angle %= MathHelper.TwoPi;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            // Draw the cube mesh.
            foreach (ModelMesh m in Assets.Models.CubeObject.Meshes)
            {
                // This is generic- eventhough the cube only has one meshpart, 
                // Let's keep the code so you can experiment with different models.
                foreach (ModelMeshPart part in m.MeshParts)
                {
                    part.Effect = GameRoot.BasicShader;

                    GameRoot.BasicShader.World = Matrix.CreateScale(5) * Matrix.CreateRotationY(_angle) * Matrix.CreateTranslation(0, 0, -3f);

                    GameRoot.BasicShader.DiffuseColor = Color.Red.ToVector3();
                }
                m.Draw();
            }
        }
    }
}
