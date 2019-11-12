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
        public void GetRootsTest2()
        {
            List<double> calculatedRoots = new List<double> { };
            List<double> expectedRoots = new List<double> { 0 };
            
            coefficientList = new List<double> { 1, 0, 0};

            calculusTests.SetPolynomialHelper("1 0 0", coefficientList);
            calculatedRoots = calculusTests.GetAllRoots(0.00001);

            Assert.AreEqual(calculatedRoots[0], expectedRoots[0]);

        }


        [TestMethod]
        public void EvaluateTest1()
        {

            coefficientList = new List<double> { 2, 3, 0};
            calculusTests.SetPolynomialHelper("2 3 0", coefficientList);
        
            double result = calculusTests.EvaluatePolynomial(2);
            Assert.AreEqual(result, 14);

        }

        [TestMethod]
        public void EvaluateTest2()
        {

            coefficientList = new List<double> { 1, -4, 4 };
            calculusTests.SetPolynomialHelper(" 1 -4 4", coefficientList);

            double result = calculusTests.EvaluatePolynomial(2);
            Assert.AreEqual(result, 0);

        }

        [TestMethod]
        public void IntegralTest1()
        {
            coefficientList = new List<double> { 4, 3, 0, 5 };
            calculusTests.SetPolynomialHelper(" 4 3 0 5", coefficientList);
            double integral = calculusTests.EvaluatePolynomialIntegral(0, 1);

            Assert.AreEqual(integral, 7);

        }


        [TestMethod]
        public void IntegralTest2()
        {
            coefficientList = new List<double> {0, 0, -1, 2, -4 };
            calculusTests.SetPolynomialHelper(" 0 0 -1 2 -4", coefficientList);
            double integral = calculusTests.EvaluatePolynomialIntegral(2, 4);

            Assert.AreEqual(integral, -14.67, 0.01);

        }


        [TestMethod]
        public void DerivativeTest1()
        {
            coefficientList = new List<double> { 4, 3, 0, 5 };
            calculusTests.SetPolynomialHelper(" 4 3 0 5", coefficientList);
            double derivative = calculusTests.EvaluatePolynomialDerivative(1);

            Assert.AreEqual(derivative, 18);

        }

        [TestMethod]
        public void DerivativeTest2()
        {
            coefficientList = new List<double> { 1 };
            calculusTests.SetPolynomialHelper(" 1 ", coefficientList);
            double derivative = calculusTests.EvaluatePolynomialDerivative(10000);

            Assert.AreEqual(derivative, 0);

        }

    }
}