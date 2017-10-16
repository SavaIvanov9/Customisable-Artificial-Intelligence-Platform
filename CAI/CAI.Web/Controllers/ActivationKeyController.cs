namespace CAI.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Common.CustomExceptions;
    using Services.Abstraction;
    using Services.Models.ActivationKey;
    using Services.Models.Intention;

    public class ActivationKeyController : Controller
    {
        private readonly IActivationKeyService _activationKeyService;

        public ActivationKeyController(IActivationKeyService activationKeyService)
        {
            this._activationKeyService = activationKeyService;
        }

        public ActionResult Details(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var key = this._activationKeyService.FindKey(id.Value);

                return View(key);
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

            var key = this._activationKeyService.FindKey(id.Value);

            if (key == null)
            {
                return HttpNotFound();
            }

            return View(key);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IntentionId")] ActivationKeyViewModel keyModel)
        {
            var key = this._activationKeyService.FindKey(keyModel.Id);

            if (this.ModelState.IsValid)
            {
                if (this._activationKeyService.EditKey(keyModel, this.User.Identity.Name))
                {
                    return RedirectToAction("Details", "Intention", new { id = key.IntentionId });
                }
            }

            return View(key);
        }

        // GET: IntentionRecognition/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var key = this._activationKeyService.FindKey(id.Value);

            if (key == null)
            {
                return HttpNotFound();
            }

            return View(key);
        }

        // POST: IntentionRecognition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var key = this._activationKeyService.FindKey(id);

            if (!this._activationKeyService.DeleteKey(id, this.User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            return RedirectToAction("Details", "Intention", new { id = key.IntentionId });
        }
    }
}