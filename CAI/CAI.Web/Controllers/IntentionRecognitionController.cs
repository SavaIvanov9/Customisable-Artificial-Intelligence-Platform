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
            var filter = new BotFilter() { IsDeleted = false };
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
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: IntentionRecognition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image")] BotCreateModel bot)
        {
            if (this.ModelState.IsValid)
            {
                bot.BotType = BotType.IntentionRecognizer;
                bot.EnvironmentType = EnvironmentType.Production;
                this._intentionRecognitionService.RegisterNewIntentionRecognitionBot(bot, this.User.Identity.Name);

                return RedirectToAction("Index");
            }

            return View(bot);
        }

        [Authorize]
        // GET: IntentionRecognition/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bot = this._botService.FindBot(id.Value);

            if (bot == null)
            {
                return HttpNotFound();
            }

            return View(bot);
        }

        // POST: IntentionRecognition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image")] BotViewModel bot)
        {
            if (this.ModelState.IsValid)
            {
                this._botService.EditBot(bot, this.User.Identity.Name);

                return RedirectToAction("Index");
            }

            return View(bot);
        }

        // GET: IntentionRecognition/Delete/5
        [Authorize]
        public ActionResult Delete(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bot = this._botService.FindBot(id.Value);

            if (bot == null)
            {
                return HttpNotFound();
            }

            return View(bot);
        }

        // POST: IntentionRecognition/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (!this._botService.DeleteBot(id, this.User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Chat(long? botId)
        {
            if (botId == null || botId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bot = this._botService.FindBot(botId.Value);

            if (bot == null)
            {
                return HttpNotFound();
            }

            return View(new ChatViewModel { Bot = bot });
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
