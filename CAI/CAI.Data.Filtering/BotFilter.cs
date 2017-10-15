namespace CAI.Data.Filtering
{
    using Base;
    using Common.Enums;

    public class BotFilter : DataFilter
    {
        public string Name { get; set; }

        public string BotType { get; set; }

        public string EnvironmentType { get; set; }
    }
}
