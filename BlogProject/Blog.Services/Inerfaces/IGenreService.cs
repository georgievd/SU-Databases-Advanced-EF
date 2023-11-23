using Blog.Models;

namespace Blog.Services.Inerfaces
{
    public interface IGenreService
    {
        Task CreateGenreAsync(Genre genre);
        Task<List<Genre>> GetGenresAsync();
    }
}