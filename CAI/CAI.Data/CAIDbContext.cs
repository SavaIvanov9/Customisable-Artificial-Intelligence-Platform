namespace CAI.Data
{
    using System.Data.Entity;
    using System.Net;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class CaiDbContext : IdentityDbContext<User> /*,DbContext*/, ICaiDbContext
    {
        //public static CaiDbContext Create()
        //{
        //    return new CaiDbContext();
        //}

        public CaiDbContext() : base("CAIData", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CaiDbContext, Configuration>());

            //Database.SetInitializer(new DropCreateDatabaseAlways<CaiDbContext>());
        }

        public virtual IDbSet<Bot> Bots { get; set; }

        public virtual IDbSet<Intention> Intentions { get; set; }

        public virtual IDbSet<ActivationKey> ActivationKeys { get; set; }

        public virtual IDbSet<NeuralNetworkData> NeuralNetworkDatas { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<NeuralNetworkData>()
        //        .HasRequired(w => w.Bot)
        //        .WithMany()
        //        .Map(m => m.MapKey("Id"));
        //}
    }
}