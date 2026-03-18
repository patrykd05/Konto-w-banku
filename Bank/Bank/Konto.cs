namespace Bank
{
    public class Konto
    {
        private string klient;
        private decimal bilans;
        private bool zablokowane = false;

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public string Nazwa
        {
            get { return klient; }
        }

        public decimal Bilans
        {
            get { return bilans; }
        }

        public bool CzyZablokowane
        {
            get { return zablokowane; }
        }

        public void Wplata(decimal kwota)
        {
            if (zablokowane == true)
            {
                throw new InvalidOperationException("Nie można dokonać wpłaty na zablokowane konto.");
            }
            else
            {
                if (kwota < 0)
                {
                    throw new ArgumentException("Kwota wpłaty musi być dodatnia.");
                }
                else if (kwota == 0)
                {
                    throw new ArgumentException("Kwota wpłaty nie może być zerowa.");

                }
                else if (kwota > 0)
                {
                    bilans += kwota;
                }
            }
        }

        public void Wyplata(decimal kwota)
        {
            if (zablokowane == true)
            {
                throw new InvalidOperationException("Nie można dokonać wypłaty na zablokowane konto.");
            }
            else
            {
                if (kwota < 0)
                {
                    throw new ArgumentException("Kwota wypłaty musi być dodatnia.");
                }
                else if (kwota == 0)
                {
                    throw new ArgumentException("Kwota wypłaty nie może być zerowa.");

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

    }
}
