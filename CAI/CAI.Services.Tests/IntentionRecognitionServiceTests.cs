namespace CAI.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.CustomExceptions;
    using Common.Enums;
    using Data.Abstraction;
    using Data.Models;
    using DeepLearning;
    using FizzWare.NBuilder;
    using Models.Bot;
    using Models.Intention;
    using Models.User;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class IntentionRecognitionServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private IntentionRecognitionService _service;

        [TearDown]
        public void TearDown()
        {
            this._service.Dispose();
        }

        [Test]
        public void RegisterNewIntentionRecognitionBot_ShouldReturnCorrectDataType()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == "id")
                .Build();
      
            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.Add(It.IsAny<Bot>()));

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            var bot = new BotCreateModel()
            {
                UserId = user.Id
            };

            var result = this._service.RegisterNewIntentionRecognitionBot(bot, "test");

            Assert.AreEqual(typeof(long), result.GetType());
        }

        [Test]
        public void RegisterNewIntentionRecognitionBot_ShouldTrowCorrectExceptionWithNoData()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == "id")
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById("id"))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.Add(It.IsAny<Bot>()));

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            var bot = new BotCreateModel()
            {
                UserId = "notThisId"
            };

            Assert.Throws<NotFoundException>(() => this._service.RegisterNewIntentionRecognitionBot(bot, "test"));

        }

        [Test]
        public void RegisterNewIntentionRecognitionBot_ShouldTrowCorrectExceptionWithIncorrectInput()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById("id"))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.Add(It.IsAny<Bot>()));

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            var bot = new BotCreateModel()
            {
                UserId = null
            };

            Assert.Throws<ArgumentException>(() => this._service.RegisterNewIntentionRecognitionBot(bot, "test"));
        }

        [Test]
        public void TrainIntentionRecognitionBot_ShouldTrowCorrectExceptionWithIncorrectInput()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<ArgumentException>(() => this._service.TrainIntentionRecognitionBot(-2));
        }

        [Test]
        public void TrainIntentionRecognitionBot_ShouldTrowCorrectExceptionWithNoData()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<NotFoundException>(() => this._service.TrainIntentionRecognitionBot(2));
        }

        [Test]
        public void TrainIntentionRecognitionBotOverload_ShouldTrowCorrectExceptionWithIncorrectInput()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<ArgumentException>(() => this._service.TrainIntentionRecognitionBot(-2, new Dictionary<string, long>()));
        }

        [Test]
        public void TrainIntentionRecognitionBotOverload_ShouldTrowCorrectExceptionWithNoData()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<NotFoundException>(() => this._service.TrainIntentionRecognitionBot(2, new Dictionary<string, long>()));
        }

        [Test]
        public void TrainIntentionRecognitionBotOverload_ShouldTrowCorrectException_BotHasIncorrectType()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1 && x.BotType == BotType.InformationFinder.ToString())
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(It.IsAny<long>(), false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<NotMatchingTypeException>(() => this._service.TrainIntentionRecognitionBot(2, new Dictionary<string, long>()));
        }

        [Test]
        public void TrainIntentionRecognitionBot_ShouldTrowCorrectException_BotHasIncorrectType()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1 && x.BotType == BotType.InformationFinder.ToString())
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(It.IsAny<long>(), false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<NotMatchingTypeException>(() => this._service.TrainIntentionRecognitionBot(2));
        }

        [Test]
        public void RecognizeIntention_ShouldTrowCorrectException_BotHasNetworkWithIncorrectType()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1 && x.BotType == BotType.InformationFinder.ToString()
                    && x.NeuralNetworkDatas.Equals(Builder<NeuralNetworkData>.CreateListOfSize(1)
                    .TheFirst(1)
                    .With(n => n.Type == "incorrectType")
                    .All()
                    .Build()))
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<NotFoundException>(() => this._service.RecognizeIntention(bot.Id, "Test"));
        }

        [Test]
        public void RecognizeIntention_ShouldTrowCorrectException_BotHasIncorrectId()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1 && x.BotType == BotType.InformationFinder.ToString()
                           && x.NeuralNetworkDatas.Equals(Builder<NeuralNetworkData>.CreateListOfSize(1)
                               .TheFirst(1)
                               .With(n => n.Type == NeuralNetworkType.IntentionRecognition.ToString())
                               .All()
                               .Build()))
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<ArgumentException>(() => this._service.RecognizeIntention(-1, "Test"));
        }

        [Test]
        public void RecognizeMultipleIntentions_ShouldTrowCorrectException_BotHasNetworkWithIncorrectType()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1 && x.BotType == BotType.InformationFinder.ToString()
                           && x.NeuralNetworkDatas.Equals(Builder<NeuralNetworkData>.CreateListOfSize(1)
                               .TheFirst(1)
                               .With(n => n.Type == "incorrectType")
                               .All()
                               .Build()))
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<NotFoundException>(() => this._service.RecognizeMultipleIntentions(bot.Id, "Test"));
        }

        [Test]
        public void RecognizeMultipleIntentions_ShouldTrowCorrectException_BotHasIncorrectId()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == null)
                .Build();

            var bot = Builder<Bot>.CreateNew()
                .With(x => x.IsDeleted == false && x.Id == 1 && x.BotType == BotType.InformationFinder.ToString()
                           && x.NeuralNetworkDatas.Equals(Builder<NeuralNetworkData>.CreateListOfSize(1)
                               .TheFirst(1)
                               .With(n => n.Type == NeuralNetworkType.IntentionRecognition.ToString())
                               .All()
                               .Build()))
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.UserRepository.FindById(It.IsAny<string>()))
                .Returns(user);

            this._unitOfWork.Setup(x => x.BotRepository.FindById(1, false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new IntentionRecognitionService(this._unitOfWork.Object, null, null, null);

            Assert.Throws<ArgumentException>(() => this._service.RecognizeMultipleIntentions(-1, "Test"));
        }
    }
}
