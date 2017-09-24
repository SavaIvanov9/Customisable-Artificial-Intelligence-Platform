using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAI.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Abstraction;

    public class TestController : Controller
    {
        private readonly ITestService service;

        public TestController(ITestService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            this.ViewBag.Message = this.service.Test();

            return View();
        }
    }
}