using BookStore.Common;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        public ActionResult Index()
        {
            try
            {
                List<Books> list = db.Books.OrderByDescending(a => a.OrderDetails.Count).Take(12).ToList();
                //ViewBag.List = list;
                return View(list);
            }
            catch (Exception exp)
            {
                //TODO:日志
                Logger.logger.Error(exp.Message);
                Logger.logger.Error(exp.StackTrace);
                return View("Error");
            }


        }

        public ActionResult About()
        {
            ViewBag.Message = "";
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
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