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
    using Models.ActivationKey;
    using Models.Bot;
    using Models.Intention;

    public class IntentionService : BaseService, IIntentionService
    {
        public IntentionService(IUnitOfWork data) : base(data)
        {
        }

        public IntentionViewModel FindIntention(long id)
        {
            var intention = base.FindIntentionById(id);

            return new IntentionViewModel()
            {
                Id = intention.Id,
                Name = intention.Name,
                BotId = intention.BotId,
                CreatedOn = intention.CreatedOn,
                ModifiedOn = intention.ModifiedOn,
                ActivationKeys = intention.ActivationKeys.Select(a => new ActivationKeyViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    CreatedOn = a.CreatedOn,
                    ModifiedOn = a.ModifiedOn
                })
            };
        }

        public bool EditIntention(IntentionViewModel model, string modifiedBy)
        {
            var intention = base.FindIntentionById(model.Id);

            intention.Name = model.Name;
            intention.ModifiedBy = modifiedBy;

            this.Data.IntentionRepository.Update(intention);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        public bool DeleteIntention(long id, string deletedBy)
        {
            var intention = base.FindIntentionById(id);

            intention.DeletedBy = deletedBy;
            this.Data.IntentionRepository.Delete(intention);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }
    }
}