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
    }
}


