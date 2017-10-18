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

            var homeModel = new HomeViewModel()
            {
                DefaultBots = defaultBots.ToList(),
                Date = DateTime.Now
            };

            //return View(homeModel);


            return this.PartialView("_SampleBotsPartial", homeModel);
        }

        //[OutputCache(Duration = 30, Location = OutputCacheLocation.Client)]
        [OutputCacheLongLived]
        public ActionResult About()
        {
            var r = new Random();
            ViewBag.Message = $"{r.Next()} Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}