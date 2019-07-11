using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN.ObjectOrientedElman
{
    abstract class Neurone
    {
        protected double state;
        protected double[] weights;
        protected double outputSignal;
        protected double errorRate;
        protected int numberOfConnections;

        public double State
        {
            get { return state; }
        }

        public double[] Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        public double OutputSignal
        {
            get { return outputSignal; }
        }

        public double ErrorRate
        {
            get { return errorRate; }
            set { errorRate = value; }
        }

        public Neurone()
        {
            resetValues();
        }

        public Neurone(double[] weights)
        {
            resetValues();
            this.weights = weights;
            numberOfConnections = weights.Length;
        }

        public virtual void inputSignal(double[] inputSignals)
        {
            if (inputSignals.Length != numberOfConnections)
            {
                //throw Exception
            }
            state = 0d;
            for (int i=0; i < numberOfConnections; i++)
            {
                state = state + (inputSignals[i] * weights[i]);
            }

            calculateOutputSignal();
        }

        public virtual void calculateErrorRate(double expected)
        {
            errorRate = state - expected;
        }

        protected virtual void calculateOutputSignal() {}

        private void resetValues()
        {
            state = 0d;
            weights = new double[] {};
            errorRate = 0d;
            outputSignal = 0d;
            errorRate = 0d;
            numberOfConnections = 0;
        }

    }
}
