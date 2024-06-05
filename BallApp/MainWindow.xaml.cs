using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

using Logic;

//  Ball, Logger - Model (Warstwa danych)
//  BallViewModel, BallManagement - Model (Warstwa logiki) 
//  MainWindows.xaml.cs - ViewModel (Warstwa prezentacji)
//  MainWindow.xaml - View

namespace Presentation
{
    public partial class MainWindow : Window
    {
        private BallViewModel ballViewModel;
        private DispatcherTimer timer;

// Metody
        public MainWindow()
        {
            InitializeComponent();
            ballViewModel = new BallViewModel(800, 450, 
                "..\\..\\..\\..\\collisions_diagnostic.log",    // Dane diagnostyczne ze zderzeń kul
                "..\\..\\..\\..\\diagnostics.log");             // Dane diagnostyczne z interwałów 3 sekundowych położeń kul
            ballViewModel.PropertyChanged += BallViewModel_PropertyChanged;
            DataContext = ballViewModel;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer?.Stop();
            if (int.TryParse(numBallsPicker.Text, out int numBalls))
            {
                await ballViewModel.StartGameAsync(numBalls);
                ballViewModel.SaveLog(); // Zapis o początkowym położeniu kul
            }
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) }; // Interwał 3 sekundowy
            timer.Tick += GameTimer_Tick;
            timer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ballViewModel.SaveLog();
        }

        private void BallViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BallViewModel.Balls))
            {
                DrawBalls();
            }
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
