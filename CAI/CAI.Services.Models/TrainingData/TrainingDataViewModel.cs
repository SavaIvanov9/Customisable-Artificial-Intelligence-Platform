namespace CAI.Services.Models.TrainingData
{
    using Base;

    public class TrainingDataViewModel : BaseViewModel
    {
        //public ICollection<ActivationKeyCreateModel> ActivationKeys { get; set; }
        public string Content { get; set; }

        public long IntentionId { get; set; }
    }
}
