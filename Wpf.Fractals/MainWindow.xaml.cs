using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wpf.Fractals;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void DrawFractal_Click(object sender, RoutedEventArgs e)
    {
        FractalsCanvas.Children.Clear();

        var center = new Point(FractalsCanvas.ActualWidth / 2, FractalsCanvas.ActualHeight / 2);
        var sideLength = Math.Min(FractalsCanvas.ActualWidth, FractalsCanvas.ActualHeight) * 0.6;
        var shrinkSideBy = 0.6;
        var maxDepth = 6;
        var colors = new[] { Brushes.Gold, Brushes.Cyan, Brushes.Magenta, Brushes.Bisque, Brushes.Chartreuse };

        DrawRecursiveTriangle(center, sideLength, 0, 1, colors, shrinkSideBy, 0, maxDepth);
    }

    private void DrawRecursiveTriangle(Point center, double sideLength, double degreesRotate, int thickness, SolidColorBrush[] colors,
        double shrinkSideBy, int iteration, int maxDepth)
    {
        if (iteration > maxDepth)
        {
            return;
        }

        var triangleHeight = sideLength * Math.Sqrt(3) / 2;

        var top = center with { Y = center.Y - 2.0 / 3.0 * triangleHeight };
        var bottomLeft = new Point(center.X - sideLength / 2, center.Y + 1.0 / 3.0 * triangleHeight);
        var bottomRight = new Point(center.X + sideLength / 2, center.Y + 1.0 / 3.0 * triangleHeight);

        if (degreesRotate != 0)
        {
            top = Rotate(top, center, degreesRotate);
            bottomLeft = Rotate(bottomLeft, center, degreesRotate);
            bottomRight = Rotate(bottomRight, center, degreesRotate);
        }

        PlotLine(top, bottomLeft, thickness, colors[iteration % colors.Length]);
        PlotLine(top, bottomRight, thickness, colors[iteration % colors.Length]);
        PlotLine(bottomLeft, bottomRight, thickness, colors[iteration % colors.Length]);

        if (iteration >= maxDepth)
        {
            return;
        }

        var recursiveLines = new[] { (top, bottomLeft), (top, bottomRight), (bottomLeft, bottomRight) };

        var lineNumber = 0;
        foreach (var (startPoint, endPoint) in recursiveLines)
        {
            lineNumber++;

            if (!(iteration < 1 || lineNumber < 3))
            {
                continue;
            }

            var newSideLength = sideLength * shrinkSideBy;
            var newRotation = degreesRotate;

            switch (lineNumber)
            {
                case 1:
                    newRotation += -60;
                    break;
                case 2:
                    newRotation += 60;
                    break;
                case 3:
                    newRotation += 180;
                    break;
            }

            var newCenter = GetNewOuterCenter(center, startPoint, endPoint, newSideLength);

            DrawRecursiveTriangle(newCenter, newSideLength, newRotation, thickness, colors, shrinkSideBy, iteration + 1, maxDepth);
        }
    }

    private static Point GetNewOuterCenter(Point parentCenter, Point lineStart, Point lineEnd, double newSideLength)
    {
        var centerOfLine = new Point((lineStart.X + lineEnd.X) / 2, (lineStart.Y + lineEnd.Y) / 2);
        var newTriangleHeight = newSideLength * Math.Sqrt(3) / 2;

        var lineVector = lineEnd - lineStart;
        var normalVector = new Vector(-lineVector.Y, lineVector.X);

        var vectorToParentCenter = parentCenter - centerOfLine;
        if (Vector.Multiply(normalVector, vectorToParentCenter) > 0)
        {
            normalVector *= -1;
        }

        normalVector.Normalize();

        var offsetDistance = newTriangleHeight / 3.0;
        return centerOfLine + normalVector * offsetDistance;
    }

    private static Point Rotate(Point pointToRotate, Point centerPoint, double degrees)
    {
        var radians = degrees * (Math.PI / 180);
        var cos = Math.Cos(radians);
        var sin = Math.Sin(radians);
        var vector = pointToRotate - centerPoint;
        var newX = vector.X * cos - vector.Y * sin;
        var newY = vector.X * sin + vector.Y * cos;
        return new Point(newX + centerPoint.X, newY + centerPoint.Y);
    }

    private void PlotLine(Point from, Point to, int thickness, SolidColorBrush color)
    {
        var line = new Line
        {
            X1 = from.X, Y1 = from.Y,
            X2 = to.X, Y2 = to.Y,
            StrokeThickness = thickness,
            Stroke = color,
        };
        FractalsCanvas.Children.Add(line);
    }
}
