namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Books()
        {
            Carts = new HashSet<Carts>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public int BookId { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Display(Name ="价格")]
        public decimal Price { get; set; }

        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }

        public virtual Authors Authors { get; set; }

        public virtual Categorys Categorys { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carts> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
