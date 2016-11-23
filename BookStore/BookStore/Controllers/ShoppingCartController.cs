using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cartList = db.Carts.Where(
                a => a.CartId == User.Identity.Name).ToList();
            decimal total = 0;
            foreach (var item in cartList)
            {
                total += item.Count * item.Books.Price;
            }
            ViewBag.CartTotal = total;
            return View(cartList);
        }

        public ActionResult AddToCart(int id,int count)
        {
            var book = db.Books.Find(id);
            if (book != null)
            {
                var cartItem = 
                    db.Carts.SingleOrDefault(
                        p => p.BookId == id
                        &&p.CartId==User.Identity.Name);
                if (cartItem != null)
                {
                    cartItem.Count+=count;
                }
                else
                {
                    cartItem = new Carts()
                    {
                        BookId = id,
                        CartId = User.Identity.Name,
                        Count = count,
                        DateCreated = DateTime.Now
                    };
                    db.Carts.Add(cartItem);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cartItem = db.Carts.SingleOrDefault(
                p => p.RecordId == id
                &&p.CartId==User.Identity.Name);
            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }
            var result = new {
                ItemID=id,
                CartTotal = ShoppingCart.CartTotal,
                CartCount = ShoppingCart.CartCount
            };
            return Json(result);
        }

        public ActionResult UpdateItemCount(int id,int count)
        {
            var cartItem = db.Carts.SingleOrDefault(
                p => p.RecordId == id
                && p.CartId == User.Identity.Name);
            if (cartItem != null)
            {
                cartItem.Count = count;
                db.SaveChanges();
            }
            var result = new
            {
                ItemID = id,
                CartTotal = ShoppingCart.CartTotal,
                CartCount = ShoppingCart.CartCount
            };
            return Json(result);
        }


        [AllowAnonymous]
        public ActionResult GetCartSummary()
        {
            ViewBag.Count = ShoppingCart.CartCount;
            return PartialView("_CartSummary");
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