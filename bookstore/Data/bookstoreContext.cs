using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using bookstore.Models;
using Microsoft.AspNetCore.Identity;

namespace bookstore.Data
{
    public class bookstoreContext : IdentityDbContext<ApplicationUser>
    {
        public bookstoreContext (DbContextOptions<bookstoreContext> options)
            : base(options)
        {
        }

        public DbSet<bookstore.Models.Book> Books { get; set; } = default!;
        public DbSet<UserBookLike> UserBookLikes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserBookLike>()
                .HasIndex(ubl => new { ubl.UserId, ubl.BookId })
                .IsUnique();

            builder.Entity<UserBookLike>()
                .HasOne(ubl => ubl.User)
                .WithMany(u => u.UserBookLikes)
                .HasForeignKey(ubl => ubl.UserId);

            builder.Entity<UserBookLike>()
                .HasOne(ubl => ubl.Book)
                .WithMany(b => b.UserBookLikes)
                .HasForeignKey(ubl => ubl.BookId);
        }
    }
}


