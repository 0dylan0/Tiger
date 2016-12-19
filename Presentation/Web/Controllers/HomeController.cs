using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Framework.Controllers;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}