using GENOM.Main.Graphic;

namespace GENOM.Main.Genom
{
    internal class Shape
    {
        public List<int> genom;

        public int AnglesCount { get; set; }

        public int Color1 { get; set; }
        public int Color2 { get; set; }
        public int GradientSize { get; set; }

        public int Brightness { get; set; }

        public Shape()
        {
            genom = Genom.CreateGenom();
            SetMainParam();
        }

        public Shape(List<int> genom1, List<int> genom2)
        {
            genom = Genom.GetDaughterGenom(genom1, genom2);
            SetMainParam();
        }

        public void SetMainParam()
        {
            AnglesCount = genom[0];
            Color1 = genom[1];
            Color2 = genom[2];
            Brightness = genom[3];
            GradientSize = Color1 - Color2;
        }
    }
}
