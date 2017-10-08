namespace CAI.Services.Abstraction
{
    using Common.Enums;
    using Data.Models;
    using DeepLearning;

    public interface INeuralNetworkService
    {
        NeuralNetworkData GenerateIntentionRecognitionNeuralNetworkData(int inputLayer, int outputLayer);

        NeuralNetwork GenerateNetwork(int inputLayer, int outputLayer, bool isLogging = false);

        void TrainNetwork(NeuralNetwork network, double[][] input, double[][] output, double errorRate);

        long RegisterNewNetwork(NeuralNetwork network, long botId, string createdBy, NeuralNetworkType networkType);

        NeuralNetwork LoadNeuralNetwork(long id);

        NeuralNetwork LoadNeuralNetwork(byte[] data);

        void TestNetwork(NeuralNetwork network, double[][] inputs, double[][] outputs);
    }
}
