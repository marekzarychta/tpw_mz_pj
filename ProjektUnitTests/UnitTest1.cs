using NUnit.Framework;

[TestFixture]
public class CalculatorTests
{
    Calculator calculator;

    [SetUp]
    public void Setup()
    {
        calculator = new Calculator();
    }



    [Test]
    public void TestAdd()
    {
        
        Assert.AreEqual(5, calculator.Add(1, 2, 3)); // Dodawanie: 2 + 3 = 5
    }

    [Test]
    public void TestSubtract()
    {
        Assert.AreEqual(2, calculator.Add(2, 5, 3)); // Odejmowanie: 5 - 3 = 2
    }

    [Test]
    public void TestMultiply()
    {
        Assert.AreEqual(15, calculator.Add(3, 5, 3)); // Mno¿enie: 5 * 3 = 15
    }

    [Test]
    public void TestDivide()
    {
        Assert.AreEqual(2, calculator.Add(4, 6, 3)); // Dzielenie: 6 / 3 = 2
    }

    [Test]
    public void TestDefault()
    {
        Assert.Throws<ArgumentException>(() => calculator.Add(5, 2, 3)); // Testuje wyj¹tek dla nieznanej operacji
    }
}