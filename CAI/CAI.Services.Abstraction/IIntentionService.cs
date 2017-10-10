﻿namespace CAI.Services.Abstraction
{
    using System;
    using Models.Intention;

    public interface IIntentionService : IDisposable
    {
        IntentionViewModel FindIntention(long id);

        //long RegisterNewBot(BotCreateModel model, string createdBy);

        bool EditIntention(IntentionViewModel model, long id, string modifiedBy);

        bool DeleteIntention(long id, string deletedBy);
    }
}
