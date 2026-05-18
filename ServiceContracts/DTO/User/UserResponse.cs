namespace ServiceContracts.DTO
{
    public class UserResponse
{
    public Guid UserID { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public bool IsLocked { get; set; }
}
}