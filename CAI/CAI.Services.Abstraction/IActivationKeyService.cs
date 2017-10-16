namespace CAI.Services.Abstraction
{
    using Models.ActivationKey;

    public interface IActivationKeyService
    {
        ActivationKeyViewModel FindKey(long id);

        //long RegisterNewBot(BotCreateModel model, string createdBy);

        bool EditKey(ActivationKeyViewModel model, string modifiedBy);

        bool DeleteKey(long id, string deletedBy);
    }
}
