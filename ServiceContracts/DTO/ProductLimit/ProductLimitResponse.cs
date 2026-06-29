using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class ProductLimitResponse
    {
        public Guid ProductLimitID { get; set; }

        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }

        public int JobID { get; set; }
        public string? JobName { get; set; }

        public int MaxQuantity { get; set; }

        public int PeriodValue { get; set; }

        public PeriodType PeriodType { get; set; }

        public bool IsActive { get; set; }

        public ProductLimitUpdateRequest ToProductLimitUpdateRequest()
        {
            return new ProductLimitUpdateRequest
            {
                ProductLimitID = ProductLimitID,
                ProductID = ProductID,
                JobID = JobID,
                MaxQuantity = MaxQuantity,
                PeriodValue = PeriodValue,
                PeriodType = PeriodType,
                IsActive = IsActive
            };
        }
    }

    public static class ProductLimitExtensions
    {
        public static ProductLimitResponse ToProductLimitResponse(this ProductLimit limit)
        {
            return new ProductLimitResponse
            {
                ProductLimitID = limit.ProductLimitID,

                ProductID = limit.ProductID,
                ProductName = limit.Product?.ProductName,

                JobID = limit.JobID,
                JobName = limit.Job?.JobName,

                MaxQuantity = limit.MaxQuantity,

                PeriodValue = limit.PeriodValue,

                PeriodType = limit.PeriodType,

                IsActive = limit.IsActive
            };
        }
    }
}