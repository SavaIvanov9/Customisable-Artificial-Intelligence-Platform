namespace CAI.Data
{
    using System.Data.Entity;
    using System.Net;
    using Migrations;
    using Models;

    public class CaiDbContext : DbContext, ICaiDbContext
    {
        public CaiDbContext() : base("CAIData")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CaiDbContext, Configuration>());

            //Database.SetInitializer(new DropCreateDatabaseAlways<ManagementSystemDbContext>());
        }

        public virtual IDbSet<Bot> Bots { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}