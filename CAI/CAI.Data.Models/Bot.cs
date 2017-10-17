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
        private ICollection<NeuralNetworkData> _neuralNetworkDatas;
        private ICollection<TrainingData> _trainingDatas;

        public Bot()
        {
            this._intentions = new HashSet<Intention>();
            this._neuralNetworkDatas = new HashSet<NeuralNetworkData>();
            this._trainingDatas = new HashSet<TrainingData>();
        }

        [Index("Name", IsUnique = true)]
        [MaxLength(30, ErrorMessage = "Bot's Name must be between 30 and 2 chacters long"), MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string BotType { get; set; }

        [Required]
        public string EnvironmentType { get; set; }

        public string Image { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<Intention> Intentions
        {
            get => this._intentions;
            set => this._intentions = value;
        }

        public virtual ICollection<NeuralNetworkData> NeuralNetworkDatas
        {
            get => this._neuralNetworkDatas;
            set => this._neuralNetworkDatas = value;
        }

        public virtual ICollection<TrainingData> TrainingDatas
        {
            get => this._trainingDatas;
            set => this._trainingDatas = value;
        }
    }
}
