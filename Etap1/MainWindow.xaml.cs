using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

using Logic;

//  BallRepository, Ball - Model (Warstwa danych)
//  BallManagement - Model (Warstwa logiki) 
//  BallViewModel, MainWindows.xaml.cs - ViewModel (Warstwa prezentacji)
//  MainWindow.xaml - View

namespace Presentation
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private BallViewModel ballViewModel;

        public MainWindow()
        {
            InitializeComponent();
            ballViewModel = new BallViewModel(800, 450);
            DataContext = ballViewModel;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _timer?.Stop();
            if (int.TryParse(numBallsPicker.Text, out int numBalls))
            {
                ballViewModel.StartGame(numBalls, (int)canvas.ActualWidth, (int)canvas.ActualHeight);
            }
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            _timer.Tick += GameTimer_Tick;
            _timer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ballViewModel.UpdateGame();
            DrawBalls();
        }

        private void DrawBalls()
        {
            canvas.Children.Clear();
            foreach (var ball in ballViewModel.Balls)
            {
                var ellipse = new Ellipse
                {
                    Fill = Brushes.Blue,
                    Width = ball.Diameter,
                    Height = ball.Diameter
                };

                Canvas.SetLeft(ellipse, ball.X - ball.Diameter / 2);
                Canvas.SetTop(ellipse, ball.Y - ball.Diameter / 2);
                canvas.Children.Add(ellipse);
            }
        }
    }
}
