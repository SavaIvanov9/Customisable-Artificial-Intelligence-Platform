namespace CAI.Services
{
    using Abstraction;
    using Base;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Filtering;
    using Models.ActivationKey;
    using Models.Bot;
    using Models.Intention;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DefaultBotsService : BaseService, IDefaultBotsService
    {
        private readonly INeuralNetworkService _neuralNetworkService;
        private readonly IIntentionRecognitionService _botService;

        public DefaultBotsService(IUnitOfWork data,
            INeuralNetworkService neuralNetworkService,
            IIntentionRecognitionService botService) : base(data)
        {
            this._neuralNetworkService = neuralNetworkService;
            this._botService = botService;
        }

        public void InitDefaultBots()
        {
            var defaultBots = this.Data
                .BotRepository
                .FindAllByFilter(new BotFilter()
                {
                    EnvironmentType = EnvironmentType.Test.ToString(),
                    IsDeleted = false
                });

            if (defaultBots == null || !defaultBots.Any())
            {
                this.CreateAndTrainIntroductionBot();
                this.CreateAndTrainPizzaLoverBot();
            }
        }

        private void CreateAndTrainIntroductionBot()
        {
            //{ "i", "like", "pizza", "eat", "lunch", "every", "day"})
            //{"i", "am", "name", "is", "hello", "hi", "how", "are", "you", "who", "what"})
            Console.WriteLine("Creating new bot started...");

            var botModel = new BotCreateModel()
            {
                Name = "Introduction Bot",
                BotType = BotType.IntentionRecognizer,
                EnvironmentType = EnvironmentType.Test,
                Image = @"http://static3.businessinsider.com/image/5661d93edd0895060c8b48ae/the-best-science-fiction-as-picked-by-20-ai-experts.jpg",
                Intentions = this.GenerateSampleIntentions()
            };

            var id = this._botService.RegisterNewIntentionRecognitionBot(botModel, "admin");

            Console.WriteLine("Creating new bot done.");
            Console.WriteLine();
            Console.WriteLine("Training started...");

            var trainingData = new Dictionary<string, long>()
            {
                { "i like pizza", 1 },
                { "i lunch pizza", 1 },
                { "i eat pizza every day", 1 },
                { "i lunch pizza every day", 1 },
                { "i like pizza for lunch", 1 },
                { "every day is pizza day", 1 },

                { "i am john", 2 },
                { "my name is john", 2 },
                { "who are you", 2 },
                { "how are you", 2 },
                { "hi", 2 },
                { "hello", 2 }
            };

            var result = this._botService.TrainIntentionRecognitionBot(id, trainingData);

            Console.WriteLine("Training done...");
            Console.WriteLine(result);
        }


        private void CreateAndTrainPizzaLoverBot()
        {
                //{ "i", "like", "pizza", "eat", "lunch", "every", "day"})
                //{"i", "am", "name", "is", "hello", "hi", "how", "are", "you", "who", "what"})
                Console.WriteLine("Creating new bot started...");

                var botModel = new BotCreateModel()
                {
                    Name = "Pizza lover Bot",
                    BotType = BotType.IntentionRecognizer,
                    EnvironmentType = EnvironmentType.Test,
                    Image = @"http://i2.cdn.turner.com/money/dam/assets/151111102126-artificial-intelligence-ai-robots-780x439.jpg",
                    Intentions = this.GenerateSampleIntentions()
                };

                var id = this._botService.RegisterNewIntentionRecognitionBot(botModel, "admin");

                Console.WriteLine("Creating new bot done.");
                Console.WriteLine();
                Console.WriteLine("Training started...");

                var trainingData = new Dictionary<string, long>()
                {
                    { "i like pizza", 1 },
                    { "i lunch pizza", 1 },
                    { "i eat pizza every day", 1 },
                    { "i lunch pizza every day", 1 },
                    { "i like pizza for lunch", 1 },
                    { "every day is pizza day", 1 },

                    { "i am john", 2 },
                    { "my name is john", 2 },
                    { "who are you", 2 },
                    { "how are you", 2 },
                    { "hi", 2 },
                    { "hello", 2 }
                };

                var result = this._botService.TrainIntentionRecognitionBot(id, trainingData);

                Console.WriteLine("Training done...");
                Console.WriteLine(result);
        }

        private ICollection<IntentionCreateModel> GenerateSampleIntentions()
        {
            return new HashSet<IntentionCreateModel>
            {
                new IntentionCreateModel()
                {
                    Name = "Love pizza",
                    ActivationKeys = this.GenerateSampleActivationKeys(new [] {"i", "like", "pizza", "eat", "lunch", "every", "day"})
                },
                new IntentionCreateModel()
                {
                    Name = "Introduction",
                    ActivationKeys = this.GenerateSampleActivationKeys(new [] {"i", "am", "name", "is", "hello", "hi", "how", "are", "you", "who", "what"})
                },
            };
        }

        private ICollection<ActivationKeyCreateModel> GenerateSampleActivationKeys(string[] values)
        {
            var result = new HashSet<ActivationKeyCreateModel>();

            foreach (var value in values)
            {
                result.Add(new ActivationKeyCreateModel()
                {
                    Name = value
                });
            }

            return result;
        }
    }
}
