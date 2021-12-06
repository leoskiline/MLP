using System;
using System.Collections.Generic;

namespace MLP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double[]> entradas = new List<double[]>();
            List<double[]> saidas = new List<double[]>();
            double taxaAprendizado = 0.3;
            double limiar = 0.01;
            int epocas = 10000;
            for (int i = 0; i < 4; i++)
            {
                entradas.Add(new double[2]);
                saidas.Add(new double[1]);
            }
            Console.WriteLine("MLP\nEscolha a Entrada Desejada:\n1. AND\n2. OR \n3. XOR\n4. XNOR\n");
            string escolha = Console.ReadLine();
            if(escolha == "1")
            {
                entradas[0][0] = 0; entradas[0][1] = 0; saidas[0][0] = 0;
                entradas[1][0] = 0; entradas[1][1] = 1; saidas[1][0] = 0;
                entradas[2][0] = 1; entradas[2][1] = 0; saidas[2][0] = 0;
                entradas[3][0] = 1; entradas[3][1] = 1; saidas[3][0] = 1;
            }
            else if(escolha == "2")
            {
                entradas[0][0] = 0; entradas[0][1] = 0; saidas[0][0] = 0;
                entradas[1][0] = 0; entradas[1][1] = 1; saidas[1][0] = 1;
                entradas[2][0] = 1; entradas[2][1] = 0; saidas[2][0] = 1;
                entradas[3][0] = 1; entradas[3][1] = 1; saidas[3][0] = 1;
            }else if(escolha == "3")
            {
                entradas[0][0] = 0; entradas[0][1] = 0; saidas[0][0] = 0;
                entradas[1][0] = 0; entradas[1][1] = 1; saidas[1][0] = 1;
                entradas[2][0] = 1; entradas[2][1] = 0; saidas[2][0] = 1;
                entradas[3][0] = 1; entradas[3][1] = 1; saidas[3][0] = 0;
            }
            else if (escolha == "4")
            {
                entradas[0][0] = 0; entradas[0][1] = 0; saidas[0][0] = 1;
                entradas[1][0] = 0; entradas[1][1] = 1; saidas[1][0] = 0;
                entradas[2][0] = 1; entradas[2][1] = 0; saidas[2][0] = 0;
                entradas[3][0] = 1; entradas[3][1] = 1; saidas[3][0] = 1;
            }

            Perceptron p = new Perceptron(new int[] { entradas[0].Length, 3, saidas[0].Length });
            p.Learn(entradas, saidas, taxaAprendizado,limiar,epocas);
            Console.WriteLine("Classificar");
            while (true)
            {
                Console.WriteLine("Insira a primeira entrada");
                double d = double.Parse(Console.ReadLine());
                Console.WriteLine("Insira a segunda entrada");
                double d2 = double.Parse(Console.ReadLine());

                Console.WriteLine("Classificação: {0}", p.Activate(new double[] { d, d2 })[0]);
            }
        }
    }

    class Perceptron
    {
        List<Layer> layers;

        public Perceptron(int[] neuronsPerlayer)
        {
            layers = new List<Layer>();
            Random r = new Random();

            for (int i = 0; i < neuronsPerlayer.Length; i++)
            {
                layers.Add(new Layer(neuronsPerlayer[i], i == 0 ? neuronsPerlayer[i] : neuronsPerlayer[i - 1], r));
            }
        }
        public double[] Activate(double[] inputs)
        {
            double[] outputs = new double[0];
            for (int i = 0; i < layers.Count; i++)
            {
                outputs = layers[i].Activate(inputs);
                inputs = outputs;
            }
            return outputs;
        }
        double IndividualError(double[] realOutput, double[] desiredOutput)
        {
            double err = 0;
            for (int i = 0; i < realOutput.Length; i++)
            {
                err += Math.Pow(realOutput[i] - desiredOutput[i], 2);
            }
            return err;
        }
        double GeneralError(List<double[]> input, List<double[]> desiredOutput)
        {
            double err = 0;
            for (int i = 0; i < input.Count; i++)
            {
                err += IndividualError(Activate(input[i]), desiredOutput[i]);
            }
            return err;
        }
        public void Learn(List<double[]> input, List<double[]> desiredOutput, double aprendizado, double limiar,int epocas)
        {
            double err = 99999;
            int i = 0;
            while (err > limiar && i < epocas)
            {
                ApplyBackPropagation(input, desiredOutput, aprendizado);
                err = GeneralError(input, desiredOutput);
                Console.WriteLine(err);
            }
        }

        List<double[]> sigmas;
        List<double[,]> deltas;

        void SetSigmas(double[] desiredOutput)
        {
            sigmas = new List<double[]>();
            for (int i = 0; i < layers.Count; i++)
            {
                sigmas.Add(new double[layers[i].numberOfNeurons]);
            }
            for (int i = layers.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < layers[i].numberOfNeurons; j++)
                {
                    if (i == layers.Count - 1)
                    {
                        double y = layers[i].neurons[j].lastActivation;
                        sigmas[i][j] = (Neuron.Sigmoid(y) - desiredOutput[j]) * Neuron.SigmoidDerivated(y);
                    }
                    else
                    {
                        double sum = 0;
                        for (int k = 0; k < layers[i + 1].numberOfNeurons; k++)
                        {
                            sum += layers[i + 1].neurons[k].weights[j] * sigmas[i + 1][k];
                        }
                        sigmas[i][j] = Neuron.SigmoidDerivated(layers[i].neurons[j].lastActivation) * sum;
                    }
                }
            }
        }
        void SetDeltas()
        {
            deltas = new List<double[,]>();
            for (int i = 0; i < layers.Count; i++)
            {
                deltas.Add(new double[layers[i].numberOfNeurons, layers[i].neurons[0].weights.Length]);
            }
        }
        void AddDelta()
        {
            for (int i = 1; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].numberOfNeurons; j++)
                {
                    for (int k = 0; k < layers[i].neurons[j].weights.Length; k++)
                    {
                        deltas[i][j, k] += sigmas[i][j] * Neuron.Sigmoid(layers[i - 1].neurons[k].lastActivation);
                    }
                }
            }
        }
        void UpdateBias(double alpha)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].numberOfNeurons; j++)
                {
                    layers[i].neurons[j].bias -= alpha * sigmas[i][j];
                }
            }
        }
        void UpdateWeights(double alpha)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].numberOfNeurons; j++)
                {
                    for (int k = 0; k < layers[i].neurons[j].weights.Length; k++)
                    {
                        layers[i].neurons[j].weights[k] -= alpha * deltas[i][j, k];
                    }
                }
            }
        }
        void ApplyBackPropagation(List<double[]> input, List<double[]> desiredOutput, double alpha)
        {
            SetDeltas();
            for (int i = 0; i < input.Count; i++)
            {
                Activate(input[i]);
                SetSigmas(desiredOutput[i]);
                UpdateBias(alpha);
                AddDelta();
            }
            UpdateWeights(alpha);

        }
    }
    class Layer
    {
        public List<Neuron> neurons;
        public int numberOfNeurons;
        public double[] output;

        public Layer(int _numberOfNeurons, int numberOfInputs, Random r)
        {
            numberOfNeurons = _numberOfNeurons;
            neurons = new List<Neuron>();
            for (int i = 0; i < numberOfNeurons; i++)
            {
                neurons.Add(new Neuron(numberOfInputs, r));
            }
        }

        public double[] Activate(double[] inputs)
        {
            List<double> outputs = new List<double>();
            for (int i = 0; i < numberOfNeurons; i++)
            {
                outputs.Add(neurons[i].Activate(inputs));
            }
            output = outputs.ToArray();
            return outputs.ToArray();
        }

    }
    class Neuron
    {
        public double[] weights;
        public double lastActivation;
        public double bias;

        public Neuron(int numberOfInputs, Random r)
        {
            bias = 10 * r.NextDouble() - 5;
            weights = new double[numberOfInputs];
            for (int i = 0; i < numberOfInputs; i++)
            {
                weights[i] = 10 * r.NextDouble() - 5;
            }
        }
        public double Activate(double[] inputs)
        {
            double activation = bias;

            for (int i = 0; i < weights.Length; i++)
            {
                activation += weights[i] * inputs[i];
            }

            lastActivation = activation;
            return Sigmoid(activation);
        }
        public static double Sigmoid(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }
        public static double SigmoidDerivated(double input)
        {
            double y = Sigmoid(input);
            return y * (1 - y);
        }

    }
}
