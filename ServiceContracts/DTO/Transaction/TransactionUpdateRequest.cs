using System.ComponentModel.DataAnnotations;
using Entities;
using InventoryApp.Entities.Enums;

namespace ServiceContracts.DTO
{
    public class TransactionUpdateRequest
    {
        [Required]
        public Guid TransactionID { get; set; }

        [Required(ErrorMessage = "Product can't be blank.")]
        public Guid? ProductID { get; set; }

        [Required(ErrorMessage = "User can't be blank.")]
        public Guid? UserID { get; set; }

        [Required(ErrorMessage = "Employee can't be blank.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Please select transaction type.")]
        public TransactionType? Type { get; set; }

        [Required(ErrorMessage = "Amount must be greater than 0.")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public int? Amount { get; set; }

        public InventoryTransactions ToTransaction()
        {
            return new InventoryTransactions()
            {
                TransactionID = TransactionID,
                ProductID = ProductID,
                UserID = UserID,
                EmployeeID = EmployeeID,
                Type = Type,
                Amount = Amount
            };
        }
    }
}