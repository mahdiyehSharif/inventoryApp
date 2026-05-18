using Entities;

namespace InventoryApp.ServiceContracts.DTO
{
    public class JobAddRequest
    {
        public string? JobName {get; set; }

        public AppJob ToJob()
        {
            return new AppJob()
        {
            JobName = this?.JobName
        };
        }
    }
}