using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using ServiceContracts.DTO;

namespace InventoryApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IProductService _productService;
        private readonly IEmployeeService _employeeService;

        public TransactionController(
            ITransactionService transactionService,
            IProductService productService,
            IEmployeeService employeeService)
        {
            _transactionService = transactionService;
            _productService = productService;
            _employeeService = employeeService;
        }

        private async Task LoadDropdowns()
        {
            var products = await _productService.GetAllProducts();

            ViewBag.Products = products.Select(p => new SelectListItem
            {
                Value = p.ProductID.ToString(),
                Text = p.ProductName
            }).ToList();

            var employees = await _employeeService.GetAllEmployees();

            ViewBag.Employees = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeID.ToString(),
                Text = e.FullName
            }).ToList();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactions();

            return View(transactions);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(request);
            }

            await _transactionService.AddTransaction(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var transaction = await _transactionService.GetTransactionByTransactionID(id);

            if (transaction == null)
                return NotFound();

            await LoadDropdowns();

            return View(transaction.ToTransactionUpdateRequest());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TransactionUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return View(request);
            }

            var result = await _transactionService.UpdateTransaction(request);

            if (result == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _transactionService.DeleteTransaction(id);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}