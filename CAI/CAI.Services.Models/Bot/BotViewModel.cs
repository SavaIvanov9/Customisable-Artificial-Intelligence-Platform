namespace CAI.Services.Models.Bot
{
    using Base;
    using System.Collections.Generic;
    using Intention;

    public class BotViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string BotType { get; set; }

        public string EnvironmentType { get; set; }

        public string Image { get; set; }

        public IEnumerable<IntentionViewModel> Intentions { get; set; }
    }
}
