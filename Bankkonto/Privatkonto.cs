using System;
using Bankkonto;

namespace Bankkonto
{

	public class Privatkonto : Bankkonto
	{
		public static double KontoGebuehr = 5.0;
		override public double AktivZins { get; } = 0.0;
        override public double PassivZins { get; } = 0.07;
		public override double Ueberzugslimite { get; } = -1000.0;

        public Privatkonto(int kontonummer) : base(kontonummer)
		{
        }

		new public void SchliesseKontoAb()
		{
			Guthaben -= KontoGebuehr;
			base.SchliesseKontoAb();
		}
    }

}