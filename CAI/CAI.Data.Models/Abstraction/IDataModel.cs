namespace CAI.Data.Models.Abstraction
{
    using System;

    public interface IDataModel : IIndex, IAuditableModel
    {
        //string CreatedBy { get; set; }
        //DateTime CreatedOn { get; set; }

        //string ModifiedBy { get; set; }
        //DateTime? ModifiedOn { get; set; }

        //bool IsDeleted { get; set; }
        //DateTime? DeletedOn { get; set; }
        //string DeletedBy { get; set; }
    }
}
