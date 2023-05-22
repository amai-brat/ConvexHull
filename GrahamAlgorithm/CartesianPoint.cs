namespace GrahamAlgorithm;

public struct CartesianPoint : IPoint
{
    public double X { get; set; }
    public double Y { get; set; }

    public CartesianPoint(double x, double y)
    {
        X = x;
        Y = y;
    }
    public void Add(IPoint other)
    {
        var tmp = other.ToCartesian();
        X += tmp.Item1;
        Y += tmp.Item2;
    }

    public bool EqualTo(IPoint other, double tolerance = 1E-07)
    {
        var tmp = other.ToCartesian();
        return Math.Abs(X - tmp.Item1) < tolerance 
               && Math.Abs(Y - tmp.Item2) < tolerance;
    }

    public Tuple<double, double> ToCartesian() => new(X, Y);

    public Tuple<double, double> ToPolar()
    {
        var radius = Math.Sqrt(X * X + Y * Y);
        var phi = (Math.Atan2(Y, X) < 0) 
            ? Math.Atan2(Y, X) + 2 * Math.PI 
            : Math.Atan2(Y,X);
        return new Tuple<double, double>(radius, phi);
    }
}