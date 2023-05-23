namespace UsedObjects;

public static class Sorter
{
    public static void ShakerSort<T>(T[] array, IComparer<T> comparer)
    {
        var begin = 0;
        var end = array.Length - 1;
        var rightDirection = true;
        while (begin <= end)
        {
            if (rightDirection)
            {
                for (int i = begin; i < end; i++)
                {
                    if (comparer.Compare(array[i], array[i+1]) > 0)
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                    }
                }
                end--;
            }
            else
            {
                for (int i = end; i > begin; i--)
                {
                    if (comparer.Compare(array[i], array[i-1]) < 0)
                    {
                        (array[i], array[i - 1]) = (array[i - 1], array[i]);
                    }
                }
                begin++;
            }
            rightDirection = !rightDirection;
        }
    }
}

public class CartesianPointsComparerByPolarCoordinates : IComparer<CartesianPoint>
{
    public PolarPoint BasePointInPolar { get; set; }
    public CartesianPoint BasePointInCartesian { get; set; }

    public CartesianPointsComparerByPolarCoordinates(CartesianPoint basePoint)
    {
        var tmp = basePoint.ToPolar();
        BasePointInPolar = new PolarPoint(tmp.Item1, tmp.Item2);
        BasePointInCartesian = basePoint;
    }
    public int Compare(CartesianPoint x, CartesianPoint y)
    {
        var dx = x.X - BasePointInCartesian.X;
        var dy = x.Y - BasePointInCartesian.Y;
        var radius = Math.Sqrt(dx*dx + dy*dy);
        var xPolar = new PolarPoint(radius, 
            Math.Atan2(dy, dx) < 0 ? 2 * Math.PI + Math.Atan2(dy, dx) : Math.Atan2(dy,dx));
        
        dx = y.X - BasePointInCartesian.X;
        dy = y.Y - BasePointInCartesian.Y;
        radius = Math.Sqrt(dx*dx + dy*dy);
        var yPolar = new PolarPoint(radius, 
            Math.Atan2(dy, dx) < 0 ? 2 * Math.PI + Math.Atan2(dy, dx) : Math.Atan2(dy,dx));
        
        var angleComparison = xPolar.RadianAngle.CompareTo(yPolar.RadianAngle);
        return angleComparison != 0 ? angleComparison : xPolar.Radius.CompareTo(yPolar.Radius);
    }
}
