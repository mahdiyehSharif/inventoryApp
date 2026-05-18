using Entities;
using Entities.Data;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using InventoryApp.Services.Helpers;

namespace InventoryApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _db;
        // private readonly List<AppProduct> _products;

        public TransactionService(ApplicationDbContext ApplicationDbContext)
        {
            _db = ApplicationDbContext;
        }


        private TransactionResponse? ConvertTransactionToTransactionResponse(InventoryTransactions transaction)
        {
            TransactionResponse transactionResponse = transaction.ToTransactionResponse();
            return transactionResponse;

        }


        public TransactionResponse AddTransaction(TransactionAddRequest? transactionAddRequest)
        {
            if (transactionAddRequest == null)
            {
                throw new ArgumentNullException(nameof(transactionAddRequest));
            }
            ValidationHelper.ModelValidation(transactionAddRequest);


            if (string.IsNullOrEmpty(transactionAddRequest.Type?.ToString()))
            {
                throw new ArgumentException("TransactionType can not be blank");
            }

            InventoryTransactions? transaction = transactionAddRequest.ToTransaction();
            transaction.TransactionID = Guid.NewGuid();

            _db.Add(transaction);
            _db.SaveChanges();

            return ConvertTransactionToTransactionResponse(transaction);
        }

        public List<TransactionResponse> GetAllTransaction()
        {
            return _db.InventoryTransactions.Select(t => t.ToTransactionResponse()).ToList();
        }

        public TransactionResponse GetTransactiontByTransactionID(Guid? transactionID)
        {
            if (transactionID == null)
                return null;

            InventoryTransactions transaction = _db.InventoryTransactions.FirstOrDefault(p => p.TransactionID == transactionID);
            if (transaction == null)
                return null;
            return transaction.ToTransactionResponse();
        }

        public bool DeleteTransaction(Guid? transactionID)
        {
            if (transactionID == null)
            {
                throw new ArgumentNullException(nameof(transactionID));
            }

            InventoryTransactions? transaction = _db.InventoryTransactions.FirstOrDefault(temp => temp.TransactionID == transactionID);
            if (transaction == null)
                return false;

            _db.InventoryTransactions.Remove(_db.InventoryTransactions.First(temp => temp.TransactionID == transactionID));    
            _db.SaveChanges();

            return true;
        }

    }
}