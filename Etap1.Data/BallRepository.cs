// BallRepository - Warstwa Danych - Model

namespace Data
{
    public interface IBallRepository
    {
        void SetBalls(int count, int gameWidth, int gameHeight);
        void SetBalls(List<Ball> balls);
        IEnumerable<Ball> GetBalls();
    }

    public class BallRepository : IBallRepository
    {
        private readonly List<Ball> balls = new List<Ball>();
        private readonly Random random = new Random();

        public void SetBalls(int count, int gameWidth, int gameHeight)
        {
            balls.Clear();
            for (int i = 0; i < count; i++)
            {
                balls.Add(new Ball
                {
                    X = random.Next(10, gameWidth - 10),
                    Y = random.Next(10, gameHeight - 10),
                    Vel_X = GenRandVel(),
                    Vel_Y = GenRandVel(),
                    Diameter = 20
                });
            }
        }

        public void SetBalls(List<Ball> balls)
        {
            this.balls.Clear();
            this.balls.AddRange(balls);
        }

        private float GenRandVel()
        {
            return (float)(random.NextDouble() * 4 - 2);
        }

        public IEnumerable<Ball> GetBalls()
        {
            return balls;
        }
    }
}
