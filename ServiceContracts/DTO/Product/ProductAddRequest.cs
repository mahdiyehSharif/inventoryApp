using System.ComponentModel.DataAnnotations;
using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class ProductAddRequest
    {
        [Required(ErrorMessage = "ProductName Can not be blank.")]
        public string? ProductName {get; set; }

        [Required(ErrorMessage = "ProductCategory is required.")]
        public ProductCategory? Category {get; set; } 
        public int? Quantity {get; set; }


        public AppProduct ToProduct()
        {
            return new AppProduct()
        {
            ProductName = this?.ProductName,
            Category = this?.Category
        };
        }
    }
}