using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities;
using ServiceContracts.DTO.Auth;
using InventoryApp.ServiceContracts;

namespace InventoryApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> Login(LoginRequest request)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.EmployeeID == request.EmployeeID);

            if (user == null)
            {
                return new AuthenticationResponse
                {
                    Success = false,
                    Message = "Employee not found"
                };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordValid)
            {
                return new AuthenticationResponse
                {
                    Success = false,
                    Message = "Invalid password"
                };
            }

            await _signInManager.SignInAsync(user, request.RememberMe);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthenticationResponse
            {
                Success = true,
                UserName = user.UserName,
                EmployeeID = user.EmployeeID,
                Roles = roles
            };
        }

        public async Task<AuthenticationResponse> Register(RegisterRequest request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                EmployeeID = request.EmployeeID
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new AuthenticationResponse
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            await _userManager.AddToRoleAsync(user, "Employee");

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthenticationResponse
            {
                Success = true,
                UserName = user.UserName,
                EmployeeID = user.EmployeeID,
                Roles = roles
            };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}