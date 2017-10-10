namespace CAI.ConsoleTestGround.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Hosting;
    using Common.Enums;
    using Data;
    using Data.Models;
    using Services;
    using Services.Models.ActivationKey;
    using Services.Models.Bot;
    using Services.Models.Intention;

    public class BotModule
    {
        public void Start()
        {
            this.TestCompute();
        }

        private void TestCompute()
        {
            using (var uw = new UnitOfWork())
            {
                var neuralNetworkService = new NeuralNetworkService(uw);
                var botService = new IntentionRecognitionService(uw,
                    neuralNetworkService, new LanguageProcessingService());

                var input = Console.ReadLine();

                while (input != "-1")
                {
                    var result = botService.RecognizeIntention(12, input);
                    Console.WriteLine($"{result.Id}: {result.Name}");
                    input = Console.ReadLine();
                }
            }
        }

        private void TestCreateAndTrain()
        {
            using (var uw = new UnitOfWork())
            {
                var neuralNetworkService = new NeuralNetworkService(uw);
                var botService =
                    new IntentionRecognitionService(uw, neuralNetworkService, new LanguageProcessingService());
                //{ "i", "like", "pizza", "eat", "lunch", "every", "day"})
                //{"i", "am", "name", "is", "hello", "hi", "how", "are", "you", "who", "what"})
                Console.WriteLine("Creating new bot started...");

                var botModel = new BotCreateModel()
                {
                    Name = "Pizza lover bot",
                    BotType = BotType.IntentionRecognizer,
                    EnvironmentType = EnvironmentType.Test,
                    Intentions = this.GenerateSampleIntentions()
                };

                var id = botService.RegisterNewIntentionRecognitionBot(botModel, "admin");

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

                var result = botService.TrainIntentionRecognitionBot(id, trainingData);

                Console.WriteLine("Training done...");
                Console.WriteLine(result);
            }
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

        private void Test()
        {
            //double[][] input = new double[][]
            //{
            //    new double[] {1, 1, 1, 0, 0, 0, 0, 0, 0},
            //    new double[] {0, 0, 0, 1, 1, 1, 0, 0, 0},

            //    new double[] {1, 0, 0, 0, 0, 0, 1, 1, 0},
            //    new double[] {0, 1, 1, 0, 0, 0, 0, 1, 1},
            //};

            //double[][] output = new double[][]
            //{
            //    new double[] {1, 0},
            //    new double[] {1, 0},
            //    new double[] {0, 1},
            //    new double[] {0, 1}
            //};

            //var network = nnService.GenerateNetwork(input[0].Length, output[0].Length, true);
            //Console.ReadLine();
            //nnService.TrainNetwork(network, input, output, 0.001);

            ////var network = nnService.LoadNeuralNetwork(1);

            //nnService.TestNetwork(network, input, output);

            ////using (var fileStream = File.Create("test1.txt"))
            ////{
            ////    network.Save(fileStream);
            ////}

            ////var id = nnService.RegisterNewNetwork(network, 1, "admin", NeuralNetworkType.Test);
            ////Console.WriteLine(id);
            ////var bot = uw.BotRepository.FindFirstByFilter(new BotFilter() { Id = 1 });
            ////Console.WriteLine(bot.NeuralNetworkDatas.Count);
        }
    }
}

