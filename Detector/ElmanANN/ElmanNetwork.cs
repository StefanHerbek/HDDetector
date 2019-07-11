using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN
{
    interface ElmanNetwork
    {
        bool isInitialized();
        void initialize(int inputLength, int numberOfOutputNeurons, int numberOfHiddenNeurons, double learningRate);
        void setLearningRate(double newLearningRate);
        void train(double[] inputSignals, double[] expectedOutput);
        double[] test(double[] inputSignals, double[] expectedOutput);
        double[] use(double[] inputSignals);
        void reset();
    }
}
