namespace CAI.Data.Models
{
    using Abstraction;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActivationKey : DataModel
    {
        [Required]
        [Index("Name", IsUnique = true)]
        public string Name { get; set; }
    }
}
