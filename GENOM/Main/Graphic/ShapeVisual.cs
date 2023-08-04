using GENOM.Main.Genom;
using System.Drawing;

namespace GENOM.Main.Graphic
{
    internal class ShapeVisual
    {
        private static string[] Symbols = {
            ".", ":", "!", "/", "r", "(", "l", "1", "Z", "4", "H", "9", "W", "8", "$", "@"
        };

        private static readonly int _maxXFieldSize = 64;
        private static readonly int _maxYFieldSize = 64;

        public string[,] _shape = new string[_maxXFieldSize, _maxYFieldSize];

        private List<Point> _tops = new();
        private List<Vector> _edges = new();

        private Shape shape;

        private int mainParamCount = 4;

        public ShapeVisual(Shape shape)
        {
            this.shape = shape;
            SetStartField();
            SetShapeTops();
        }

        private void SetShapeTops()
        {
            bool isY = true;
            for (int i = 0; i < shape.AnglesCount * 2; i++)
            {
                if (isY)
                {
                    _shape[shape.genom[i - 1 + mainParamCount], shape.genom[i + mainParamCount]] = Symbols[shape.Brightness];
                    _tops.Add(new Point(shape.genom[i - 1 + mainParamCount], shape.genom[i + mainParamCount]));
                    isY = false;
                }
                else
                {
                    isY = true;
                }
            }

            SetEdges();
        }

        private void SetEdges()
        {
            for(int i = 0; i < _tops.Count; i++)
            {
                if(i == _tops.Count - 1)
                {
                    _edges.Add(new Vector(_tops.Last(), _tops.First()));
                }
                else
                {
                    _edges.Add(new Vector(_tops[i], _tops[i + 1]));
                }
            }

            UpdatePoints();
            UpdateEdges();
        }

        public enum CustomColor
        {
            White,
            Red,
            Green,
            Blue,
            Yellow,
            Cyan,
            Magenta,
            Gray,
            DarkYellow,
            DarkCyan
        }

        public ConsoleColor GetColor(int index)
        {
            switch (index)
            {
                case 1:
                    return ConsoleColor.Yellow;
                case 2:
                    return ConsoleColor.Magenta;
                case 3:
                    return ConsoleColor.Red;
                case 4:
                    return ConsoleColor.Cyan;
                case 5:
                    return ConsoleColor.Gray;
                case 6:
                    return ConsoleColor.DarkGray;
                case 7:
                    return ConsoleColor.Blue;
                case 8:
                    return ConsoleColor.DarkRed;
                case 9:
                    return ConsoleColor.DarkGreen;
                case 10:
                    return ConsoleColor.DarkBlue;

                default:
                    return ConsoleColor.White;
            }
        }

        public void PrintShape(Point point)
        {
            for (int y = 0; y < _maxXFieldSize; y++, Console.WriteLine())
            {
                for (int x = 0; x < _maxYFieldSize; x++)
                {
                    double step;

                    if (shape.GradientSize != 0)
                        step =  Math.Abs(64.00 / shape.GradientSize);
                    else
                        step = 1;

                    Console.ForegroundColor = GetColor(Math.Max(shape.Color1, shape.Color2) + (x + y) / 2 / (int)step);

                    Console.SetCursorPosition(point.X + x, point.Y + y);
                    Console.Write(_shape[x, y]);
                }
            }
        }

        public void PrintGenome(Point point)
        {
            int y = 0;
            int x = 0;
            Console.SetCursorPosition(point.X, point.Y);
            for (int i = 0; i < shape.genom.Count; i++)
            {
                if (i % 5 == 0 && i != 0)
                {
                    y++;
                    x = 0;
                    Console.SetCursorPosition(point.X, point.Y + y);
                }
                Console.Write($"{shape.genom[i]} ");
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private void SetStartField()
        {
            for (int y = 0; y < _maxXFieldSize; y++)
            {
                for (int x = 0; x < _maxYFieldSize; x++)
                {
                    _shape[x, y] = " ";
                }
            }
        }

        private void UpdateEdges()
        {
            for (int i = 0; i < _edges.Count; i++)
            {
                int dx = Math.Abs(_edges[i].EndPoint.X - _edges[i].StartPoint.X);
                int dy = Math.Abs(_edges[i].EndPoint.Y - _edges[i].StartPoint.Y);
                int steps = Math.Max(dx, dy);

                for (int j = 0; j <= steps; j++)
                {
                    float t = j / (float)steps;
                    int x = (int)Math.Round(_edges[i].StartPoint.X + t * (_edges[i].EndPoint.X - _edges[i].StartPoint.X));
                    int y = (int)Math.Round(_edges[i].StartPoint.Y + t * (_edges[i].EndPoint.Y - _edges[i].StartPoint.Y));
                    _shape[x, y] = Symbols[shape.Brightness];
                }
            }
        }

        private void UpdatePoints()
        {
            for (int y = 0; y < _maxXFieldSize; y++)
            {
                for (int x = 0; x < _maxYFieldSize; x++)
                {
                    Point point = new(x, y);

                    if (IsPointInsideFigure(point))
                    {
                        _shape[x, y] = Symbols[shape.Brightness];
                    }
                }
            }
        }

        public bool IsPointInsideFigure(Point point)
        {
            int count = 0;
            int n = _edges.Count;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                if ((_edges[i].StartPoint.Y > point.Y) != (_edges[j].StartPoint.Y > point.Y) &&
                    point.X < (_edges[j].StartPoint.X - _edges[i].StartPoint.X) * (point.Y - _edges[i].StartPoint.Y) / (_edges[j].StartPoint.Y - _edges[i].StartPoint.Y) + _edges[i].StartPoint.X)
                {
                    count++;
                }
            }

            return count % 2 == 1;
        }

        public static void ChangeConsoleColor(CustomColor color)
        {
            switch (color)
            {
                case CustomColor.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case CustomColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case CustomColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case CustomColor.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case CustomColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case CustomColor.Cyan:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case CustomColor.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case CustomColor.Gray:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case CustomColor.DarkYellow:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case CustomColor.DarkCyan:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White; // Значение по умолчанию
                    break;
            }
        }

        public static double LinearInterpolate(double value, double minInput, double maxInput, double minOutput, double maxOutput)
        {
            return minOutput + (value - minInput) * (maxOutput - minOutput) / (maxInput - minInput);
        }
    }
}
