using System.Diagnostics;
using System.Diagnostics.Metrics;
using UsedObjects;

namespace Benchmark;

public class Meter
{
    public static long MeasureConvexHullAlgorithm(IConvexHullAlgorithm algorithm, CartesianPoint[] points)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        algorithm.GetConvexHull(points);
        
        stopWatch.Stop();
        return stopWatch.ElapsedTicks;
    }
}