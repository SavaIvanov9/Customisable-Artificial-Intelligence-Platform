namespace CAI.Services.Models.Intention
{
    using ActivationKey;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Base;
    using TrainingData;

    public class IntentionViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public long BotId { get; set; }

        public double Factor { get; set; }

        public IEnumerable<ActivationKeyViewModel> ActivationKeys { get; set; }

        public IEnumerable<TrainingDataViewModel> TrainingData { get; set; }
    }
}
