namespace CAI.Services
{
    using DeepLearning;
    using System;
    using System.IO;
    using Abstraction;
    using Base;
    using Common.CustomExceptions;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Models;

    public class NeuralNetworkService : BaseService, INeuralNetworkService
    {
        public NeuralNetworkService(IUnitOfWork data) : base(data)
        {
        }

        public NeuralNetworkData GenerateIntentionRecognitionNeuralNetworkData(
            int inputLayer, int outputLayer)
        {
            var neuralNetwork = this.GenerateNetwork(inputLayer, outputLayer);

            var neuralNetworkData = new NeuralNetworkData()
            {
                Type = NeuralNetworkType.IntentionRecognition.ToString(),
                Data = neuralNetwork.GetNetworkBytes()
            };

            return neuralNetworkData;
        }

        public NeuralNetwork GenerateNetwork(int inputLayer, int outputLayer, bool isLogging = false)
        {
            return new NeuralNetwork(inputLayer, outputLayer, isLogging);
        }

        public bool TrainNetwork(NeuralNetwork network, double[][] input, double[][] output, double errorRate = 0.01)
        {
            return network.TrainNetwork(input, output, errorRate);
        }

        public long RegisterNewNetwork(NeuralNetwork network, long botId, string createdBy, NeuralNetworkType networkType)
        {
            var bot = this.FindBot(botId);
            var networkBytes = network.GetNetworkBytes();

            var networkDataModel = new NeuralNetworkData()
            {
                CreatedBy = createdBy,
                Data = networkBytes,
                Bot = bot,
                BotId = bot.Id,
                Type = networkType.ToString()
            };

            this.Data.NeuralNetworkDataRepository.Add(networkDataModel);
            this.Data.SaveChanges();

            return networkDataModel.Id;
        }

        public NeuralNetwork LoadNeuralNetwork(long id)
        {
            var networkData = this.FindNeuralNetworkData(id);

            NeuralNetwork network;

            using (var stream = new MemoryStream(networkData.Data))
            {
                network = new NeuralNetwork(stream);
            }

            return network;
        }

        public NeuralNetwork LoadNeuralNetwork(byte[] data)
        {
            NeuralNetwork network;

            using (var stream = new MemoryStream(data))
            {
                network = new NeuralNetwork(stream);
            }

            return network;
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
