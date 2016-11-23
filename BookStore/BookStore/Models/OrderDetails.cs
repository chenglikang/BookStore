namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Books Books { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
