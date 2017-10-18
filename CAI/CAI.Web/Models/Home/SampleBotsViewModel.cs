namespace CAI.Web.Models.Home
{
    using System;
    using Services.Models.Bot;
    using System.Collections.Generic;

    public class SampleBotsViewModel
    {
        public IList<BotViewModel> DefaultBots { get; set; }

        public DateTime Date { get; set; }
    }
}