using ServiceContracts.DTO.Auth;

namespace InventoryApp.ServiceContracts
{
public interface IAuthService
{
    Task<AuthenticationResponse> Login(LoginRequest request);

    Task<AuthenticationResponse> Register(RegisterRequest request);

    Task Logout();
}
}