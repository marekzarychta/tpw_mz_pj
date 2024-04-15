namespace Etap1.Tests
{
    [TestClass()]
    public class BallRepositoryTests
    {
        [TestMethod()]
        public void SetBallsTest_WithValidCount()
        {
            BallRepository ballRepository = new BallRepository();
            int expectedCount = 5;

            ballRepository.SetBalls(expectedCount, 800, 600);
            var balls = ballRepository.GetBalls();
            int actualCount = balls.Count();

            Assert.AreEqual(expectedCount, actualCount, "SetBalls method should add specified number of balls.");
        }

        [TestMethod()]
        public void SetBallsTest_WithZeroCount()
        {
            BallRepository ballRepository = new BallRepository();
            int expectedCount = 0;

            ballRepository.SetBalls(expectedCount, 800, 600);
            var balls = ballRepository.GetBalls();
            int actualCount = balls.Count();

            Assert.AreEqual(expectedCount, actualCount, "SetBalls method should add zero balls.");
        }

        [TestMethod()]
        public void SetBallsTest_WithNegativeCount()
        {
            BallRepository ballRepository = new BallRepository();
            int expectedCount = -5;

            // Ensure that SetBalls method doesn't add any balls for negative count
            ballRepository.SetBalls(expectedCount, 800, 600);
            var balls = ballRepository.GetBalls();
            int actualCount = balls.Count();

            Assert.AreEqual(0, actualCount, "SetBalls method should not add any balls for negative count.");
        }

        [TestMethod()]
        public void GetBallsTest_WithEmptyRepository()
        {
            BallRepository ballRepository = new BallRepository();

            var balls = ballRepository.GetBalls();
            int actualCount = balls.Count();

            Assert.AreEqual(0, actualCount, "GetBalls method should return empty list when repository is empty.");
        }
    }
}