namespace UsedObjects;

public struct PolarPoint : IPoint
{
    public double Radius { get; set; }
    public double RadianAngle { get; set; }

    public PolarPoint(double radius, double radianAngle)
    {
        Radius = radius;
        RadianAngle = radianAngle;
    }
    
    public void Add(IPoint other)
    {
        var tmp = other.ToPolar();
        Radius = Math.Sqrt(Radius * Radius + 
                           tmp.Item1 * tmp.Item1 + 
                           2 * Radius * tmp.Item1 * Math.Cos(tmp.Item2 - RadianAngle)
                           );
        RadianAngle = Math.Asin(tmp.Item1 * Math.Sin(tmp.Item2 - RadianAngle) / Radius);
    }

    public bool EqualTo(IPoint other, double tolerance = 1E-07)
    {
        var tmp = other.ToPolar();
        return Math.Abs(Radius - tmp.Item1) < tolerance
               && Math.Abs(RadianAngle - tmp.Item2) < tolerance;
    }

    public Tuple<double, double> ToCartesian() => 
        new(Radius * Math.Cos(RadianAngle), Radius * Math.Sin(RadianAngle));

    public Tuple<double, double> ToPolar() =>
        new(Radius, RadianAngle);

    public override string ToString()
    {
        return $"Radius: {Radius}, Angle: {RadianAngle}";
    }
}