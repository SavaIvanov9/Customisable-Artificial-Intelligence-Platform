namespace CAI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Data.Filtering;
    using Models.Bot;

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

        public IEnumerable<BotViewModel> GetAllBotsByFilter(BotFilter filter)
        {
            var result = this.Data.BotRepository
                .FindAllByFilter(filter)
                .Select(x => new BotViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BotType = x.BotType,
                    EnvironmentType = x.EnvironmentType,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn
                })
                .AsEnumerable();

            return result;
        }

        //protected long RegisterNewBot(Bot bot)
        //{
        //    this.CheckForExistingName(bot.Name);

        //    this.Data.BotRepository.Add(bot);
        //    this.Data.SaveChanges();

        //    return bot.Id;
        //}

        public BotViewModel FindBotById(long id)
        {
            var bot = this.FindBot(id);

            return new BotViewModel()
            {
                Id = bot.Id,
                Name = bot.Name,
                CreatedOn = bot.CreatedOn,
                ModifiedOn = bot.ModifiedOn
            };
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
    }
}
