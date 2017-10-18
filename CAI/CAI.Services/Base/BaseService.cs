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

        protected IUnitOfWork Data { get => this._data; }

        protected Bot FindBotById(long id)
        {
            this.ValidateId(id);

            var bot = this.Data.BotRepository.FindById(id);

            if (bot == null)
            {
                throw new NotFoundException("Bot");
            }

            return bot;
        }

        protected Intention FindIntentionById(long id)
        {
            this.ValidateId(id);

            var intention = this.Data.IntentionRepository.FindById(id);

            if (intention == null)
            {
                throw new NotFoundException("Intention");
            }

            return intention;
        }

        protected NeuralNetworkData FindNeuralNetworkDataById(long id)
        {
            this.ValidateId(id);

            var network = this.Data.NeuralNetworkDataRepository.FindById(id);

            if (network == null)
            {
                throw new NotFoundException("Neural Network Data");
            }

            return network;
        }

        protected ActivationKey FindKeyById(long id)
        {
            this.ValidateId(id);

            var key = this.Data.ActivationKeyRepository.FindById(id);

            if (key == null)
            {
                throw new NotFoundException("key");
            }

            return key;
        }

        protected User FindUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must benot null or empty");
            }

            var user = this.Data.UserRepository.FindById(id);

            if (user == null)
            {
                throw new NotFoundException("user");
            }

            return user;
        }

        protected TrainingData FindTrainingDataById(long id)
        {
            this.ValidateId(id);

            var data = this.Data.TrainingDataRepository.FindById(id);

            if (data == null)
            {
                throw new NotFoundException("TrainingData");
            }

            return data;

        }

        protected void ValidateId(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id must be greater than 0");
            }
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
