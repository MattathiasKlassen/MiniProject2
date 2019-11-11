using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP2;


namespace MP2.Tests
{
    [TestClass()]
    class FirstFourMethodsTests
    {
        [TestMethod()]
        void IsPolynomialTest1
        {
            string polynomial = " 3 5.5 9 -1001";

            Assert.AreEqual(IsValidPolynomial(polynomial));
        }


    }
}
