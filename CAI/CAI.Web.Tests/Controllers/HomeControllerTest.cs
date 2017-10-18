namespace CAI.Web.Tests.Controllers
{
    using System.Web.Mvc;
    using Models.Home;
    using Moq;
    using NUnit.Framework;
    using Services.Abstraction;
    using Web.Controllers;

    [TestFixture]
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

        //    Assert.AreEqual(typeof(HomeViewModel), model.GetType());
        //}

        [Test]
        public void About()
        {
            ViewResult result = this._controller.About() as ViewResult;

            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            ViewResult result = this._controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
