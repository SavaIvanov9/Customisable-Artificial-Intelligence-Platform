namespace CAI.Services
{
    using DeepLearning;
    using System;

    public class NeuralNetworkService
    {
        public NeuralNetwork GenerateNetwork(int inputLayer, int outputLayer, bool isLogging = false)
        {
            return new NeuralNetwork(inputLayer, outputLayer, isLogging);
        }

        public void TrainNetwork(NeuralNetwork network, double[][] input, double[][] output,
            double errorRate)
        {
            network.TrainNetwork(input, output, errorRate);
        }

        public void TestNetwork(NeuralNetwork network, double[][] inputs, double[][] outputs)
        {
            for (var i = 0; i < inputs.Length; i++)
            {
                var result = network.Compute(inputs[i]);

                Console.WriteLine($"{string.Join(" / ", result)} - Target: {string.Join(" / ", outputs[i])}");
            }
        }
    }
}
