using Blog.Models.ViewModels;
using Blog.Services.Inerfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.WEB.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IGenreService _genreService;

        public ArticlesController(IArticleService articleService, IGenreService genreService)
        {
            _articleService = articleService;
            _genreService = genreService;

        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetArticlesAsync();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genres = await _genreService.GetGenresAsync();

            var viewModel = new InputArticleViewModel
            {
                Genres = genres
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InputArticleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userName = User.FindFirstValue(ClaimTypes.Name);

            await _articleService.CreateArticleAsync(viewModel, userName);

            return RedirectToAction("Index");
        }
    }
}
