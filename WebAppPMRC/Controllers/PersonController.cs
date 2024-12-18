using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Globalization;
using WebAppPMRC.Data;
using WebAppPMRC.Models;
using WebAppPMRC.Services;
using WebAppPMRC.ViewModels;

namespace WebAppPMRC.Controllers
{
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;
        private readonly FileService _fileService;

        public PersonController(AppDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }



        public async Task<IActionResult> Index()
        {
            var persons = await _context.Persons
                .Include(p => p.Localite)
                .ThenInclude(l => l.Region)
                .ToListAsync();

            var viewModels = persons.Select(p => new PersonViewModel
            {
                Id = p.Id,
                Nom = p.Nom,
                Prenom = p.Prenom,
                Surnom = p.Surnom,
                Contact = p.Contact,
                Longueur = p.Longueur,
                Largeur = p.Largeur,
                PrixM2 = p.PrixM2,
                MontantComp = p.MontantComp,
                Commentaire = p.Commentaire,
                PhotoPath = p.PhotoPath,
                LocaliteId = p.LocaliteId,
                RegionId = p.Localite?.RegionId ?? 0
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var regions = await _context.Regions.ToListAsync();
            ViewBag.Regions = regions.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Nom
            }).ToList();

            var localites = await _context.Localites.Include(l => l.Region).ToListAsync();

            var viewModel = new PersonViewModel
            {
                Regions = regions.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Nom
                }).ToList(),
                Localites = localites.Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Nom
                }).ToList()
            };

            if (id.HasValue)
            {
                var person = await _context.Persons.FindAsync(id.Value);
                if (person == null) return NotFound();

                viewModel.Id = person.Id;
                viewModel.Nom = person.Nom;
                viewModel.Prenom = person.Prenom;
                viewModel.Surnom = person.Surnom;
                viewModel.Contact = person.Contact;
                viewModel.Longueur = person.Longueur;
                viewModel.Largeur = person.Largeur;
                viewModel.PrixM2 = person.PrixM2;
                viewModel.MontantComp = person.MontantComp;
                viewModel.Commentaire = person.Commentaire;
                viewModel.PhotoPath = person.PhotoPath;
                viewModel.LocaliteId = person.LocaliteId;
                viewModel.RegionId = person.Localite?.RegionId ?? 0;
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(PersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Recharger les dropdowns en cas d'erreur
                model.Regions = await _context.Regions
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Nom })
                    .ToListAsync();

                model.Localites = await _context.Localites
                    .Where(l => l.RegionId == model.RegionId)
                    .Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.Nom })
                    .ToListAsync();

                return View(model);
            }

            if (model.Id == 0)
            {
                var person = new Person
                {
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    Surnom = model.Surnom,
                    Contact = model.Contact,
                    Longueur = model.Longueur,
                    Largeur = model.Largeur,
                    PrixM2 = model.PrixM2,
                    MontantComp = model.MontantComp,
                    Commentaire = model.Commentaire,
                    LocaliteId = model.LocaliteId,
                    PhotoPath = model.Photo != null ? await _fileService.UploadFileAsync(model.Photo) : null
                };
                _context.Persons.Add(person);
            }
            else
            {
                var person = await _context.Persons.FindAsync(model.Id);
                if (person == null) return NotFound();

                person.Nom = model.Nom;
                person.Prenom = model.Prenom;
                person.Surnom = model.Surnom;
                person.Contact = model.Contact;
                person.Longueur = model.Longueur;
                person.Largeur = model.Largeur;
                person.PrixM2 = model.PrixM2;
                person.MontantComp = model.MontantComp;
                person.Commentaire = model.Commentaire;
                person.LocaliteId = model.LocaliteId;

                if (model.Photo != null)
                {
                    _fileService.DeleteFile(person.PhotoPath);
                    person.PhotoPath = await _fileService.UploadFileAsync(model.Photo);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            // Supprimer la photo associée si elle existe
            if (!string.IsNullOrEmpty(person.PhotoPath))
            {
                _fileService.DeleteFile(person.PhotoPath);
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Personne supprimée avec succès.";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Veuillez sélectionner un fichier.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (extension == ".csv")
                {
                    await ImportCsv(file);
                }
                else if (extension == ".xlsx")
                {
                    await ImportExcel(file);
                }
                else
                {
                    TempData["Error"] = "Format de fichier non supporté. Utilisez un fichier CSV ou Excel.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Success"] = "Fichier importé avec succès.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erreur lors de l'importation : {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // Méthode pour importer un fichier CSV
        private async Task ImportCsv(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    var person = new Person
                    {
                        Nom = values[0],
                        Prenom = values[1],
                        Surnom = values[2],
                        Contact = values[3],
                        Longueur = Convert.ToDouble(values[4], CultureInfo.InvariantCulture),
                        Largeur = Convert.ToDouble(values[5], CultureInfo.InvariantCulture),
                        PrixM2 = Convert.ToDecimal(values[6], CultureInfo.InvariantCulture),
                        MontantComp = Convert.ToDecimal(values[7], CultureInfo.InvariantCulture),
                        Commentaire = values[8],
                        LocaliteId = int.Parse(values[9])
                    };

                    _context.Persons.Add(person);
                }
            }
            await _context.SaveChangesAsync();
        }

        // Méthode pour importer un fichier Excel
        private async Task ImportExcel(IFormFile file)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            // Obligatoire pour EPPlus

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var person = new Person
                        {
                            Nom = worksheet.Cells[row, 1].Text,
                            Prenom = worksheet.Cells[row, 2].Text,
                            Surnom = worksheet.Cells[row, 3].Text,
                            Contact = worksheet.Cells[row, 4].Text,
                            Longueur = Convert.ToDouble(worksheet.Cells[row, 5].Text, CultureInfo.InvariantCulture),
                            Largeur = Convert.ToDouble(worksheet.Cells[row, 6].Text, CultureInfo.InvariantCulture),
                            PrixM2 = Convert.ToDecimal(worksheet.Cells[row, 7].Text, CultureInfo.InvariantCulture),
                            MontantComp = Convert.ToDecimal(worksheet.Cells[row, 8].Text, CultureInfo.InvariantCulture),
                            Commentaire = worksheet.Cells[row, 9].Text,
                            LocaliteId = int.Parse(worksheet.Cells[row, 10].Text)
                        };

                        _context.Persons.Add(person);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
