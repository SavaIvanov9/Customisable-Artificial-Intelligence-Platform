namespace CAI.Services.Models.Intention
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ActivationKey;

    public class IntentionCreateModel
    {
        [Required]
        public string Name { get; set; }

        public long BotId { get; set; }

        public ICollection<ActivationKeyCreateModel> ActivationKeys { get; set; }
    }
}
