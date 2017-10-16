namespace CAI.Services
{
    using System;
    using System.Collections.Generic;
    using Abstraction;
    using Base;
    using Data.Abstraction;
    using Models.ActivationKey;

    public class ActivationKeyService : BaseService, IActivationKeyService
    {
        public ActivationKeyService(IUnitOfWork data) : base(data)
        {
        }

        public ActivationKeyViewModel FindKey(long id)
        {
            var key = base.FindKeyById(id);

            return new ActivationKeyViewModel()
            {
                Id = key.Id,
                Name = key.Name,
                CreatedOn = key.CreatedOn,
                ModifiedOn = key.ModifiedOn,
            };
        }

        public bool EditKey(ActivationKeyViewModel model, string modifiedBy)
        {
            var key = base.FindKeyById(model.Id);

            key.Name = model.Name;
            key.ModifiedBy = modifiedBy;

            this.Data.ActivationKeyRepository.Update(key);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }

        public bool DeleteKey(long id, string deletedBy)
        {
            var key = base.FindKeyById(id);

            key.DeletedBy = deletedBy;
            this.Data.ActivationKeyRepository.Delete(key);

            return Convert.ToBoolean(this.Data.SaveChanges());
        }
    }
}
