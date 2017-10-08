namespace CAI.Services.Models.TrainingData
{
    using ActivationKey;
    using Intention;
    using System.Collections.Generic;

    public class TrainingDataCreateModel
    {
        public ICollection<ActivationKeyCreateModel> ActivationKeys { get; set; }

        public IntentionCreateModel Intention { get; set; }
    }
}
