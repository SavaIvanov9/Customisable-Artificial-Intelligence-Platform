namespace CAI.Data.Models
{
    using Abstraction;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActivationKey : DataModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Intention")]
        public long IntentionId { get; set; }

        [Required]
        public Intention Intention { get; set; }
    }
}
