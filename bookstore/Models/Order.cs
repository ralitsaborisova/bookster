using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class Order
    {
        
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Book is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 1000)]
        public decimal Price { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual Book? Book { get; set; }
    }
}
