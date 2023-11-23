using Blog.Data;
using Blog.Models;
using Blog.Services.Inerfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            return await _context.Genres
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }
    }
}
