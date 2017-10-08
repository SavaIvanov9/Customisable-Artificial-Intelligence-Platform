namespace CAI.Services.Models.ActivationKey
{
    using System.ComponentModel.DataAnnotations;

    public class ActivationKeyCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
