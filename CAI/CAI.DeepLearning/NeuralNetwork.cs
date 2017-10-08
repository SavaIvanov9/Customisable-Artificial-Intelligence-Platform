namespace CAI.DeepLearning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using AForge.Neuro;
    using AForge.Neuro.Learning;

    public class NeuralNetwork
    {
        private readonly Network _network;
        private readonly double _sigmoidAlphaValue;
        private readonly Stopwatch _stopwatch;
        private bool _isLogging;

        public NeuralNetwork(int inputLayer, int outputLayer, bool isLogging = false)
        {
            this._sigmoidAlphaValue = 2;
            this._isLogging = isLogging;
            this._stopwatch = new Stopwatch();
            this._network = this.GenerateNetwork(inputLayer, outputLayer);
        }

        public NeuralNetwork(MemoryStream stream, bool isLogging = false)
        {
            this._sigmoidAlphaValue = 2;
            this._isLogging = isLogging;
            this._stopwatch = new Stopwatch();
            this._network = this.LoadNetwork(stream);
        }

        public bool IsLogging
        {
            get => this._isLogging;
            set => this._isLogging = value;
        }

        public double[] Compute(double[] input)
        {
            return this._network.Compute(input);
        }

        public void TrainNetwork(double[][] input, double[][] output, double errorRate = 0.01)
        {
            this.Log("Started learning...");
            this.StartStopwatch();

            BackPropagationLearning teacher = new BackPropagationLearning((ActivationNetwork) this._network);

            bool isTraining = true;
            while (isTraining)
            {
                double error = teacher.RunEpoch(input, output);
                this.Log($"Learning rate: {error}");

                if (error < errorRate)
                {
                    isTraining = false;
                }
            }

            this.Log($"Target rate: {errorRate}");
            this.Log("----------------------------");
            this.StopStopwatch();
        }

        public byte[] GetNetworkBytes()
        {
            byte[] bytes;

            using (var stream = new MemoryStream())
            {
                this._network.Save(stream);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public void TestNetwork(double[][] input)
        {
            foreach (var i in input)
            {
                var result = this._network.Compute(i);

                this.Log(string.Join(" / ", result));
            }
        }

        private Network LoadNetwork(MemoryStream stream)
        {
            return Network.Load(stream);
        }

        private ActivationNetwork GenerateNetwork(int inputLayer, int outputLayer)
        {
            var structure = this.GenerateNetworkStructure(inputLayer, outputLayer);

            this.Log($"Structure: {inputLayer}/{string.Join("/", structure)}");

            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(this._sigmoidAlphaValue),
                inputLayer,
                structure.ToArray());

            return network;
        }

        private IEnumerable<int> GenerateNetworkStructure(int inputLayer, int outputLayer)
        {
            var structure = new[] { (inputLayer + outputLayer) / 2, outputLayer };

            return structure;
        }

        private void StartStopwatch()
        {
            if (this._isLogging)
            {
                this._stopwatch.Stop();
                this._stopwatch.Reset();
                this._stopwatch.Start();
            }
        }

        private void StopStopwatch()
        {
            if (this._isLogging)
            {
                this._stopwatch.Stop();
                this.Log($"Time elapsed: {this._stopwatch.Elapsed}");
            }
        }

        private void Log(string msg)
        {
            if (this._isLogging)
            {
                Console.WriteLine(msg);
            }
        }
    }
}
