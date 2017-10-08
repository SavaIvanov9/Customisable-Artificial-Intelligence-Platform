namespace CAI.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstraction;
    using Data.Abstraction.Repositories;
    using Models;

    public class ActivationKeyRepository : DataRepository<ActivationKey>, IActivationKeyRepository
    {
        public ActivationKeyRepository(ICaiDbContext context)
            : base(context)
        {
        }

        public IEnumerable<ActivationKey> FindAllByNames(string[] names, bool isDeleted = false)
        {
            return this.Set
                .Where(x => names.Any(n => n == x.Name) && x.IsDeleted == isDeleted)
                .AsEnumerable<ActivationKey>();
        }
        
        //public IEnumerable<ActivationKey> FindAllByNames(long botId, bool isDeleted = false)
        //{
        //    return this.Set
        //        .Where(x => x && x.IsDeleted == isDeleted)
        //        .AsEnumerable<ActivationKey>();
        //}
    }
}

