using InventoryApp.ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts
{
    public interface IJobService
    {
        JobResponse AddJob(JobAddRequest? JobAddRequest);
    }
}
