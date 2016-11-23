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
        public ActionResult Index()
        {
            try
            {
                using (BookStoreDB db = new BookStoreDB())
                {
                    List<Books> list = db.Books.OrderByDescending(a => a.OrderDetails.Count).Take(12).ToList();
                    //ViewBag.List = list;
                    return View(list);
                }
            }
            catch (Exception exp)
            {
                //TODO:日志
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
    }
}