using System;
using RatioLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RatioLibrary.Test {
    [TestClass]
    public class UnitTest1 {
        private Ratio R1, R2;
        [TestMethod]
        public void Test_CreateRatio() {
            R1 = new Ratio(1, 2);
            Assert.AreEqual(0.5, R1.ToDouble());
        }
        [TestMethod]
        public void Test_ZeroDenominationException_atCreation() =>
            Assert.ThrowsException<DenominatorZeroException>(() => new Ratio(1, 0));

        [TestMethod]
        public void Test_ZeroDenominationException_atCalculate() =>
            Assert.ThrowsException<DenominatorZeroException>(() => 
                { var result = new Ratio(1, 2) / new Ratio(0, 3);
            });
        [TestMethod]
        public void Test_CorrectOfReduction() {
            R1 = new Ratio(5, 10);
            Assert.AreEqual(new Ratio(1, 2).ToString(), R1.ToString());
        }

        [TestMethod]
        public void Test_CorrectOfAddition() {
            R1 = new Ratio(1, 2);
            R2 = new Ratio(1, 3);

            Assert.AreEqual(new Ratio(5, 6).ToDouble(), (R1 + R2).ToDouble());
        }

        [TestMethod]
        public void Test_CorrectOfDeduction() {
            R1 = new Ratio(1, 2);
            R2 = new Ratio(1, 3);

            Assert.AreEqual(new Ratio(1, 6).ToDouble(), (R1 - R2).ToDouble());
        }

        [TestMethod]
        public void Test_CorrectOfMultiply() {
            R1 = new Ratio(1, 2);
            R2 = new Ratio(1, 3);

            Assert.AreEqual(new Ratio(1, 6).ToDouble(), (R1 * R2).ToDouble());
        }

        [TestMethod]
        public void Test_CorrectOfDivision() {
            R1 = new Ratio(1, 2);
            R2 = new Ratio(1, 3);

            Assert.AreEqual(new Ratio(3, 2).ToDouble(), (R1 / R2).ToDouble());
        }
    }
}
