namespace CAI.Data.Filtering
{
    using Base;
    using Common.Enums;

    public class BotFilter : DataFilter
    {
        public string Name { get; set; }

        public BotType? BotType { get; set; }
    }
}
