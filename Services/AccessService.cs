using ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.AspNetCore.Identity;
using Entities;

namespace InventoryApp.Services
{
    public class AccessService : IAccessService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccessService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            var result = new List<UserResponse>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserResponse
                {
                    UserID = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "",
                    IsLocked = user.LockoutEnd > DateTimeOffset.UtcNow
                });
            }

            return result;
        }
    }
}