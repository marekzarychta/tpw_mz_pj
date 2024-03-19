using NUnit.Framework;
using KalkulatorApp;

namespace ZAD0_TESTS
{
    public class Tests
    {
        private Kalkulator kalkulator;

        [SetUp]
        public void Setup()
        {
            kalkulator = new Kalkulator();
        }

        [Test]
        public void Dodaj_DwaDodatnieZwracaSumê()
        {
            int wynik = kalkulator.Dodaj(5, 3);
            Assert.AreEqual(8, wynik);
        }

        [Test]
        public void Dodaj_DwaUjemneZwracaSumê()
        {
            int wynik = kalkulator.Dodaj(-4, -6);
            Assert.AreEqual(-10, wynik);
        }

        [Test]
        public void Dodaj_DodatniaIUjemnaZwracaSumê()
        {
            int wynik = kalkulator.Dodaj(-5, 10);
            Assert.AreEqual(5, wynik);
        }

        [Test]
        public void Dodaj_ZeremZwracaT¹Sam¹Liczbe()
        {
            int wynik = kalkulator.Dodaj(0, 7);
            Assert.AreEqual(7, wynik);
        }
    }
}