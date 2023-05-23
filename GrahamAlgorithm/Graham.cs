using System.Diagnostics;
using UsedObjects;

namespace GrahamAlgorithm;

public class Graham : IConvexHullAlgorithm
{
    public static CartesianPoint[] GetRandomPoints(int n, int rangeX, int rangeY)
    {
        var points = new CartesianPoint[n];
        var rnd = new Random();
        for (var i = 0; i < 20; i++)
        {
            points[i] = new CartesianPoint(rnd.Next(-rangeX, rangeX), rnd.Next(-rangeY, rangeY));
        }

        return points;
    }

    public static long GrahamScan_Stack(CartesianPoint[] points, Stack<CartesianPoint> resultStack)
    {
        var basePoint = points.MinBy(point => (point.Y, point.X));
        Sorter.ShakerSort(points, new CartesianPointsComparerByPolarCoordinates(basePoint));

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        
        resultStack.Push(basePoint);
        resultStack.Push(points[0]);
        for (var i = 1; i < points.Length; i++)
        {
            while (resultStack.Count > 1 && Orientation(resultStack.PeekNext(), resultStack.Peek(), points[i]) <= 0)
            {
                resultStack.Pop();
            }
            resultStack.Push(points[i]);
        }
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks;
    }
    
    public static long GrahamScan_List(CartesianPoint[] points, List<CartesianPoint> resultConvexHull)
    {
        var basePoint = points.MinBy(point => point.Y);
        Sorter.ShakerSort(points, new CartesianPointsComparerByPolarCoordinates(basePoint));

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        
        resultConvexHull.AddRange(new[]{basePoint, points[0]});
        for (var i = 1; i < points.Length; i++)
        {
            while (resultConvexHull.Count > 1 && Orientation(resultConvexHull[^2], resultConvexHull[^1], points[i]) <= 0)
                resultConvexHull.RemoveAt(resultConvexHull.Count - 1);
            resultConvexHull.Add(points[i]);
        }
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks;
    }

    public static long GrahamScam_LinkedList(CartesianPoint[] points, DoublyLinkedList<CartesianPoint> resultConvexHull)
    {
        var basePoint = points.MinBy(point => point.Y);
        Sorter.ShakerSort(points, new CartesianPointsComparerByPolarCoordinates(basePoint));

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        
        resultConvexHull.AddLast(basePoint);
        resultConvexHull.AddLast(points[0]);
        for (var i = 1; i < points.Length; i++)
        {
            while (resultConvexHull.Count > 1 && 
                   Orientation(resultConvexHull.LastNode!.Data, resultConvexHull.LastNode.Previous!.Data, points[i]) <= 0)
                resultConvexHull.RemoveLast();
            resultConvexHull.AddLast(points[i]);
        }
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks;
    }
    
    private static void PrintArray(IEnumerable<CartesianPoint> polarPoints)
    {
        foreach (var t in polarPoints)
        {
            Console.WriteLine($"PP: {t.X} {t.Y}");
        }

        Console.WriteLine();
    }
    
    private static double Orientation(CartesianPoint p1, CartesianPoint p2, CartesianPoint p3)
    {
        return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
    }

    public IEnumerable<CartesianPoint> GetConvexHull(CartesianPoint[] points)
    {
        var result = new List<CartesianPoint>();
        GrahamScan_List(points, result);
        return result;
    }
}