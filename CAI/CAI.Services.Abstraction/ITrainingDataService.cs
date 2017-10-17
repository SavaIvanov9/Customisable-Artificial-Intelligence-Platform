namespace CAI.Services.Abstraction
{
    using Models.TrainingData;

    public interface ITrainingDataService
    {
        TrainingDataViewModel Find(long id);

        long Register(TrainingDataCreateModel model, string createdBy);

        bool Edit(TrainingDataViewModel model, string modifiedBy);

        bool Delete(long id, string deletedBy);

    }
}
