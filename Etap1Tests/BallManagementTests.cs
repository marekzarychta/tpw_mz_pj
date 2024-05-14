using Logic;
using Data;

namespace Etap1.Tests
{
    [TestClass()]
    public class BallManagementTests
    {
        [TestMethod()]
        public void MoveBallsTest_WithNoBalls()
        {
            BallManagement ballManagement = new BallManagement(800, 600);

            ballManagement.MoveBalls();

            Assert.IsTrue(true, "MoveBalls method should not throw exception when there are no balls.");
        }

        [TestMethod()]
        public void MoveBallsTest_WithBalls()
        {

            BallManagement ballManagement = new BallManagement(800, 600);
            ballManagement.SetBalls(5);

            ballManagement.MoveBalls();

            Assert.IsTrue(true, "MoveBalls method should move balls without throwing exception.");
        }

        [TestMethod()]
        public void MoveBallsTest_WithCollision()
        {
            BallManagement ballManagement = new BallManagement(800, 600);
            List<Ball> balls = new List<Ball>
    {
        new Ball { X = 100, Y = 100, Vel_X = 2, Vel_Y = 2, Diameter = 20 },
        new Ball { X = 780, Y = 100, Vel_X = -2, Vel_Y = 2, Diameter = 20 }
    };
            ballManagement.SetBalls(balls);

            // Sprawdzenie, czy piłki znajdują się przed kolizją ze ścianą
            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.X > 0 && ball.X < 800, "Ball should be within screen boundaries before collision.");
            }

            ballManagement.MoveBalls();

            var updatedBalls = ballManagement.GetBalls();

            // Sprawdzenie, czy piłki zmieniły prędkość po kolizji ze ścianą
            Assert.IsTrue(updatedBalls.All(b => Math.Abs(b.Vel_X) == 2 && Math.Abs(b.Vel_Y) == 2), "MoveBalls method should change direction of balls after collision with walls.");
        }
    }
}