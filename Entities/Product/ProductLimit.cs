using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApp.Entities.Enums;

namespace Entities
{
    public class ProductLimit
    {
        [Key]
        public Guid ProductLimitID { get; set; }

        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public AppProduct Product { get; set; }

        [ForeignKey("Job")]
        public int JobID { get; set; }
        public AppJob Job { get; set; }

        public int MaxQuantity { get; set; }

        public int PeriodValue { get; set; }

        public PeriodType PeriodType { get; set; }

        public bool IsActive { get; set; } = true;
    }
}