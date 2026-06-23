using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.Auth

{
    public class LoginRequest
    {
        [Required(ErrorMessage = "EmployeeID is required")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}