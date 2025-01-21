using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TetrisTutorial.Enums;

namespace TetrisTutorial.Assets.Tetrimino
{
    internal class Tetrimino
    {
        private int _shapeId;

        public Shape[] Shapes;
        public Color Color;
        public Tetriminoes ShapeType;

        public Shape CurrentShape
        {
            get
            {
                return Shapes[_shapeId];
            }
        }

        public Tetrimino()
        {
            _shapeId = 0;
        }

        public void SetShape(Shape[] shapes, Tetriminoes shapeType, Color color)
        {
            Shapes = shapes;
            ShapeType = shapeType;
            Color = color;
        }

        public void RotateLeft()
        {
            _shapeId++;
            if (_shapeId > Shapes.Length - 1)
            {
                _shapeId = 0;
            }
        }

        public void RotateRight()
        {
            _shapeId--;
            if (_shapeId < 0)
            {
                _shapeId = Shapes.Length - 1;
            }
        }

        public void Draw(Matrix world)
        {
            for (int y = 0; y < CurrentShape.ShapeBits.Length; y++)
            {
                for(int x = 0; x < CurrentShape.ShapeBits[y].Length; x++)
                {
                    if (CurrentShape.ShapeBits[y][x] == false)
                        continue;

                    foreach (ModelMesh modelMesh in Models.CubeObject.Meshes)
                    {
                        foreach (ModelMeshPart meshPart in modelMesh.MeshParts)
                        {
                            meshPart.Effect = GameRoot.BasicShader;

                            GameRoot.BasicShader.World = Matrix.CreateTranslation(0.2f * x, 0.2f * -y, 0) * world;
                            GameRoot.BasicShader.DiffuseColor = Color.ToVector3();
                        }
                        modelMesh.Draw();
                    }
                }
            }
        }
    }
}
