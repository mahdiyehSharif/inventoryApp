using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="EmployeeID can not be blank")]
        public string? EmployeeID {get; set; }

        [Required(ErrorMessage ="Password can not be blank")]
        public string? Password {get; set; }

        [Required(ErrorMessage ="Password Confirm can not be blank")]
        public string? ConfirmPassword {get; set; }
    }
}