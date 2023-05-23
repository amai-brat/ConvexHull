namespace UsedObjects;
public interface IPoint
{
    void Add(IPoint other);
    bool EqualTo(IPoint other,double tolerance=0.0000001);
    Tuple<double, double> ToCartesian();
    Tuple<double, double> ToPolar();
}