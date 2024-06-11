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
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace bookstore.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly bookstore.Data.bookstoreContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(bookstore.Data.bookstoreContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Book> Book { get;set; } = default!;
        public List<UserBookLike> UserLikes { get; set; } = new List<UserBookLike>();
        public HashSet<int> LikedBooks { get; set; } = new HashSet<int>();



        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Titles { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookTitle { get; set; }


        public async Task OnGetAsync()
        {
            var books = from b in _context.Books select b;

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.Title.Contains(SearchString));
            }

            Book = await books.ToListAsync();

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                UserLikes = await _context.UserBookLikes.Where(ubl => ubl.UserId == userId).ToListAsync();
                LikedBooks = UserLikes.Select(ul => ul.BookId).ToHashSet();
            }
        }

        public async Task<IActionResult> OnPostLikeAsync(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var like = await _context.UserBookLikes
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);

            if (like == null)
            {
                // User hasn't liked this book before, so add the like
                _context.UserBookLikes.Add(new UserBookLike
                {
                    UserId = userId,
                    BookId = bookId
                });

                var book = await _context.Books.FindAsync(bookId);
                book.Likes++;
            }
            else
            {
                // User has already liked this book, so remove the like
                _context.UserBookLikes.Remove(like);

                var book = await _context.Books.FindAsync(bookId);
                book.Likes--;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    BookId = bookId,
                    Quantity = 1
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
