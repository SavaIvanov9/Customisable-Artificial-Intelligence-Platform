namespace CAI.Data.Abstraction
{
    using CAI.Data.Models;
    using CAI.Data.Repositories.Abstraction;

    public interface IUnitOfWork
    {
        IRepository<Bot> BotRepository { get; }

        int SaveChanges();
    }
}
