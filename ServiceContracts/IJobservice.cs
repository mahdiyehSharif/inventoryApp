using InventoryApp.ServiceContracts.DTO;
using InventoryApp.ServiceContracts.DTO.Enums;
using ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts
{
    public interface IJobService
    {
        Task<JobResponse> AddJob(JobAddRequest jobAddRequest);

        Task<List<JobResponse>> GetAllJobs();

        Task<JobResponse?> GetJobByJobID(int jobID);

        Task<List<JobResponse>> GetFilteredJobs(
            string searchBy,
            string? searchString);

        Task<List<JobResponse>> GetSortedJobs(
            List<JobResponse> allJobs,
            string sortBy,
            SortOrderOptions sortOrder);

        Task<JobResponse?> UpdateJob(
            JobUpdateRequest jobUpdateRequest);

        Task<bool> DeleteJob(int jobID);
    }
}
