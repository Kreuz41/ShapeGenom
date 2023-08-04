namespace GENOM.Main.Graphic
{
    internal class Vector
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Vector(Point point1, Point point2)
        {
            StartPoint = point1;
            EndPoint = point2;
        }

        public bool IsPointOnVector(Point point)
        {
            double dx = point.X - StartPoint.X;
            double dy = point.Y - StartPoint.Y;
            double dx2 = EndPoint.X - StartPoint.X;
            double dy2 = EndPoint.Y - StartPoint.Y;

            double crossProduct = dx * dy2 - dy * dx2;
            double dotProduct = dx * dx2 + dy * dy2;

            return Math.Abs(crossProduct) < 1e-9 && dotProduct >= 0;
        }
    }
}
