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
    public class BankkontoTests
    {
        [TestMethod()]
        public void AnfangsGuthabenIstNull()
        {
            // arrange
            Bankkonto leeresBankkonto = new Bankkonto(0);

            // act
            // assert
            Assert.AreEqual(0, leeresBankkonto.Guthaben, 0);
        }

        [TestMethod()]
        public void KannEinzahlen()
        {
            // arrange
            Bankkonto konto = new Bankkonto(0);

            // act
            konto.ZahleEin(100.0);

            // assert
            Assert.AreEqual(100.0, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void KannBeziehen()
        {
            // arrange
            Bankkonto konto = new(0);

            // act
            konto.Beziehe(100.0);

            // assert
            Assert.AreEqual(-100.0, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void KeinZinsAufLeeremKOnto()
        {
            // arrange
            Bankkonto konto = new(0);

            // act
            konto.SchribeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(0.0, konto.Guthaben, 0.001);

        }

        [TestMethod()]
        public void EinTagZinsWirdGutgeschriebenAberNichtGebucht()
        {
            // arrange
            Bankkonto konto = new(0);
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchribeZinsGut(1);

            // assert
            Assert.AreEqual(AnfangsBetrag, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void EinTagAktivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new(0);
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchribeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(AnfangsBetrag + (AnfangsBetrag * Bankkonto.AktivZins) / 360, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void ZweiTageAktivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new(0);
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchribeZinsGut(2);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(AnfangsBetrag + 2 * AnfangsBetrag * Bankkonto.AktivZins / 360, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void ZweimalEinTagAktivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new(0);
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchribeZinsGut(1);
            konto.SchribeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(AnfangsBetrag + 2 * AnfangsBetrag * Bankkonto.AktivZins / 360, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void EinTagPassivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new(0);
            const double TestBetrag = 100.0;

            // act
            konto.Beziehe(TestBetrag);
            konto.SchribeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            double ErwarteterBetrag = - TestBetrag - (TestBetrag * Bankkonto.PassivZins / 360);
            Assert.AreEqual(ErwarteterBetrag, konto.Guthaben, 0.001);
        }
    }
}