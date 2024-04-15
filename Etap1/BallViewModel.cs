using Etap1;
using System.Collections.ObjectModel;

// BallViewModel - Warstwa Prezentacji - ViewModel

namespace Etap1
{
    public class BallViewModel
    {
        private readonly IBallManagement ballManagement;
        private readonly IBallRepository ballRepository;
        public ObservableCollection<Ball> Balls { get; private set; }

        public BallViewModel(int gameWidth, int gameHeigh)
        {
            this.ballRepository = new BallRepository();
            this.ballManagement = new BallManagement(ballRepository, gameWidth, gameHeigh);
            this.Balls = new ObservableCollection<Ball>();
        }

        public void StartGame(int ballCount, int gameWidth, int gameHeight)
        {
            ballRepository.SetBalls(ballCount, gameWidth, gameHeight);
            UpdateBallsCollection();
        }

        public void UpdateGame()
        {
            ballManagement.MoveBalls();
            UpdateBallsCollection();
        }

        private void UpdateBallsCollection()
        {
            Balls.Clear();
            foreach (var ball in ballRepository.GetBalls())
            {
                Balls.Add(ball);
            }
        }

    }
}
