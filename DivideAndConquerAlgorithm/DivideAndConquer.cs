using GrahamAlgorithm;
using UsedObjects;

namespace DivideAndConquerAlgorithm;

public class DivideAndConquer : IConvexHullAlgorithm
{
    public IEnumerable<CartesianPoint> GetConvexHull(CartesianPoint[] points)
    {
        return GetConvexHull(points, 0, points.Length);
    }

    public CartesianPoint[] GetConvexHull(CartesianPoint[] points, int left, int right)
    {
        if (right - left <= 5)
        {
            var pointFromLeftToRight = new CartesianPoint[right-left];
            Array.Copy(points, left,pointFromLeftToRight, 0, right-left);
            var result = new Graham().GetConvexHull(pointFromLeftToRight).ToArray();

            return result;
        }

        var firstHull = GetConvexHull(points, left, left + (right - left) / 2);
        var secondHull = GetConvexHull(points, left + (right - left) / 2, right);
        return MergeConvexHulls(firstHull, secondHull);
         
    }
    
    private static double Orientation(CartesianPoint p1, CartesianPoint p2, CartesianPoint p3)
    {
        return (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
    }
    
    //TODO: Неправильно реализован MergeConvexHulls, он должен быть за O(n), а тут за O(n*log(n))
    public static CartesianPoint[] MergeConvexHulls(CartesianPoint[] firstHull, CartesianPoint[] secondHull)
    {
        var pivot = firstHull[0];
        var pivotInsideSecondHull = false;
        for (var i = 1; i < secondHull.Length; i++)
        {
            if (Orientation(secondHull[i - 1], secondHull[i], pivot) <= 0)
            {
                pivotInsideSecondHull = true;
            }
        }
        var result = new CartesianPoint[firstHull.Length+secondHull.Length];
        var firstHullCurrentIndex = 0;
        var secondHullCurrentIndex = 0;
        for (var i = 0; i < result.Length; i++)
        {
            if (firstHullCurrentIndex >= firstHull.Length && secondHullCurrentIndex >= secondHull.Length)
            {
                break;
            }
            else if (firstHullCurrentIndex >= firstHull.Length)
            {
                result[i] = secondHull[secondHullCurrentIndex++];
            }
            else if (secondHullCurrentIndex >= secondHull.Length)
            {
                result[i] = firstHull[firstHullCurrentIndex++];
            }
            else
            {
                if (new CartesianPointsComparerByPolarCoordinates(pivot)
                        .Compare(firstHull[firstHullCurrentIndex],
                            secondHull[secondHullCurrentIndex]) < 0)
                {
                    result[i] = firstHull[firstHullCurrentIndex++];
                }
                else
                {
                    result[i] = secondHull[secondHullCurrentIndex++];
                }
            }
        }

        if (pivotInsideSecondHull)
        {
            Array.Sort(result, new CartesianPointsComparerByPolarCoordinates(pivot));
            var resultConvexHull = new List<CartesianPoint>();
            resultConvexHull.AddRange(new[] {result[0],result[1]});
            for (var i = 2; i < result.Length; i++)
            {
                while (resultConvexHull.Count > 1 &&
                       Orientation(resultConvexHull[^2], resultConvexHull[^1], result[i]) <= 0)
                    resultConvexHull.RemoveAt(resultConvexHull.Count - 1);
                resultConvexHull.Add(result[i]);
            }
        
            return resultConvexHull.ToArray();
        }
        
        return new Graham().GetConvexHull(result).ToArray();
    }
}