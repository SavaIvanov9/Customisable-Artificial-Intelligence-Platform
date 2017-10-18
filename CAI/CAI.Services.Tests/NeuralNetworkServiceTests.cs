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
    using Models.User;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class NeuralNetworkServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private NeuralNetworkService _service;

        [TearDown]
        public void TearDown()
        {
            this._service.Dispose();
        }

        [Test]
        public void GenerateNetwork_ShouldReturnCorrectDataType()
        {
            var bots = Builder<Bot>.CreateListOfSize(10)
                .TheFirst(1)
                .With(x => x.Id = 1)
                .All()
                .With(x => x.Id > 0 && x.IsDeleted == false)
                .Build();

            var networks = Builder<NeuralNetworkData>.CreateListOfSize(10)
                .All()
                .With(x => x.Id > 0)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.NeuralNetworkDataRepository.All())
                .Returns(networks.AsEnumerable());

            this._unitOfWork.Setup(x => x.BotRepository.All())
                .Returns(bots.AsEnumerable());

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new NeuralNetworkService(this._unitOfWork.Object);

            var result = this._service.GenerateNetwork(1, 1, false);

            Assert.AreEqual(typeof(NeuralNetwork), result.GetType());
        }

        [Test]
        public void GenerateNetwork_ShouldReturnNoTNull()
        {
            var bots = Builder<Bot>.CreateListOfSize(10)
                .TheFirst(1)
                .With(x => x.Id = 1)
                .All()
                .With(x => x.Id > 0 && x.IsDeleted == false)
                .Build();

            var networks = Builder<NeuralNetworkData>.CreateListOfSize(10)
                .All()
                .With(x => x.Id > 0)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.NeuralNetworkDataRepository.All())
                .Returns(networks.AsEnumerable());

            this._unitOfWork.Setup(x => x.BotRepository.All())
                .Returns(bots.AsEnumerable());

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new NeuralNetworkService(this._unitOfWork.Object);

            var result = this._service.GenerateNetwork(1, 1, false);

            Assert.IsNotNull(result);
        }

        [Test]
        public void RegisterNewNetwork_ShouldReturnCorrectDataType()
        {
            var bot = Builder<Bot>.CreateNew()
                .With(x => x.Id == 1 && x.IsDeleted == false)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.BotRepository.FindById(It.IsAny<long>(), false))
                .Returns(bot);

            this._unitOfWork.Setup(x => x.NeuralNetworkDataRepository.Add(It.IsAny<NeuralNetworkData>()));

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new NeuralNetworkService(this._unitOfWork.Object);

            var result = this._service.RegisterNewNetwork(new NeuralNetwork(1, 1, false), 1, "test", NeuralNetworkType.Test);

            Assert.IsNotNull(result);
        }

        [Test]
        public void RegisterNewNetwork_ShouldThrowCorrectException_IfIncorrectInput()
        {
            var bots = Builder<Bot>.CreateListOfSize(10)
                .TheFirst(1)
                .With(x => x.Id = 1)
                .All()
                .With(x => x.Id > 0 && x.IsDeleted == false)
                .Build();

            var networks = Builder<NeuralNetworkData>.CreateListOfSize(10)
                .All()
                .With(x => x.Id > 0)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.NeuralNetworkDataRepository.All())
                .Returns(networks.AsEnumerable());

            this._unitOfWork.Setup(x => x.BotRepository.All())
                .Returns(bots.AsEnumerable());

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new NeuralNetworkService(this._unitOfWork.Object);

            Assert.Throws<ArgumentException>(() => this._service.RegisterNewNetwork(new NeuralNetwork(1, 1, false), 0, "test", NeuralNetworkType.Test));
        }

        [Test]
        public void RegisterNewNetwork_ShouldThrowCorrectException_IfNoDataFound()
        {
            var bots = Builder<Bot>.CreateListOfSize(10)
                .TheFirst(1)
                .With(x => x.Id = 1)
                .All()
                .With(x => x.Id > 0 && x.Id < 99 && x.IsDeleted == false)
                .Build();

            var networks = Builder<NeuralNetworkData>.CreateListOfSize(10)
                .All()
                .With(x => x.Id > 0)
                .Build();

            this._unitOfWork = new Mock<IUnitOfWork>();

            this._unitOfWork.Setup(x => x.NeuralNetworkDataRepository.All())
                .Returns(networks.AsEnumerable());

            this._unitOfWork.Setup(x => x.BotRepository.All())
                .Returns(bots.AsEnumerable());

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);

            this._service = new NeuralNetworkService(this._unitOfWork.Object);

            Assert.Throws<NotFoundException>(() => this._service.RegisterNewNetwork(new NeuralNetwork(1, 1, false), 100, "test", NeuralNetworkType.Test));
        }
    }
}
