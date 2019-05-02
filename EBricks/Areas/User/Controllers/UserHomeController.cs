using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBricks.Areas.User.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: User/UserHome
        public ActionResult Index()
        {
            return View();
        }
    }
}