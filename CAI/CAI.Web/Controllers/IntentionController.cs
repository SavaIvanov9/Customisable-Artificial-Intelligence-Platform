namespace CAI.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Common.CustomExceptions;
    using Services.Abstraction;
    using Services.Models.Intention;

    public class IntentionController : Controller
    {
        private readonly IIntentionService _intentionService;

        public IntentionController(IIntentionService intentionService)
        {
            this._intentionService = intentionService;
        }

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
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Edit(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var intention = this._intentionService.FindIntention(id.Value);

            if (intention == null)
            {
                return HttpNotFound();
            }

            return View(intention);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] IntentionViewModel intention)
        {
            if (this.ModelState.IsValid)
            {
                if (this._intentionService.EditIntention(intention, this.User.Identity.Name))
                {
                    return RedirectToAction("Index", "IntentionRecognition", new { area = "" });
                }
            }

            return View(intention);
        }

        // GET: IntentionRecognition/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var intention = this._intentionService.FindIntention(id.Value);

            if (intention == null)
            {
                return HttpNotFound();
            }

            return View(intention);
        }

        // POST: IntentionRecognition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (!this._intentionService.DeleteIntention(id, this.User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            return RedirectToAction("Index", "IntentionRecognition", new { area = "" });
            //return RedirectToAction("Index");
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