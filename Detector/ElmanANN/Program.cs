using ElmanANN.ObjectOrientedElman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectOrientedElmanNetwork.NetworkInstance.initialize(3, 3, 3, 1.0);
            double[] input = new double[] { -0.3, 0.2, 0.8 };
            double[] input2 = new double[] { -0.3, 0.2, 0.8 };
            ObjectOrientedElmanNetwork.NetworkInstance.use(input);
            double[] output = ObjectOrientedElmanNetwork.NetworkInstance.use(input2);
            for (int i=0; i < output.Length; i++)
            {
                Console.WriteLine(output[i].ToString());
            }
        }
    }
}
