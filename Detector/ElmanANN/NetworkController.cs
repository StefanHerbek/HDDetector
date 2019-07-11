using DNASequenceGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN
{
    class NetworkController
    {
        ElmanNetwork network;

        NetworkController(ElmanNetwork network)
        {
            this.network = network;
        }

        public void train(TrainCase trainCase)
        {
            int numberOfCAGRepeats = 0;
    
            for (int i=0; i < trainCase.sequence.Length; i+=3)
            {
                string trinucleotide = trainCase.sequence[i].ToString() + trainCase.sequence[i + 1] + trainCase.sequence[i + 2];
                double[] input = convertTrinucleotideToInputLayer(trinucleotide);
                double[] expectedOutput;
                if (trinucleotide.Equals("cag"))
                {
                    
                    numberOfCAGRepeats++;
                } else
                {
                    numberOfCAGRepeats = 0;
                }
                if (numberOfCAGRepeats >= 27 && numberOfCAGRepeats<=35)
                {
                    expectedOutput = new double[] { 0d, 1d, 0d };
                } else if (numberOfCAGRepeats>35 && numberOfCAGRepeats<=39)
                {
                    expectedOutput = new double[] { 0.5d, 1d, 0.5d };
                } else if (numberOfCAGRepeats>=40)
                {
                    expectedOutput = new double[] { 1d, 1d, 0d };
                } else
                {
                    expectedOutput = new double[] { 0d, 0d, 0d };
                }
                network.train(input, expectedOutput);
            }
        }
        private double[] convertTrinucleotideToInputLayer(string trinucleotide)
        {
            double[] input = new double[3];
            for (int i=0; i<3; i++)
            {
                input[i] = convertNucleotideIntoRealNumber(trinucleotide[i]);
            }
            return input;
        }

        private double convertNucleotideIntoRealNumber(char nucleotide)
        {
            int asciiNumber = nucleotide;
            double nucleotideValue = (double)asciiNumber / 100;
            return nucleotideValue;
        }
    }
}
