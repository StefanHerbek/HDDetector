using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmanANN.ObjectOrientedElman
{
    public sealed class ObjectOrientedElmanNetwork : ElmanNetwork
    {
        //SINGLETON
        private static readonly ObjectOrientedElmanNetwork network = new ObjectOrientedElmanNetwork();

        private bool initialized = false;

        private int inputLength;
        private int numberOfHiddenOrContextNeurons;
        private int numberOfOutputNeurons;
        private double[] inputVectorForHiddenLayer;

        private double learningRate;

        private ContextNeurone[] contextLayer;
        private HiddenNeurone[] hiddenLayer;
        private OutputNeurone[] outputLayer;
        
        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        private double[] HiddenLayerOutputSignals
        {
            get
            {
                double[] hiddenLayerOutputSignals = new double[numberOfHiddenOrContextNeurons];
                for (int i=0; i < numberOfHiddenOrContextNeurons; i++)
                {
                    hiddenLayerOutputSignals[i] = hiddenLayer[i].OutputSignal;
                }
                return hiddenLayerOutputSignals;
            }
        }

        private double[] OutputLayerOutputSignals
        {
            get
            {
                double[] outputLayerOutputSignals = new double[numberOfOutputNeurons];
                for (int i=0; i < numberOfOutputNeurons; i++)
                {
                    outputLayerOutputSignals[i] = outputLayer[i].OutputSignal;
                }
                return outputLayerOutputSignals;
            }
        }

        static ObjectOrientedElmanNetwork() {}


        public static ObjectOrientedElmanNetwork NetworkInstance
        {
            get
            {
                return network;
            }
        }

        public bool isInitialized()
        {
            return initialized;
        }

        public void initialize(int inputLength, int numberOfOutputNeurons, int numberOfHiddenNeurons, double learningRate)
        {
            this.inputLength = inputLength;
            this.numberOfOutputNeurons = numberOfOutputNeurons;
            numberOfHiddenOrContextNeurons = numberOfHiddenNeurons;
            this.learningRate = learningRate;
      
            createLayers();
            initialized = true;
        }

        public void reset()
        {
        inputLength = 0;
        numberOfHiddenOrContextNeurons = 0;
        numberOfOutputNeurons = 0;
        inputVectorForHiddenLayer = null;
        learningRate = 0d;
        contextLayer = null;
        hiddenLayer = null;
        outputLayer = null;

        initialized = false;
        }

        public void setLearningRate(double newLearningRate)
        {
            learningRate = newLearningRate;
        }

        public double[] test(double[] inputSignals, double[] expectedOutput)
        {
            throw new NotImplementedException();
        }

        public void train(double[] inputSignals, double[] expectedOutput)
        {
            feedForward(inputSignals);
            calculateOutputLayerErrorRates(expectedOutput);
            calculateHiddenLayerErrorRates();
            updateOutputLayerWeights();
            updateHiddenLayerWeights();
        }

        public double[] use(double[] inputSignals)
        {
            if (inputSignals.Length != inputLength)
            {
                //THROW EXCEPTION
            }
            feedForward(inputSignals);
            return OutputLayerOutputSignals;
        }

        private void createLayers()
        {
            hiddenLayer = new HiddenNeurone[numberOfHiddenOrContextNeurons];
            for (int i = 0; i < numberOfHiddenOrContextNeurons; i++)
            {
                double[] weights = MathematicalFunction.uniformDistribution(inputLength + numberOfHiddenOrContextNeurons);
                hiddenLayer[i] = new HiddenNeurone(weights);
            }

            contextLayer = new ContextNeurone[numberOfHiddenOrContextNeurons];
            for (int i = 0; i < numberOfHiddenOrContextNeurons; i++)
            {
                contextLayer[i] = new ContextNeurone();
            }

            outputLayer = new OutputNeurone[numberOfOutputNeurons];
            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                double[] weights = MathematicalFunction.uniformDistribution(numberOfHiddenOrContextNeurons);
                outputLayer[i] = new OutputNeurone(weights);
            }
        }

        private void feedForward(double[] inputSignals)
        {
            createInputVectorForHiddenLayer(inputSignals);
            propagateSignalsToHiddenLayer();
            propagateSignalsToContextLayer();
            propagateSignalsToOutputLayer();
        }

        private void createInputVectorForHiddenLayer(double[] inputSignals)
        {
            inputVectorForHiddenLayer = new double[inputLength + numberOfHiddenOrContextNeurons];
            int inputVectorIndex = 0;

            for (int i = 0; i < inputLength; i++)
            {
                inputVectorForHiddenLayer[inputVectorIndex] = inputSignals[i];
                inputVectorIndex++;
            }

            for (int j = 0; j < numberOfHiddenOrContextNeurons; j++)
            {
                inputVectorForHiddenLayer[inputVectorIndex] = contextLayer[j].OutputSignal;
                inputVectorIndex++;
            }
        }

        private void propagateSignalsToHiddenLayer()
        {
            foreach (HiddenNeurone hiddenNeurone in hiddenLayer)
            {
                hiddenNeurone.inputSignal(inputVectorForHiddenLayer);
            }
        }

        private void propagateSignalsToContextLayer()
        {
            for (int i=0; i < numberOfHiddenOrContextNeurons; i++)
            {
                double[] hiddenNeuroneCopy = new double[] { HiddenLayerOutputSignals[i] };
                contextLayer[i].inputSignal(hiddenNeuroneCopy);
            }
        }

        private void propagateSignalsToOutputLayer()
        {
            
            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                outputLayer[i].inputSignal(HiddenLayerOutputSignals);
            }
        }

        private void calculateOutputLayerErrorRates(double[] expectedOutput)
        {
            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                outputLayer[i].calculateErrorRate(expectedOutput[i]);
            }
        }

        private void calculateHiddenLayerErrorRates()
        {
            for (int i = 0; i < numberOfHiddenOrContextNeurons; i++)
            {
                hiddenLayer[i].ErrorRate = 0d;
                for (int j=0; j < numberOfOutputNeurons; j++)
                {
                    hiddenLayer[i].ErrorRate = hiddenLayer[i].ErrorRate + outputLayer[j].ErrorRate * outputLayer[j].Weights[i];
                }
                hiddenLayer[i].ErrorRate = hiddenLayer[i].ErrorRate * MathematicalFunction.sigmoidDerivative(hiddenLayer[i].OutputSignal);
            }
        }

        private void updateOutputLayerWeights()
        {
            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                for (int j = 0; j < numberOfHiddenOrContextNeurons; j++)
                {
                    outputLayer[i].Weights[j] += learningRate * outputLayer[i].ErrorRate * hiddenLayer[j].OutputSignal;
                }
            }
        }

        private void updateHiddenLayerWeights()
        {
            for (int i = 0; i < numberOfHiddenOrContextNeurons; i++)
            {
                for (int j = 0; j < inputLength + numberOfHiddenOrContextNeurons; j++)
                {
                    hiddenLayer[i].Weights[j] += learningRate * hiddenLayer[i].ErrorRate * inputVectorForHiddenLayer[j];
                }
            }
        }

    }
}
