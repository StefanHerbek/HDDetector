using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElmanANN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN.Tests
{
    [TestClass()]
    public class MathematicalFunctionTest
    {
        [TestMethod()]
        public void uniformDistributionTest()
        {
            double[] setOfNumbers = MathematicalFunction.uniformDistribution(10);
            double sum = 0.0d;
            foreach (double number in setOfNumbers)
            {
                sum += number;
            }

            if (sum > 1.0d)
            {
                Assert.Fail();
            }
        }
    }
}