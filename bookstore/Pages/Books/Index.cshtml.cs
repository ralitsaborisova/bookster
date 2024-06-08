using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bookstore.Data;
using bookstore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookstore.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly bookstore.Data.bookstoreContext _context;

        public IndexModel(bookstore.Data.bookstoreContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Titles { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookTitle { get; set; }


        public async Task OnGetAsync()
        {
            var books = from b in _context.Books
                         select b;
            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.Title.Contains(SearchString));
            }

            Book = await books.ToListAsync();
        }

        public async Task<IActionResult> OnPostLikeAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
            {
                return NotFound();
            }

            book.Likes = (book.Likes ?? 0) + 1;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
