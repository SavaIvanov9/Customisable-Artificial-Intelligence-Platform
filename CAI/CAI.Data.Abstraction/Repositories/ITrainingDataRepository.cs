namespace CAI.Data.Abstraction.Repositories
{
    using Models;
    using System.Collections.Generic;

    public interface ITrainingDataRepository : IDataRepository<TrainingData>
    {
        IEnumerable<TrainingData> FindByIntention(long intentionId);
    }
}
