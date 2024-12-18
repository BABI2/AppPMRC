using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppPMRC.Data;
using WebAppPMRC.Models;
using WebAppPMRC.ViewModels;

namespace WebAppPMRC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        // Injecter le contexte de la base de données
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var persons = await _context.Persons
                .Include(p => p.Localite)
                .ThenInclude(l => l.Region)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                PersonCount = persons.Count,
                TotalAmount = persons.Sum(p => p.MontantComp),
                LocalityCount = persons.Select(p => p.Localite).Distinct().Count(),
                Localities = persons.Select(p => p.Localite?.Nom).Distinct().ToList(),
                Amounts = persons.Select(p => p.MontantComp).ToList(),
                PersonNames = persons.Select(p => p.Nom).ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
