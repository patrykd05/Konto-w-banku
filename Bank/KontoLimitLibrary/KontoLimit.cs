using Bank;

namespace KontoLimitLibrary
{
    public class KontoLimit
    {
        private Konto konto;
        private decimal limit;
        private bool debetUzyty = false;


        public KontoLimit(Konto konto, decimal limit = 100)
        {
            if (konto == null)
            {
                throw new ArgumentNullException(nameof(konto));
            }
            this.konto = konto;

            if (limit < 0)
            {
                throw new ArgumentException("Limit nie może być ujemny.");
            }
            
            this.limit = limit;
        }


        public decimal Bilans => konto.Bilans;
        public string Nazwa => konto.Nazwa;
        public bool Zablokowane => konto.CzyZablokowane;


        public decimal Limit
        {
            get => limit;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Limit nie może być ujemny.");
                }
                limit = value;
            }
        }

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota musi być dodatnia.");
            }

            konto.Wplata(kwota);

            if (konto.Bilans >= 0)
            {
                debetUzyty = false;
                konto.OdblokujKonto();
            }
        }

        public void Wyplata(decimal kwota)
        {
            if (konto.CzyZablokowane)
            {
                throw new InvalidOperationException("Nie można wypłacić z zablokowanego konta.");
            }

            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota musi być dodatnia.");
            }

            if (kwota <= konto.Bilans)
            {
                konto.Wyplata(kwota);
                return;
            }

            if (!debetUzyty && kwota <= konto.Bilans + limit)
            {
                konto.Wyplata(kwota);
                debetUzyty = true;
                konto.BlokujKonto();
                return;
            }

            throw new InvalidOperationException("Nie można wykonać operacji – brak środków i debetu.");
        }
        public void BlokujKonto() => konto.BlokujKonto();
        public void OdblokujKonto() => konto.OdblokujKonto();
    }
}
