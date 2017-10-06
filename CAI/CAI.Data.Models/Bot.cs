namespace CAI.Data.Models
{
    using Abstraction;
    using Common.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bot : DataModel
    {
        private ICollection<Intention> _intentions;

        public Bot()
        {
            this._intentions = new HashSet<Intention>();
        }

        [Index("Name", IsUnique = true)]
        [MaxLength(30, ErrorMessage = "Bot's Name must be between 30 and 2 chacters long"), MinLength(2)]
        public string Name { get; set; }

        [Required]
        public BotType BotType { get; set; }

        [Required]
        public EnvironmentType EnvironmentType { get; set; }

        public virtual ICollection<Intention> Intentions
        {
            get => this._intentions;
            set => this._intentions = value;
        }
    }
}
