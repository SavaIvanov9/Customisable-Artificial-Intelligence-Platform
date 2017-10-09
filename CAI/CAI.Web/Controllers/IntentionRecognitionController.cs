using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAI.Data.Models;
using CAI.Web.Models;

namespace CAI.Web.Controllers
{
    public class IntentionRecognitionController : Controller
    {
        private CAIWebContext db = new CAIWebContext();

        // GET: IntentionRecognition
        public async Task<ActionResult> Index()
        {
            return View(await db.Bots.ToListAsync());
        }

        // GET: IntentionRecognition/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bot bot = await db.Bots.FindAsync(id);
            if (bot == null)
            {
                return HttpNotFound();
            }
            return View(bot);
        }

        // GET: IntentionRecognition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IntentionRecognition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,BotType,EnvironmentType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsDeleted,DeletedOn,DeletedBy")] Bot bot)
        {
            if (ModelState.IsValid)
            {
                db.Bots.Add(bot);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bot);
        }

        // GET: IntentionRecognition/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bot bot = await db.Bots.FindAsync(id);
            if (bot == null)
            {
                return HttpNotFound();
            }
            return View(bot);
        }

        // POST: IntentionRecognition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,BotType,EnvironmentType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsDeleted,DeletedOn,DeletedBy")] Bot bot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bot);
        }

        // GET: IntentionRecognition/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bot bot = await db.Bots.FindAsync(id);
            if (bot == null)
            {
                return HttpNotFound();
            }
            return View(bot);
        }

        // POST: IntentionRecognition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Bot bot = await db.Bots.FindAsync(id);
            db.Bots.Remove(bot);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
