namespace CAI.Services.Base
{
    using System;
    using Common.CustomExceptions;
    using Data.Abstraction;
    using Data.Filtering;
    using Data.Models;

    public abstract class BaseService : IDisposable
    {
        private readonly IUnitOfWork _data;

        protected BaseService(IUnitOfWork data)
        {
            this._data = data;
        }

        protected IUnitOfWork Data { get => this._data;  }

        protected Bot FindBotById(long id)
        {
            var bot = this.Data.BotRepository.FindById(id);

            if (bot == null)
            {
                throw new NotFoundException("Bot");
            }

            return bot;
        }

        protected Intention FindIntentionById(long id)
        {
            var intention = this.Data.IntentionRepository.FindById(id);

            if (intention == null)
            {
                throw new NotFoundException("Intention");
            }

            return intention;
        }

        protected NeuralNetworkData FindNeuralNetworkDataById(long id)
        {
            var bot = this.Data.NeuralNetworkDataRepository.FindById(id);

            if (bot == null)
            {
                throw new NotFoundException("Neural Network Data");
            }

            return bot;
        }

        //protected void CheckBotForExistingName(string name)
        //{
        //    if (this.Data.BotRepository.FindFirstByFilter(new BotFilter { Name = name }) != null)
        //    {
        //        throw new ExistingObjectException("Bot", "Use different name!");
        //    }
        //}

        public void Dispose()
        {
            this._data.Dispose();
        }
    }
}
