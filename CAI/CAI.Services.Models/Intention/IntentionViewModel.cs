namespace CAI.Services.Models.Intention
{
    using ActivationKey;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IntentionViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long BotId { get; set; }

        public ICollection<ActivationKeyCreateModel> ActivationKeys { get; set; }
    }
}
