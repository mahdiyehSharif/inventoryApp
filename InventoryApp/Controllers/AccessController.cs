using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace InventoryApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAccessService _accessService;

        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _accessService.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _accessService.GetUser(id);

            if (user == null)
                return NotFound();

            var model = new UserUpdateRequest
            {
                UserID = user.UserID,
                UserName = user.UserName,
                EmployeeID = user.EmployeeID,
                Role = user.Role
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var updatedUser = await _accessService.UpdateUser(request);

            if (updatedUser == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _accessService.DeleteUser(id);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Lock(Guid id)
        {
            var result = await _accessService.LockUser(id);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Unlock(Guid id)
        {
            var result = await _accessService.UnlockUser(id);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}