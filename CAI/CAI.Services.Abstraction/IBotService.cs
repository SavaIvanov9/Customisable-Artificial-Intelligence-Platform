namespace CAI.Services.Abstraction
{
    using Models.Bot;
    using System.Collections.Generic;

    public interface IBotService
    {
        IEnumerable<BotViewModel> GetAllBots();

        long CreateNewBot(BotCreateModel model);
    }
}
