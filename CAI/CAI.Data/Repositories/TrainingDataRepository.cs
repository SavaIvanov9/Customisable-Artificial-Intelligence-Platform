namespace CAI.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Data.Abstraction.Repositories;
    using Models;

    public class TrainingDataRepository : DataRepository<TrainingData>, ITrainingDataRepository
    {
        public TrainingDataRepository(ICaiDbContext context)
            : base(context)
        {
        }

        public IEnumerable<TrainingData> FindByIntention(long intentionId)
        {
            return this.Set.Where(x => x.IntentionId == intentionId).ToArray();
        }
    }
}
