namespace CAI.ConsoleTestGround.Modules
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using AForge.Neuro;
    using Services;

    public class NeuralNetworkModule
    {
        private NeuralNetworkService _nnService = new NeuralNetworkService();

        public void Start()
        {
            double[][] input = new double[][] {
                new double[] { 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                new double[] { 0, 0, 0, 1, 1, 1, 0, 0, 0 },

                new double[] { 1, 0, 0, 0, 0, 0, 1, 1, 0 },
                new double[] { 0, 1, 1, 0, 0, 0, 0, 1, 1 },
            };

            double[][] output = new double[][] {
                new double[] {1, 0},
                new double[] {1, 0},
                new double[] {0, 1},
                new double[] {0, 1}
            };

            var st = new Stopwatch();
            st.Start();

            var network = this._nnService.GenerateNetwork(input[0].Length, output[0].Length);
            Console.ReadLine();
            this._nnService.TrainNetwork(network, input, output, 0.0001);

            //var network = this.LoadNetwork();

            st.Stop();
            Console.WriteLine(st.Elapsed);

            this._nnService.TestNN(network, input);

            using (var fileStream = File.Create("test1.txt"))
            {
                network.Save(fileStream);
            }
        }

        private Network LoadNetwork()
        {
            return Network.Load("test1.txt");
        }
    }
}
