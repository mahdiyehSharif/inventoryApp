using System.ComponentModel.DataAnnotations;
using InventoryApp.Entities.Enums;

namespace Entities;

public class AppProduct
{
    [Key]
    public Guid? ProductID { get; set; }

    [MaxLength]
    public string? ProductName { get; set; }

    [MaxLength]
    public ProductCategory? Category { get; set; }

    public List<InventoryTransactions> Transactions { get; set; } = new List<InventoryTransactions>();


}
