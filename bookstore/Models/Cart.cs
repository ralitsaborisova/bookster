using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }

        public void CalculateCartTotal()
        {
            CartTotal = CartItems.Sum(item => item.Book.Price.GetValueOrDefault() * item.Quantity);
        }
    }
}
