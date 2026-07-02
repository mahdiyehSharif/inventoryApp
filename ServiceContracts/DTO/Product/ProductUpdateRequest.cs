using System.ComponentModel.DataAnnotations;
using Entities;
using InventoryApp.Entities.Enums;


namespace InventoryApp.ServiceContracts.DTO
{
    public class ProductUpdateRequest
    {
        public Guid? ProductID {get; set;}

        [Required(ErrorMessage = "ProductName Can not be blank.")]
        public string? ProductName {get; set; }

        [Required(ErrorMessage = "ProductCategory is required.")]
        public ProductCategory? Category {get; set; } 

        public int Quantity { get; set; }

        // [Required(ErrorMessage = "Transaction Type is required.")]
        // public List<InventoryTransactions>? Transactions {get; set; }


        public AppProduct ToProduct()
        {
            return new AppProduct()
        {
            ProductID = this?.ProductID,
            ProductName = this?.ProductName,
            Category = this?.Category,
            // Transactions = this?.Transactions
        };
        }
    }
}