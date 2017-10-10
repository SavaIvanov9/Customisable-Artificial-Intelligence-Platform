namespace CAI.Services.Models.Intention
{
    using ActivationKey;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Base;

    public class IntentionViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ActivationKeyViewModel> ActivationKeys { get; set; }
    }
}
