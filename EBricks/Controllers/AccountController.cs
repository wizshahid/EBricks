using BLL;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EBricks.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        AccountBLL accountBll = new AccountBLL();
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
            account.UserRole = "User";
            if (ModelState.IsValid)
            {
                ViewBag.Message = accountBll.CreateAccount(account);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM login)
        {
            Account account = accountBll.Login(login);

            if (account != null)
            {
                if(account.IsActive==false)
                {
                    ModelState.AddModelError("", "Your Account has been blocked");
                    return View();
                }
                Session["AccountId"] = account.Id;
                Session["UserRole"] = account.UserRole;


                FormsAuthentication.Initialize();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, login.UserName, DateTime.Now, DateTime.Now.AddMinutes(30),
                                                                               login.IsChecked, FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                var returnValue = Request["ReturnUrl"];
                if (returnValue != null)
                {
                    return Redirect(Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, Request["ReturnUrl"]));
                }

                if (account.UserRole == "Admin")
                {
                    return RedirectToAction("Index", new { Area = "Admin", Controller = "AdminHome" });
                }

                else if (account.UserRole == "User")
                {
                    return RedirectToAction("Index", new { Area = "User", Controller = "UserHome" });
                }
            }
          
                ModelState.AddModelError("", "Invalid UserName and / Or password");
                return View();
        }
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}