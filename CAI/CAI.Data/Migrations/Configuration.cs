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
                                           && b.Type == bot.Type
                                           && b.CreatedBy == bot.CreatedBy
                                           && !b.IsDeleted))
                {
                    context.Bots.Add(bot);
                }
            }
        }

        private void SeedTestBots(CaiDbContext context, int count)
        {
            if (!context.Bots.Any(b => b.Type == BotType.Test))
            {
                var testBots = this.GenerateTestBots(count);

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
                    Name = "Information Test Bot",
                    Type = BotType.System,
                    CreatedOn = DateTime.Now,
                    CreatedBy = BotType.System.ToString(),
                    IsDeleted = false
                },
                new Bot
                {
                    Name = "Library Test Bot",
                    Type = BotType.Test,
                    CreatedOn = DateTime.Now,
                    CreatedBy = BotType.System.ToString(),
                    IsDeleted = false
                }
            };
        }

        private IEnumerable<Bot> GenerateTestBots(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Bot()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = $"Test User {i}",
                    Name = $"Test Name {i}",
                    Type = BotType.Production
                };
            }
        }
    }
}
