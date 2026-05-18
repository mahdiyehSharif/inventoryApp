using Microsoft.AspNetCore.Mvc;
using InventoryApp.ServiceContracts;


namespace InventoryApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IEmployeeService _employeeService;
        private readonly IProductService _productService;

        public TransactionController(ITransactionService transactionService, IEmployeeService employeeService, IProductService productService)
        {
            _transactionService = transactionService;
            _employeeService = employeeService;
            _productService = productService;
        }

        [HttpGet]
        [Route("transaction/index")]
        public IActionResult Index()
        {
            var transactions = _transactionService.GetAllTransaction();
            return View(transactions);
        }

        [HttpGet]
        [Route("transaction/create")]
        public IActionResult Create()
        {
            ViewBag.Products = _productService.GetAllProducts();
            ViewBag.Employees = _employeeService.GetAllEmployees();

            return View();
        }
    }
}