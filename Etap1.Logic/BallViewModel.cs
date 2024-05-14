using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

// BallViewModel - Warstwa Prezentacji - ViewModel

namespace Logic
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private readonly IBallManagement ballManagement;
        private ObservableCollection<Ball> _balls;

        public ObservableCollection<Ball> Balls
        {
            get { return _balls; }
            set
            {
                _balls = value;
                NotifyPropertyChanged(nameof(Balls));
            }
        }
        //public event EventHandler GameUpdated;
        public event PropertyChangedEventHandler PropertyChanged;


        public BallViewModel(int gameWidth, int gameHeigh)
        {
            ballManagement = new BallManagement(gameWidth, gameHeigh);
            Balls = new ObservableCollection<Ball>();
            ballManagement.GameUpdated += OnGameUpdated;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task StartGameAsync(int ballCount)
        {
            //ballManagement.SetBalls(ballCount);
            await ballManagement.StartGameAsync(ballCount, UpdateGame); // Przekazanie metody do callbacku

            //await UpdateBallsCollectionAsync();
        }

        public async Task UpdateGameAsync()
        {
            await ballManagement.UpdateGameAsync();
            //GameUpdated?.Invoke(this, EventArgs.Empty);

/*            await ballManagement.HandleCollisionsAsync();
            await Task.Run(() =>
            {
                ballManagement.MoveBalls();
            });
            await UpdateBallsCollectionAsync();*/
        }

        private void UpdateGame(IEnumerable<Ball> balls)
        {
            Balls.Clear();
            foreach (var ball in balls)
            {
                Balls.Add(ball);
            }
        }

        private void OnGameUpdated(object sender, IEnumerable<Ball> balls)
        {
            Balls = new ObservableCollection<Ball>(balls);

            UpdateBallsCollectionAsync(); // Aktualizacja kolekcji Balls na podstawie aktualnego stanu gry
        }

        private async Task UpdateBallsCollectionAsync()
        {
            await Task.Run(() =>
            {
                var balls = ballManagement.GetBalls();
                Balls.Clear();
                foreach (var ball in ballManagement.GetBalls())
                {
                    Balls.Add(ball);
                }
            });
        }

    }
}
