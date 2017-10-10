﻿namespace CAI.Services.Abstraction
{
    using Data.Filtering;
    using Models.Bot;
    using Models.Intention;
    using System;
    using System.Collections.Generic;

    public interface IIntentionRecognitionService : IDisposable
    {
        long RegisterNewIntentionRecognitionBot(BotCreateModel model, string createdBy);

        bool TrainIntentionRecognitionBot(long id, Dictionary<string, long> data);

        IntentionViewModel RecognizeIntention(long botId, string inputText);
    }
}