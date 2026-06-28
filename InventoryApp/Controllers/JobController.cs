using Microsoft.AspNetCore.Mvc;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using ServiceContracts.DTO;

namespace InventoryApp.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var jobs = await _jobService.GetAllJobs();
            return View(jobs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobAddRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _jobService.AddJob(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var job = await _jobService.GetJobByJobID(id);

            if (job == null)
                return NotFound();

            return View(job.ToJobUpdateRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _jobService.UpdateJob(request);

            if (result == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _jobService.DeleteJob(id);

            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}