using Bank;
using KontoPlusLibrary;

namespace TestKontoPlus
{
    [TestClass]
    public sealed class WyplataTesty
    {
        [TestMethod]
        public void Wyplata_BezDebetu()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            konto.Wyplata(40);

            Assert.AreEqual(60, konto.Bilans);
        }

        [TestMethod]
        public void Wyplata_PrzekraczaLimit()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(200));
        }

        [TestMethod]
        public void Wyplata_ZDebetem()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            konto.Wyplata(120);

            Assert.IsTrue(konto.CzyZablokowane);
            Assert.AreEqual(-20, konto.Bilans);
        }
    }


    [TestClass]
    public sealed class WplataTesty
    {
        [TestMethod]
        public void Wplata_ZwiekszaBilans()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            konto.Wplata(50);

            Assert.AreEqual(150, konto.Bilans);
        }
    }

    [TestClass]
    public sealed class BilansTesty 
    {
        [TestMethod]
        public void Bilans_ZwracaDostepneSrodki_PrzedDebetem()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            Assert.AreEqual(100, konto.Bilans);
        }

        [TestMethod]
        public void Bilans_ZwracaRzeczywistyStan_PoDebecie()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            konto.Wyplata(120);

            Assert.AreEqual(-20, konto.Bilans);
        }
    }

    [TestClass]
    public sealed class DebetTesty
    {
        [TestMethod]
        public void Debet_MoznaUzycTylkoRaz()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            konto.Wyplata(120);

            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(10));
        }

        [TestMethod]
        public void Debet_ResetujeSie_PoSplacie()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            konto.Wyplata(120);
            konto.Wplata(50);

            konto.Wyplata(60);

            Assert.IsTrue(konto.CzyZablokowane);
        }
    }

    [TestClass]
    public sealed class KontoTesty
    {
        [TestMethod]
        public void Konto_OdblokowujeSie_PoSplacie()
        {
            var konto = new KontoPlus(50, new Konto("Jan", 100));

            konto.Wyplata(120);

            konto.Wplata(50);

            Assert.IsFalse(konto.CzyZablokowane);
        }
    }
}
