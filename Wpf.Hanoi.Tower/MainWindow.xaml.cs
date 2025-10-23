using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wpf.Hanoi.Tower;

public partial class MainWindow
{
    private const int RodWidth = 10;
    private const int DiskHeight = 20;
    private const int BaseYPosition = 350;

    private readonly Stack<Rectangle>[] _rods = new Stack<Rectangle>[3];
    private int _moveCount;

    public MainWindow()
    {
        InitializeComponent();
        for (var i = 0; i < 3; i++)
        {
            _rods[i] = new Stack<Rectangle>();
        }

        SetupInitialState(3);
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(NumDisksTextBox.Text, out var numDisks) && numDisks is > 0 and < 10)
        {
            _ = SolveAndVisualize(numDisks);
        }
        else
        {
            MessageBox.Show("Enter valid number of disks (1-9)");
        }
    }

    private async Task SolveAndVisualize(int numDisks)
    {
        StartButton.IsEnabled = false;
        SetupInitialState(numDisks);
        _moveCount = 0;
        MovesTextBlock.Text = "Moves: 0";

        await SolveHanoi(numDisks, 0, 2, 1);

        StartButton.IsEnabled = true;
    }

    private void SetupInitialState(int numDisks)
    {
        HanoiCanvas.Children.Clear();
        foreach (var rod in _rods)
        {
            rod.Clear();
        }

        for (var i = 0; i < 3; i++)
        {
            var rodBase = new Rectangle { Width = RodWidth, Height = 200, Fill = Brushes.SaddleBrown };
            Canvas.SetLeft(rodBase, GetRodXPosition(i) - RodWidth / 2);
            Canvas.SetTop(rodBase, BaseYPosition - 200);
            HanoiCanvas.Children.Add(rodBase);
        }

        for (var i = numDisks; i > 0; i--)
        {
            var diskWidth = 40 + i * 20;
            var disk = new Rectangle
            {
                Width = diskWidth,
                Height = DiskHeight,
                Fill = GetDiskBrush(i),
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };

            PlaceDisk(disk, 0, _rods[0].Count);
            _rods[0].Push(disk);
            HanoiCanvas.Children.Add(disk);
        }
    }

    private async Task SolveHanoi(int n, int fromRod, int toRod, int auxRod)
    {
        if (n == 0)
        {
            return;
        }

        await SolveHanoi(n - 1, fromRod, auxRod, toRod);

        await AnimateDiskMove(fromRod, toRod);
        _moveCount++;
        MovesTextBlock.Text = $"Moves: {_moveCount}";

        await SolveHanoi(n - 1, auxRod, toRod, fromRod);
    }

    private async Task AnimateDiskMove(int fromRodIndex, int toRodIndex)
    {
        if (_rods[fromRodIndex].Count == 0)
        {
            return;
        }

        var disk = _rods[fromRodIndex].Pop();

        const double liftHeight = BaseYPosition - 250;
        await AnimateToPosition(disk, Canvas.GetLeft(disk), liftHeight);

        var targetX = GetRodXPosition(toRodIndex) - disk.Width / 2;
        await AnimateToPosition(disk, targetX, liftHeight);

        double targetY = BaseYPosition - (_rods[toRodIndex].Count + 1) * DiskHeight;
        await AnimateToPosition(disk, targetX, targetY);

        _rods[toRodIndex].Push(disk);
    }

    private static async Task AnimateToPosition(Rectangle disk, double toX, double toY, int durationMs = 500)
    {
        var fromX = Canvas.GetLeft(disk);
        var fromY = Canvas.GetTop(disk);

        const int frameRate = 60;
        var frames = durationMs / (1000 / frameRate);
        var xStep = (toX - fromX) / frames;
        var yStep = (toY - fromY) / frames;

        for (var i = 0; i < frames; i++)
        {
            Canvas.SetLeft(disk, fromX + xStep * i);
            Canvas.SetTop(disk, fromY + yStep * i);
            await Task.Delay(1000 / frameRate);
        }

        Canvas.SetLeft(disk, toX);
        Canvas.SetTop(disk, toY);
    }

    private static void PlaceDisk(Rectangle disk, int rodIndex, int positionInStack)
    {
        Canvas.SetLeft(disk, GetRodXPosition(rodIndex) - disk.Width / 2);
        Canvas.SetTop(disk, BaseYPosition - (positionInStack + 1) * DiskHeight);
    }

    private static double GetRodXPosition(int rodIndex)
    {
        return 150 + rodIndex * 250;
    }

    private static SolidColorBrush GetDiskBrush(int diskNumber)
    {
        var colors = new[]
            { Brushes.SkyBlue, Brushes.MediumSeaGreen, Brushes.Salmon, Brushes.Gold, Brushes.Orchid, Brushes.SlateBlue, Brushes.Tomato };
        return colors[diskNumber % colors.Length];
    }
}
