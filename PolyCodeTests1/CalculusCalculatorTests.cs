using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP2.Tests
{
    [TestClass()]
    public class CalculusCalculatorTests
    {
        CalculusCalculator tests = new CalculusCalculator();

        [TestMethod()]
        public void IsValidTest1()
        {
            string test1 = "3.0 4 -8.0 0";
            bool isValid = tests.IsValidPolynomial(test1);

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void IsValidTest2()
        {
            string test2 = "2x^2+1";
            bool isValid = tests.IsValidPolynomial(test2);

            Assert.IsFalse(isValid);
        }

        [TestMethod()]
        public void IsValidTest3()
        {
            string test3 = "0";
            bool isValid = tests.IsValidPolynomial(test3);

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetPolynomialStringTest1()
        {
            tests.GetPolynomialString();
            
        }

        [TestMethod]
        public void GetRootsTest1()
        {
            List<double> calculatedRoots = new List<double>;
            List<double> expectedRoots = new List<double>;

            //calculatedRoots = CalculusCalculator.GetAllRoots(0.0001);

        }

    }
}