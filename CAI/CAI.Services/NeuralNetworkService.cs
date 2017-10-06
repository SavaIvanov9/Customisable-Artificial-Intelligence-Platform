namespace CAI.Services
{
    using AForge.Neuro;
    using AForge.Neuro.Learning;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class NeuralNetworkService
    {
        private double _sigmoidAlphaValue = 2;

        public void Start()
        {
            double[][] input = new double[][] {
                new double[] { 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                new double[] { 0, 0, 0, 1, 1, 1, 0, 0, 0 },

                new double[] { 1, 0, 0, 0, 0, 0, 1, 1, 0 },
                new double[] { 0, 1, 1, 0, 0, 0, 0, 1, 1 },
            };

            double[][] output = new double[][] {
                new double[] {1, 0},
                new double[] {1, 0},
                new double[] {0, 1},
                new double[] {0, 1}
            };

            var st = new Stopwatch();
            st.Start();

            var network = this.GenerateNetwork(input[0].Length, output[0].Length);
            Console.ReadLine();
            this.TrainNetwork(network, input, output, 0.0001);

            st.Stop();
            Console.WriteLine(st.Elapsed);

            this.TestNN(network, input);
        }

        public ActivationNetwork GenerateNetwork(int inputLayer, int outputLayer)
        {
            //ActivationNetwork network = new ActivationNetwork(
            //    new SigmoidFunction(this._sigmoidAlphaValue),
            //    inputLayer,
            //    5,
            //    3,
            //    //(int) Math.Sqrt(inputLayer + outputLayer),
            //    //(inputLayer + outputLayer) / 2,
            //    //(inputLayer + outputLayer) / 2,
            //    //(inputLayer + outputLayer) / 2,
            //    outputLayer);

            var structure = this.GenerateNetworkStructure(inputLayer, outputLayer);

            Console.WriteLine($"Structure: {inputLayer}/{string.Join("/", structure)}");

            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(this._sigmoidAlphaValue),
                inputLayer,
                structure.ToArray());

            return network;
        }

        public void TrainNetwork(ActivationNetwork network,
            double[][] input, double[][] output, double errorRate)
        {
            BackPropagationLearning teacher = new BackPropagationLearning(network);

            bool isTraining = true;
            while (isTraining)
            {
                double error = teacher.RunEpoch(input, output);
                Console.WriteLine($"Error: {error}");

                if (error < errorRate)
                //if (Math.Abs(error - 0.1) < 0.001)
                {
                    isTraining = false;
                }
            }

            Console.WriteLine($"Error rate: {errorRate}");
            Console.WriteLine("----------------------------");
        }

        public void TestNN(Network network, double[][] input)
        {
            foreach (var i in input)
            {
                var result = network.Compute(i);

                Console.WriteLine(string.Join(" / ", result));
            }
        }

        private IEnumerable<int> GenerateNetworkStructure(int inputLayer, int outputLayer)
        {
            //var fib = new List<int> { 1, 1, 2, 3 };

            //for (int i = 3; i < outputLayer * 2; i++)
            //{
            //    fib.Add(fib[i - 1] + fib[i]);
            //}

            ////var firstLayer = fib.FirstOrDefault();
            ////var index1 = fib.FindIndex(x => x > inputLayer);
            ////var index2 = fib.FindIndex(x => x > outputLayer);

            //var index1 = fib.FirstOrDefault(x => x > inputLayer);
            //var index2 = fib.FirstOrDefault(x => x > outputLayer);

            //Console.WriteLine($"{index1} - ");
            //Console.WriteLine($"{index2} - ");

            //var structure = fib.GetRange(index1, index2 - index1);
            //structure.Add(outputLayer);

            var structure = new[] { (inputLayer + outputLayer) / 2, outputLayer };

            return structure;
        }

        private void TestXOR()
        {
            //var nn = new NeuralNetworkSystem();

            // initialize input and output values
            double[][] input = new double[4][] {
                new double[] {0, 0},
                new double[] {0, 1},
                new double[] {1, 0},
                new double[] {1, 1}
            };
            double[][] output = new double[4][] {
                new double[] {0},
                new double[] {1},
                new double[] {1},
                new double[] {0}
            };

            // create neural network
            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(2),
                2, // two inputs in the network
                2, // two neurons in the first layer
                1); // one neuron in the second layer

            // create teacher
            BackPropagationLearning teacher = new BackPropagationLearning(network);

            bool isTraining = true;
            while (isTraining)
            {
                // run epoch of learning procedure
                double error = teacher.RunEpoch(input, output);
                //teacher.Run(input, output);
                // check error value to see if we need to stop
                // ...
                Console.WriteLine($"Error: {error}");

                if (error < 0.0001)
                //if (Math.Abs(error - 0.1) < 0.001)
                {
                    isTraining = false;
                }
            }

            foreach (var i in input)
            {
                var result = network.Compute(i);
                Console.WriteLine(result[0]);
            }
        }
    }
}
