using System.Drawing;

namespace GENOM.Main.Genom
{
    internal class Genom
    {
        public static readonly int ShapeColorsMaxIndex = 10;

        public static readonly int ShapeAnglesMaxIndex = 10;
        public static readonly int ShapeAnglesMinIndex = 3;

        public static readonly int ShapeBrightnessMinIndex = 0;
        public static readonly int ShapeBrightnessMaxIndex = 16;

        public static readonly int ShapeMaxX = 60;
        public static readonly int ShapeMaxY = 60;

        public static List<int> CreateGenom()
        {
            List<int> genom = new();
            Random random = new();

            genom.Add(random.Next(ShapeAnglesMinIndex, ShapeAnglesMaxIndex)); //Angle count

            genom.Add(random.Next(1, ShapeColorsMaxIndex)); //Shape color 1
            genom.Add(random.Next(1, ShapeColorsMaxIndex)); //Shape color 2

            genom.Add(random.Next(ShapeBrightnessMinIndex, ShapeBrightnessMaxIndex)); //Shape brightness

            for(int i = 0; i < genom[0]; i++)
            {
                genom.Add(random.Next(0, ShapeMaxX));
                genom.Add(random.Next(0, ShapeMaxY));
            }

            return genom;
        }

        public static List<int> GetDaughterGenom(List<int> parent1, List<int> parent2)
        {
            List<int> genom = new();
            Random random = new();
            int GenomLenght = parent1.Count > parent2.Count ? parent1.Count : parent2.Count;

            for (int i = 0; i < GenomLenght; i++)
            {
                if (i >= parent1.Count)
                    genom.Add(parent2[i]);
                else if (i >= parent2.Count)
                    genom.Add(parent1[i]);
                else
                    switch (random.Next(2))
                    {
                        case 0:
                            genom.Add(parent1[i]);
                            break;
                        case 1:
                            genom.Add(parent2[i]);
                            break;
                    }
            }

            return genom;
        }
    }
}
