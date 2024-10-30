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
        const double TestAktivZins = 0.015;
        class TestBankkonto : Bankkonto
        {
            override public double AktivZins { get; } = TestAktivZins;
            override public double PassivZins { get; } = 0.015;
            public override double Ueberzugslimite { get; } = -1000.0;
            public TestBankkonto() : base(0) { }

            public double AktivzinsDepotSpy { get => AktivzinsGuthaben; }
            public double PassivzinsDepotSpy { get => PassivzinsGuthaben; }
        }

        [TestMethod()]
        public void AnfangsGuthabenIstNull()
        {
            // arrange
            Bankkonto leeresBankkonto = new TestBankkonto();

            // act
            // assert
            Assert.AreEqual(0, leeresBankkonto.Guthaben, 0);
        }

        [TestMethod()]
        public void KannEinzahlen()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();

            // act
            konto.ZahleEin(100.0);

            // assert
            Assert.AreEqual(100.0, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void KannBeziehen()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();

            // act
            konto.Beziehe(100.0);

            // assert
            Assert.AreEqual(-100.0, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void KeinZinsAufLeeremKonto()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();

            // act
            konto.SchreibeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(0.0, konto.Guthaben, 0.001);

        }

        [TestMethod()]
        public void EinTagZinsWirdGutgeschriebenAberNichtGebucht()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchreibeZinsGut(1);

            // assert
            Assert.AreEqual(AnfangsBetrag, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void EinTagAktivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchreibeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(AnfangsBetrag + (AnfangsBetrag * konto.AktivZins) / 360, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void ZweiTageAktivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchreibeZinsGut(2);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(AnfangsBetrag + 2 * AnfangsBetrag * konto.AktivZins / 360, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void ZweimalEinTagAktivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();
            const double AnfangsBetrag = 100.0;

            // act
            konto.ZahleEin(AnfangsBetrag);
            konto.SchreibeZinsGut(1);
            konto.SchreibeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            Assert.AreEqual(AnfangsBetrag + 2 * AnfangsBetrag * konto.AktivZins / 360, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void EinTagPassivZinsWirdGutgeschriebenUndBeiAbschlussGebucht()
        {
            // arrange
            Bankkonto konto = new TestBankkonto();
            const double TestBetrag = 100.0;

            // act
            konto.Beziehe(TestBetrag);
            konto.SchreibeZinsGut(1);
            konto.SchliesseKontoAb();

            // assert
            double ErwarteterBetrag = - TestBetrag - (TestBetrag * konto.PassivZins / 360);
            Assert.AreEqual(ErwarteterBetrag, konto.Guthaben, 0.001);
        }

        [TestMethod()]
        public void SollUndHabezinsWerdenSeparatVerrechnet()
        {
            // arrange
            TestBankkonto konto = new TestBankkonto();
            const double TestBetrag = 1.0;

            // act
            konto.ZahleEin(TestBetrag);
            konto.SchreibeZinsGut(1);
            konto.Beziehe(2 * TestBetrag);
            konto.SchreibeZinsGut(1);

            // assert
            Assert.AreEqual(TestBetrag * konto.AktivZins / 360.0, konto.AktivzinsDepotSpy);
            Assert.AreEqual(-TestBetrag * konto.PassivZins / 360.0, konto.PassivzinsDepotSpy);
        }

        [TestMethod()]
        [DataRow(5000, TestAktivZins)]
        [DataRow(30000, TestAktivZins - 0.005)]
        [DataRow(70000, TestAktivZins - 0.0075)]
        [DataRow(120000, TestAktivZins - 0.01)]
        public void ZinsIstAbhaengigVomKontostand(double Guthaben, double ErwarteterZins)
        {
            // arrange
            TestBankkonto konto = new TestBankkonto();

            // act
            konto.ZahleEin(Guthaben);
            konto.SchreibeZinsGut(1);

            // assert
            double zins = konto.AktivzinsDepotSpy / Guthaben * 360.0;
            Assert.AreEqual(ErwarteterZins, zins, 0.000001);
        }
    }
}