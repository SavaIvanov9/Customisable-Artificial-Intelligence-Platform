namespace CAI.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abstraction;

    public class TrainingData : DataModel
    {
        private ICollection<ActivationKey> _activationKeys;

        public TrainingData()
        {
            this._activationKeys = new HashSet<ActivationKey>();
        }

        [Required]
        [ForeignKey("Intention")]
        public long IntentionId { get; set; }

        [Required]
        public Intention Intention { get; set; }

        public virtual ICollection<ActivationKey> ActivationKeys
        {
            get => this._activationKeys;
            set => this._activationKeys = value;
        }
    }
}
