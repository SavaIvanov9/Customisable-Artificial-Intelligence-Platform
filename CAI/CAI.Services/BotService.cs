namespace CAI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Data.Filtering;
    using Models.ActivationKey;
    using Models.Bot;
    using Models.Intention;

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
                    Image = x.Image,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn
                });

            return result;
        }

        //protected long RegisterNewBot(Bot bot)
        //{
        //    this.CheckForExistingName(bot.Name);

        //    this.Data.BotRepository.Add(bot);
        //    this.Data.SaveChanges();

        //    return bot.Id;
        //}

        public BotViewModel FindBot(long id)
        {
            var bot = base.FindBotById(id);

            return new BotViewModel()
            {
                Id = bot.Id,
                Name = bot.Name,
                BotType = bot.BotType,
                EnvironmentType = bot.EnvironmentType,
                Image = bot.Image,
                CreatedOn = bot.CreatedOn,
                ModifiedOn = bot.ModifiedOn,
                Intentions = bot.Intentions
                    .Where(i => i.IsDeleted == false)
                    .Select(i => new IntentionViewModel()
                    {
                        Id = i.Id,
                        Name = i.Name,
                        BotId = i.BotId,
                        CreatedOn = i.CreatedOn,
                        ModifiedOn = i.ModifiedOn,
                        ActivationKeys = i.ActivationKeys
                            .Where(a => a.IsDeleted == false)
                            .Select(a => new ActivationKeyViewModel()
                            {
                                Id = a.Id,
                                Name = a.Name,
                                IntentionId = a.IntentionId,
                                CreatedOn = a.CreatedOn,
                                ModifiedOn = a.ModifiedOn
                            })
                    })
            };
        }

        public bool EditBot(BotViewModel model, string modifiedBy)
        {
            //this.CheckBotForExistingName(model.Name);

            var bot = base.FindBotById(model.Id);

            bot.Name = model.Name;
            bot.ModifiedBy = modifiedBy;
            bot.Image = model.Image;

            this.Data.BotRepository.Update(bot);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        public bool DeleteBot(long id, string deletedBy)
        {
            var bot = base.FindBotById(id);

            bot.DeletedBy = deletedBy;
            this.Data.BotRepository.Delete(bot);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }
    }
}
