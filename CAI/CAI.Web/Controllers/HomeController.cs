namespace CAI.Web.Controllers
{
    using Common.Enums;
    using CustomAttributes;
    using Data.Filtering;
    using Models.Home;
    using Services.Abstraction;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;

    public class HomeController : Controller
    {
        private readonly IBotService _botService;
        private readonly IDefaultBotsService _defaultBotsService;

        public HomeController(IBotService botService, IDefaultBotsService defaultBotsService)
        {
            this._botService = botService;
            this._defaultBotsService = defaultBotsService;
        }

        [OutputCacheLongLived]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult CachedSampleBots()
        {
            this._defaultBotsService.InitDefaultBots();

            var defaultBots = this._botService.GetAllBotsByFilter(
                new BotFilter() { EnvironmentType = EnvironmentType.Test.ToString(), IsDeleted = false });

            var homeModel = new SampleBotsViewModel()
            {
                DefaultBots = defaultBots.ToList(),
                Date = DateTime.Now
            };

            return this.PartialView("_SampleBotsPartial", homeModel);
        }

        public ActionResult About()
        {
            this.ViewBag.Message = $"Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}