namespace CAI.Services.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Abstraction;
    using Common.CustomExceptions;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Models;
    using Models.Bot;
    using Models.Intention;

    public class BotIntentionRecognitionService : BotService
    {
        private readonly INeuralNetworkService _neuralNetworkService;
        private readonly ILanguageProcessingService _languageProcessinService;

        public BotIntentionRecognitionService(IUnitOfWork data,
            INeuralNetworkService neuralNetworkService,
            ILanguageProcessingService languageProcessinService)
            : base(data)
        {
            this._neuralNetworkService = neuralNetworkService;
            this._languageProcessinService = languageProcessinService;
        }

        public long RegisterNewIntentionRecognitionBot(BotCreateModel model, string createdBy)
        {
            //var intentions = model.Intentions
            //    .Select(i => new Intention
            //    {
            //        Name = i.Name,
            //        CreatedBy = createdBy,
            //        ActivationKeys = i.ActivationKeys
            //            .Select(a => new ActivationKey
            //            {
            //                Name = a.Name,
            //                CreatedBy = createdBy
            //            })
            //            .ToArray()
            //    })
            //    .ToArray();
            
            var bot = new Bot()
            {
                CreatedBy = createdBy,
                Name = model.Name,
                BotType = model.BotType.ToString(),
                EnvironmentType = model.EnvironmentType.ToString(),
                //Intentions = intentions,
                //NeuralNetworkDatas = new List<NeuralNetworkData> { this.GenerateIntentionRecognizerBotNetwork(model) }
                //NeuralNetworkDatas = new List<NeuralNetworkData>()
                //{
                //    new NeuralNetworkData()
                //    {
                //        CreatedOn = DateTime.Now,
                //        CreatedBy = UserRoleType.Admin.ToString(),
                //        Type = NeuralNetworkType.Test.ToString(),
                //        Data =  Encoding.ASCII.GetBytes("test")
                //    }
                //}
            };

            //bot.NeuralNetworkDatas.Add(network);

            var id = this.RegisterNewBot(bot);

            var network = this.GenerateIntentionRecognizerBotNetwork(model);
            network.Bot = bot;
            network.BotId = id;
            network.CreatedBy = createdBy;

            this.Data.NeuralNetworkDataRepository.Add(network);
            this.Data.SaveChanges();

            foreach (var intentionModel in model.Intentions)
            {
                var intention = new Intention
                {
                    Bot = bot,
                    BotId = id,
                    Name = intentionModel.Name,
                    CreatedBy = createdBy,
                };

                this.Data.IntentionRepository.Add(intention);
                this.Data.SaveChanges();

                foreach (var keymodel in intentionModel.ActivationKeys)
                {
                    var key = new ActivationKey
                    {
                        Name = keymodel.Name,
                        CreatedBy = createdBy,
                        Intention = intention,
                        IntentionId = intention.Id
                    };

                    this.Data.ActivationKeyRepository.Add(key);
                }

                this.Data.SaveChanges();
            }

            return id;
        }

        public bool TrainIntentionRecognitionBot(long id, Dictionary<string, long> data)
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
                throw new NotFoundException($"{NeuralNetworkType.IntentionRecognition} neural network");
            }

            var knownKeys = this.ExtractKeys(bot);
            var knownIntentions = this.ExtractIntentions(bot);

            var networkIO = this.GenerateNeuralNetworkIO(data, knownKeys, knownIntentions);
            var network = this._neuralNetworkService.LoadNeuralNetwork(networkData.Data);

            var trainResult = this._neuralNetworkService.TrainNetwork(network, networkIO.Item1, networkIO.Item2);

            if (!trainResult)
            {
                throw new UnsuccessfulOperationException("Neural network training");
            }

            networkData.Data = network.GetNetworkBytes();
            this.Data.NeuralNetworkDataRepository.Update(networkData);
            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        public IntentionViewModel RecognizeIntention(long botId, string inputText)
        {
            var bot = this.FindBot(botId);
            var networkData = bot.NeuralNetworkDatas.FirstOrDefault(x => x.Type == NeuralNetworkType.IntentionRecognition.ToString());

            if (networkData == null)
            {
                throw new NotFoundException("Network data");
            }

            var knownKeys = this.ExtractKeys(bot);
            var values = this._languageProcessinService
                .Tokenize(inputText, false)
                .Select(x => x.ToLower());

            var input = knownKeys.Select(x => values.Contains(x) ? 1d : 0d).ToArray();
            var network = this._neuralNetworkService.LoadNeuralNetwork(networkData.Data);

            var output = network.Compute(input);
            Console.WriteLine(string.Join(" ", output));

            var max = output.Max();
            var intentionId = output.ToList().FindIndex(x => Math.Abs(x - max) < 0.001);

            var intention = this.FindIntention(intentionId + 1);

            return new IntentionViewModel()
            {
                Id = intention.Id,
                Name = intention.Name
            };
        }

        private NeuralNetworkData GenerateIntentionRecognizerBotNetwork(BotCreateModel bot)
        {
            var inputLayer = 0;

            foreach (var intention in bot.Intentions)
            {
                for (int i = 0; i < intention.ActivationKeys.Count; i++)
                {
                    inputLayer++;
                }
            }

            return this._neuralNetworkService.GenerateIntentionRecognitionNeuralNetworkData(
                inputLayer, bot.Intentions.Count);
        }

        private IEnumerable<string> ExtractKeys(Bot bot)
        {
            return bot.Intentions
                .SelectMany(i => i.ActivationKeys)
                .OrderBy(x => x.Id)
                .Select(x => x.Name);
        }

        private IEnumerable<long> ExtractIntentions(Bot bot)
        {
            return bot.Intentions
                .Select(x => x.Id)
                .OrderBy(x => x);
        }

        private Tuple<double[][], double[][]> GenerateNeuralNetworkIO(Dictionary<string, long> data,
            IEnumerable<string> knownKeys, IEnumerable<long> knownIntentions)
        {
            var input = new List<double[]>();
            var output = new List<double[]>();

            foreach (var pair in data)
            {
                var values = this._languageProcessinService
                    .Tokenize(pair.Key, false)
                    .Select(x => x.ToLower());

                input.Add(knownKeys.Select(x => values.Contains(x) ? 1d : 0d).ToArray());
                output.Add(knownIntentions.Select(x => pair.Value == x ? 1d : 0d).ToArray());
            }

            return new Tuple<double[][], double[][]>(input.ToArray(), output.ToArray());
        }
    }
}
