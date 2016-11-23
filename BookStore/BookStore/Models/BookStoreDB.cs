namespace BookStore.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookStoreDB : DbContext
    {
        public BookStoreDB()
            : base("name=BookStoreEntity")
        {
        }

        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Categorys> Categorys { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
