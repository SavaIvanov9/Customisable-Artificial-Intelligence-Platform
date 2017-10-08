﻿namespace CAI.ConsoleTestGround
{
    using System;
    using System.Linq;
    using Common.Enums;
    using Data;
    using Data.Abstraction;
    using Data.Filtering;
    using Modules;
    using Services;
    using Services.Bots;

    public class TestGroundEngine
    {
        public void Start()
        {
            //var module = new NeuralNetworkModule();
            var module = new NLPModule();

            module.Start();
        }

        private void TestDb()
        {
            using (IUnitOfWork db = new UnitOfWork())
            {
                //this.TestBotService(db);
                this.TestFiltering(db);
                Console.WriteLine(DateTime.Now.ToLocalTime());
            }
        }

        private void TestFiltering(IUnitOfWork db)
        {
            var bots = db.BotRepository.FindAllByFilter(new BotFilter { IsDeleted = false});

            Console.WriteLine(bots.Count());
        }

        private void TestBotService(IUnitOfWork db)
        {
            var service = new BotService(db);

            //var bots = service

            Console.WriteLine();
        }
    }
}
