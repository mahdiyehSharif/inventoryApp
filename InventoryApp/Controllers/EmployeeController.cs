using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using ServiceContracts.DTO;

namespace InventoryApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IJobService _jobService;

        public EmployeeController(
            IEmployeeService employeeService,
            IJobService jobService)
        {
            _employeeService = employeeService;
            _jobService = jobService;
        }

        private async Task LoadJobsAsync()
        {
            var jobs = await _jobService.GetAllJobs();

            ViewBag.Jobs = jobs.Select(j => new SelectListItem
            {
                Value = j.JobID.ToString(),
                Text = j.JobName
            }).ToList();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployees();
            return View(employees);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadJobsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadJobsAsync();
                return View(request);
            }

            await _employeeService.AddEmployee(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByEmployeeID(id);

            if (employee == null)
                return NotFound();

            await LoadJobsAsync();

            return View(employee.ToEmployeeUpdateRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadJobsAsync();
                return View(request);
            }

            var employee = await _employeeService.UpdateEmployee(request);

            if (employee == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _employeeService.DeleteEmployee(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}