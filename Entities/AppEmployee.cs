using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Job;

namespace Entities;

public class AppEmployee
{
    [Key]
    public int EmployeeID { get; set; }

    [StringLength(50)]
    public string? FName { get; set; }

    [StringLength(50)]
    public string? LName { get; set; }

    [ForeignKey("Job")]
    public Guid? JobID { get; set; }
    public AppJob? Job { get; set; }


}