using ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace InventoryApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAccessService _accessService;

        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _accessService.GetAllUsers();

            return View(users);
        }
    }
}