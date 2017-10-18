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

        [Authorize]
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
                var key = this._activationKeyService.FindKey(id.Value);

                return View(key);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IntentionId")] ActivationKeyViewModel keyModel)
        {
            try
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
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        public ActionResult Create(long intentionId)
        {
            return View(new ActivationKeyCreateModel { IntentionId = intentionId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IntentionId")] ActivationKeyCreateModel model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var id = this._activationKeyService.RegisterKey(model, this.User.Identity.Name);

                    //return RedirectToAction("Details", new { id = id });
                    return RedirectToAction("Details", "Intention", new { id = model.IntentionId });
                }

                return View(model);
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [Authorize]
        public ActionResult Delete(long? id)
        {
            try
            {
                if (id == null || id < 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var key = this._activationKeyService.FindKey(id.Value);

                return View(key);
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
                var key = this._activationKeyService.FindKey(id);

                if (!this._activationKeyService.DeleteKey(id, this.User.Identity.Name))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict);
                }

                return RedirectToAction("Details", "Intention", new { id = key.IntentionId });
            }
            catch (NotFoundException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }
    }
}