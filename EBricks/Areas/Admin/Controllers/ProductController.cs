using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace EBricks.Areas.Admin.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        ProductBLL productBll = new ProductBLL();
        CategoryBLL categoryBll = new CategoryBLL();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
          ViewBag.CategoryId=  new SelectList(categoryBll.GetCategories(), "Id", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            product.PostedBy = Convert.ToInt32(Session["AccountId"]);
            product.IsAvailable = true;
           ViewBag.Message= productBll.AddProduct(product);
            ViewBag.CategoryId = new SelectList(categoryBll.GetCategories(), "Id", "CategoryName");
            return View();
        }

        public ActionResult GetProducts()
        {
            return View(productBll.GetProducts());
        }

        public ActionResult ProductDetails(long id)
        {
            return View(productBll.GetProductById(id));
        }

        public ActionResult DeleteProduct(long id)
        {
            return View(productBll.GetProductById(id));
        }
        [HttpPost,ActionName("DeleteProduct")]
        public ActionResult DelProduct(long id)
        {
            productBll.DeleteProduct(id);
            return RedirectToAction("GetProducts");
        }
        public ActionResult EditProduct(long id)
        {
            ViewBag.CategoryId = new SelectList(categoryBll.GetCategories(), "Id", "CategoryName");
            return View(productBll.GetProductById(id));
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
           if(ModelState.IsValid)
            {
                productBll.EditProduct(product);
            }
            return RedirectToAction("GetProducts");
        }
        public void DeleteImage(long id,string imagePath)
        {
            productBll.DeleteImage(id,imagePath);
        }
    }
}