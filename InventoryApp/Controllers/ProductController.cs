using Microsoft.AspNetCore.Mvc;
using InventoryApp.ServiceContracts;


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
    }
}