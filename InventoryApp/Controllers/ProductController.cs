using Microsoft.AspNetCore.Mvc;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;


namespace InventoryApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("product/index")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        [Route("product/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("product/create")]
        public async Task<IActionResult> Create(ProductAddRequest productAddRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(productAddRequest);
            }

            ProductResponse productResponse =
                await _productService.AddProduct(productAddRequest);

            return RedirectToAction(nameof(Index));
        }
    }
}