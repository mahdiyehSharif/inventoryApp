using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace Entities;

public class AppEmployee
{
    [Key]
    //  [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int EmployeeID { get; set; }

    [MaxLength]
    public string? FName { get; set; }

    [MaxLength]
    public string? LName { get; set; }

    [ForeignKey("Job")]
    public int JobID { get; set; }
    public AppJob? Job { get; set; }


}