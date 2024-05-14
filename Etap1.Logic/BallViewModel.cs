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
        private ObservableCollection<Data.Data> _balls;
        private ObservableCollection<Data.Data> _previousBalls;


        public ObservableCollection<Data.Data> Balls
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
            Balls = new ObservableCollection<Data.Data>();
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

            // Sprawdzenie, czy wystąpiła zmiana w pozycji kulek
            if (!AreBallsEqual(_previousBalls, Balls))
            {
                _previousBalls = new ObservableCollection<Data.Data>(Balls);
                await UpdateGameAsync();
            }
            //GameUpdated?.Invoke(this, EventArgs.Empty);

            /*            await ballManagement.HandleCollisionsAsync();
                        await Task.Run(() =>
                        {
                            ballManagement.MoveBalls();
                        });
                        await UpdateBallsCollectionAsync();*/

        }

        private bool AreBallsEqual(ObservableCollection<Data.Data> balls1, ObservableCollection<Data.Data> balls2)
        {
            if (balls1.Count != balls2.Count)
                return false;

            for (int i = 0; i < balls1.Count; i++)
            {
                if (balls1[i].X != balls2[i].X || balls1[i].Y != balls2[i].Y)
                    return false;
            }

            return true;
        }

        private void UpdateGame(IEnumerable<Data.Data> balls)
        {
            Balls.Clear();
            foreach (var ball in balls)
            {
                Balls.Add(ball);
            }
        }

        private void OnGameUpdated(object sender, IEnumerable<Data.Data> balls)
        {
            _previousBalls = new ObservableCollection<Data.Data>(Balls);
            Balls = new ObservableCollection<Data.Data>(balls);
            Debug.WriteLine("OnGame");
            Balls = new ObservableCollection<Data.Data>(balls);

            UpdateBallsCollectionAsync(); // Aktualizacja kolekcji Balls na podstawie aktualnego stanu gry
        }

        private async Task UpdateBallsCollectionAsync()
        {
            await Task.Run(() =>
            {
                var balls = ballManagement.GetBalls();
                Debug.WriteLine("Updatecollection");

                Balls.Clear();
                foreach (var ball in ballManagement.GetBalls())
                {
                    Balls.Add(ball);
                }
            });
        }

    }
}
