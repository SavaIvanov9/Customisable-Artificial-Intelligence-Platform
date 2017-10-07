namespace CAI.Data.Repositories
{
    using Abstraction;
    using Data.Abstraction.Repositories;
    using Models;

    public class NeuralNetworkDataRepository : DataRepository<NeuralNetworkData>, INeuralNetworkDataRepository
    {
        public NeuralNetworkDataRepository(ICaiDbContext context)
            : base(context)
        {
        }
    }
}