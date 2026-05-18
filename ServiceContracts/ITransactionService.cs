using InventoryApp.ServiceContracts.DTO;

namespace InventoryApp.ServiceContracts
{
    public interface ITransactionService
    {
        public TransactionResponse AddTransaction(TransactionAddRequest? transactionAddRequest);

        public List<TransactionResponse> GetAllTransaction();
        
        public TransactionResponse GetTransactiontByTransactionID(Guid? transactionID);

        public bool DeleteTransaction(Guid? transactionID);
    }
}