using Bank;
namespace TestBank
{
    [TestClass]
    public sealed class TestyKonstruktor
    {
        [TestMethod]
        public void KonstruktorPoprawneDane()
        {
            // Arrange
            var nazwa = "Jan";
            decimal saldoPoczatkowe = 1000;

            // Act
            var konto = new Konto(nazwa, saldoPoczatkowe);

            // Assert
            Assert.AreEqual(nazwa, konto.Nazwa);
            Assert.AreEqual(saldoPoczatkowe, konto.Bilans);
        }
    }

    [TestClass]
    public sealed class TestyWplata
    {
        [TestMethod]
        public void Wplata_PoprawnaKwota()
        {
            var konto = new Konto("Jan", 100);

            konto.Wplata(50);

            Assert.AreEqual(150, konto.Bilans);
        }

        [TestMethod]
        public void Wplata_UjemnaKwota()
        {
            var konto = new Konto("Jan", 100);

            Assert.Throws<ArgumentException>(() => konto.Wplata(-10));
        }

        [TestMethod]
        public void Wplata_Zero()
        {
            var konto = new Konto("Jan", 100);

            Assert.Throws<ArgumentException>(() => konto.Wplata(0));
        }

        [TestMethod]
        public void Wplata_ZablokowaneKonto()
        {
            var konto = new Konto("Jan", 100);
            konto.BlokujKonto();
            Assert.Throws<InvalidOperationException>(() => konto.Wplata(50));
        }
    }

    [TestClass]
    public sealed class TestyWyplata
    {
        [TestMethod]
        public void Wyplata_PoprawnaKwota()
        {
            var konto = new Konto("Jan", 200);

            konto.Wyplata(50);

            Assert.AreEqual(150, konto.Bilans);
        }

        [TestMethod]
        public void Wyplata_ZaDuzo()
        {
            var konto = new Konto("Jan", 100);

            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(200));
        }

        [TestMethod]
        public void Wyplata_Zero()
        {
            var konto = new Konto("Jan", 100);

            Assert.Throws<ArgumentException>(() => konto.Wyplata(0));
        }

        [TestMethod]
        public void Wyplata_ZablokowaneKonto()
        {
            var konto = new Konto("Jan", 100);
            konto.BlokujKonto();

            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(50));
        }
    }

    [TestClass]
    public sealed class TestyBlokowanieKonta
    {
        [TestMethod]
        public void BlokujKonto()
        {
            var konto = new Konto("Jan");

            konto.BlokujKonto();

            Assert.IsTrue(konto.CzyZablokowane);
        }
    }

    [TestClass]
    public sealed class TestyOdblokowanieKonta
    {
        [TestMethod]
        public void OdblokujKonto()
        {
            var konto = new Konto("Jan");
            konto.BlokujKonto();

            konto.OdblokujKonto();

            Assert.IsFalse(konto.CzyZablokowane);
        }
    }
}
