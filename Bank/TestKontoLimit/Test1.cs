using Bank;
using KontoLimitLibrary;

namespace TestKontoLimit
{
    [TestClass]
    public sealed class KonstruktorTesty
    {
        [TestMethod]
        public void Konstruktor_InicjalizujeKontoILimit()
        {
            var konto = new Konto("Jan Kowalski", 500);
            decimal limit = 200;

            var kontoLimit = new KontoLimit(konto, limit);

            Assert.AreEqual("Jan Kowalski", kontoLimit.Nazwa);
            Assert.AreEqual(500, kontoLimit.Bilans);
            Assert.AreEqual(limit, kontoLimit.Limit);
            Assert.IsFalse(kontoLimit.Zablokowane);
        }
    }

    [TestClass]
    public sealed class WplataTesty
    {
        [TestMethod]
        public void Wplata_PowiekszaBilansIOdblokowujeDebet()
        {
            var konto = new Konto("Anna", 100);
            var kontoLimit = new KontoLimit(konto, 200);

            kontoLimit.Wplata(50);

            Assert.AreEqual(150, kontoLimit.Bilans);
            Assert.IsFalse(kontoLimit.Zablokowane);
        }

        [TestMethod]
        public void Wplata_NieDodatnia_Wyjatek()
        {
            var konto = new Konto("Anna", 100);
            var kontoLimit = new KontoLimit(konto, 200);

            Assert.Throws<ArgumentException>(() => kontoLimit.Wplata(0));
            Assert.Throws<ArgumentException>(() => kontoLimit.Wplata(-10));
        }
    }

    [TestClass]
    public sealed class WyplataTesty
    {
        [TestMethod]
        public void Wyplata_Zwykla_MniejszaNizBilans()
        {
            var konto = new Konto("Marek", 300);
            var kontoLimit = new KontoLimit(konto, 100);

            kontoLimit.Wyplata(200);

            Assert.AreEqual(100, kontoLimit.Bilans);
            Assert.IsFalse(kontoLimit.Zablokowane);
        }

        [TestMethod]
        public void Wyplata_WykraczaPozaBilans_UzywaDebetuIBlokuje()
        {
            var konto = new Konto("Marek", 100);
            var kontoLimit = new KontoLimit(konto, 50);

            Assert.Throws<InvalidOperationException>(() => kontoLimit.Wyplata(140));
        }

        [TestMethod]
        public void Wyplata_PowyzejBilansPlusLimit_WyrzucaWyjatek()
        {
            var konto = new Konto("Marek", 100);
            var kontoLimit = new KontoLimit(konto, 50);

            Assert.Throws<InvalidOperationException>(() => kontoLimit.Wyplata(200));
        }

        [TestMethod]
        public void Wyplata_ZablokowaneKonto_WyrzucaWyjatek()
        {
            var konto = new Konto("Marek", 100);
            var kontoLimit = new KontoLimit(konto, 50);

            kontoLimit.BlokujKonto();

            Assert.Throws<InvalidOperationException>(() => kontoLimit.Wyplata(50));
        }
    }

    [TestClass]
    public sealed class Blok_OdblokTest
    {
        [TestMethod]
        public void BlokujIOdblokujKonto_DzialajaPoprawnie()
        {
            var konto = new Konto("Anna", 100);
            var kontoLimit = new KontoLimit(konto, 100);

            kontoLimit.BlokujKonto();
            Assert.IsTrue(kontoLimit.Zablokowane);

            kontoLimit.OdblokujKonto();
            Assert.IsFalse(kontoLimit.Zablokowane);
        }
    }

    [TestClass]
    public sealed class ZmianaLimituTest
    {
        [TestMethod]
        public void ZmianaLimitu_PoprawnieAktualizuje()
        {
            var konto = new Konto("Jan", 100);
            var kontoLimit = new KontoLimit(konto, 50);

            kontoLimit.Limit = 200;
            Assert.AreEqual(200, kontoLimit.Limit);

            Assert.Throws<ArgumentException>(() => kontoLimit.Limit = -10);
        }
    }
}
