using Bank;

namespace KontoPlusLibrary
{
    public class KontoPlus : Konto
    {
        public decimal DebetLimit { get; private set; }
        private bool debetUzyty = false;
        public KontoPlus(decimal debetLimit)
        {

            if (debetLimit < 0)
            {
                throw new ArgumentException();
            }
                

            DebetLimit = debetLimit;
        }

        public KontoPlus(decimal debetLimit, Konto konto) : base(konto.Nazwa, konto.Bilans)
        {
            DebetLimit = debetLimit;
        }

        public void ZmienLimitDebetowy(decimal nowyLimit)
        {
            if (nowyLimit < 0)
            {
                throw new ArgumentException("Limit nie może być ujemny.");
            }
                
            DebetLimit = nowyLimit;
        }

        public override void Wyplata(decimal kwota)
        {
            if (CzyZablokowane)
            {
                throw new InvalidOperationException("Nie można wypłacić z zablokowanego konta.");
            }
                
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota musi być dodatnia.");
            }

            if (kwota <= base.Bilans)
            {
                ZmienBilans(-kwota);
                return;
                
            }

            if (!debetUzyty && kwota <= base.Bilans + DebetLimit)
            {
                ZmienBilans(-kwota);
                debetUzyty = true;
                BlokujKonto();
                return;
            }

            throw new InvalidOperationException("Nie można wykonać operacji.");
        }

        public override void Wplata(decimal kwota)
        {
            if (kwota <= 0) throw new ArgumentException("Kwota musi być dodatnia.");
            ZmienBilans(kwota);

            if (base.Bilans >= 0)
            {
                debetUzyty = false;
                OdblokujKonto();
            }
        }


        public override decimal Bilans => base.Bilans;

    }
}
