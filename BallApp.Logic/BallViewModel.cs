using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;


// BallViewModel - Warstwa logiki

namespace Logic
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private readonly IBallManagement ballManagement;
        public ObservableCollection<Ball> Balls { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private CancellationTokenSource cancellationTokenSource;

// Metody
        public BallViewModel(int width, int height, string collisionslogFilePath, string logFilePath)
        {
            ballManagement = new BallManagement(width, height, collisionslogFilePath, logFilePath);
            Balls = new ObservableCollection<Ball>();
            ballManagement.GameUpdated += OnGameUpdated;
        }
        
        public async Task StartGameAsync(int ballCount)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            await ballManagement.StartGameAsync(ballCount, UpdateBalls);
            StartUpdatingGame(cancellationTokenSource.Token);
        }

        private async void StartUpdatingGame(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UpdateGameAsync();
                await Task.Delay(12);
            }
        }
        public async Task UpdateGameAsync()
        {
            await ballManagement.UpdateGameAsync();
        }

        public void SaveLog()
        {
            ballManagement.SaveLog();
        }

        private void OnGameUpdated(object sender, IEnumerable<Ball> e)
        {
            UpdateBalls(e);
            OnPropertyChanged(nameof(Balls));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateBalls(IEnumerable<Ball> balls)
        {
            Balls.Clear();
            foreach (var ball in balls)
            {
                Balls.Add(ball);
            }
            OnPropertyChanged(nameof(Balls));
        }

    }
}
