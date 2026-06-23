using System.ComponentModel.DataAnnotations;
using InventoryApp.Entities.Enums;

namespace Entities;

public class AppProduct
{
    [Key]
    public Guid? ProductID { get; set; }

    [StringLength(50)]
    public string? ProductName { get; set; }

    [StringLength(50)]
    public ProductCategory? Category { get; set; }

    public List<InventoryTransactions> Transactions { get; set; } = new List<InventoryTransactions>();

    // public int Quantity =>
        // Transactions.Where(t => t.Type == TransactionType.StockIn).Sum(t => t.Amount) 
        // Transactions.Where(t => t.Type == TransactionType.StockOut).Sum(t => t.Amount);
}
