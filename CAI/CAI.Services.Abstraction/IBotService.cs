namespace CAI.Services.Abstraction
{
    using Models.Bot;
    using System.Collections.Generic;

    public interface IBotService
    {
        IEnumerable<BotViewModel> GetAllBots();

        long CreateNewBot(BotCreateModel model, string createdBy);

        bool EditBot(BotCreateModel model, long id, string modifiedBy);

        bool DeleteBot(long id, string deletedBy);
    }
}
