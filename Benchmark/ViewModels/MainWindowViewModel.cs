using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DivideAndConquerAlgorithm;
using GrahamAlgorithm;
using JarvisAlgorithm;
using QuickHullAlgorithm;

namespace Benchmark.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Measurements = new Collection<Measurement>();
        Measure(200);
        var measure = Measure(10000);
        for (var i = 0; i<measure.Count; i++)
        {
            Measurements.Add(new Measurement()
            {
                N = 10 * (i+1),
                Graham = measure[i].Item1,
                Jarvis = measure[i].Item2,
                DivideAndConquer = measure[i].Item3,
                QuickHull = measure[i].Item4
            });
        }
    }

    public Collection<Measurement> Measurements { get; private set; }

    private static List<Tuple<long, long, long, long>> Measure(int n)
    {
        var result = new List<Tuple<long, long, long, long>>();
        for (var i = 100; i < n; i += 10)
        {
            if (i%1000==0) Console.WriteLine(i);
            var points = Graham.GetRandomPoints(i, 10000, 10000);
            var timeGraham = Meter.MeasureConvexHullAlgorithm(new Graham(), points);
            var timeJarvis = Meter.MeasureConvexHullAlgorithm(new Jarvis(), points);
            var timeDivideAndConquer = Meter.MeasureConvexHullAlgorithm(new DivideAndConquer(), points);
            var timeQuickHull = Meter.MeasureConvexHullAlgorithm(new QuickHull(), points);
            result.Add(new Tuple<long, long, long, long>(timeGraham, timeJarvis, timeDivideAndConquer, timeQuickHull));
        }

        return result;
    }

}