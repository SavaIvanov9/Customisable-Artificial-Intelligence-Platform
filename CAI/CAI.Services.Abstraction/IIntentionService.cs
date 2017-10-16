namespace CAI.Services.Abstraction
{
    using System;
    using Models.ActivationKey;
    using Models.Intention;

    public interface IIntentionService : IDisposable
    {
        IntentionViewModel FindIntention(long id);

        long RegisterIntention(IntentionCreateModel model, string createdBy);

        bool EditIntention(IntentionViewModel model, string modifiedBy);

        bool DeleteIntention(long id, string deletedBy);
    }
}
