using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN
{
    public static class MathematicalFunction
    {
        private static Random random = new Random();

        public static double sigmoid(double value)
        {
            return 1.0d / (1.0d + Math.Exp(-value));
        }

        public static double sigmoidDerivative(double value)
        {
            return value * (1.0d - value);
        }

        public static double[] uniformDistribution(int numberOfNumbers)
        {
            double[] setOfNumbers = new double[numberOfNumbers];
            double poolOfDistribution = 1.0d;
            
            for (int i =0; i < numberOfNumbers; i++)
            {
                setOfNumbers[i] = random.NextDouble() % poolOfDistribution;
                //COMMENT HERE
                int positivity = random.Next(-1, 1);
                if (positivity < 0)
                {
                    setOfNumbers[i] = -setOfNumbers[i];
                }
                poolOfDistribution = poolOfDistribution - setOfNumbers[i];
               
            }
            return setOfNumbers;

        }
    }
}
