using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace EBricks.Controllers
{
    public class ProfileController : Controller
    {
        AccountBLL accountBll = new AccountBLL();
        // GET: Profile
        public ActionResult Profile()
        {
            long id = Convert.ToInt64(Session["AccountId"]);
            return View(accountBll.Profile(id));
        }
        public ActionResult EditProfile()
        {
            long id = Convert.ToInt64(Session["AccountId"]);
            return View(accountBll.Profile(id));
        }
        [HttpPost]
        public ActionResult EditProfile(Account account)
        {
            accountBll.EditProfile(account);
            return RedirectToAction("Profile");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel passwordVm)
        {
            ViewBag.Message= accountBll.ChangePassword(passwordVm);
            return View();
        }
    }
}