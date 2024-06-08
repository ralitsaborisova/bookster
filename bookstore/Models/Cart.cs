namespace bookstore.Models
{
    using System.Collections.Generic;

    public class Cart
    {
        public List<(Book book, int quantity)> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }


}
