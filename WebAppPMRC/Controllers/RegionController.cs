using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPMRC.Data;
using WebAppPMRC.Models;
using WebAppPMRC.ViewModels;

namespace WebAppPMRC.Controllers
{
    public class RegionController : Controller
    {
        private readonly AppDbContext _context;

        public RegionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var regions = await _context.Regions
                                        .Include(r => r.Localites)
                                        .ToListAsync();
            var regionViewModels = regions.Select(r => new RegionViewModel
            {
                Id = r.Id,
                Nom = r.Nom,
                Localites = r.Localites.Select(l => new LocaliteViewModel
                {
                    Id = l.Id,
                    Nom = l.Nom,
                    RegionId = l.RegionId
                }).ToList()
            }).ToList();

            return View(regionViewModels);
        }

        [HttpGet]
        public IActionResult AddOrEdit(int? id)
        {
            var region = id == null
                ? new RegionViewModel()
                : _context.Regions
                          .Include(r => r.Localites)
                          .Where(r => r.Id == id)
                          .Select(r => new RegionViewModel
                          {
                              Id = r.Id,
                              Nom = r.Nom,
                              Localites = r.Localites.Select(l => new LocaliteViewModel
                              {
                                  Id = l.Id,
                                  Nom = l.Nom,
                                  RegionId = l.RegionId
                              }).ToList()
                          }).FirstOrDefault();

            return region == null ? NotFound() : View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(RegionViewModel regionViewModel)
        {
            if (!ModelState.IsValid) return View(regionViewModel);

            var region = new Region
            {
                Id = regionViewModel.Id,
                Nom = regionViewModel.Nom,
                Localites = regionViewModel.Localites.Select(l => new Localite
                {
                    Id = l.Id,
                    Nom = l.Nom,
                    RegionId = regionViewModel.Id
                }).ToList()
            };

            if (region.Id == 0) _context.Regions.Add(region);
            else _context.Regions.Update(region);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region == null) return NotFound();

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
