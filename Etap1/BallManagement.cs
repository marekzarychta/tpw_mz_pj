using Etap1;

// BallManagement - Warstwa logiki - Model

namespace Etap1
{
    public interface IBallManagement
    {
        void MoveBalls();
    }


    public class BallManagement : IBallManagement
    {

        private readonly IBallRepository ballRepository;
        private readonly Random random = new Random();

        private readonly int gameWidth;
        private readonly int gameHeight;


        public BallManagement(IBallRepository ballRepository, int gameWidth, int gameHeight)
        {
            this.ballRepository = ballRepository;
            this.gameWidth = gameWidth;
            this.gameHeight = gameHeight;
        }

        private void CheckCollisionWithWalls(Ball ball)
        {
            if (ball.X - ball.Diameter / 2 <= 0 || ball.X + ball.Diameter / 2 >= gameWidth)
            {
                ball.Vel_X = -ball.Vel_X;
            }
            if (ball.Y - ball.Diameter / 2 <= 0 || ball.Y + ball.Diameter / 2 >= gameHeight)
            {
                ball.Vel_Y = -ball.Vel_Y;
            }
        }

        public void MoveBalls()
        {
            foreach (var ball in ballRepository.GetBalls())
            {
                ball.X += ball.Vel_X;
                ball.Y += ball.Vel_Y;

                CheckCollisionWithWalls(ball);
            }
        }

    }
}
