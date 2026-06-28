using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO
{
    public class JobUpdateRequest
    {
        [Required]
        public int JobID { get; set; }

        [Required(ErrorMessage = "JobName can't be blank.")]
        [StringLength(50)]
        public string? JobName { get; set; }

        [Required(ErrorMessage = "ManagementName can't be blank.")]
        [StringLength(50)]
        public string? ManagementName { get; set; }

        [Required(ErrorMessage = "Please type a DeputyName.")]
        public string? DeputyName { get; set; }

        [Required(ErrorMessage = "Please type a PositionName.")]
         public string? PositionName { get; set; }


        public AppJob ToJob()
        {
            return new AppJob()
            {
                JobID = JobID,
                JobName = JobName,
                ManagementName = ManagementName,
                DeputyName = DeputyName,
                PositionName = PositionName
            };
        }
    }
}