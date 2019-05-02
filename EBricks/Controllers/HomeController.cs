using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBricks.Controllers
{
    public class HomeController : Controller
    {
        ProductBLL productBll = new ProductBLL();
        public ActionResult Index()
        {
            return View(productBll.GetProducts());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Categories()
        {
            CategoryBLL catBll = new CategoryBLL();
            return Json(catBll.GetCategories(), JsonRequestBehavior.AllowGet);
        }
    }
}