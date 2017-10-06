namespace CAI.Data.Importer
{
    using Abstraction;
    using Common.Enums;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ImporterEngine
    {
        public ImporterEngine()
        {
        }

        public void Start()
        {
            using (IUnitOfWork db = new UnitOfWork())
            {
                this.DisplayDbStatus(db);
                //this.AddTestData(db);
                //this.AddTestData(db);
                //this.DisplayDbStatus(db);
            }
        }

        private void AddTestData(IUnitOfWork db)
        {
            Console.WriteLine("Seeding test data...");

            var testBots = this.GenerateSampleBots(10);
            foreach (var bot in testBots)
            {
                db.BotRepository.Add(bot);
            }

            Console.WriteLine("Seeding test data done.");
            Console.WriteLine($"Save changes result: {db.SaveChanges()}");
            Console.WriteLine();
        }

        private IEnumerable<Bot> GenerateSampleBots(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Bot()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = i % 2 == 0 ? "Test User 1" : "Test User 2",
                    Name = $"Test Name {i}",
                    BotType = i % 2 == 0 ? BotType.IntentionRecognizer : BotType.InformationFinder,
                    EnvironmentType = EnvironmentType.Production
                };
            }
        }

        private void DisplayDbStatus(IUnitOfWork db)
        {
            Console.WriteLine("Database status:");
            Console.WriteLine($" >Bots count: {db.BotRepository.All().Count()}");
            Console.WriteLine();
        }
    }
}
