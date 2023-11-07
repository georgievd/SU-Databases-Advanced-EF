using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace BookShop
{
    using Data;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();

            // DbInitializer.ResetDatabase(db);
            // Console.WriteLine(GetMostRecentBooks(db));

            Console.WriteLine(GetBookTitlesContaining(db, "Sk"));
        }

        // Ex. 2
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {

            // We get the input string - command, then we try to parse it to AgeRestriction enum
            // ie. checking if the value of 'command' is either Minor, Teen or Adult
            // if not, we return an exception message
            // if yes, we write the data into a new variable called ageRestriction
            // More about the out parameter modifier can be found here: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters

            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return $"{command} is not a valid type";
            }

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));

        }

        // Ex. 3
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold
                                    && b.Copies < 5000)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        // Ex. 4
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - ${b.Price:f2}"));
        }

        // Ex. 5
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue &&
                            b.ReleaseDate.Value.Year != year)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        // Ex. 6
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            if (input is null) throw new ArgumentNullException(nameof(input));

            string[] categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();


            var books = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        // Ex. 7
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < parsedDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate);


            return string.Join(Environment.NewLine,
                books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}"));
        }

        // Ex. 8
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName);

            return string.Join(Environment.NewLine, authors.Select(a => a.FullName));
        }

        // Ex. 9
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            // Option 1
            //var books = context.Books
            //    .Where(b => b.Title.ToLower().Contains(input.ToLower()))
            //    .Select(b => new
            //    {
            //        b.Title
            //    })
            //    .OrderBy(b => b.Title);

            //return string.Join(Environment.NewLine, books.Select(b => b.Title));


            // Option 2
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{input}%")) 
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title);

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        // Ex. 10
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            // Option 1

            //var books = context.Books
            //    .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
            //    .Select(b => new
            //    {
            //        BookTitle = b.Title,
            //        AuthorName = b.Author.FirstName + " " + b.Author.LastName
            //    });

            //return string.Join(Environment.NewLine,
            //    books.Select(b => $"{b.BookTitle} ({b.AuthorName})"));


            var books = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName, $"%{input}%"))
                .Select(b => new
                {
                    BookTitle = b.Title,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName
                });

            return string.Join(Environment.NewLine,
                books.Select(b => $"{b.BookTitle} ({b.AuthorName})"));
        }

        // Ex. 11
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books
                .Count(b => b.Title.Length > lengthCheck);

        }

        // Ex. 12
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    AuhtorName = string.Join(" ", a.FirstName, a.LastName),
                    TotalBooks = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.TotalBooks).ToList();

            return string.Join(Environment.NewLine,
                authors.Select(a => $"{a.AuhtorName} - {a.TotalBooks}"));
        }

        // Ex. 13
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profitsByCategory = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks
                        .Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName);

            foreach (var prof in profitsByCategory)
            {
                Console.WriteLine(prof.CategoryName);
            }

            return string.Join(Environment.NewLine,
                profitsByCategory.Select(pc => $"{pc.CategoryName} ${pc.TotalProfit}"));
        }

        // Ex. 14
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    CatName = c.Name,
                    MostRecentBooks = c.CategoryBooks.OrderByDescending(bc => bc.Book.ReleaseDate)
                        .Take(3)
                        .Select(cb => new
                        {
                            BookTitle = cb.Book.Title,
                            cb.Book.ReleaseDate!.Value.Year
                        })
                })
                .OrderBy(c => c.CatName);

            StringBuilder sb = new StringBuilder();
            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.CatName}");
                foreach (var mostRecentBook in category.MostRecentBooks)
                {
                    sb.AppendLine($"{mostRecentBook.BookTitle} ({mostRecentBook.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        // Ex. 15
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate!.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        // Ex. 16
        public static int RemoveBooks(BookShopContext context)
        {
            context.ChangeTracker.Clear();

            var books = context.Books
                .Where(b => b.Copies < 4200);

            context.RemoveRange(books);

            return context.SaveChanges();
        }
    }
}