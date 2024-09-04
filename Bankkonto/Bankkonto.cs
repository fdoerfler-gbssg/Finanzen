using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankkonto
{
    public abstract class Bankkonto(int kontonummer)
    {
        public class UeberzugslimiteException : Exception;
        public class BezugslimiteException : Exception;

        // Zinssätze nicht in Prozent
        abstract public double AktivZins { get; }
        abstract public double PassivZins { get; }
        abstract public double Ueberzugslimite { get; }

        private int KontoNummer { get; } = kontonummer;
        public double Guthaben { get; protected set; } = 0;
        protected double AktivzinsGuthaben {get; private set;} = 0.0;
        protected double PassivzinsGuthaben { get; private set; } = 0.0;

        public void ZahleEin(double betrag)
        {
            Guthaben += betrag;
        }

        public void Beziehe(double betrag)
        {
            double neuerBetrag = Guthaben - betrag;
            if (neuerBetrag < Ueberzugslimite)
                throw new UeberzugslimiteException();
            else
                Guthaben -= betrag;
        }

        public void SchribeZinsGut(int anzTage)
        {
            if (Guthaben > 0.0)
                AktivzinsGuthaben += Guthaben * AktivZins / 360 * anzTage;
            else
                PassivzinsGuthaben += Guthaben * PassivZins / 360 * anzTage;
        }

        public void SchliesseKontoAb()
        {
            Guthaben += AktivzinsGuthaben + PassivzinsGuthaben;
        }
    }
}
