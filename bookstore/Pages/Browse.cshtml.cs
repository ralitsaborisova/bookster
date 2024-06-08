using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bookstore.Models;
using bookstore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.Pages

{
    public class BooksModel : PageModel
    {
        private readonly bookstoreContext _dbContext; 

        public List<Book> Books { get; set; }

        public BooksModel(bookstoreContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            Books = _dbContext.Books.ToList();
        }

        
    }
}
