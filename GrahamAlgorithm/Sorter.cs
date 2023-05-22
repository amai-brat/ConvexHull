namespace GrahamAlgorithm;

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

public class CartesianPointsComparerByPolar : IComparer<CartesianPoint>
{
    public PolarPoint BasePoint { get; set; }

    public CartesianPointsComparerByPolar(CartesianPoint basePoint)
    {
        var tmp = basePoint.ToPolar();
        BasePoint = new PolarPoint(tmp.Item1, tmp.Item2);
    }
    public int Compare(CartesianPoint x, CartesianPoint y)
    {
        var tmp = x.ToPolar();
        var xPolar = new PolarPoint(tmp.Item1,
            (tmp.Item2 > BasePoint.RadianAngle)
                ? tmp.Item2 - BasePoint.RadianAngle
                : 2 * Math.PI - BasePoint.RadianAngle + tmp.Item2);
        tmp = y.ToPolar();
        var yPolar = new PolarPoint(tmp.Item1, (tmp.Item2 > BasePoint.RadianAngle)
            ? tmp.Item2 - BasePoint.RadianAngle
            : 2 * Math.PI - BasePoint.RadianAngle + tmp.Item2);
        
        var angleComparison = xPolar.RadianAngle.CompareTo(yPolar.RadianAngle);
        return angleComparison != 0 ? angleComparison : xPolar.Radius.CompareTo(yPolar.Radius);
    }
}
