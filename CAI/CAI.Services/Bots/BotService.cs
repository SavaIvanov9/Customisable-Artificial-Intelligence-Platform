namespace CAI.Services.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Base;
    using Common.CustomExceptions;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Filtering;
    using Data.Models;
    using Models.Bot;

    public abstract class BotService : BaseService//, IBotService
    {
        protected BotService(IUnitOfWork data) : base(data)
        {
        }

        protected IEnumerable<BotViewModel> GetAllBots()
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

        protected long RegisterNewBot(Bot bot)
        {
            this.CheckForExistingName(bot.Name);

            this.Data.BotRepository.Add(bot);
            this.Data.SaveChanges();

            return bot.Id;
        }

        protected bool EditBot(BotCreateModel model, long id, string modifiedBy)
        {
            this.CheckForExistingName(model.Name);

            var bot = this.FindBot(id);

            bot.Name = model.Name;
            bot.ModifiedBy = modifiedBy;
            this.Data.BotRepository.Update(bot);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        protected bool DeleteBot(long id, string deletedBy)
        {
            var bot = this.FindBot(id);

            bot.DeletedBy = deletedBy;
            this.Data.BotRepository.Delete(bot);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        protected void CheckForExistingName(string name)
        {
            if (this.Data.BotRepository.FindFirstByFilter(new BotFilter { Name = name }) != null)
            {
                throw new ExistingObjectException("Bot", "Use different name!");
            }
        }
    }
}
