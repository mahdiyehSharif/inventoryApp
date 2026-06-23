using InventoryApp.ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.ServiceContracts.DTO;

namespace InventoryApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProductService _productService;

        public DashboardController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("Dashboard/Index")]
        [Route("/")]
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
    }
}