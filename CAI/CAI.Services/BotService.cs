namespace CAI.Services
{
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Models.Bot;
    using System.Collections.Generic;
    using System.Linq;
    using Common.CustomExceptions;
    using Data.Models;

    public class BotService : BaseService, IBotService
    {
        public BotService(IUnitOfWork data) : base(data)
        {
        }

        public IEnumerable<BotViewModel> GetAllBots()
        {
            var result = this.Data.BotRepository
                .All()
                .Where(x => !x.IsDeleted)
                .Select(x => new BotViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn
                })
                .AsEnumerable();

            return result;
        }

        public long CreateNewBot(BotCreateModel model)
        {
            if (this.Data.BotRepository
                .All()
                .Any(x => x.Name == model.Name))
            {
                throw new ExistingObjectException("Bot", "Use different name!");
            }

            var bot = new Bot()
            {
                CreatedBy = "S",
                Name = model.Name
            };

            this.Data.BotRepository
                .Add(bot);
            this.Data.SaveChanges();

            return bot.Id;
        }
    }
}
