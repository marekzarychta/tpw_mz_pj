using System;
using System.Xml.Serialization;

public class Calculator
{
    //private int choice = 0;
    /*
    public void setChoice()
    {
        Console.WriteLine("Wybierz jedna z opcji kalkualatora");
        Console.WriteLine("1. Dodawanie\n2. Odejmowanie\n3. Mnożenie\n4. Dzielenie\n Inna wartosc zamknie program");
        choice = int.Parse(Console.ReadLine());
    }
    */


    public int Add(int choice, int a, int b)
    {
        switch(choice)
        {
            case 1:
                return a + b;
            case 2:
                return a - b;
            case 3:
                return a * b;
            case 4:
                return a / b;
            default:
                throw new ArgumentException("Zamkniecie programu");
        }
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        //int zm = 0;

        Calculator calculator = new Calculator();
        //calculator.setChoice();
        Console.WriteLine(calculator.Add(1, 2, 3));

    }
}