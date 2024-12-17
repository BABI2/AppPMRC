using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPMRC.Data;
using WebAppPMRC.Models;
using WebAppPMRC.ViewModels;

namespace WebAppPMRC.Controllers
{
    public class NumeroDossierController : Controller
    {
        private readonly AppDbContext _context;

        public NumeroDossierController(AppDbContext context)
        {
            _context = context;
        }

        // Afficher la liste des numéros de dossier
        public async Task<IActionResult> Index(string search)
        {
            var numeroDossiersQuery = _context.NumeroDossiers
                .Include(nd => nd.Person)
                .Select(nd => new NumeroDossierViewModel
                {
                    Id = nd.Id,
                    DossierNumero = nd.DossierNumero,
                    PersonId = nd.PersonId,
                    NomPerson = $"{nd.Person.Nom} {nd.Person.Prenom}"
                });

            if (!string.IsNullOrEmpty(search))
            {
                numeroDossiersQuery = numeroDossiersQuery.Where(nd => nd.NomPerson.Contains(search));
            }

            var numeroDossiers = await numeroDossiersQuery.ToListAsync();
            return View(numeroDossiers);
        }

        // Afficher le formulaire pour ajouter ou modifier un numéro de dossier
        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var personnes = await _context.Persons
                .Where(p => !_context.NumeroDossiers.Any(nd => nd.PersonId == p.Id)) // Exclure les personnes déjà attribuées
                .ToListAsync();

            ViewBag.PersonList = personnes.Select(p => new { p.Id, FullName = $"{p.Nom} {p.Prenom}" });

            if (id == null)
                return View(new NumeroDossierViewModel());

            var numeroDossier = await _context.NumeroDossiers
                .Include(nd => nd.Person)
                .FirstOrDefaultAsync(nd => nd.Id == id);

            if (numeroDossier == null) return NotFound();

            var viewModel = new NumeroDossierViewModel
            {
                Id = numeroDossier.Id,
                DossierNumero = numeroDossier.DossierNumero,
                PersonId = numeroDossier.PersonId,
                NomPerson = $"{numeroDossier.Person.Nom} {numeroDossier.Person.Prenom}"
            };

            return View(viewModel);
        }

        // Ajouter ou modifier un numéro de dossier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(NumeroDossierViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Vérifier si le numéro de dossier existe déjà
            if (_context.NumeroDossiers.Any(nd => nd.DossierNumero == model.DossierNumero && nd.Id != model.Id))
            {
                ModelState.AddModelError("DossierNumero", "Ce numéro de dossier existe déjà.");
                return View(model);
            }

            if (model.Id == 0)
            {
                var numeroDossier = new NumeroDossier
                {
                    DossierNumero = model.DossierNumero,
                    PersonId = model.PersonId
                };

                _context.NumeroDossiers.Add(numeroDossier);
            }
            else
            {
                var numeroDossier = await _context.NumeroDossiers.FindAsync(model.Id);
                if (numeroDossier == null) return NotFound();

                numeroDossier.DossierNumero = model.DossierNumero;
                numeroDossier.PersonId = model.PersonId;

                _context.NumeroDossiers.Update(numeroDossier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Supprimer un numéro de dossier
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var numeroDossier = await _context.NumeroDossiers.FindAsync(id);
            if (numeroDossier == null) return NotFound();

            _context.NumeroDossiers.Remove(numeroDossier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
