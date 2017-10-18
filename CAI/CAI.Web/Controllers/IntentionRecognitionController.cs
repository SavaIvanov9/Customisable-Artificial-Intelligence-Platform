namespace CAI.Web.Controllers
{
    using Data.Filtering;
    using Services.Abstraction;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Common.CustomExceptions;
    using Common.Enums;
    using Microsoft.AspNet.Identity;
    using Models.IntentionRecognition;
    using Services.Models.Bot;

    public class IntentionRecognitionController : Controller
    {
        private readonly IIntentionRecognitionService _intentionRecognitionService;
        private readonly IBotService _botService;

        public IntentionRecognitionController(IBotService botService,
            IIntentionRecognitionService intentionRecognitionService)
        {
            this._intentionRecognitionService = intentionRecognitionService;
            this._botService = botService;
        }

        [Authorize]
        public ActionResult Index()
        {
            var filter = new BotFilter() { IsDeleted = false, UserId = this.User.Identity.GetUserId() };
            var data = this._botService.GetAllBotsByFilter(filter);

            return View(data);
        }

        [Authorize]
        public ActionResult Details(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var bot = this._botService.FindBot(id.Value);

                return View(bot);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image")] BotCreateModel bot)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    bot.BotType = BotType.IntentionRecognizer;
                    bot.EnvironmentType = EnvironmentType.Production;
                    bot.UserId = this.User.Identity.GetUserId();
                    this._intentionRecognitionService.RegisterNewIntentionRecognitionBot(bot, this.User.Identity.Name);

                    return RedirectToAction("Index");
                }

                return View(bot);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        public ActionResult Edit(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var bot = this._botService.FindBot(id.Value);

                return View(bot);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image")] BotViewModel bot)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._botService.EditBot(bot, this.User.Identity.Name);

                    return RedirectToAction("Index");
                }

                return View(bot);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        public ActionResult Delete(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var bot = this._botService.FindBot(id.Value);

                return View(bot);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        public ActionResult TrainBot(long botId)
        {
            try
            {
                this._intentionRecognitionService.TrainIntentionRecognitionBot(botId);

                this.ViewBag.Message = "Training done";

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                if (!this._botService.DeleteBot(id, this.User.Identity.Name))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict);
                }

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        public ActionResult Chat(long? botId)
        {
            if (botId == null || botId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var bot = this._botService.FindBot(botId.Value);

                if (bot == null)
                {
                    return HttpNotFound();
                }

                return View(new ChatViewModel { Bot = bot });
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._intentionRecognitionService.Dispose();
                this._botService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
