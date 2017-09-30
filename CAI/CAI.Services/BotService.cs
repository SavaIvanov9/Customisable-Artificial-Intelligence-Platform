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

        public long CreateNewBot(BotCreateModel model, string createdBy)
        {
            if (this.Data.BotRepository.FindByName(model.Name) != null)
            {
                throw new ExistingObjectException("Bot", "Use different name!");
            }

            var bot = new Bot()
            {
                CreatedBy = createdBy,
                Name = model.Name
            };

            this.Data.BotRepository.Add(bot);
            this.Data.SaveChanges();

            return bot.Id;
        }
    }
}
