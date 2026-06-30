using Entities;
using Entities.Data;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using InventoryApp.ServiceContracts.DTO.Enums;
using InventoryApp.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;

namespace InventoryApp.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _db;

        public JobService(ApplicationDbContext db)
        {
            _db = db;
        }

        private JobResponse ConvertJobToJobResponse(AppJob job)
        {
            return job.ToJobResponse();
        }

        public async Task<JobResponse> AddJob(JobAddRequest jobAddRequest)
        {
            if (jobAddRequest == null)
                throw new ArgumentNullException(nameof(jobAddRequest));

            ValidationHelper.ModelValidation(jobAddRequest);

            // bool exists = await _db.AppJobs
            //     .AnyAsync(j => j.JobID == jobAddRequest.JobID);

            // if (exists)
            //     throw new ArgumentException("Job ID already exists.");

            bool nameExists = await _db.AppJobs
                .AnyAsync(j => j.JobName == jobAddRequest.JobName);

            if (nameExists)
                throw new ArgumentException("Job Name already exists.");

            AppJob job = jobAddRequest.ToJob();

            _db.AppJobs.Add(job);

            await _db.SaveChangesAsync();

            return ConvertJobToJobResponse(job);
        }

        public async Task<List<JobResponse>> GetAllJobs()
        {
            return await _db.AppJobs
                .Select(j => j.ToJobResponse())
                .ToListAsync();
        }

        public async Task<JobResponse?> GetJobByJobID(int jobID)
        {
            AppJob? job = await _db.AppJobs
                .FirstOrDefaultAsync(j => j.JobID == jobID);

            if (job == null)
                return null;

            return ConvertJobToJobResponse(job);
        }

        public async Task<List<JobResponse>> GetFilteredJobs(
    string searchBy,
    string? searchString)
        {
            List<JobResponse> allJobs = await GetAllJobs();

            if (string.IsNullOrWhiteSpace(searchBy) ||
                string.IsNullOrWhiteSpace(searchString))
            {
                return allJobs;
            }

            searchString = searchString.Trim();

            return searchBy switch
            {
                nameof(JobResponse.JobName) =>
                    allJobs
                    .Where(j => !string.IsNullOrWhiteSpace(j.JobName) &&
                                j.JobName.Contains(searchString,
                                StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(JobResponse.ManagementName) =>
                    allJobs
                    .Where(j => !string.IsNullOrWhiteSpace(j.ManagementName) &&
                                j.ManagementName.Contains(searchString,
                                StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(JobResponse.DeputyName) =>
                    allJobs
                    .Where(j => !string.IsNullOrWhiteSpace(j.DeputyName) &&
                                j.DeputyName.Contains(searchString,
                                StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                nameof(JobResponse.PositionName) =>
                    allJobs
                    .Where(j => !string.IsNullOrWhiteSpace(j.PositionName) &&
                                j.PositionName.Contains(searchString,
                                StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                _ => allJobs
            };
        }

        public async Task<List<JobResponse>> GetSortedJobs(
    List<JobResponse> allJobs,
    string sortBy,
    SortOrderOptions sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return allJobs;

            List<JobResponse> sortedJobs =
                (sortBy, sortOrder) switch
                {
                    (nameof(JobResponse.JobName), SortOrderOptions.ASC) =>
                        allJobs.OrderBy(j => j.JobName).ToList(),

                    (nameof(JobResponse.JobName), SortOrderOptions.DESC) =>
                        allJobs.OrderByDescending(j => j.JobName).ToList(),

                    (nameof(JobResponse.ManagementName), SortOrderOptions.ASC) =>
                        allJobs.OrderBy(j => j.ManagementName).ToList(),

                    (nameof(JobResponse.ManagementName), SortOrderOptions.DESC) =>
                        allJobs.OrderByDescending(j => j.ManagementName).ToList(),

                    (nameof(JobResponse.DeputyName), SortOrderOptions.ASC) =>
                        allJobs.OrderBy(j => j.DeputyName).ToList(),

                    (nameof(JobResponse.DeputyName), SortOrderOptions.DESC) =>
                        allJobs.OrderByDescending(j => j.DeputyName).ToList(),

                    (nameof(JobResponse.PositionName), SortOrderOptions.ASC) =>
                        allJobs.OrderBy(j => j.PositionName).ToList(),

                    (nameof(JobResponse.PositionName), SortOrderOptions.DESC) =>
                        allJobs.OrderByDescending(j => j.PositionName).ToList(),

                    _ => allJobs
                };

            return await Task.FromResult(sortedJobs);
        }

        public async Task<JobResponse?> UpdateJob(JobUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            AppJob? job = await _db.AppJobs
                .FirstOrDefaultAsync(j => j.JobID == request.JobID);

            if (job == null)
                return null;

            job.JobName = request.JobName;
            job.ManagementName = request.ManagementName;
            job.DeputyName = request.DeputyName;
            job.PositionName = request.PositionName;

            await _db.SaveChangesAsync();

            return job.ToJobResponse();
        }

        public async Task<bool> DeleteJob(int jobID)
        {
            AppJob? job = await _db.AppJobs
                .FirstOrDefaultAsync(j => j.JobID == jobID);

            if (job == null)
                return false;

            bool isUsed = await _db.AppEmployees
                .AnyAsync(e => e.JobID == jobID);

            if (isUsed)
            {
                throw new InvalidOperationException(
                    "This job is assigned to one or more employees.");
            }

            _db.AppJobs.Remove(job);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
