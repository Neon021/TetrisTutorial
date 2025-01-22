namespace TetrisTutorial.Assets.Tetrimino
{
    internal class Shape
    {
        public bool[][] ShapeBits;

        public Shape(string[] shapeDefinition)
        {
            //Think of it like how we export the tilemap data as CSV and match the indices from the asset atlas
            ShapeBits = new bool[shapeDefinition.Length][];
            int i = 0;
            foreach (string s in shapeDefinition)
            {
                ShapeBits[i] = new bool[shapeDefinition.Length];
                int j = 0;
                foreach (char c in s)
                {
                    ShapeBits[i][j] = c == '1';
                    j++;
                }
                i++;
            }
        }
    }
}
