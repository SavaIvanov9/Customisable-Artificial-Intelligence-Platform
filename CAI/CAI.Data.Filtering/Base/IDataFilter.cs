namespace CAI.Data.Filtering.Base
{
    using System;

    public interface IDataFilter
    {
        long? Id { get; set; }
        string CreatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        bool? IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
        string DeletedBy { get; set; }
    }
}
