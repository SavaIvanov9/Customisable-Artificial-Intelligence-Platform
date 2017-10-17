﻿namespace CAI.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Common.CustomExceptions;
    using Services.Abstraction;
    using Services.Models.Intention;
    using Services.Models.TrainingData;

    public class TrainingDataController : Controller
    {
        private readonly ITrainingDataService _trainingDataService;

        public TrainingDataController(ITrainingDataService trainingDataService)
        {
            this._trainingDataService = trainingDataService;
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
                var data = this._trainingDataService.Find(id.Value);

                return View(data);
            }
            catch (NotFoundException)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        public ActionResult Edit(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = this._trainingDataService.Find(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,IntentionId")] TrainingDataViewModel dataModel)
        {
            var data = this._trainingDataService.Find(dataModel.Id);

            if (this.ModelState.IsValid)
            {
                if (this._trainingDataService.Edit(dataModel, this.User.Identity.Name))
                {
                    return RedirectToAction("Details", "Intention", new {id = data.IntentionId});
                }
            }

            return View(data);
        }

        [Authorize]
        public ActionResult Create(long intentionId)
        {
            return View(new TrainingDataCreateModel() {IntentionId = intentionId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,IntentionId")] TrainingDataCreateModel dataModel)
        {
            if (this.ModelState.IsValid)
            {
                var id = this._trainingDataService.Register(dataModel, this.User.Identity.Name);

                return RedirectToAction("Details", "Intention", new { id = dataModel.IntentionId });
                //return RedirectToAction("Details", new {id = id});
            }

            return View(dataModel);
        }

        [Authorize]
        public ActionResult Delete(long? id)
        {
            if (id == null || id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = this._trainingDataService.Find(id.Value);

            if (data == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var data = this._trainingDataService.Find(id);

            if (!this._trainingDataService.Delete(id, this.User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            return RedirectToAction("Details", "Intention", new {id = data.IntentionId});
        }
    }
}