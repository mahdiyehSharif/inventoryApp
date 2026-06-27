using Microsoft.AspNetCore.Mvc;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using ServiceContracts.DTO;

namespace InventoryApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployees();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeAddRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _employeeService.AddEmployee(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByEmployeeID(id);

            if (employee == null)
                return NotFound();

            var model = employee.ToEmployeeUpdateRequest();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _employeeService.UpdateEmployee(request);

            if (result == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}