namespace CAI.Web.Areas.Admin.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Services.Abstraction;
    using Services.Models.User;

    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            this._userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var filter = new BotFilter() { IsDeleted = false };
            //var data = this._botService.GetAllBotsByFilter(filter);
            var data = this._userService.GetAllUsers();

            return View(data);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this._userService.FindUser(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,PasswordHash,IsDeleted")] UserViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                this._userService.EditUser(user, this.User.Identity.Name);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this._userService.FindUser(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (!this._userService.DeleteUser(id, this.User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            return RedirectToAction("Index");
        }
    }
}