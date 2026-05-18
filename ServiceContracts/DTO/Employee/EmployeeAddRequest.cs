using System.ComponentModel.DataAnnotations;
using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class EmployeeAddRequest
    {
        [Required(ErrorMessage ="ID can not be null")]
        public int EmployeeID {get; set; }

        [Required(ErrorMessage = "First Name Can not be blank.")]
        public string? FName { get; set; }

        [Required(ErrorMessage = "Last Name Can not be blank.")]
        public string? LName { get; set; }

        [Required(ErrorMessage = "Job Can not be blank.")]
        public Guid? JobID { get; set; }


        public AppEmployee ToEmployee()
        {
            return new AppEmployee()
            {
                FName = this?.FName,
                LName = this?.LName,
                JobID = this.JobID
            };
        }
    }
}