using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankkonto
{
    public class Bankkonto(int kontonummer)
    {
        // Zinse nicht in Prozent
        public static double AktivZins { get; } = 0.001;
        public static double PassivZins { get; } = 0.015;

        private int KontoNummer { get; } = kontonummer;
        public double Guthaben { get; private set; } = 0;

        private double ZinsGuthaben = 0.0;

        public void ZahleEin(double betrag)
        {
            Guthaben += betrag;
        }

        public void Beziehe(double betrag)
        {
            Guthaben -= betrag;
        }

        public void SchribeZinsGut(int anzTage)
        {
            if (Guthaben > 0.0)
                ZinsGuthaben += Guthaben * AktivZins / 360 * anzTage;
            else
                ZinsGuthaben += Guthaben * PassivZins / 360 * anzTage;
        }

        public void SchliesseKontoAb()
        {
            Guthaben += ZinsGuthaben;
        }
    }
}
