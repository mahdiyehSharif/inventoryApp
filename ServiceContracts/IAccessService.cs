using ServiceContracts.DTO;


namespace ServiceContracts
{
    public interface IAccessService
    {
        Task<List<UserResponse>> GetAllUsers();

        Task<UserResponse?> GetUser(Guid userId);

        Task<bool> DeleteUser(Guid userId);

        Task<bool> LockUser(Guid userId);

        Task<bool> UnlockUser(Guid userId);
        Task<UserResponse> UpdateUser(UserUpdateRequest request);
    }

}