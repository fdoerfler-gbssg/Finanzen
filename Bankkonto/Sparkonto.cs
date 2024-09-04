using System;
using Bankkonto;

namespace Bankkonto
{

	public class Sparkonto : Bankkonto
	{
        override public double AktivZins { get; } = 0.01;
        override public double PassivZins { get; } = 0.07;
        public override double Ueberzugslimite { get; } = 0.0;
        public Sparkonto(int kontonummer) : base(kontonummer)
		{
        }
	}

}