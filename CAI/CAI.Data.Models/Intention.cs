namespace CAI.Data.Models
{
    using Abstraction;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Intention : DataModel
    {
        private ICollection<ActivationKey> _activationKeys;
        private ICollection<TrainingData> _trainingData;

        public Intention()
        {
            this._activationKeys = new HashSet<ActivationKey>();
            this._trainingData = new HashSet<TrainingData>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Bot")]
        public long BotId { get; set; }

        public Bot Bot { get; set; }

        public virtual ICollection<ActivationKey> ActivationKeys
        {
            get => this._activationKeys;
            set => this._activationKeys = value;
        }

        public virtual ICollection<TrainingData> TrainingData
        {
            get => this._trainingData;
            set => this._trainingData = value;
        }
    }
}
