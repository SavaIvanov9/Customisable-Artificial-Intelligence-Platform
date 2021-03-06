﻿namespace CAI.Services
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
    using Models.TrainingData;

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
                ActivationKeys = intention.ActivationKeys
                    .Where(a => a.IsDeleted == false)
                    .Select(a => new ActivationKeyViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        CreatedOn = a.CreatedOn,
                        ModifiedOn = a.ModifiedOn
                    }),
                TrainingData = intention.TrainingData
                    .Where(a => a.IsDeleted == false)
                    .Select(a => new TrainingDataViewModel()
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IntentionId = intention.Id,
                        CreatedOn = a.CreatedOn,
                        ModifiedOn = a.ModifiedOn
                    })
            };
        }

        public long RegisterIntention(IntentionCreateModel model, string createdBy)
        {
            var bot = this.FindBotById(model.BotId);

            var intention = new Intention()
            {
                CreatedBy = createdBy,
                Name = model.Name,
                BotId = bot.Id,
                Bot = bot
            };

            this.Data.IntentionRepository.Add(intention);
            this.Data.SaveChanges();

            return intention.Id;
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