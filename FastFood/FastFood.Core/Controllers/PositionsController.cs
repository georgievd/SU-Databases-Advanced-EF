namespace FastFood.Core.Controllers
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Positions;

    public class PositionsController : Controller
    {
        private readonly FastFoodContext _context;
        private readonly IMapper _mapper;

        public PositionsController(FastFoodContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePositionInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            var position = _mapper.Map<Position>(model);

            _context.Positions.Add(position);

            _context.SaveChanges();

            return RedirectToAction("All", "Positions");
        }

        public IActionResult All()
        {
            var positions = _context.Positions
                .ProjectTo<PositionsAllViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return View(positions);
        }
    }
}
