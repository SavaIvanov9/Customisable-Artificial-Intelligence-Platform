namespace CAI.Web.Controllers
{
    using CAI.Services.Abstraction;
    using System.Web.Mvc;
    using Models;
    using Services.Models.Bot;

    public class BotController : Controller
    {
        private readonly IBotService _botService;

        public BotController(IBotService botService)
        {
            this._botService = botService;
        }

        public ActionResult Index()
        {
            var bots = this._botService.GetAllBots();

            return View(bots);
        }
        
        public ActionResult Create()
        {
            //ViewData["MeasuringUnitId"] = new SelectList(
            //    this.Data.MeasuringUnits
            //        .All()
            //        .Where(x => x.IsDeleted != true)
            //        .ToList(),
            //    "Id", "Name");

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(BotCreateModel model)
        {
            if (!ModelState.IsValid)
            {

            }

            this._botService.RegisterNewBot(model, this.User.Identity.Name);
            return RedirectToAction("Index");
        }

        [HttpPut]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(BotCreateModel model, long id)
        {
            if (!ModelState.IsValid)
            {

            }

            this._botService.EditBot(model, id, this.User.Identity.Name);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
            {

            }

            this._botService.DeleteBot(id, this.User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}