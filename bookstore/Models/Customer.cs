using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50)]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public Cart Cart { get; set; }  
    }
}
