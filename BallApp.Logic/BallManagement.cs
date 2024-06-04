using Data;
using System.Numerics;


// BallManagement - Warstwa logiki - Model

namespace Logic
{
    public interface IBallManagement
    {
        IEnumerable<Ball> GetBalls();
        Task StartGameAsync(int ballCount, Action<IEnumerable<Ball>> callback);
        Task UpdateGameAsync();
        event EventHandler<IEnumerable<Ball>> GameUpdated;

    }


    public class BallManagement : IBallManagement
    {

        private readonly Random random = new Random();
        private readonly List<Ball> balls = new List<Ball>();
        public event EventHandler<IEnumerable<Ball>> GameUpdated;
        private readonly object lockObject = new object();
        private readonly Rect gameArea;
        private readonly Logger logger;

        public BallManagement(int gameWidth, int gameHeight, string logFilePath)
        {
            gameArea = new Rect(gameWidth, gameHeight);
            logger = new Logger(logFilePath);
        }

        public async Task StartGameAsync(int ballCount, Action<IEnumerable<Ball>> callback)
        {
            SetBalls(ballCount);
            callback?.Invoke(GetBalls());
        }

        private void NotifyGameUpdated()
        {
            GameUpdated?.Invoke(this, GetBalls());
        }

        public async Task UpdateGameAsync()
        {
            MoveBalls();
            await HandleCollisionsAsync();
            NotifyGameUpdated();
        }

        private void LogCollision(Ball ball1, Ball ball2)
        {
            var collisionInfo = new
            {
                Timestamp = DateTime.Now,
                Ball1Id = ball1.ID,
                Ball2Id = ball2.ID,
                Ball1Position = new { X = ball1.X, Y = ball1.Y },
                Ball2Position = new { X = ball2.X, Y = ball2.Y }
            };
            logger.LogAsync(collisionInfo);
        }

        private void CheckCollisionWithWalls(Ball ball)
        {
            if (ball.X - ball.Diameter / 2 <= 0 || ball.X + ball.Diameter / 2 >= gameArea.width)
                ball.Vel_X = -ball.Vel_X;
           
            if (ball.Y - ball.Diameter / 2 <= 0 || ball.Y + ball.Diameter / 2 >= gameArea.height)
                ball.Vel_Y = -ball.Vel_Y;
        }

        public async Task HandleCollisionsAsync()
        {
            await Task.Run(() =>
            {
                lock (lockObject)
                {
                    for (int i = 0; i < balls.Count; i++)
                    {
                        for (int j = i + 1; j < balls.Count; j++)
                        {
                            if (CheckCollision(balls[i], balls[j]))
                            {
                                HandleCollision(balls[i], balls[j]);
                            }
                        }
                    }

                    foreach (var ball in balls)
                    {
                        CheckCollisionWithWalls(ball);
                    }
                }

                
            });
        }

        private bool CheckCollision(Ball ball1, Ball ball2)
        {
            double dx = ball1.X - ball2.X;
            double dy = ball1.Y - ball2.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double minDistance = ball1.Diameter / 2 + ball2.Diameter / 2;
            return distance < minDistance;
        }

        private void HandleCollision(Ball ball1, Ball ball2)
        {
            float dx = ball2.X - ball1.X;
            float dy = ball2.Y - ball1.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            float normalX = dx / distance;
            float normalY = dy / distance;

            float vel1Normal = ball1.Vel_X * normalX + ball1.Vel_Y * normalY;
            float vel2Normal = ball2.Vel_X * normalX + ball2.Vel_Y * normalY;

            float newVel1Normal = (vel1Normal * (ball1.Weight - ball2.Weight) + 2 * ball2.Weight * vel2Normal) / (ball1.Weight + ball2.Weight);
            float newVel2Normal = (vel2Normal * (ball2.Weight - ball1.Weight) + 2 * ball1.Weight * vel1Normal) / (ball1.Weight + ball2.Weight);

            ball1.Vel_X += (newVel1Normal - vel1Normal) * normalX;
            ball1.Vel_Y += (newVel1Normal - vel1Normal) * normalY;
            ball2.Vel_X += (newVel2Normal - vel2Normal) * normalX;
            ball2.Vel_Y += (newVel2Normal - vel2Normal) * normalY;

            LogCollision(ball1, ball2);
        }

        public void MoveBalls()
        {

            lock (lockObject)
            {
                 foreach (var ball in GetBalls())
                 {
                     ball.X += ball.Vel_X;
                     ball.Y += ball.Vel_Y;
                 }
            };
        }

        public void SetBalls(int count)
        {
            lock (lockObject)
            {
                balls.Clear();
                for (int i = 0; i < count; i++)
                {
                    Ball newBall;
                    bool collisionDetected;
                    do{
                        newBall = new Ball
                        {
                            X = random.Next(25, (int)gameArea.width - 25),
                            Y = random.Next(25, (int)gameArea.height - 25),
                            Weight = (float)(random.NextDouble() * 2 + 1),
                        };
                        float speedMultiplier = 2 / newBall.Weight;
                        newBall.Vel_X = GenRandVel() * speedMultiplier;
                        newBall.Vel_Y = GenRandVel() * speedMultiplier;
                        newBall.Diameter = newBall.Weight * 25;

                        collisionDetected = balls.Any(existingBall => CheckCollision(newBall, existingBall));
                    } while (collisionDetected);

                    balls.Add(newBall);
                }
            }
        }

        public void SetBalls(List<Ball> balls)
        {
            lock (lockObject)
            {
                this.balls.Clear();
                this.balls.AddRange(balls);
            }
        }

        public IEnumerable<Ball> GetBalls()
        {
            lock (lockObject)
            {
                return new List<Ball>(balls);
            }
        }

        private float GenRandVel()
        {
            return (float)(random.NextDouble() * 4 - 2);
        }

        
    }

}
