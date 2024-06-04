using Logic;
using Data;


namespace BallApp.Tests
{
    [TestClass()]
    public class BallViewModelTests
    {
        String filelog = "..\\..\\..\\..\\testdiagnostic.log";

        [TestMethod()]
        public void StartGameTest()
        {
            int ballCount = 5;
            int gameWidth = 800;
            int gameHeight = 600;
            var ballViewModel = new BallViewModel(gameWidth, gameHeight, filelog);

            ballViewModel.StartGameAsync(ballCount);

            Assert.IsNotNull(ballViewModel.Balls, "UpdateGame method should not return null collection of balls.");
            Assert.AreEqual(ballCount, ballViewModel.Balls.Count, "StartGame method should add specified number of balls.");
        }

        [TestMethod()]
        public async Task UpdateGameTest()
        {
            int ballCount = 5;
            int gameWidth = 800;
            int gameHeight = 600;
            var ballViewModel = new BallViewModel(gameWidth, gameHeight, filelog);
            await ballViewModel.StartGameAsync(ballCount);

            await ballViewModel.UpdateGameAsync();

            Assert.IsNotNull(ballViewModel.Balls, "UpdateGame method should not return null collection of balls.");
            Assert.IsTrue(ballViewModel.Balls.All(ball => ball.X != 0 && ball.Y != 0), "UpdateGame method should update positions of all balls.");
        }


        [TestMethod]
        public async Task OnPropertyChanged_ShouldInvokePropertyChangedEvent_WhenBallsPropertyChanges()
        {
            // Arrange
            var ballViewModel = new BallViewModel(800, 600, filelog);
            bool eventInvoked = false;
            ballViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(BallViewModel.Balls))
                {
                    eventInvoked = true;
                }
            };

            // Act
            await ballViewModel.StartGameAsync(5);
            await Task.Delay(100); // Small delay to allow async operations to complete

            // Assert
            Assert.IsTrue(eventInvoked, "PropertyChanged event should be invoked when Balls property changes.");
        }

        [TestMethod]
        public async Task UpdateBalls_ShouldUpdateBallsCollection_WhenCalled()
        {
            // Arrange
            var ballViewModel = new BallViewModel(800, 600, filelog);
            var balls = new List<Ball>
            {
                new Ball { X = 100, Y = 100, Vel_X = 2, Vel_Y = 2, Diameter = 20 },
                new Ball { X = 200, Y = 200, Vel_X = -2, Vel_Y = 2, Diameter = 20 }
            };

            // Act
            ballViewModel.StartGameAsync(5).Wait();
            ballViewModel.UpdateGameAsync().Wait();
            ballViewModel.UpdateBalls(balls);

            // Assert
            Assert.AreEqual(balls.Count, ballViewModel.Balls.Count, "Balls collection should be updated with new balls.");
            Assert.IsTrue(balls.SequenceEqual(ballViewModel.Balls), "Balls collection should contain the updated balls.");
        }
    }


}