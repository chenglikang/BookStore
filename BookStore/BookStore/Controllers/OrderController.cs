using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        // GET: Order
        public ActionResult Index()
        {
            var list = db.Orders.Where(p => p.Username == User.Identity.Name).ToList();
            return View(list);
        }

        public ActionResult Details(int id)
        {
            var list = db.Orders.Single(p => p.OrderId == id);
            return View(list);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}