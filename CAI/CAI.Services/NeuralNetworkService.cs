namespace CAI.Services
{
    using DeepLearning;
    using System;
    using Base;
    using Common.CustomExceptions;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Models;

    public class NeuralNetworkService : BaseService
    {
        public NeuralNetworkService(IUnitOfWork data) : base(data)
        {
        }

        public NeuralNetwork GenerateNetwork(int inputLayer, int outputLayer, bool isLogging = false)
        {
            return new NeuralNetwork(inputLayer, outputLayer, isLogging);
        }

        public void TrainNetwork(NeuralNetwork network, double[][] input, double[][] output,
            double errorRate)
        {
            network.TrainNetwork(input, output, errorRate);
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
            //this.Data.BotRepository.Update(bot);
            this.Data.SaveChanges();

            return networkDataModel.Id;
        }

        public void TestNetwork(NeuralNetwork network, double[][] inputs, double[][] outputs)
        {
            for (var i = 0; i < inputs.Length; i++)
            {
                var result = network.Compute(inputs[i]);

                Console.WriteLine($"{string.Join(" / ", result)} - Target: {string.Join(" / ", outputs[i])}");
            }
        }

        private Bot FindBot(long id)
        {
            var bot = this.Data.BotRepository.FindById(id);

            if (bot == null)
            {
                throw new NotFoundException("Bot");
            }

            return bot;
        }
    }
}
