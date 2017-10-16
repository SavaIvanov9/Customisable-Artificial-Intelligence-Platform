namespace CAI.Web.Tests.Controllers
{
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Home;
    using Moq;
    using NUnit.Framework;
    using Services.Abstraction;
    using Web.Controllers;
    using Assert = NUnit.Framework.Assert;

    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _controller;
        private Mock<IBotService> _botService;
        private Mock<IDefaultBotsService> _defaultBotsService;

        public HomeControllerTest()
        {
            this._botService = new Mock<IBotService>();
            this._defaultBotsService = new Mock<IDefaultBotsService>();
            this._controller = new HomeController(this._botService.Object, this._defaultBotsService.Object);
        }

        [SetUp]
        public void SetUp()
        {
            this._botService = new Mock<IBotService>();
            this._defaultBotsService = new Mock<IDefaultBotsService>();
            this._controller = new HomeController(this._botService.Object, this._defaultBotsService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this._controller.Dispose();
        }

        [TestMethod]
        public void IndexShouldReturnNotNull()
        {
            var result = this._controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexShouldReturnCorrectModelType()
        {
            var result = this._controller.Index() as ViewResult;
            var model = result.ViewData.Model as HomeViewModel;

            Assert.AreEqual(typeof(HomeViewModel), model.GetType());
        }

        [TestMethod]
        public void About()
        {
            ViewResult result = this._controller.About() as ViewResult;

            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            ViewResult result = this._controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
