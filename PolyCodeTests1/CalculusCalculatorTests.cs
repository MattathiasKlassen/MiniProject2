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
        CalculusCalculator calculusTests = new CalculusCalculator();

        string polynomialtest = string.Empty;
        List<double> coefficientList = new List<double>();

       
        [TestMethod()]
        public void IsValidTest1()
        {
            string test1 = "3.0 4 -8.0 0";
            bool isValid = calculusTests.IsValidPolynomial(test1);

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void IsValidTest2()
        {
            string test2 = "2x^2+1";
            bool isValid = calculusTests.IsValidPolynomial(test2);

            Assert.IsFalse(isValid);
        }

        [TestMethod()]
        public void IsValidTest3()
        {
            string test3 = "0";
            bool isValid = calculusTests.IsValidPolynomial(test3);

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void IsValidTest4()
        {
            string test4 = "1 b c";
            bool isValid = calculusTests.IsValidPolynomial(test4);

            Assert.IsFalse(isValid);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetPolynomialStringTest1()
        {
            calculusTests.GetPolynomialString();
            
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetRootsTest1()
        {
            List<double> calculatedRoots = new List<double> { };
            calculatedRoots = calculusTests.GetAllRoots(0.0001);

        }

        
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void GetRootsTest2()
        {
            List<double> calculatedRoots = new List<double> { };
            List<double> expectedRoots = new List<double> { -0.5, 0.5 };

            polynomialtest = "1 0 0";

            calculatedRoots = calculusTests.GetAllRoots(0.0001);

            Assert.AreEqual(calculatedRoots, expectedRoots);

        }
        

    }
}