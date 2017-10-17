namespace CAI.Services
{
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Data.Models;
    using Models.TrainingData;
    using System;

    public class TrainingDataService : BaseService, ITrainingDataService

    {
        public TrainingDataService(IUnitOfWork data) : base(data)
        {
        }

        public TrainingDataViewModel Find(long id)
        {
            var data = base.FindTrainingDataById(id);

            return new TrainingDataViewModel()
            {
                Id = data.Id,
                Content = data.Content,
                CreatedOn = data.CreatedOn,
                ModifiedOn = data.ModifiedOn,
            };
        }

        public long Register(TrainingDataCreateModel model, string createdBy)
        {
            var intention = base.FindIntentionById(model.IntentionId);

            var data = new TrainingData()
            {
                CreatedBy = createdBy,
                Content = model.Content,
                Intention = intention,
                IntentionId = intention.Id
            };

            this.Data.TrainingDataRepository.Add(data);
            this.Data.SaveChanges();

            return intention.Id;
        }

        public bool Edit(TrainingDataViewModel model, string modifiedBy)
        {
            var data = base.FindTrainingDataById(model.Id);

            data.Content = model.Content;
            data.ModifiedBy = modifiedBy;

            this.Data.TrainingDataRepository.Update(data);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        public bool Delete(long id, string deletedBy)
        {
            var data = base.FindTrainingDataById(id);

            data.DeletedBy = deletedBy;
            this.Data.TrainingDataRepository.Delete(data);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }
    }
}
