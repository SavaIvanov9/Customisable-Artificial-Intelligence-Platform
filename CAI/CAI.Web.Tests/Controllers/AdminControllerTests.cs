namespace CAI.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Models.Home;
    using Moq;
    using NUnit.Framework;
    using Services.Abstraction;
    using Services.Models.User;
    using Web.Controllers;

    [TestFixture]
    public class AdminControllerTests
    {
        private AdminController _controller;
        private Mock<IUserService> _userService;

        public AdminControllerTests()
        {
            this.SetUp();
        }

        [SetUp]
        public void SetUp()
        {
            this._userService = new Mock<IUserService>();
            this._controller = new AdminController(this._userService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this._controller.Dispose();
        }

        [Test]
        public void IndexShouldReturnNotNull()
        {
            var result = this._controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        //[Test]
        //public void IndexShouldReturnCorrectModelType()
        //{
        //    var result = this._controller.Index() as ViewResult;
        //    var model = result.ViewData.Model as HomeViewModel;

        //    Assert.AreEqual(typeof(IEnumerable<UserViewModel>), model.GetType());
        //}

        //[Test]
        //public void EditShouldReturnNotNull()
        //{
        //    ViewResult result = this._controller.Edit("id") as ViewResult;

        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void DeleteShouldReturnNotNull()
        //{
        //    ViewResult result = this._controller.Delete("id") as ViewResult;

        //    Assert.IsNotNull(result);
        //}
    }
}
