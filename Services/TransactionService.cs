using Entities;
using Entities.Data;
using InventoryApp.Entities.Enums;
using InventoryApp.ServiceContracts;
using InventoryApp.ServiceContracts.DTO;
using InventoryApp.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _db;

        public TransactionService(ApplicationDbContext db)
        {
            _db = db;
        }

        private TransactionResponse ConvertToResponse(InventoryTransactions transaction)
        {
            return transaction.ToTransactionResponse();
        }

        private async Task<AppProduct> GetProductAsync(Guid? productId)
        {
            var product = await _db.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product == null)
                throw new Exception("Product not found.");

            return product;
        }

        private async Task ApplyTransactionAsync(InventoryTransactions transaction)
        {
            var product = await _db.Products
                .FirstOrDefaultAsync(p => p.ProductID == transaction.ProductID);

            if (product == null)
                throw new Exception("Product not found.");

            if (transaction.Amount == null || transaction.Amount <= 0)
                throw new Exception("Invalid amount.");

            switch (transaction.Type)
            {
                case TransactionType.StockIn:

                    product.Quantity += transaction.Amount.Value;
                    break;

                case TransactionType.StockOut:

                    if (product.Quantity < transaction.Amount.Value)
                        throw new Exception("Insufficient stock.");

                    product.Quantity -= transaction.Amount.Value;
                    break;

                default:
                    throw new Exception("Invalid transaction type.");
            }
        }

        private async Task RollbackTransactionAsync(InventoryTransactions transaction)
        {
            var product = await _db.Products
                .FirstOrDefaultAsync(p => p.ProductID == transaction.ProductID);

            if (product == null)
                throw new Exception("Product not found.");

            if (transaction.Amount == null || transaction.Amount <= 0)
                throw new Exception("Invalid amount.");

            switch (transaction.Type)
            {
                case TransactionType.StockIn:

                    if (product.Quantity < transaction.Amount.Value)
                        throw new Exception("Cannot rollback transaction.");

                    product.Quantity -= transaction.Amount.Value;
                    break;

                case TransactionType.StockOut:

                    product.Quantity += transaction.Amount.Value;
                    break;

                default:
                    throw new Exception("Invalid transaction type.");
            }
        }

        public async Task<TransactionResponse> AddTransaction(TransactionAddRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            InventoryTransactions transaction = request.ToTransaction();

            transaction.TransactionID = Guid.NewGuid();
            transaction.DateTime = DateTime.Now;

            await ApplyTransactionAsync(transaction);

            _db.InventoryTransactions.Add(transaction);

            await _db.SaveChangesAsync();

            transaction = await _db.InventoryTransactions
                .Include(t => t.Product)
                .Include(t => t.Employee)
                .FirstAsync(t => t.TransactionID == transaction.TransactionID);

            return ConvertToResponse(transaction);
        }

        public async Task<List<TransactionResponse>> GetAllTransactions()
        {
            return await _db.InventoryTransactions
                .Include(t => t.Product)
                .Include(t => t.Employee)
                .Select(t => t.ToTransactionResponse())
                .ToListAsync();
        }

        public async Task<TransactionResponse?> GetTransactionByTransactionID(Guid transactionID)
        {
            InventoryTransactions? transaction =
                await _db.InventoryTransactions
                    .Include(t => t.Product)
                    .Include(t => t.Employee)
                    .FirstOrDefaultAsync(t => t.TransactionID == transactionID);

            if (transaction == null)
                return null;

            return ConvertToResponse(transaction);
        }

        public async Task<TransactionResponse?> UpdateTransaction(TransactionUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);

            InventoryTransactions? transaction =
                await _db.InventoryTransactions
                    .FirstOrDefaultAsync(t => t.TransactionID == request.TransactionID);

            if (transaction == null)
                return null;

            await RollbackTransactionAsync(transaction);

            transaction.ProductID = request.ProductID;
            transaction.UserID = request.UserID;
            transaction.EmployeeID = request.EmployeeID;
            transaction.Type = request.Type;
            transaction.Amount = request.Amount;

            await ApplyTransactionAsync(transaction);

            await _db.SaveChangesAsync();

            transaction = await _db.InventoryTransactions
                .Include(t => t.Product)
                .Include(t => t.Employee)
                .FirstAsync(t => t.TransactionID == request.TransactionID);

            return ConvertToResponse(transaction);
        }

        public async Task<bool> DeleteTransaction(Guid transactionID)
        {
            InventoryTransactions? transaction =
                await _db.InventoryTransactions
                    .FirstOrDefaultAsync(t => t.TransactionID == transactionID);

            if (transaction == null)
                return false;


            await RollbackTransactionAsync(transaction);

            _db.InventoryTransactions.Remove(transaction);

            await _db.SaveChangesAsync();

            return true;
        }
    }
}