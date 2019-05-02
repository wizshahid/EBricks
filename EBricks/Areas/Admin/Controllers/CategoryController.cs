using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;

namespace EBricks.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryBLL bll = new CategoryBLL();
        public ActionResult Categories()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            return Json(bll.GetCategories(),JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddCategory(Category category)
        {
            bll.AddCategory(category);
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditCategory(Category category)
        {
            bll.EditCategory(category);
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeleteCategory(int id)
        {
            bll.DeleteCategory(id);
        }
    }
}