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
            string polynomialTest1 = "3.0 4 -8.0 0";
            bool isValid = tests.IsValidPolynomial(polynomialTest1);

            Assert.IsTrue(isValid);
        }



    }
}