using System;
using Entities;
using ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts.DTO
{
    public class JobResponse
    {
        public int JobID { get; set; }
        public string? JobName { get; set; }
        public string? ManagementName { get; set; }
        public string? DeputyName { get; set; }
        public string? PositionName { get; set; }

        public JobUpdateRequest ToJobUpdateRequest()
        {
            return new JobUpdateRequest()
            {
                JobID = JobID,
                JobName = JobName,
                ManagementName = ManagementName,
                DeputyName = DeputyName,
                PositionName = PositionName
            };
        }

    }

    public static class JobExtensions
    {
        public static JobResponse ToJobResponse(this AppJob job)
        {
            return new JobResponse()
            {
                JobID = job.JobID,
                JobName = job.JobName,
                ManagementName = job.ManagementName,
                DeputyName = job.DeputyName,
                PositionName = job.PositionName

            };
        }
    }
}