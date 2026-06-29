using System.ComponentModel.DataAnnotations;
using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class ProductLimitAddRequest
    {
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

        public bool IsActive { get; set; } = true;

        public ProductLimit ToProductLimit()
        {
            return new ProductLimit
            {
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