using Microsoft.EntityFrameworkCore;
using bookstore.Data;
namespace bookstore.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new bookstoreContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<bookstoreContext>>()))
            {
                if (context == null || context.Books == null)
                {
                    throw new ArgumentNullException("Null bookstoreContext");
                }

                // Look for any movies.
                if (context.Books.Any())
                {
                    return;   // DB has been seeded
                }

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Harry Pottter and the Philosopher's Stone",
                        Author = "J.K. Rowling",
                        Price = 17,
                        ImageUrl = "/img/harry.jpg",
                        ReleaseDate = DateTime.Parse("2018-11-15"),
                        Description = "Harry Potter thinks he is an ordinary boy - until he is rescued by an owl, taken to Hogwarts School of Witchcraft and Wizardry, learns to play Quidditch and does battle in a deadly duel. The Reason ... HARRY POTTER IS A WIZARD!"
                    },

                    new Book
                    {
                        Title = "Diplomacy",
                        Author = "Henry Kissenger",
                        Price = 25,
                        ImageUrl = "/img/diplomacy.jpg",
                        ReleaseDate = DateTime.Parse("2012-05-02"),
                        Description = "A brilliant, sweeping history of diplomacy that includes personal stories from the noted former Secretary of State, including his stunning reopening of relations with China.\r\n\r\nThe seminal work on foreign policy and the art of diplomacy."
                    },

                    new Book
                    {
                        Title = "The Alchemist",
                        Author = "Paulo Coelho",
                        Price = 12,
                        ImageUrl = "/img/alchemist.jpg",
                        ReleaseDate = DateTime.Parse("1998-12-23"),
                        Description = "Combining magic, mysticism, wisdom, and wonder into an inspiring tale of self-discovery, The Alchemist has become a modern classic, selling millions of copies around the world and transforming the lives of countless readers across generations."
                    },

                    new Book
                    {
                        Title = "The Old Man And The Sea",
                        Author = "Earnest Hemingway",
                        Price = 10,
                        ImageUrl = "/img/oldman.jpg",
                        ReleaseDate = DateTime.Parse("1945-01-20"),
                        Description = "This short novel, already a modern classic, is the superbly told, tragic story of a Cuban fisherman in the Gulf Stream and the giant Marlin he kills and loses—specifically referred to in the citation accompanying the author's Nobel Prize for literature in 1954."
                    },
                    new Book
                    {
                        Title = "Chip War",
                        Author = "Chris Miller",
                        Price = 21,
                        ImageUrl = "/img/chipwar.jpg",
                        ReleaseDate = DateTime.Parse("2022-01-20"),
                        Description = "The fight for the world's most critical technology."
                    },
                    new Book
                    {
                        Title = "The Three Body Problem",
                        Author = "Cixin Liu",
                        Price = 19,
                        ImageUrl = "/img/threebody.jpg",
                        ReleaseDate = DateTime.Parse("2015-03-20"),
                        Description = "."
                    },
                    new Book
                    {
                        Title = "Zorba The Greek",
                        Author = "Nikos Kazandzakhis",
                        Price = 15,
                        ImageUrl = "/img/zorba.jpg",
                        ReleaseDate = DateTime.Parse("1932-07-19"),
                        Description = "."
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
