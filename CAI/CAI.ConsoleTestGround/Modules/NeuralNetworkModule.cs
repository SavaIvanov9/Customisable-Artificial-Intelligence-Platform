namespace CAI.ConsoleTestGround.Modules
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using AForge.Neuro;
    using Common.Enums;
    using Data;
    using Data.Filtering;
    using Services;

    public class NeuralNetworkModule
    {
        public void Start()
        {
            using (var uw = new UnitOfWork())
            {
                var nnService = new NeuralNetworkService(uw);

                double[][] input = new double[][]
                {
                    new double[] {1, 1, 1, 0, 0, 0, 0, 0, 0},
                    new double[] {0, 0, 0, 1, 1, 1, 0, 0, 0},

                    new double[] {1, 0, 0, 0, 0, 0, 1, 1, 0},
                    new double[] {0, 1, 1, 0, 0, 0, 0, 1, 1},
                };

                double[][] output = new double[][]
                {
                    new double[] {1, 0},
                    new double[] {1, 0},
                    new double[] {0, 1},
                    new double[] {0, 1}
                };

                //var network = nnService.GenerateNetwork(input[0].Length, output[0].Length, true);
                //Console.ReadLine();
                //nnService.TrainNetwork(network, input, output, 0.001);

                var network = nnService.LoadNeuralNetwork(1);

                nnService.TestNetwork(network, input, output);

                //using (var fileStream = File.Create("test1.txt"))
                //{
                //    network.Save(fileStream);
                //}

                //var id = nnService.RegisterNewNetwork(network, 1, "admin", NeuralNetworkType.Test);
                //Console.WriteLine(id);
                //var bot = uw.BotRepository.FindFirstByFilter(new BotFilter() { Id = 1 });
                //Console.WriteLine(bot.NeuralNetworkDatas.Count);
            }
        }

        private Network LoadNetwork()
        {
            return Network.Load("test1.txt");
        }
    }
}
