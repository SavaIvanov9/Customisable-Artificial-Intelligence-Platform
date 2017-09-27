namespace CAI.Data.Abstraction
{
    using CAI.Data.Models;

    public interface IUnitOfWork
    {
        IRepository<Bot> BotRepository { get; }

        int SaveChanges();
    }
}
