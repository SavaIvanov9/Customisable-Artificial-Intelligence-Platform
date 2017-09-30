namespace CAI.Data.Importer
{
    class Launcher
    {
        static void Main(string[] args)
        {
            var engine = new ImporterEngine();
            engine.Start();
        }
    }
}
