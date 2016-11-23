using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using X.PagedList;
using System.IO;

namespace BookStore.Controllers
{
    [Authorize(Roles ="admin")]
    public class StoreManagerController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: StoreManager
        public ActionResult Index(int page=1)
        {
                var books = db.Books.Include(a => a.Authors)
                    .Include(a => a.Categorys)
                    .OrderByDescending(a => a.BookId);
            return View(books.ToPagedList(page,30));
        }

        // GET: StoreManager/Details/5
        public ActionResult Details(int? id)
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

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,CategoryId,AuthorId,Title,Price,AlbumArtUrl")] Books albums,
            HttpPostedFileBase imageFile)
        {
            //Request.Form[""]
            //
            //Request.Cookies
            //Response.Cookies
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string guid = Guid.NewGuid().ToString();
                    string imageName = guid + 
                        Path.GetFileName(imageFile.FileName);
                    string serverPath = 
                        Server.MapPath("~/BookImages/" + imageName);
                    imageFile.SaveAs(serverPath);
                    albums.AlbumArtUrl = "/BookImages/" + imageName;
                }
                db.Books.Add(albums);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", albums.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", albums.CategoryId);
            return View(albums);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // POST: StoreManager/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,CategoryId,AuthorId,Title,Price,AlbumArtUrl")] Books albums)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albums).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name", albums.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", albums.CategoryId);
            return View(albums);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Books.Find(id);
            db.Books.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index");
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
