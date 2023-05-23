namespace UsedObjects;

public interface IConvexHullAlgorithm
{
    IEnumerable<CartesianPoint> GetConvexHull(CartesianPoint[] points);
}