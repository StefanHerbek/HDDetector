using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN.ObjectOrientedElman
{
    class OutputNeurone : Neurone
    {
        public OutputNeurone(double[] weights) : base(weights) {}

        protected override void calculateOutputSignal()
        {
            outputSignal = MathematicalFunction.sigmoid(state);
        }
    }
}
