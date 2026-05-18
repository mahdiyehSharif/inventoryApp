using ServiceContracts.DTO;


namespace ServiceContracts
{
    public interface IAccessService
    {
        Task<List<UserResponse>> GetAllUsers();

        // Task<UserResponse?> GetUser(Guid id);

        // Task CreateUser(UserAddRequest request);

        // Task DeleteUser(Guid id);

        // Task ChangeRole(Guid id, string role);

        // Task LockUser(Guid id);

        // Task UnlockUser(Guid id);
    }
}