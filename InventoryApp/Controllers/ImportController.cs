using ServiceContracts;
using ServiceContracts.DTO.Enum;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Controllers
{
    public class ImportController : Controller
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(
            IFormFile file,
            ImportType importType)
        {
            Console.WriteLine($"ImportType = {importType} ({(int)importType})");
            
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Please select a file.");

                return View();
            }

            using var stream = file.OpenReadStream();

            var result =
                await _importService.ImportAsync(stream, importType);

            if (result.Success)
            {
                TempData["Success"] =
                    $"{result.ImportedCount} records imported successfully.";
            }
            else
            {
                TempData["Error"] =
                    string.Join("<br>", result.Errors);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}