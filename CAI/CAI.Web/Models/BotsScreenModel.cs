namespace CAI.Web.Models
{
    using Services.Models.Bot;
    using System.Collections.Generic;

    public class BotsScreenModel
    {
        public IEnumerable<BotViewModel> Bots { get; set; }
    }
}