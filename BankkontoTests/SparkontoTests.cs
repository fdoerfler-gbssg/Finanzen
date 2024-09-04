using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bankkonto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankkonto.Tests
{
    [TestClass()]
    public class SparkontoTests
    {
        [TestMethod()]
        [ExpectedException(typeof(Bankkonto.UeberzugslimiteException))]
        public void KannNichtUeberziehen()
        {
            // arrange
            Sparkonto konto = new Sparkonto(0);

            // act and assert
            konto.Beziehe(1.0);
        }
    }
}