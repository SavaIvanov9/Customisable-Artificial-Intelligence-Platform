namespace CAI.Services.Abstraction
{
    using Models.Bot;
    using System;
    using System.Collections.Generic;

    public interface IBotService : IDisposable
    {
        IEnumerable<BotViewModel> GetAllBots();

        BotViewModel FindBotById(long id);

        //long RegisterNewBot(BotCreateModel model, string createdBy);

        bool EditBot(BotCreateModel model, long id, string modifiedBy);

        bool DeleteBot(long id, string deletedBy);
    }
}
