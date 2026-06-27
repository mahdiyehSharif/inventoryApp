using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts.DTO;

namespace InventoryApp.Models.ViewModels
{
    public class UserEditViewModel
    {
        public UserUpdateRequest User { get; set; } = new();

        public List<SelectListItem> Employees { get; set; } = new();

        public List<SelectListItem> Roles { get; set; } = new();
    }
}