using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class UserUpdateRequest
    {
        [Required]
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "User Name can't be blank.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "EmplyeeID can't be blank.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Please select a role.")]
        public string? Role { get; set; }
    }
}