using ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.AspNetCore.Identity;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
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

                result.Add(new UserResponse()
                {
                    UserID = user.Id,
                    UserName = user.UserName,
                    EmployeeID = user.EmployeeID,
                    Role = roles.FirstOrDefault() ?? "",
                    IsLocked = user.LockoutEnabled &&
                               user.LockoutEnd.HasValue &&
                               user.LockoutEnd > DateTimeOffset.UtcNow
                });
            }

            return result;
        }

        public async Task<UserResponse?> GetUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserResponse()
            {
                UserID = user.Id,
                UserName = user.UserName,
                EmployeeID = user.EmployeeID,
                Role = roles.FirstOrDefault() ?? "",
                IsLocked = user.LockoutEnabled &&
                           user.LockoutEnd.HasValue &&
                           user.LockoutEnd > DateTimeOffset.UtcNow
            };
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> LockUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.SetLockoutEndDateAsync(
                user,
                DateTimeOffset.UtcNow.AddYears(100));

            return result.Succeeded;
        }

        public async Task<bool> UnlockUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.SetLockoutEndDateAsync(
                user,
                null);

            return result.Succeeded;
        }


        public async Task<UserResponse?> UpdateUser(UserUpdateRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserID.ToString());

            if (user == null)
                return null;

            user.UserName = request.UserName;
            user.EmployeeID = request.EmployeeID;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                return null;

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            await _userManager.AddToRoleAsync(user, request.Role!);

            var newRoles = await _userManager.GetRolesAsync(user);

            return new UserResponse
            {
                UserID = user.Id,
                UserName = user.UserName,
                EmployeeID = user.EmployeeID,
                Role = newRoles.FirstOrDefault() ?? "",
                IsLocked = user.LockoutEnabled &&
                           user.LockoutEnd.HasValue &&
                           user.LockoutEnd > DateTimeOffset.UtcNow
            };
        }
    }
}