namespace CAI.Services
{
    using System;
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Models.Bot;
    using System.Collections.Generic;
    using System.Linq;
    using Common.CustomExceptions;
    using Data.Filtering;
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

        public long RegisterNewBot(BotCreateModel model, string createdBy)
        {
            this.CheckForExistingName(model.Name);

            var bot = new Bot()
            {
                CreatedBy = createdBy,
                Name = model.Name
            };

            this.Data.BotRepository.Add(bot);
            this.Data.SaveChanges();

            return bot.Id;
        }

        public bool EditBot(BotCreateModel model, long id, string modifiedBy)
        {
            this.CheckForExistingName(model.Name);

            var bot = this.FindBot(id);

            bot.Name = model.Name;
            bot.ModifiedBy = modifiedBy;
            this.Data.BotRepository.Update(bot);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        public bool DeleteBot(long id, string deletedBy)
        {
            var bot = this.FindBot(id);

            bot.DeletedBy = deletedBy;
            this.Data.BotRepository.Delete(bot);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        private void CheckForExistingName(string name)
        {
            if (this.Data.BotRepository.FindFirstByFilter(new BotFilter { Name = name }) != null)
            {
                throw new ExistingObjectException("Bot", "Use different name!");
            }
        }

        private Bot FindBot(long id)
        {
            var bot = this.Data.BotRepository.FindById(id);

            if (bot == null)
            {
                throw new NotFoundException("Bot");
            }

            return bot;
        }
    }
}
