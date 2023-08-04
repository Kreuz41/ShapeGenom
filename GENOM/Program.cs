using GENOM.Main.Genom;
using GENOM.Main.Graphic;

string[,] field = new string[200, 64]; 

while (true)
{
    Shape shape = new();
    ShapeVisual visual = new(shape);
    visual.PrintShape(new Point(0, 0));

    Shape shape1 = new();
    ShapeVisual visual1 = new(shape1);
    visual1.PrintShape(new Point(66, 0));

    Shape shapeChildren = new(shape.genom, shape1.genom);
    ShapeVisual visual2 = new(shapeChildren);
    visual2.PrintShape(new Point(132, 0));


    
    visual.PrintGenome(new Point(33, 66));
    visual1.PrintGenome(new Point(99, 66));
    visual2.PrintGenome(new Point(165, 66));

    Console.ReadLine();
}