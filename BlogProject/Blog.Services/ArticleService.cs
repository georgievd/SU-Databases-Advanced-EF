using Blog.Data;
using Blog.Models;
using Blog.Models.ViewModels;
using Blog.Services.Inerfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await _context.Articles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateArticleAsync(InputArticleViewModel model, string userName)
        {
            Article article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                GenreId = model.GenreId,
                Author = userName
            };

            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

    }
}
