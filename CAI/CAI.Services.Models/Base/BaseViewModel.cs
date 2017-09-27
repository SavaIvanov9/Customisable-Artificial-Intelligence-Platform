namespace CAI.Services.Models.Base
{
    using System;

    public abstract class BaseViewModel
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
