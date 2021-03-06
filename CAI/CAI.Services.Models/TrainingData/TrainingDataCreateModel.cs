﻿namespace CAI.Services.Models.TrainingData
{
    using ActivationKey;
    using Intention;
    using System.Collections.Generic;
    using Base;

    public class TrainingDataCreateModel : BaseViewModel
    {
        //public ICollection<ActivationKeyCreateModel> ActivationKeys { get; set; }
        public string Content { get; set; }

        public long IntentionId { get; set; }
    }
}
