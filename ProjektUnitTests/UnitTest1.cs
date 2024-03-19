using NUnit.Framework;

namespace SimpleCalculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_WhenCalled_ReturnsSum()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            int result = calculator.Add(3, 5);

            // Assert
            Assert.AreEqual(8, result);
        }
    }
}
