using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class ShoppingCart
    {
        public static int CartCount
        {
            get {
                using (BookStoreDB db = new BookStoreDB())
                {
                    int? count = db.Carts.Where(
                        p => p.CartId == HttpContext.Current.User.Identity.Name)
                        .Select(p => (int?)p.Count).Sum();
                    return count ?? 0;
                }
            }
        }

        public static decimal CartTotal
        {
            get
            {
                using (BookStoreDB db = new BookStoreDB())
                {
                    decimal? count = db.Carts.Where(
                        p => p.CartId == HttpContext.Current.User.Identity.Name)
                        .Select(p => (int?)p.Count * p.Books.Price).Sum();
                    return count ?? 0;
                }
            }
        }
    }
}