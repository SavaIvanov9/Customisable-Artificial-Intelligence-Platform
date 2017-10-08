namespace CAI.Services.Bots
{
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Abstraction;
    using Common.CustomExceptions;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Models;
    using Models.Bot;

    public class BotIntentionRecognitionService : BotService
    {
        private readonly INeuralNetworkService _neuralNetworkService;

        public BotIntentionRecognitionService(IUnitOfWork data, INeuralNetworkService neuralNetworkService1)
            : base(data)
        {
            this._neuralNetworkService = neuralNetworkService1;
        }

        public long RegisterNewIntentionRecognitionBot(BotCreateModel model, string createdBy)
        {
            var intentions = model.Intentions
                .Select(i => new Intention
                {
                    Name = i.Name,
                    CreatedBy = createdBy,
                    ActivationKeys = i.ActivationKeys
                        .Select(a => new ActivationKey
                        {
                            Name = a.Name,
                            CreatedBy = createdBy
                        })
                        .ToArray()
                })
                .ToArray();

            var bot = new Bot()
            {
                CreatedBy = createdBy,
                Name = model.Name,
                BotType = model.BotType.ToString(),
                EnvironmentType = model.EnvironmentType.ToString(),
                Intentions = intentions,
                NeuralNetworkDatas = this.GenerateIntentionRecognizerBotNetworks(model).ToArray()
            };

            return this.RegisterNewBot(bot);
        }

        public bool TrainIntentionRecognitionBot(long id)
        {
            var bot = this.FindBot(id);

            if (bot.BotType != BotType.IntentionRecognizer.ToString())
            {
                throw new NotMatchingTypeException(BotType.IntentionRecognizer.ToString());
            }

            var networkData = bot.NeuralNetworkDatas
                .FirstOrDefault(n => n.Type == NeuralNetworkType.IntentionRecognition.ToString());

            if (networkData == null)
            {
                throw new NotFoundException($"{NeuralNetworkType.IntentionRecognition.ToString()} neural network");
            }
            
            var network = this._neuralNetworkService.LoadNeuralNetwork(networkData.Data);
            return this._neuralNetworkService.TrainNetwork(network, input, output);
        }

        private IEnumerable<NeuralNetworkData> GenerateIntentionRecognizerBotNetworks(BotCreateModel bot)
        {
            var inputLayer = 0;

            foreach (var intention in bot.Intentions)
            {
                for (int i = 0; i < intention.ActivationKeys.Count; i++)
                {
                    inputLayer++;
                }
            }

            return new[]
            {
                this._neuralNetworkService.GenerateIntentionRecognitionNeuralNetworkData(
                    inputLayer, bot.Intentions.Count)
            };
        }
    }
}
