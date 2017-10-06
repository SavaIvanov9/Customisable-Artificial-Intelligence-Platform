﻿namespace CAI.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abstraction;
    using Common.Enums;

    public class Bot : DataModel
    {
        //[Index(IsUnique = true)]
        [MaxLength(20, ErrorMessage = "Bot's Name must be between 20 and 5 chacters long"), MinLength(5)]
        public string Name { get; set; }

        [Required]
        public BotType BotType { get; set; }

        [Required]
        public EnvironmentType EnvironmentType { get; set; }

    }
}
