using GrahamAlgorithm;
using UsedObjects;

public class Program
{
    public static void Main(string[] args)
    {
        var basePoint = new CartesianPoint(241,260);
        var point1 = new CartesianPoint(-436,-447);
        var point2 = new CartesianPoint(85,401);
        Console.WriteLine(new CartesianPointsComparerByPolarCoordinates(basePoint).Compare(point1, point2));
    }
}