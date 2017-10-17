namespace CAI.Services.Tests.BotService
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Abstraction;
    using Data.Models;
    using FizzWare.NBuilder;
    using Models.User;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class UserServiceTest
    {
        private IList<User> _controlData;
        private Mock<IUnitOfWork> _unitOfWork;
        private UserService _service;

        public UserServiceTest()
        {
            this._controlData = this.GenerateUsers(10);
            this.SetUp();
        }

        [SetUp]
        public void SetUp()
        {
            this._unitOfWork = new Mock<IUnitOfWork>();
            this._service = new UserService(this._unitOfWork.Object);

            this._unitOfWork.Setup(x => x.UserRepository.All())
                .Returns(this._controlData.AsEnumerable());

            this._unitOfWork.Setup(x => x.SaveChanges())
                .Returns(1);
        }

        private IList<User> GenerateUsers(int count)
        {
            return Builder<User>.CreateListOfSize(count)
                .All()
                .With(x => x.Id != null && x.Email != null &&
                    x.PasswordHash != null && x.UserName != null)
                .Build();
        }

        [Test]
        public void GetAllUsersShouldReturnNotNullWhenThereIsdata()
        {
            var result = this._service.GetAllUsers();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAllUsersShouldReturnCorrectDataType()
        {
            var result = this._service.GetAllUsers();
            var expectedType = typeof(IEnumerable<UserViewModel>);

            Assert.IsInstanceOf(expectedType, result);
        }

        [Test]
        public void GetAllUsersShouldReturnCorrectData()
        {
            var result = this._service.GetAllUsers().ToList();

            for (int i = 0; i < result.Count(); i++)
            {
                var props = typeof(UserViewModel).GetProperties();

                foreach (var prop in props)
                {
                    Assert.AreEqual(prop.GetValue(result[i]), prop.GetValue(this._controlData[i]));    
                }
            }
        }
    }
}
