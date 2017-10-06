namespace CAI.Data.Abstraction
{
    using System.Threading.Tasks;
    using CAI.Data.Models;
    using Repositories;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        IBotRepository BotRepository { get; }

        IIntentionRepository IntentionRepository { get; }

        IActivationKeyRepository ActivationKeyRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
