namespace FastFood.Core.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly FastFoodContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(FastFoodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
           if (!ModelState.IsValid)
            {
                RedirectToAction("Error", "Home");
            }
            var newCategory = _mapper.Map<Category>(model);

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("All");

        }

        public async Task<IActionResult> All()
        {
            var categories = await _context.Categories
                .ProjectTo<CategoryAllViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return View(categories);
        }
    }
}
