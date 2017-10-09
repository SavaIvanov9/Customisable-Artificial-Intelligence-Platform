namespace CAI.Services.Abstraction
{
    using Common.Enums;
    using Data.Models;
    using DeepLearning;
    using System;

    public interface INeuralNetworkService : IDisposable
    {
        NeuralNetworkData GenerateIntentionRecognitionNeuralNetworkData(int inputLayer, int outputLayer);

        NeuralNetwork GenerateNetwork(int inputLayer, int outputLayer, bool isLogging = false);

        bool TrainNetwork(NeuralNetwork network, double[][] input, double[][] output, double errorRate = 0.01);

        long RegisterNewNetwork(NeuralNetwork network, long botId, string createdBy, NeuralNetworkType networkType);

        NeuralNetwork LoadNeuralNetwork(long id);

        NeuralNetwork LoadNeuralNetwork(byte[] data);

        void TestNetwork(NeuralNetwork network, double[][] inputs, double[][] outputs);
    }
}
