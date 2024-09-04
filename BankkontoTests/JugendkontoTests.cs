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
    public class JugendkontoTests
    {
        [TestMethod()]
        [ExpectedException(typeof(Bankkonto.UeberzugslimiteException))]
        public void KannNichtUeberziehen()
        {
            // arrange
            Jugendkonto konto = new Jugendkonto(0);

            // act and assert
            konto.Beziehe(1.0);
        }

        [TestMethod()]
        public void KannBeziehen()
        {
            // arrange
            Jugendkonto konto = new Jugendkonto(0);
            konto.ZahleEin(1000.0);

            // act and assert
            konto.Beziehe(1.0);

            // assert
            Assert.AreEqual(999.0, konto.Guthaben, 0.01);
        }

        [TestMethod()]
        [ExpectedException(typeof(Bankkonto.BezugslimiteException))]
        public void KannNurUnterBezugslimiteBeziehen()
        {
            // arrange
            Jugendkonto konto = new Jugendkonto(0);
            konto.ZahleEin(1000.0);

            // act and assert
            konto.Beziehe(101.0);            
        }
    }
}