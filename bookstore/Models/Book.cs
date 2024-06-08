using System;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public decimal? Price { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? ImageUrl { get; set; }

        public int? Likes { get; set; }
    }
}
