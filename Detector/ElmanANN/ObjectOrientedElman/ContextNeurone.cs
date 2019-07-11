using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN.ObjectOrientedElman
{
    class ContextNeurone : Neurone
    {
        public ContextNeurone() : base()
        {
            weights = new double[1] { 1.0d };
            numberOfConnections = 1;
        }
        //WHY YOU DON'T IMPLEMENT THIS METHODS
        public override void calculateErrorRate(double expected) {}

        protected override void calculateOutputSignal()
        {
            outputSignal = state;
        }

    }
}
