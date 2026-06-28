using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO
{
    public class EmployeeUpdateRequest
    {
        [Required]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First name can't be blank.")]
        [StringLength(50)]
        public string? FName { get; set; }

        [Required(ErrorMessage = "Last name can't be blank.")]
        [StringLength(50)]
        public string? LName { get; set; }

        [Required(ErrorMessage = "Please select a job.")]
        public int JobID { get; set; }


        public AppEmployee ToEmployee()
        {
            return new AppEmployee()
            {
                EmployeeID = EmployeeID,
                FName = FName,
                LName = LName,
                JobID = JobID
            };
        }
    }
}