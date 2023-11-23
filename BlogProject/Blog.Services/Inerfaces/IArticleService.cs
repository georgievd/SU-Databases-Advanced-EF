using Blog.Models;
using Blog.Models.ViewModels;

namespace Blog.Services.Inerfaces
{
    public interface IArticleService
    {
        Task CreateArticleAsync(InputArticleViewModel model, string userId);
        Task<IEnumerable<Article>> GetArticlesAsync();
    }
}