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
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            if (id == null) return View(new RegionViewModel());

            var region = await _context.Regions.FindAsync(id);
            if (region == null) return NotFound();

            return View(new RegionViewModel
            {
                Id = region.Id,
                Nom = region.Nom
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(RegionViewModel regionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(regionViewModel);
            }

            try
            {
                var region = new Region
                {
                    Id = regionViewModel.Id,
                    Nom = regionViewModel.Nom
                };

                if (region.Id == 0)
                {
                    _context.Regions.Add(region);
                }
                else
                {
                    _context.Entry(region).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return View(regionViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var region = await _context.Regions.FindAsync(id);
                if (region == null) return NotFound();

                _context.Regions.Remove(region);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

