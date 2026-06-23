using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApp.Entities.Enums;

namespace Entities;

public class InventoryTransactions
{
    [Key]
    public Guid TransactionID { get; set; }

    [ForeignKey("User")]
    public Guid? UserID { get; set; }
    public AppUser? User { get; set; }

    [ForeignKey("Product")]
    public Guid? ProductID { get; set; }
    public AppProduct? Product { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeID { get; set; }
    public AppEmployee? Employee { get; set; }

    public TransactionType? Type { get; set; }
    public int? Amount { get; set; }
    public DateTime? DateTime { get; set; }
}