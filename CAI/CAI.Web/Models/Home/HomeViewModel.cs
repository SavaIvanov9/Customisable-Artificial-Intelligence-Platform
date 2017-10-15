namespace CAI.Web.Models.Home
{
    using Services.Models.Bot;
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IList<BotViewModel> DefaultBots { get; set; }
    }
}