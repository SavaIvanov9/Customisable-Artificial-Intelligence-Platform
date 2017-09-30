using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI.Data.Importer
{
    using Abstraction;
    using Common.Enums;
    using Models;
    using Services;
    using Services.Abstraction;

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
                //this.DisplayDbStatus(db);
            }
        }

        private void AddTestData(IUnitOfWork db)
        {
            Console.WriteLine("Seeding test data...");

            var testBots = this.GenerateTestBots(10);
            foreach (var bot in testBots)
            {
                db.BotRepository.Add(bot);
            }

            Console.WriteLine("Seeding test data done.");
            Console.WriteLine($"Save changes result: {db.SaveChanges()}");
            Console.WriteLine();
        }

        private IEnumerable<Bot> GenerateTestBots(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Bot()
                {
                    CreatedBy = $"Test User {i}",
                    Name = $"Test Name {i}",
                    Type = BotType.Production
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
