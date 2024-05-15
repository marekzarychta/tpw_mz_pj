using Logic;
using Data;


namespace BallApp.Tests
{
    [TestClass()]
    public class BallViewModelTests
    {
        [TestMethod()]
        public void StartGameTest()
        {
            int ballCount = 5;
            int gameWidth = 800;
            int gameHeight = 600;
            var ballViewModel = new BallViewModel(gameWidth, gameHeight);

            ballViewModel.StartGameAsync(ballCount);

            Assert.IsNotNull(ballViewModel.Balls, "UpdateGame method should not return null collection of balls.");
            Assert.AreEqual(ballCount, ballViewModel.Balls.Count, "StartGame method should add specified number of balls.");
        }

        [TestMethod()]
        public void UpdateGameTest()
        {
            int ballCount = 5;
            int gameWidth = 800;
            int gameHeight = 600;
            var ballViewModel = new BallViewModel(gameWidth, gameHeight);
            ballViewModel.StartGameAsync(ballCount);

            Assert.IsNotNull(ballViewModel.Balls, "UpdateGame method should not return null collection of balls.");
            Assert.IsTrue(ballViewModel.Balls.All(ball => ball.X != 0 && ball.Y != 0), "UpdateGame method should update positions of all balls.");
        }
    }
}