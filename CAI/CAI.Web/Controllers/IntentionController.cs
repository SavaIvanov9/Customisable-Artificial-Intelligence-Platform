namespace CAI.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using Common.CustomExceptions;
    using Common.Enums;
    using Services.Abstraction;
    using Services.Models.Bot;
    using Services.Models.Intention;

    public class IntentionController : Controller
    {
        private readonly IIntentionService _intentionService;

        public IntentionController(IIntentionService intentionService)
        {
            this._intentionService = intentionService;
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
                var intention = this._intentionService.FindIntention(id.Value);

                return View(intention);
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
                var intention = this._intentionService.FindIntention(id.Value);

                if (intention == null)
                {
                    return HttpNotFound();
                }

                return View(intention);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] IntentionViewModel intentionModel)
        {
            try
            {
                var intention = this._intentionService.FindIntention(intentionModel.Id);

                if (this.ModelState.IsValid)
                {
                    if (this._intentionService.EditIntention(intentionModel, this.User.Identity.Name))
                    {
                        return RedirectToAction("Details", "IntentionRecognition", new { id = intention.BotId });
                    }
                }

                return View(intention);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        public ActionResult Create(long botId)
        {
            return View(new IntentionCreateModel { BotId = botId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,BotId")] IntentionCreateModel intention)
        {
            if (this.ModelState.IsValid)
            {
                var id = this._intentionService.RegisterIntention(intention, this.User.Identity.Name);

                return RedirectToAction("Details", new { id = id });
            }

            return View(intention);
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
                var intention = this._intentionService.FindIntention(id.Value);
                return View(intention);
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
                var intention = this._intentionService.FindIntention(id);

                if (!this._intentionService.DeleteIntention(id, this.User.Identity.Name))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict);
                }

                return RedirectToAction("Details", "IntentionRecognition", new { id = intention.BotId });
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
                this._intentionService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}