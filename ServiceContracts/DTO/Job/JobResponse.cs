using System;
using Entities.Job;

namespace InventoryApp.ServiceContracts.DTO
{
    public class JobResponse
    {
        public Guid? JobID {get; set; }
        public string? JobName{get; set;}
        public string? JobCategory {get; set; }
        public int? Quantity{get; set; }
    }

    public static class JobExtensions
    {
        public static JobResponse ToProductRequest(this AppJob job)
        {
            return new JobResponse()
            {
                JobID = job.JobID
            };
        }
    }
}