namespace CAI.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
    using Common;
    using Common.Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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
            this.SeedDefaultUsers(context);
            //this.SeedDefaultBots(context);
            //this.SeedTestBots(context, 10);

            context.SaveChanges();
        }

        private void SeedDefaultUsers(CaiDbContext context)
        {
            const string administratorUserName = GlobalConstants.AdminUsername;
            const string administratorPassword = administratorUserName;

            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                // Create client role
                var roleClient = new IdentityRole { Name = UserRoleType.Client.ToString() };
                roleManager.Create(roleClient);

                // Create admin role
                var roleAdmin = new IdentityRole { Name = UserRoleType.Admin.ToString() };
                roleManager.Create(roleAdmin);

                // Create admin user
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User { UserName = administratorUserName, Email = administratorUserName };
                userManager.Create(user, administratorPassword);

                // Assign user to admin role
                userManager.AddToRole(user.Id, UserRoleType.Admin.ToString());
            }
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
            if (!context.Bots.Any(b => b.EnvironmentType == EnvironmentType.Test.ToString()))
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
                //new Bot
                //{
                //    Name = "Intention Recognizer Test Bot",
                //    EnvironmentType = EnvironmentType.Test.ToString(),
                //    BotType = BotType.IntentionRecognizer.ToString(),
                //    CreatedOn = DateTime.Now,
                //    CreatedBy = UserRoleType.Admin.ToString(),
                //    IsDeleted = false,
                //    Intentions = this.GenerateSampleIntentions()

                //},
                new Bot
                {
                    Name = "Introduction Bot",
                    EnvironmentType = EnvironmentType.Test.ToString(),
                    BotType = BotType.InformationFinder.ToString(),
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserRoleType.Admin.ToString(),
                    IsDeleted = false,
                    //Intentions = this.GenerateSampleIntentions()
                    NeuralNetworkDatas = new List<NeuralNetworkData>()
                    {
                        new NeuralNetworkData()
                        {
                            CreatedOn = DateTime.Now,
                            CreatedBy = UserRoleType.Admin.ToString(),
                            Type = NeuralNetworkType.Test.ToString(),
                            Data =  Encoding.ASCII.GetBytes("test")
                        }
                    }
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
                    BotType = i % 2 == 0 ? BotType.IntentionRecognizer.ToString() : BotType.InformationFinder.ToString(),
                    EnvironmentType = EnvironmentType.Production.ToString()
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
