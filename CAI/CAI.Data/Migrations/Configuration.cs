namespace CAI.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Common.Enums;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<CaiDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(CaiDbContext context)
        {
            this.SeedDefaultBots(context);
            this.SeedTestBots(context, 10);

            context.SaveChanges();
        }

        private void SeedDefaultBots(CaiDbContext context)
        {
            var defaultBots = this.GenerateDefaultBots();

            foreach (var bot in defaultBots)
            {
                if (!context.Bots.Any(b => b.Name == bot.Name
                                           && b.BotType == bot.BotType
                                           && b.CreatedBy == bot.CreatedBy
                                           && b.EnvironmentType == bot.EnvironmentType
                                           && !b.IsDeleted))
                {
                    context.Bots.Add(bot);
                }
            }
        }

        private void SeedTestBots(CaiDbContext context, int count)
        {
            if (!context.Bots.Any(b => b.EnvironmentType == EnvironmentType.Test))
            {
                var testBots = this.GenerateSampleBots(count);

                foreach (var bot in testBots)
                {
                    context.Bots.Add(bot);
                }
            }
        }

        private IEnumerable<Bot> GenerateDefaultBots()
        {
            return new[]
            {
                new Bot
                {
                    Name = "Intention Recognizer Test Bot",
                    EnvironmentType = EnvironmentType.Test,
                    BotType = BotType.IntentionRecognizer,
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserRoleType.Admin.ToString(),
                    IsDeleted = false,
                    Intentions = this.GenerateSampleIntentions()

                },
                new Bot
                {
                    Name = "Information Finder Test Bot",
                    EnvironmentType = EnvironmentType.Test,
                    BotType = BotType.InformationFinder,
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserRoleType.Admin.ToString(),
                    IsDeleted = false,
                    //Intentions = this.GenerateSampleIntentions()
                }
            };
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

        private ICollection<Intention> GenerateSampleIntentions()
        {
            return new HashSet<Intention>
            {
                new Intention()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserRoleType.Admin.ToString(),
                    Name = "Love pizza",
                    ActivationKeys = this.GenerateSampleActivationKeys(new [] {"i", "like", "pizza", "eat", "lunch", "every", "day"})
                },
                new Intention()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserRoleType.Admin.ToString(),
                    Name = "Introduction",
                    ActivationKeys = this.GenerateSampleActivationKeys(new [] {"i", "am", "name", "is", "hello", "hi", "how", "are", "you", "who", "what"})
                },
            };
        }

        private ICollection<ActivationKey> GenerateSampleActivationKeys(string[] values)
        {
            var result = new HashSet<ActivationKey>();

            foreach (var value in values)
            {
                result.Add(new ActivationKey()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserRoleType.Admin.ToString(),
                    Name = value
                });
            }

            return result;
        }
    }
}
