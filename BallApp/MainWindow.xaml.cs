using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Logic;

//  Ball - Model (Warstwa danych)
//  BallViewModel, BallManagement - Model (Warstwa logiki) 
//  MainWindows.xaml.cs - ViewModel (Warstwa prezentacji)
//  MainWindow.xaml - View

namespace Presentation
{
    public partial class MainWindow : Window
    {
        private BallViewModel ballViewModel;

        public MainWindow()
        {
            InitializeComponent();
            ballViewModel = new BallViewModel(800, 450, "..\\..\\..\\..\\diagnostics.log");
            ballViewModel.PropertyChanged += BallViewModel_PropertyChanged;
            DataContext = ballViewModel;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(numBallsPicker.Text, out int numBalls))
            {
                await ballViewModel.StartGameAsync(numBalls);
            }
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
