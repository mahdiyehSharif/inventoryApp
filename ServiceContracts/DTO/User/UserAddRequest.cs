using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class UserAddRequest
    {
        [Required(ErrorMessage = "User Name can't be blank.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "EmployeeID can't be blank.")]
        public int? EmployeeID { get; set; }

        [Required(ErrorMessage = "Password can't be blank.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please select a role.")]
        public string? Role { get; set; }
    }
}