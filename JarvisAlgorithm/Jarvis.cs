using UsedObjects;
namespace JarvisAlgorithm;

public class Jarvis : IConvexHullAlgorithm
{
    public IEnumerable<CartesianPoint> GetConvexHull(CartesianPoint[] points)
    {
        var convexHull = new List<CartesianPoint> { points.MinBy(point => (point.Y,point.X)) };
        var lowMost = 0;
        for (var i = 0; i < points.Length; i++)
        {
            if (new CartesianPointsComparerByPolarCoordinates(convexHull[^1]).Compare(points[i], points[lowMost]) < 0)
            {
                lowMost = i;
            }
        }
        var p = lowMost;
        do
        {
            convexHull.Add(points[p]);
            var q = (p + 1) % points.Length;
            for (var i = 0; i < points.Length; i++)
            {
                if (Orientation(points[p], points[i], points[q]) > 0)
                {
                    q = i;
                }
            }
            p = q;
            
        } while (p != Array.IndexOf(points, convexHull[0]));
        return convexHull;
    }
    private static double Orientation(CartesianPoint p1, CartesianPoint p2, CartesianPoint p3)
    {
        return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
    }
}

