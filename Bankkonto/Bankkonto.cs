using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankkonto
{
    public abstract class Bankkonto(int kontonummer)
    {
        public class UngueltigeEingabeException : Exception;
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

        public virtual void Beziehe(double betrag)
        {
            double neuerBetrag = Guthaben - betrag;
            if (neuerBetrag < Ueberzugslimite)
                throw new UeberzugslimiteException();
            else
                Guthaben -= betrag;
        }

        private double berechneEffektiverAktivzins()
        {
            double aktivZinsAbzug = 0.0;
            if (Guthaben < 10000.0) { aktivZinsAbzug = 0.0; }
            else if (Guthaben < 50000.0) { aktivZinsAbzug = 0.005; }
            else if (Guthaben < 100000.0) { aktivZinsAbzug = 0.0075; }
            else { aktivZinsAbzug = 0.01; }
            return AktivZins - aktivZinsAbzug;
        }
        public void SchreibeZinsGut(int anzTage)
        {
            if (anzTage < 0) throw new UngueltigeEingabeException();
            if (Guthaben > 0.0)
                AktivzinsGuthaben += Guthaben * berechneEffektiverAktivzins() / 360 * anzTage;
            else
                PassivzinsGuthaben += Guthaben * PassivZins / 360 * anzTage;
        }

        public void SchliesseKontoAb()
        {
            Guthaben += AktivzinsGuthaben + PassivzinsGuthaben;
        }
    }
}
