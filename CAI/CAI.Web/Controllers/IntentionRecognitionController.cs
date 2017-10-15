namespace CAI.Web.Controllers
{
    using Data.Filtering;
    using Services.Abstraction;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Common.CustomExceptions;

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

        // GET: IntentionRecognition
        public ActionResult Index()
        {
            var filter = new BotFilter() { IsDeleted = false };
            var data = this._botService.GetAllBotsByFilter(filter);

            return View(data);
        }

        // GET: IntentionRecognition/Details/5
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

        // GET: IntentionRecognition/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: IntentionRecognition/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,BotType,EnvironmentType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsDeleted,DeletedOn,DeletedBy")] Bot bot)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Bots.Add(bot);
        //        db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(bot);
        //}

        //// GET: IntentionRecognition/Edit/5
        //public async Task<ActionResult> Edit(long? id)
        //{
        //    if (id == null || id < 1)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Bot bot = await db.Bots.FindAsync(id);

        //    if (bot == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(bot);
        //}

        //// POST: IntentionRecognition/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Name,BotType,EnvironmentType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsDeleted,DeletedOn,DeletedBy")] Bot bot)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(bot).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(bot);
        //}

        // GET: IntentionRecognition/Delete/5
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
