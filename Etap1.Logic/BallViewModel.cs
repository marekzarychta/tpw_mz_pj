using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;


// BallViewModel - Warstwa logiki

namespace Logic
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private readonly IBallManagement ballManagement;


        public ObservableCollection<Ball> Balls { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public BallViewModel(int gameWidth, int gameHeigh)
        {

            ballManagement = new BallManagement(gameWidth, gameHeigh);
            Balls = new ObservableCollection<Ball>();
        }

        public async Task StartGameAsync(int ballCount)
        {
            await ballManagement.StartGameAsync(ballCount, UpdateGame); 
        }

        public async Task UpdateGameAsync()
        {
            await ballManagement.UpdateGameAsync();
        }

        private void UpdateGame(IEnumerable<Ball> balls)
        {
            Balls.Clear();
            foreach (var ball in balls)
            {
                Balls.Add(ball);
            }
        }

    }
}
