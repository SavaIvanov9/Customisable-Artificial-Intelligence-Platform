﻿namespace CAI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Base;
    using Common.CustomExceptions;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Filtering;
    using Data.Models;
    using Models.Bot;
    using Models.Intention;

    public class IntentionRecognitionService : BaseService, IIntentionRecognitionService
    {
        private readonly INeuralNetworkService _neuralNetworkService;
        private readonly ILanguageProcessingService _languageProcessinService;
        private readonly ITrainingDataService _trainingDataService;

        public IntentionRecognitionService(IUnitOfWork data,
            INeuralNetworkService neuralNetworkService,
            ILanguageProcessingService languageProcessinService, ITrainingDataService trainingDataService)
            : base(data)
        {
            this._neuralNetworkService = neuralNetworkService;
            this._languageProcessinService = languageProcessinService;
            this._trainingDataService = trainingDataService;
        }

        public long RegisterNewIntentionRecognitionBot(BotCreateModel model, string createdBy)
        {
            var user = this.FindUserById(model.UserId);

            var bot = new Bot()
            {
                CreatedBy = createdBy,
                Name = model.Name,
                BotType = model.BotType.ToString(),
                EnvironmentType = model.EnvironmentType.ToString(),
                Image = model.Image,
                User = user,
                UserId = user.Id
            };

            this.Data.BotRepository.Add(bot);
            this.Data.SaveChanges();

            //var network = this.GenerateIntentionRecognizerBotNetwork(model);
            //network.Bot = bot;
            //network.BotId = bot.Id;
            //network.CreatedBy = createdBy;

            //this.Data.NeuralNetworkDataRepository.Add(network);
            //this.Data.SaveChanges();

            //foreach (var intentionModel in model.Intentions)
            //{
            //    var intention = new Intention
            //    {
            //        Bot = bot,
            //        BotId = bot.Id,
            //        Name = intentionModel.Name,
            //        CreatedBy = createdBy,
            //    };

            //    this.Data.IntentionRepository.Add(intention);
            //    this.Data.SaveChanges();

            //    foreach (var keymodel in intentionModel.ActivationKeys)
            //    {
            //        var key = new ActivationKey
            //        {
            //            Name = keymodel.Name,
            //            CreatedBy = createdBy,
            //            Intention = intention,
            //            IntentionId = intention.Id
            //        };

            //        this.Data.ActivationKeyRepository.Add(key);
            //    }

            //    this.Data.SaveChanges();
            //}

            return bot.Id;
        }

        public long FullRegisterNewIntentionRecognitionBot(BotCreateModel model, string createdBy)
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

            var user = this.FindUserById(model.UserId);

            var bot = new Bot()
            {
                CreatedBy = createdBy,
                Name = model.Name,
                BotType = model.BotType.ToString(),
                EnvironmentType = model.EnvironmentType.ToString(),
                Image = model.Image,
                User = user,
                UserId = user.Id
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

            //this.CheckBotForExistingName(bot.Name);
            this.Data.BotRepository.Add(bot);
            this.Data.SaveChanges();

            //var network = this.GenerateIntentionRecognizerBotNetwork(model);
            //network.Bot = bot;
            //network.BotId = bot.Id;
            //network.CreatedBy = createdBy;

            //this.Data.NeuralNetworkDataRepository.Add(network);
            //this.Data.SaveChanges();

            this.GenerateNetwork(model, createdBy, bot);

            foreach (var intentionModel in model.Intentions)
            {
                var intention = new Intention
                {
                    Bot = bot,
                    BotId = bot.Id,
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

            return bot.Id;
        }

        public bool TrainIntentionRecognitionBot(long id)
        {
            var bot = base.FindBotById(id);
            var data = new Dictionary<string, long>();

            foreach (var intention in bot.Intentions)
            {
                foreach (var trainData in intention.TrainingData)
                {
                    data.Add(trainData.Content, intention.Id);
                }
            }

            return this.TrainIntentionRecognitionBot(bot.Id, data);
        }

        public bool TrainIntentionRecognitionBot(long id, Dictionary<string, long> data)
        {
            var bot = base.FindBotById(id);

            if (bot.BotType != BotType.IntentionRecognizer.ToString())
            {
                throw new NotMatchingTypeException(BotType.IntentionRecognizer.ToString());
            }

            var networkData = bot.NeuralNetworkDatas
                .FirstOrDefault(n => n.Type == NeuralNetworkType.IntentionRecognition.ToString());

            if (networkData == null)
            {
                //throw new NotFoundException($"{NeuralNetworkType.IntentionRecognition} neural network");
                this.GenerateNetwork(bot);
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
            var bot = base.FindBotById(botId);
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
            //Console.WriteLine(string.Join(" ", output));

            //var max = output.Max();
            //var intentionId = output.ToList().FindIndex(x => Math.Abs(x - max) < 0.001);

            //var intention = base.FindIntentionById(intentionId + 1);

            //return new IntentionViewModel()
            //{
            //    Id = intention.Id,
            //    Name = intention.Name
            //};

            var max = output.Max();
            var intentionId = output.ToList().FindIndex(x => Math.Abs(x - max) < 0.001);

            var intention = base.FindIntentionById(intentionId + 1);

            return new IntentionViewModel()
            {
                Id = intention.Id,
                Name = intention.Name
            };
        }

        public IList<IntentionViewModel> RecognizeMultipleIntentions(long botId, string inputText)
        {
            var bot = base.FindBotById(botId);
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
            //Console.WriteLine(string.Join(" ", output));

            //var max = output.Max();
            //var intentionId = output.ToList().FindIndex(x => Math.Abs(x - max) < 0.001);

            //var intention = base.FindIntentionById(intentionId + 1);

            //return new IntentionViewModel()
            //{
            //    Id = intention.Id,
            //    Name = intention.Name
            //};

            var intentions = new List<IntentionViewModel>();

            //var max = output.Max();
            //var intentionId = output.ToList().FindIndex(x => Math.Abs(x - max) < 0.001);

            for (int i = 0; i < output.Length; i++)
            {
                var knownIntentions = bot.Intentions
                    .ToArray();

                var intention = knownIntentions[i];

                //var intention = base.FindIntentionById(i + 1);
                //var intention = bot.Intentions.ToArray()[i + 1];

                intentions.Add(new IntentionViewModel
                {
                    Id = intention.Id,
                    Name = intention.Name,
                    Factor = output[i]
                });
            }

            intentions = new List<IntentionViewModel>(intentions.OrderByDescending(x => x.Factor));
            return intentions;
        }

        private NeuralNetworkData GenerateIntentionRecognizerBotNetwork(Bot bot)
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

        private void GenerateNetwork(Bot bot)
        {
            var network = this.GenerateIntentionRecognizerBotNetwork(bot);
            network.Bot = bot;
            network.BotId = bot.Id;
            network.CreatedBy = bot.CreatedBy;

            this.Data.NeuralNetworkDataRepository.Add(network);
            this.Data.SaveChanges();
        }

        private void GenerateNetwork(BotCreateModel model, string createdBy, Bot bot)
        {
            var network = this.GenerateIntentionRecognizerBotNetwork(model);
            network.Bot = bot;
            network.BotId = bot.Id;
            network.CreatedBy = createdBy;

            this.Data.NeuralNetworkDataRepository.Add(network);
            this.Data.SaveChanges();
        }
    }
}
