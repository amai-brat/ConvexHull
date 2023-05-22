﻿using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using GrahamAlgorithm;

namespace UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public void HandleButtonClick(Canvas canvas)
    {
        while (canvas.Children.Count > 3) canvas.Children.RemoveAt(canvas.Children.Count - 1);
        var points = Graham.GetRandomPoints(20, 450, 450);
        
        foreach (var point in points)
        {
            var ellipse = new Ellipse()
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Black,
                ContextFlyout = new Flyout(){Content = $"{point.X} {point.Y}"}
            };
            Canvas.SetLeft(ellipse, point.X + 450);
            Canvas.SetTop(ellipse, -point.Y + 450);
            canvas.Children.Add(ellipse);
        }

        var convexHull = new GrahamAlgorithm.Stack<CartesianPoint>();
        Graham.GrahamScan_Stack(points, convexHull);
        DrawLines(canvas, convexHull.ToList());
    }

    public void DrawLines(Canvas canvas, List<CartesianPoint> convexHull)
    {
        var n = convexHull.Count;
        for (int i = 1; i < n; i++)
        {
            var line = new Line()
            {
                StrokeThickness = 2,
                Stroke = Brushes.Blue,
                StartPoint = new Point(convexHull[i - 1].X, -convexHull[i-1].Y),
                EndPoint = new Point(convexHull[i].X, -convexHull[i].Y),
            };
            Canvas.SetTop(line, 450);
            Canvas.SetLeft(line, 450);
            canvas.Children.Add(line);
        }
    }
}