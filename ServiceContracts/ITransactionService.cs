using InventoryApp.ServiceContracts.DTO;
using ServiceContracts.DTO;
using Entities;

namespace InventoryApp.ServiceContracts
{
    public interface ITransactionService
{
    Task<TransactionResponse> AddTransaction(TransactionAddRequest request);
    Task<List<TransactionResponse>> GetAllTransactions();

    Task<TransactionResponse?> GetTransactionByTransactionID(Guid transactionID);

    Task<TransactionResponse?> UpdateTransaction(TransactionUpdateRequest request);

    Task<bool> DeleteTransaction(Guid transactionID);
}
}