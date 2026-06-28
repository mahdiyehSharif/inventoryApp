using Entities;
using InventoryApp.Entities.Enums;
using ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts.DTO
{
    public class TransactionResponse
    {
        public Guid TransactionID { get; set; }
        public Guid? ProductID { get; set; }
        public string? ProductName { get; set; }
        public Guid? UserID { get; set; }
        public int EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public TransactionType? Type { get; set; }
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TransactionResponse)) return false;

            TransactionResponse transaction = (TransactionResponse)obj;
            return TransactionID == transaction.TransactionID && 
            ProductName == transaction.ProductName && 
            UserID == transaction.UserID &&
            EmployeeID == transaction.EmployeeID && 
            Type == transaction.Type &&
            Amount == transaction.Amount &&
            Date == transaction.Date;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public TransactionUpdateRequest ToTransactionUpdateRequest()
        {
            return new TransactionUpdateRequest()
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
    public static class TransactionExtensions
    {
        public static TransactionResponse ToTransactionResponse(this InventoryTransactions transaction)
        {
            return new TransactionResponse
            {
                TransactionID = transaction.TransactionID,

                ProductID = transaction.ProductID,
                ProductName = transaction.Product?.ProductName,

                EmployeeID = transaction.EmployeeID,
                EmployeeName = $"{transaction.Employee?.FName} {transaction.Employee?.LName}",

                Type = transaction.Type,

                Amount = transaction.Amount,

                Date = transaction.DateTime
            };
        }
    }
}