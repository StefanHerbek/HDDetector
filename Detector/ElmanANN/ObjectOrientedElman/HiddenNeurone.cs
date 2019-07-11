using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN.ObjectOrientedElman
{
    class HiddenNeurone : Neurone
    {
        public HiddenNeurone(double[] weights) : base(weights) {}

        protected override void calculateOutputSignal()
        {
            outputSignal = MathematicalFunction.sigmoid(state);
        }

    }
}
