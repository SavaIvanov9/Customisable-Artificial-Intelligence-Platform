namespace CAI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Base;
    using Common.CustomExceptions;
    using Data.Abstraction;
    using Data.Filtering;
    using Data.Models;
    using Models.Bot;
    using Models.Intention;

    public class IntentionService : BaseService, IIntentionService
    {
        public IntentionService(IUnitOfWork data) : base(data)
        {
        }

        //public Intention FindIntention(long id)
        //{

        //}

        //public long RegisterNewIntention(BotCreateModel model, string createdBy)
        //{
        //    //this.CheckForExistingName(model.Name);

        //    var bot = new Bot()
        //    {
        //        CreatedBy = createdBy,
        //        Name = model.Name
        //    };

        //    this.Data.BotRepository.Add(bot);
        //    this.Data.SaveChanges();

        //    return bot.Id;
        //}

        public bool EditBot(IntentionViewModel model, long id, string modifiedBy)
        {
            var intention = this.FindIntention(id);

            intention.Name = model.Name;
            intention.ModifiedBy = modifiedBy;

            this.Data.IntentionRepository.Update(intention);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        //public bool DeleteBot(long id, string deletedBy)
        //{
        //    var bot = this.FindBot(id);

        //    bot.DeletedBy = deletedBy;
        //    this.Data.BotRepository.Delete(bot);

        //    return Convert.ToBoolean(this.Data.SaveChanges());
        //}

        //private void CheckForExistingName(string name)
        //{
        //    if (this.Data.BotRepository.FindFirstByFilter(new BotFilter {Name = name}) != null)
        //    {
        //        throw new ExistingObjectException("Bot", "Use different name!");
        //    }
        //}
    }
}