namespace CAI.Services.Models.Bot
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Enums;
    using Intention;
    using TrainingData;

    public class BotCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public BotType BotType { get; set; }

        public string Image { get; set; }

        [Required]
        public EnvironmentType EnvironmentType { get; set; }

        public ICollection<IntentionCreateModel> Intentions { get; set; }

        public ICollection<TrainingDataCreateModel> TrainingData { get; set; }
    }
}
