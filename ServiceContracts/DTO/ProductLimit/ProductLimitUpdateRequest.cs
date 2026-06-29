using System.ComponentModel.DataAnnotations;
using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class ProductLimitUpdateRequest
    {
        [Required]
        public Guid ProductLimitID { get; set; }

        [Required]
        public Guid ProductID { get; set; }

        [Required]
        public int JobID { get; set; }

        [Range(1, int.MaxValue)]
        public int MaxQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public int PeriodValue { get; set; }

        [Required]
        public PeriodType PeriodType { get; set; }

        public bool IsActive { get; set; }

        public ProductLimit ToProductLimit()
        {
            return new ProductLimit
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
}