namespace CAI.Data.Abstraction
{
    using System.Threading.Tasks;
    using Repositories;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IBotRepository BotRepository { get; }

        IIntentionRepository IntentionRepository { get; }

        IActivationKeyRepository ActivationKeyRepository { get; }

        INeuralNetworkDataRepository NeuralNetworkDataRepository { get; }

        ITrainingDataRepository TrainingDataRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
