using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;

namespace EBricks.Areas.Admin.Controllers
{

    //[Authorize(Roles ="Admin")]
    public class AdminAccountController : Controller
    {
        AccountBLL accountBll = new AccountBLL();
        // GET: Admin/AdminAccount
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(Account account)
        {
            account.UserRole = "Admin";
            if(ModelState.IsValid)
            {
               ViewBag.Message= accountBll.CreateAccount(account);
            }
            return View();
        }

        public ActionResult GetAccounts()
        {
            return View(accountBll.GetAccounts());
        }

        public ActionResult BlockUser(long id)
        {
            accountBll.BlockUser(id);
            return RedirectToAction("GetAccounts");
        }
    }
}