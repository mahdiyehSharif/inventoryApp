using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace Entities;

public class ProductLimit
{
    [Key]
    public Guid ProductLimitID { get; set; }

    [ForeignKey("Product")]
    public Guid? ProductID { get; set; }
    public AppProduct? Product { get; set; }

    [ForeignKey("Job")]
    public Guid? JobID { get; set; }
    public AppJob? Job { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeID { get; set; }
    public AppEmployee? Employee { get; set; }
}