namespace CAI.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Abstraction;

    public class Bot : DataModel
    {
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
