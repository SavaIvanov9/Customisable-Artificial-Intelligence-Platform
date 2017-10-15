namespace CAI.Web.Controllers
{
    using System.Linq;
    using Services.Abstraction;
    using System.Web.Mvc;
    using Common.Enums;
    using Data.Filtering;
    using Models.Home;

    public class HomeController : Controller
    {
        private readonly IBotService _botService;
        private readonly IDefaultBotsService _defaultBotsService;

        public HomeController(IBotService botService, IDefaultBotsService defaultBotsService)
        {
            this._botService = botService;
            this._defaultBotsService = defaultBotsService;
        }

        public ActionResult Index()
        {
            this._defaultBotsService.InitDefaultBots();

            var defaultBots = this._botService.GetAllBotsByFilter(
                new BotFilter() {EnvironmentType = EnvironmentType.Test.ToString(), IsDeleted = false});

            var homeModel = new HomeViewModel()
            {
                DefaultBots = defaultBots.ToList()
            };

            return View(homeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}