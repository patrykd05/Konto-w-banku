namespace Bank
{
    public class Konto
    {
        private string klient;
        private decimal bilans;
        private bool zablokowane = false;

        public Konto()
        {

        }

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public string Nazwa
        {
            get { return klient; }
        }

        public virtual decimal Bilans
        {
            get { return bilans; }
        }

        public bool CzyZablokowane
        {
            get { return zablokowane; }
        }

        public virtual void Wplata(decimal kwota)
        {
            if (zablokowane == true)
            {
                throw new InvalidOperationException("Nie można dokonać wpłaty na zablokowane konto.");
            }
            else
            {
                if (kwota <= 0)
                {
                    throw new ArgumentException("Kwota musi być dodatnia.");
                }
                else if (kwota > 0)
                {
                    bilans += kwota;
                }
            }
        }

        public virtual void Wyplata(decimal kwota)
        {
            if (zablokowane)
            {
                throw new InvalidOperationException("Nie można wypłacić z zablokowanego konta.");
            }
            else
            {
                if (kwota <= 0)
                {
                    throw new ArgumentException("Kwota musi być dodatnia.");
                }    
                else if (kwota > bilans)
                {
                    throw new InvalidOperationException("Nie można wypłacić więcej niż aktualny bilans.");
                }
                else if (kwota > 0)
                {
                    bilans -= kwota;
                }
            }
        }

        public void BlokujKonto()
        {
            zablokowane = true;
        }

        public void OdblokujKonto()
        {
            zablokowane = false;
        }

        protected void ZmienBilans(decimal kwota)
        {
            bilans += kwota;
        }

    }
}
