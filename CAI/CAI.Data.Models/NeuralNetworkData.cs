namespace CAI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abstraction;

    public class NeuralNetworkData : DataModel
    {
        [MaxLength]
        public string Data { get; set; }

        [Required]
        [ForeignKey("Bot")]
        public long BotId { get; set; }

        [Required]
        public Bot Bot { get; set; }

        [Required]
        public string Type { get; set; }
    }
}