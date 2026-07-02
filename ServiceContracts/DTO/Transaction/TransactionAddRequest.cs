using System.ComponentModel.DataAnnotations;
using Entities;
// using Identity.Models;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class TransactionAddRequest
    {
        [Required(ErrorMessage = "Product can not be blank.")]
        public Guid? ProductID { get; set; }

        [Required(ErrorMessage = "Employee can not be blank.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Transaction type is required.")]
        public TransactionType? Type { get; set; }

        [Required(ErrorMessage = "Amount must be greater than 0")]
        public int Amount { get; set; }

        public InventoryTransactions ToTransaction()
        {
            return new InventoryTransactions()
            {
                ProductID = this?.ProductID,
                UserID = this?.UserID,
                EmployeeID = this.EmployeeID,
                Type = this.Type,
                Amount = this?.Amount

            };
        }
    }
}

