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

    public class IntentionService : BaseService
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

        //public bool EditBot(BotCreateModel model, long id, string modifiedBy)
        //{
        //    this.CheckForExistingName(model.Name);

        //    var bot = this.FindBot(id);

        //    bot.Name = model.Name;
        //    bot.ModifiedBy = modifiedBy;
        //    this.Data.BotRepository.Update(bot);

        //    return Convert.ToBoolean(this.Data.SaveChanges());
        //}

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