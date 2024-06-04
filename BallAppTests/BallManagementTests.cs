using Logic;
using Data;

namespace BallApp.Tests
{
    [TestClass()]
    public class BallManagementTests
    {
        String filelog = "..\\..\\..\\..\\testdiagnostic.log";

        [TestMethod()]
        public void MoveBallsTest_WithNoBalls()
        {
            BallManagement ballManagement = new BallManagement(800, 600, filelog);

            ballManagement.MoveBalls();

            Assert.IsTrue(true, "MoveBalls method should not throw exception when there are no balls.");
        }

        [TestMethod()]
        public void MoveBallsTest_WithBalls()
        {

            BallManagement ballManagement = new BallManagement(800, 600, filelog);
            ballManagement.SetBalls(5);

            ballManagement.MoveBalls();

            Assert.IsTrue(true, "MoveBalls method should move balls without throwing exception.");
        }

        [TestMethod()]
        public void MoveBallsTest_WithCollision()
        {
            BallManagement ballManagement = new BallManagement(800, 600, filelog);
            List<Ball> balls = new List<Ball>
            {
                new Ball { X = 100, Y = 100, Vel_X = 2, Vel_Y = 2, Diameter = 20 },
                new Ball { X = 780, Y = 100, Vel_X = -2, Vel_Y = 2, Diameter = 20 }
            };
            ballManagement.SetBalls(balls);

            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.X > 0 && ball.X < 800, "Ball should be within screen boundaries before collision.");
            }

            ballManagement.MoveBalls();

            var updatedBalls = ballManagement.GetBalls();

            Assert.IsTrue(updatedBalls.All(b => Math.Abs(b.Vel_X) == 2 && Math.Abs(b.Vel_Y) == 2), "MoveBalls method should change direction of balls after collision with walls.");
        }

        [TestMethod()]
        public async Task HandleCollisionsAsyncTest()
        {
            BallManagement ballManagement = new BallManagement(800, 600, filelog);
            List<Ball> balls = new List<Ball>
            {
                new Ball { X = 100, Y = 100, Vel_X = 2, Vel_Y = 0, Diameter = 20, Weight = 1 },
                new Ball { X = 110, Y = 100, Vel_X = -2, Vel_Y = 0, Diameter = 20, Weight = 1 }
            };
            ballManagement.SetBalls(balls);

            await ballManagement.HandleCollisionsAsync();

            var updatedBalls = ballManagement.GetBalls().ToList();
            Assert.AreEqual(-2, updatedBalls[0].Vel_X, "Ball 1 should change direction after collision.");
            Assert.AreEqual(2, updatedBalls[1].Vel_X, "Ball 2 should change direction after collision.");
        }

        [TestMethod()]
        public async Task LoggerTest()
        {
            BallManagement ballManagement = new BallManagement(800, 600, filelog);
            ballManagement.SetBalls(5);

            ballManagement.MoveBalls();

            string logContent;
            lock (ballManagement)
            {
                logContent = File.ReadAllText(filelog);
            }

            Assert.IsFalse(string.IsNullOrEmpty(logContent), "Log should not be empty after MoveBalls.");
        }
    }
}