using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppPMRC.Data;
using WebAppPMRC.Models;
using WebAppPMRC.ViewModels;

namespace WebAppPMRC.Controllers
{
    public class LocaliteController : Controller
    {
        private readonly AppDbContext _context;

        public LocaliteController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var localites = await _context.Localites
                                          .Include(l => l.Region)
                                          .OrderBy(l => l.Nom)
                                          .ToListAsync();

            var localiteViewModels = localites.Select(l => new LocaliteViewModel
            {
                Id = l.Id,
                Nom = l.Nom,
                RegionId = l.RegionId,
                NomRegion = l.Region?.Nom
            }).ToList();

            return View(localiteViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");

            if (id == null) return View(new LocaliteViewModel());

            var localite = await _context.Localites.FindAsync(id);
            if (localite == null) return NotFound();

            return View(new LocaliteViewModel
            {
                Id = localite.Id,
                Nom = localite.Nom,
                RegionId = localite.RegionId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(LocaliteViewModel localiteViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Regions = new SelectList(await _context.Regions.ToListAsync(), "Id", "Nom");
                return View(localiteViewModel);
            }

            try
            {
                if (localiteViewModel.Id == 0)
                {
                    _context.Localites.Add(new Localite
                    {
                        Nom = localiteViewModel.Nom,
                        RegionId = localiteViewModel.RegionId
                    });
                }
                else
                {
                    var localite = await _context.Localites.FindAsync(localiteViewModel.Id);
                    if (localite == null) return NotFound();

                    localite.Nom = localiteViewModel.Nom;
                    localite.RegionId = localiteViewModel.RegionId;
                    _context.Localites.Update(localite);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return View(localiteViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var localite = await _context.Localites.FindAsync(id);
                if (localite == null) return NotFound();

                _context.Localites.Remove(localite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByRegion(int id)
        {
            var localites = await _context.Localites
                                          .Where(l => l.RegionId == id)
                                          .OrderBy(l => l.Nom)
                                          .ToListAsync();
            return Json(localites.Select(l => new { l.Id, l.Nom }));
        }
    }
}

