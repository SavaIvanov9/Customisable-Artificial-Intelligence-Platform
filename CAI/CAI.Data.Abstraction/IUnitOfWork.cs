namespace CAI.Data.Abstraction
{
    using System.Threading.Tasks;
    using CAI.Data.Models;
    using Repositories;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IBotRepository BotRepository { get; }

        IUserRepository UserRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
