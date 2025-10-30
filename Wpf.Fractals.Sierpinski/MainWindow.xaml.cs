using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf.Fractals.Sierpinski;

public partial class MainWindow
{
    private readonly Color _fractalColor = Colors.DeepSkyBlue;
    private readonly Color _backgroundColor = Color.FromRgb(28, 28, 28);

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void DrawFractal_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await DrawFractal();
        }
        catch (Exception error)
        {
            MessageBox.Show($"{error}", "Error");
            Console.WriteLine(error.ToString());
        }
    }

    private async Task DrawFractal()
    {
        if (!int.TryParse(ParameterTextBox.Text, out var parameter) || parameter < 0)
        {
            MessageBox.Show("wrong input", "Error");
            return;
        }

        SetUiBusyState(true);
        var stopwatch = Stopwatch.StartNew();

        WriteableBitmap? bitmap;
        if (RecursiveMethodRadio.IsChecked == true)
        {
            if (parameter > 14)
            {
                MessageBox.Show("Глубина выше 14 может занять очень много времени.",
                    "Warning");
                SetUiBusyState(false);
                return;
            }

            bitmap = await Task.Run(() => GenerateRecursiveFractal(parameter));
        }
        else
        {
            bitmap = await Task.Run(() => GenerateChaosGameFractal(parameter));
        }

        FractalImage.Source = bitmap;

        stopwatch.Stop();
        SetUiBusyState(false);
        StatusTextBlock.Text = $"Took: {stopwatch.ElapsedMilliseconds} ms";
    }

    private WriteableBitmap? CreateBitmap()
    {
        int width = 0, height = 0;
        Dispatcher.Invoke(() =>
        {
            width = (int)Math.Max(0, ActualWidth - 220);
            height = (int)Math.Max(0, ActualHeight);
        });
        if (width <= 0 || height <= 0)
        {
            return null;
        }

        return new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
    }

    private void SetUiBusyState(bool isBusy)
    {
        StatusTextBlock.Text = isBusy ? "Doing..." : "";
        DrawButton.IsEnabled = !isBusy;
        DrawButton.Content = isBusy ? "Generating..." : "Draw";
    }

    private WriteableBitmap? GenerateRecursiveFractal(int depth)
    {
        var bitmap = CreateBitmap();
        if (bitmap == null)
        {
            return null;
        }

        FillBitmap(bitmap, _backgroundColor);

        var size = Math.Min(bitmap.PixelWidth, bitmap.PixelHeight) * 0.9;
        var top = new Point(bitmap.PixelWidth / 2.0, (bitmap.PixelHeight - size) / 2.0);
        var bottomLeft = new Point((bitmap.PixelWidth - size) / 2.0, (bitmap.PixelHeight + size) / 2.0);
        var bottomRight = new Point((bitmap.PixelWidth + size) / 2.0, (bitmap.PixelHeight + size) / 2.0);

        try
        {
            bitmap.Lock();
            DrawSierpinskiToBitmap(bitmap, top, bottomLeft, bottomRight, depth);
        }
        finally
        {
            bitmap.Unlock();
        }

        bitmap.Freeze();
        return bitmap;
    }

    private void DrawSierpinskiToBitmap(WriteableBitmap bmp, Point p1, Point p2, Point p3, int depth)
    {
        if (depth == 0)
        {
            FillTriangle(bmp, p1, p2, p3, _fractalColor);
            return;
        }

        var mid12 = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        var mid23 = new Point((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2);
        var mid31 = new Point((p3.X + p1.X) / 2, (p3.Y + p1.Y) / 2);

        DrawSierpinskiToBitmap(bmp, p1, mid12, mid31, depth - 1);
        DrawSierpinskiToBitmap(bmp, mid12, p2, mid23, depth - 1);
        DrawSierpinskiToBitmap(bmp, mid31, mid23, p3, depth - 1);
    }

    private WriteableBitmap? GenerateChaosGameFractal(int numPoints)
    {
        var bitmap = CreateBitmap();
        if (bitmap == null)
        {
            return null;
        }

        FillBitmap(bitmap, _backgroundColor);

        var size = Math.Min(bitmap.PixelWidth, bitmap.PixelHeight) * 0.9;
        var vertices = new[]
        {
            new Point(bitmap.PixelWidth / 2.0, (bitmap.PixelHeight - size) / 2.0),
            new Point((bitmap.PixelWidth - size) / 2.0, (bitmap.PixelHeight + size) / 2.0),
            new Point((bitmap.PixelWidth + size) / 2.0, (bitmap.PixelHeight + size) / 2.0),
        };

        var random = new Random();
        var currentPoint = new Point(random.Next(bitmap.PixelWidth), random.Next(bitmap.PixelHeight));

        try
        {
            bitmap.Lock();
            for (var i = 0; i < numPoints; i++)
            {
                var targetVertex = vertices[random.Next(3)];
                currentPoint.X = (currentPoint.X + targetVertex.X) / 2;
                currentPoint.Y = (currentPoint.Y + targetVertex.Y) / 2;
                DrawPixel(bitmap, (int)currentPoint.X, (int)currentPoint.Y, _fractalColor);
            }
        }
        finally
        {
            bitmap.Unlock();
        }

        bitmap.Freeze();
        return bitmap;
    }

    private static void FillBitmap(WriteableBitmap bmp, Color color)
    {
        var colorData = (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
        var pixels = new int[bmp.PixelWidth * bmp.PixelHeight];
        for (var i = 0; i < pixels.Length; i++)
        {
            pixels[i] = colorData;
        }

        bmp.WritePixels(new Int32Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight), pixels, bmp.PixelWidth * 4, 0);
    }

    private static void FillTriangle(WriteableBitmap bmp, Point p1, Point p2, Point p3, Color color)
    {
        var points = new[] { p1, p2, p3 };
        Array.Sort(points, (a, b) => a.Y.CompareTo(b.Y));

        var v1 = points[0];
        var v2 = points[1];
        var v3 = points[2];

        if ((int)v2.Y == (int)v1.Y)
        {
            FillBottomFlatTriangle(bmp, v1, v2, v3, color);
        }
        else if ((int)v2.Y == (int)v3.Y)
        {
            FillTopFlatTriangle(bmp, v1, v2, v3, color);
        }
        else
        {
            var v4 = new Point(v1.X + (v2.Y - v1.Y) / (v3.Y - v1.Y) * (v3.X - v1.X), v2.Y);
            FillTopFlatTriangle(bmp, v1, v2, v4, color);
            FillBottomFlatTriangle(bmp, v2, v4, v3, color);
        }
    }

    private static void FillTopFlatTriangle(WriteableBitmap bmp, Point v1, Point v2, Point v3, Color color)
    {
        var invslope1 = (float)((v2.X - v1.X) / (v2.Y - v1.Y));
        var invslope2 = (float)((v3.X - v1.X) / (v3.Y - v1.Y));
        var curx1 = (float)v1.X;
        var curx2 = (float)v1.X;

        for (var scanlineY = (int)v1.Y; scanlineY <= (int)v2.Y; scanlineY++)
        {
            DrawHorizontalLine(bmp, (int)curx1, (int)curx2, scanlineY, color);
            curx1 += invslope1;
            curx2 += invslope2;
        }
    }

    private static void FillBottomFlatTriangle(WriteableBitmap bmp, Point v1, Point v2, Point v3, Color color)
    {
        var invslope1 = (float)((v3.X - v1.X) / (v3.Y - v1.Y));
        var invslope2 = (float)((v3.X - v2.X) / (v3.Y - v2.Y));
        var curx1 = (float)v3.X;
        var curx2 = (float)v3.X;

        for (var scanlineY = (int)v3.Y; scanlineY > (int)v1.Y; scanlineY--)
        {
            DrawHorizontalLine(bmp, (int)curx1, (int)curx2, scanlineY, color);
            curx1 -= invslope1;
            curx2 -= invslope2;
        }
    }

    private static void DrawHorizontalLine(WriteableBitmap bmp, int x1, int x2, int y, Color color)
    {
        var startX = Math.Min(x1, x2);
        var endX = Math.Max(x1, x2);
        for (var x = startX; x <= endX; x++)
        {
            DrawPixel(bmp, x, y, color);
        }
    }

    private static void DrawPixel(WriteableBitmap bmp, int x, int y, Color color)
    {
        if (x < 0 || y < 0 || x >= bmp.PixelWidth || y >= bmp.PixelHeight)
        {
            return;
        }

        var colorData = (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
        var rect = new Int32Rect(x, y, 1, 1);

        bmp.WritePixels(rect, new[] { colorData }, 4, 0);
    }

    private void Algorithm_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (ParameterLabel == null || ParameterTextBox == null)
        {
            return;
        }

        if (RecursiveMethodRadio.IsChecked == true)
        {
            ParameterLabel.Text = "Depth:";
            ParameterTextBox.Text = "6";
        }
        else
        {
            ParameterLabel.Text = "Dots count:";
            ParameterTextBox.Text = "100000";
        }
    }
}
