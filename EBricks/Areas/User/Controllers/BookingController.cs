using BLL;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBricks.Areas.User
{
    [Authorize]
    public class BookingController : Controller
    {
        ProductBLL productBll = new ProductBLL();
        BookingBLL bookingBll = new BookingBLL();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BookNow(long id)
        {
            ViewBag.OldAddress = bookingBll.GetMyAdresses(Convert.ToInt64(Session["AccountId"]));
            ViewBag.Product = productBll.GetProductById(id);
            return View();
        }

        [HttpPost]
        public ActionResult BookNow(Booking booking)
        {
            booking.CustomerId = Convert.ToInt32(Session["AccountId"]);
            booking.BookingDate = DateTime.Now;
            booking.ShippingAddress.AccountId= Convert.ToInt32(Session["AccountId"]);
            bookingBll.BookNow(booking);
            return View();
        }
    }
}