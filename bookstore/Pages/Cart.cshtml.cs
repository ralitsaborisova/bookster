using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bookstore.Data;
using bookstore.Models;
using System.Security.Claims;
using System.Collections.Generic;

namespace bookstore.Pages
{
    public class CartModel : PageModel
    {
        private readonly bookstoreContext _context;

        public CartModel(bookstoreContext context)
        {
            _context = context;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal CartTotal { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                CartItems = new List<CartItem>();
                CartTotal = 0;
            }
            else
            {
                CartItems = cart.CartItems.ToList();
                CartTotal = CartItems.Sum(ci => (ci.Book.Price ?? 0) * ci.Quantity);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
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

        public async Task<IActionResult> OnPostRemoveFromCartAsync(int cartItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var cartItem = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.Cart.UserId == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
