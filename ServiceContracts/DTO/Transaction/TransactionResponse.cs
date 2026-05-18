using Entities;
using InventoryApp.Entities.Enums;

namespace InventoryApp.ServiceContracts.DTO
{
    public class TransactionResponse
    {
        public Guid TransactionID { get; set; }
        public Guid? ProductID { get; set; }
        public string? ProductName { get; set; }
        public AppEmployee? EmployeeID {get; set; }
        public TransactionType? Type { get; set; }
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }
        

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TransactionResponse)) return false;

            TransactionResponse transaction = (TransactionResponse)obj;
            return TransactionID == transaction.TransactionID && ProductID == transaction.ProductID && Type == transaction.Type;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class TransactionExtensions
    {
        public static TransactionResponse ToTransactionResponse(this InventoryTransactions? transaction)
        {
            return new TransactionResponse()
            {
                TransactionID = transaction.TransactionID,
                ProductID = transaction.ProductID,      
                ProductName = transaction.Product?.ProductName,
                EmployeeID = transaction.Employee ,      
                Type = transaction.Type,
                Amount = transaction.Amount,
                Date = transaction.DateTime

            };
        }
    }
}