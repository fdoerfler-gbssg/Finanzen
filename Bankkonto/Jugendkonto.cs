using System;
using Bankkonto;

namespace Bankkonto
{

	public class Jugendkonto : Bankkonto
	{
        override public double AktivZins { get; } = 0.015;
        override public double PassivZins { get; } = 0.05;
        public override double Ueberzugslimite { get; } = 0.0;

        public static double Bezugslimite = 100;

		public Jugendkonto(int kontonummer) : base(kontonummer)
		{
		}

		public new void Beziehe(double betrag)
		{
			if (betrag > Bezugslimite)
				throw new BezugslimiteException();
			else
				base.Beziehe(betrag);
		}
	}
}