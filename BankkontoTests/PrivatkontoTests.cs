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
    public class PrivatkontoTests
    {
        [TestMethod()]
        public void KannUeberziehen()
        {
            // arrange
            Privatkonto konto = new Privatkonto(0);

            // act and assert
            konto.Beziehe(100.0);
            
            //assert
            Assert.AreEqual(konto.Guthaben, -100.0, 0.01);
        }

        [TestMethod()]
        [ExpectedException(typeof(Bankkonto.UeberzugslimiteException))]
        public void KannNurBisLimiteUeberziehen()
        {
            // arrange
            Privatkonto konto = new Privatkonto(0);

            // act and assert
            konto.Beziehe(1100.0);
        }

        [TestMethod()]
        public void GebuehrWirdBeimAbschlussVerrechnet()
        {
            // arrange
            Privatkonto konto = new Privatkonto(0);

            // act
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(-Privatkonto.KontoGebuehr, konto.Guthaben, 0.01);
        }
    }
}