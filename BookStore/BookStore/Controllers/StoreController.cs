using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace BookStore.Controllers
{
    [HandleError]
    public class StoreController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        
        // GET: Store
        public ActionResult Index(string search, int page=1)
        {
            var pageSize = 16;
            IPagedList<Books> list = null;
            if (string.IsNullOrEmpty(search))
            {
                list = db.Books.OrderByDescending(a => a.BookId)
                    .ToPagedList(page, pageSize);
            }
            else
            {
                ViewBag.search = search;
                list = db.Books.Where(a=>a.Title.Contains(search)).OrderByDescending(a => a.BookId)
                    .ToPagedList(page, pageSize);
            }

            return View(list);
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Books book = db.Books.Find(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            catch (Exception exp)
            {
                //TODO 日志
                return View("Error");
            }

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