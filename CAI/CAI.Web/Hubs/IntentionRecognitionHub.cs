namespace CAI.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.SignalR;
    using Services.Abstraction;
    using Services.Models.Bot;

    public class IntentionRecognitionHub : Hub
    {
        private readonly INeuralNetworkService _neuralNetworkService;
        private readonly IIntentionRecognitionService _intentionRecognitionService;

        public IntentionRecognitionHub(INeuralNetworkService neuralNetworkService,
            IIntentionRecognitionService intentionRecognitionService)
        {
            if (neuralNetworkService == null)
            {
                throw new ArgumentNullException(nameof(neuralNetworkService));
            }
            this._neuralNetworkService = neuralNetworkService;

            if (intentionRecognitionService == null)
            {
                throw new ArgumentNullException(nameof(intentionRecognitionService));
            }
            this._intentionRecognitionService = intentionRecognitionService;
        }

        public void SendMessege(long botId, string message)
        {
            var chatMessage = $"IntentionRecognitionHub>> Message from {this.Context.ConnectionId}: {message}";
            var responce = this.GenerateResponce(botId, message);
            //this.Clients.Caller.receiveMessage(message);
            this.Clients.All.receiveMessage(responce);
        }

        private List<string> GenerateResponce(long botId, string input)
        {
            var intentions = this._intentionRecognitionService.RecognizeMultipleIntentions(botId, input);

            var responce = new List<string>();

            foreach (var intention in intentions)
            {
                responce.Add("Intnention:");
                responce.Add($"-- Name: {intention.Name}");
                responce.Add($"-- Factor: {intention.Factor}");
                responce.Add("");
            }

            return responce;

            //var responce = new StringBuilder();

            //foreach (var intention in intentions)
            //{
            //    responce.AppendLine("Intnention:");
            //    responce.AppendLine($"  Name: {intention.Name}");
            //    responce.AppendLine($"  Factor: {intention.Factor}");
            //    responce.AppendLine();
            //}

            //return responce.ToString();
        }
    }
}