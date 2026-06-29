using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using ServiceContracts;
using InventoryApp.Entities.Enums;

namespace InventoryApp.Controllers
{
    public class ProductLimitController : Controller
    {
        private readonly IProductLimitService _productLimitService;
        private readonly IProductService _productService;
        private readonly IJobService _jobService;

        public ProductLimitController(
            IProductLimitService productLimitService,
            IProductService productService,
            IJobService jobService)
        {
            _productLimitService = productLimitService;
            _productService = productService;
            _jobService = jobService;
        }

        private async Task LoadDropdowns()
        {
            var products = await _productService.GetAllProducts();
            var jobs = await _jobService.GetAllJobs();

            ViewBag.Products = products.Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.ProductName
            }).ToList();

            ViewBag.Jobs = jobs.Select(j => new SelectListItem
            {
                Value = j.JobID.ToString(),
                Text = j.JobName
            }).ToList();

            ViewBag.PeriodTypes = Enum.GetValues(typeof(PeriodType))
                .Cast<PeriodType>()
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                }).ToList();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var limits = await _productLimitService.GetAllProductLimits();
            return View(limits);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductLimitAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(request);
            }

            await _productLimitService.AddProductLimit(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var limit = await _productLimitService.GetProductLimitByID(id);

            if (limit == null)
                return NotFound();

            await LoadDropdowns();

            ProductLimitUpdateRequest model = new ProductLimitUpdateRequest
            {
                ProductLimitID = limit.ProductLimitID,
                ProductID = limit.ProductID,
                JobID = limit.JobID,
                MaxQuantity = limit.MaxQuantity,
                PeriodValue = limit.PeriodValue,
                PeriodType = limit.PeriodType,
                IsActive = limit.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductLimitUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(request);
            }

            var result = await _productLimitService.UpdateProductLimit(request);

            if (result == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await _productLimitService.DeleteProductLimit(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}