using UsedObjects;
using System;
using System.Collections.Generic;

namespace QuickHullAlgorithm;
public class QuickHull : IConvexHullAlgorithm
{
    private static double Distance(CartesianPoint p, CartesianPoint a, CartesianPoint b)
    {
        var numerator = Math.Abs((b.Y - a.Y) * p.X - (b.X - a.X) * p.Y + b.X * a.Y - b.Y * a.X);
        var denominator = Math.Sqrt(Math.Pow(b.Y - a.Y, 2) + Math.Pow(b.X - a.X, 2));
        return numerator / denominator;
    }
    
    private static CartesianPoint FindFarthestPoint(List<CartesianPoint> points, CartesianPoint a, CartesianPoint b)
    {
        double maxDistance = 0;
        var farthestPoint = new CartesianPoint();

        foreach (var point in points)
        {
            var distance = Distance(point, a, b);

            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestPoint = point;
            }
        }

        return farthestPoint;
    }
    
    private static List<CartesianPoint> FindConvexHull(List<CartesianPoint> points)
    {
        var leftmostPoint = points.MinBy(point => (point.X, point.Y));
        var rightmostPoint = points.MaxBy(point => (point.X, point.Y));
        var convexHull = new List<CartesianPoint>
        {
            leftmostPoint,
            rightmostPoint
        };
        
        var abovePoints = new List<CartesianPoint>();
        var belowPoints = new List<CartesianPoint>();

        foreach (var point in points)
        {
            if (point.Equals(leftmostPoint) || point.Equals(rightmostPoint)) continue;
            var orientation = Orientation(leftmostPoint, rightmostPoint, point);

            switch (orientation)
            {
                case > 0:
                    abovePoints.Add(point);
                    break;
                case < 0:
                    belowPoints.Add(point);
                    break;
            }
        }
        
        FindConvexHullRecursive(abovePoints, leftmostPoint, rightmostPoint, convexHull);
        FindConvexHullRecursive(belowPoints, rightmostPoint, leftmostPoint, convexHull);

        return convexHull;
    }

    private static void FindConvexHullRecursive(List<CartesianPoint> points, CartesianPoint a, CartesianPoint b, List<CartesianPoint> convexHull)
    {
        if (points.Count == 0)
        {
            return;
        }
        
        var farthestPoint = FindFarthestPoint(points, a, b);

        // Добавляем найденную точку в список точек, находящихся на выпуклой оболочке
        convexHull.Insert(convexHull.IndexOf(b), farthestPoint);

        // Находим точки, находящиеся выше и ниже прямой, проходящей через aи farthestPoint, и a и b и рекурсивно строим выпуклую оболочку для этих двух множеств точек
        var abovePoints = new List<CartesianPoint>();
        var belowPoints = new List<CartesianPoint>();

        foreach (var point in points)
        {
            if (!point.Equals(farthestPoint))
            {
                var orientation1 = Orientation(a, farthestPoint, point);
                var orientation2 = Orientation(farthestPoint, b, point);

                if (orientation1 > 0)
                {
                    abovePoints.Add(point);
                }
                else if (orientation2 > 0)
                {
                    belowPoints.Add(point);
                }
            }
        }

        FindConvexHullRecursive(abovePoints, a, farthestPoint, convexHull);
        FindConvexHullRecursive(belowPoints, farthestPoint, b, convexHull);
    }
    private static double Orientation(CartesianPoint p1, CartesianPoint p2, CartesianPoint p3)
    {
        return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
    }

    public IEnumerable<CartesianPoint> GetConvexHull(CartesianPoint[] points)
    {
        return FindConvexHull(points.ToList());
    }
}