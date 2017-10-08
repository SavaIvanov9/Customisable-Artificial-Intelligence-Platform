namespace CAI.Services
{
    using System.Collections.Generic;
    using Base;
    using Data.Abstraction;
    using Models.ActivationKey;

    public class ActivationKeyService : BaseService
    {
        public ActivationKeyService(IUnitOfWork data) : base(data)
        {
        }

        //public IEnumerable<ActivationKeyViewModel> FindKeys(string[] values)
        //{
        //    this.Data.ActivationKeyRepository.
        //}
    }
}
