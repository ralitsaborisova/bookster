using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class UserBookLike
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
    }
}
